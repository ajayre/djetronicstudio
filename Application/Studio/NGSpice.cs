using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DJetronicStudio
{
    // dll source: https://sourceforge.net/p/ngspice/ngspice/ci/ngspice-37/tree/visualc/

    // adapted from: https://github.com/nextguyover/sharedspice-csharp-interface/blob/main/Ngspice_Interface_Concept.cs
    public class NGSpice
    {
        public delegate void OnShowMessageHandler(object sender, string Message);
        public event OnShowMessageHandler OnShowMessage = null;

        public delegate void OnEndedHandler(object sender);
        public event OnEndedHandler OnEnded = null;

        public delegate void OnPercentageCompletedHandler(object sender, double PercentageCompleted);
        public event OnPercentageCompletedHandler OnPercentageCompleted = null;

        // define the delegates required to interface with Ngspice.dll
        public delegate int SendChar(string callerOut, int idNum, IntPtr pointer);                              
        public delegate int SendStat(string simStatus, int idNum, IntPtr pointer);
        public delegate int ControlledExit(int exitStatus, bool unloadStatus, bool exitType, int idNum, IntPtr pointer);
        public delegate int SendData(IntPtr pvecvaluesall, int structNum, int idNum, IntPtr pointer);
        public delegate int SendInitData(IntPtr pvecinfoall, int idNum, IntPtr pointer);
        public delegate int BGThreadRunning(bool backgroundThreadRunning, int idNum, IntPtr pointer);

        public double TotalTimeMs;

        [DllImport("ngspice.dll")] private static extern int ngSpice_Init(SendChar aa, SendStat bb, ControlledExit cc, SendData dd, SendInitData ee, BGThreadRunning ff, IntPtr pointer);    //define external dll functions (aa, bb, cc, dd, ee, ff are random variable names)
        [DllImport("ngspice.dll")] private static extern int ngSpice_Command(string commandString);
        [DllImport("ngspice.dll")] private static extern int ngSpice_Circ(string[] Lines);

        private IntPtr dummyIntPtr = new IntPtr();
        private enum Commands
        {
            None,
            Version
        };
        private Commands CurrentCommand;
        private List<string> OutputLines = new List<string>();
        private bool CommandError;
        private List<vecValue[]> SimulationData = new List<vecValue[]>();
        private Regex ReferenceValueRegEx;
        private Regex NumDataRowsRegEx;

        // references to keep callbacks in memory
        private SendChar SendCharCallback;
        private SendStat SendStatCallback;
        private ControlledExit ControlledExitCallback;
        private SendData SendDataCallback;
        private SendInitData SendInitDataCallback;
        private BGThreadRunning BGThreadRunningCallback;

        public class SimDataPoint
        {
            public double Time;
            public double Value;

            public SimDataPoint
                (
                double Time,
                double Value
                )
            {
                this.Time = Time;
                this.Value = Value;
            }

            public override string ToString()
            {
                return Time.ToString() + ": " + Value.ToString();
            }
        }

        public NGSpice
            (
            )
        {
            SendCharCallback = new SendChar(SendCharReceive);
            SendStatCallback = new SendStat(SendStatReceive);
            ControlledExitCallback = new ControlledExit(ControlledExitReceive);
            SendDataCallback = new SendData(SendDataReceive);
            SendInitDataCallback = new SendInitData(SendInitDataReceive);
            BGThreadRunningCallback = new BGThreadRunning(BGThreadRunningReceive);

            ReferenceValueRegEx = new Regex(@"^\s*Reference\s+value\s*\:\s*([0-9\.\-eE]+)\s*$", RegexOptions.IgnoreCase);
            NumDataRowsRegEx = new Regex(@"^\s*No\. of Data Rows\s*\:.*$", RegexOptions.IgnoreCase);

            ngSpice_Init(SendCharCallback, SendStatCallback, ControlledExitCallback, SendDataCallback, SendInitDataCallback, BGThreadRunningCallback, dummyIntPtr);

            Reset();
        }

        /// <summary>
        /// Reinitalizes the module
        /// </summary>
        public void ReInit
            (
            )
        {
            ngSpice_Init(SendCharCallback, SendStatCallback, ControlledExitCallback, SendDataCallback, SendInitDataCallback, BGThreadRunningCallback, dummyIntPtr);

            Reset();
        }

        /// <summary>
        /// Reset the module, call before each simulation run
        /// </summary>
        public void Reset
            (
            )
        {
            CurrentCommand = Commands.None;
            SimulationData.Clear();
            if (OnPercentageCompleted != null) OnPercentageCompleted(this, 0);
        }

        /// <summary>
        /// Runs a command
        /// </summary>
        /// <param name="Command">Command to run</param>
        /// <returns>true for success, false for error</returns>
        public bool RunCommand
            (
            string Command
            )
        {
            try
            {
                CommandError = false;
                ngSpice_Command(Command);
                return !CommandError;
            }
            catch
            {
                // this will happen when ngspice is sent the 'quit' comamnd during simulation
                return false;
            }
        }

        /// <summary>
        /// Specifies a circuit
        /// </summary>
        /// <param name="Lines">Array of lines that define the circuit</param>
        /// <returns></returns>
        public bool SpecifyCircuit
            (
            string[] Lines
            )
        {
            CommandError = false;
            ngSpice_Circ(Lines);
            return !CommandError;
        }

        /// <summary>
        /// Gets the spice version
        /// </summary>
        /// <returns>Array of strings containing version information</returns>
        public string[] GetVersion
            (
            )
        {
            try
            {
                CurrentCommand = Commands.Version;
                OutputLines.Clear();
                ngSpice_Command("version");
            }
            finally
            {
                CurrentCommand = Commands.None;
            }

            return OutputLines.ToArray();
        }

        #region Structures for parsing data from SendDataReceive
        private struct vecValuesAll
        {
            public int vecCount;                    // number of vectors in plot
            public int vecIndex;                    // index of actual set of vectors , i.e.	the number of accepted data points
            public IntPtr vecArray;                 // values of actual set of vectors, indexed from 0 to veccount - 1
        };

        // WARNING: The boolean variables of the structure below (isScale and isComplex) may not be memory mapped correctly between C and C#
        // and as such, may not yield correct values. (I did no testing on these two variables) 
        [StructLayout(LayoutKind.Sequential)]
        private struct vecValue
        {
            public string vecName;                  // name of a specific vector (as char*, this pointer can be turned into a string later)
            public double cReal;                    // actual data value (real)
            public double cImag;                    // actual data value (imaginary)
            public bool isScale;                    // if ’name ’ is the scale vector
            public bool isComplex;                  // if the data are complex numbers
        };

        // this structure purely makes parsing easier using C# Marshalling
        private struct vecValuePtrStruct
        {
            public IntPtr vecValuePtr;
        };
        #endregion

        #region Callback functions
        private int SendCharReceive(string callerOut, int idNum, IntPtr pointer)
        {
            if (callerOut.StartsWith("stderr")) CommandError = true;

            string Line = callerOut.Replace("stdout", "").Replace("stderr", "ERROR:");

            switch (CurrentCommand)
            {
                case Commands.Version:
                    OutputLines.Add(Line);
                    break;

                default:
                    Match RefValMatch = ReferenceValueRegEx.Match(Line);
                    if (RefValMatch.Success)
                    {
                        double RefVal = double.Parse(RefValMatch.Groups[1].Value);
                        double PercentCompleted = (RefVal * 1000) / TotalTimeMs * 100;
                        if (OnPercentageCompleted != null) OnPercentageCompleted(this, PercentCompleted);
                    }
                    Match NoDataRowsMatch = NumDataRowsRegEx.Match(Line);
                    if (NoDataRowsMatch.Success)
                    {
                        if (OnPercentageCompleted != null) OnPercentageCompleted(this, 100);
                    }
                    if (OnShowMessage != null) OnShowMessage(this, Line);
                    break;
            }

            return 0;
        }

        private int SendStatReceive(string simStatus, int idNum, IntPtr pointer)
        {
            if (simStatus == "--ready--")
            {
                if (OnEnded != null) OnEnded(this);
            }

            return 0;
        }

        private int ControlledExitReceive(int exitStatus, bool unloadStatus, bool exitType, int idNum, IntPtr pointer)
        {
            return 0;
        }

        private int SendDataReceive(IntPtr pvecvaluesall, int structNum, int idNum, IntPtr pointer)
        {
            // get allValues struct from unmanaged memory
            vecValuesAll allValues = (vecValuesAll)Marshal.PtrToStructure(pvecvaluesall, typeof(vecValuesAll));

            vecValuePtrStruct[] vecValuePtrs = new vecValuePtrStruct[allValues.vecCount];
            vecValue[] values = new vecValue[allValues.vecCount];

            // marshal array of pointers from location of allValues.vecArray, and paste into vecValuePtrs array
            MarshalUnmananagedArray2Struct(allValues.vecArray, allValues.vecCount, out vecValuePtrs);

            for (int i = 0; i < allValues.vecCount; i++)
            {                                                        
                // iterate through each vector
                // marshal each vector value structure from each pointer value in vecValuePtrs array
                values[i] = (vecValue)Marshal.PtrToStructure(vecValuePtrs[i].vecValuePtr, typeof(vecValue)); 
            }

            SimulationData.Add(values);

            return 0;
        }

        /// <summary>
        /// Gets the data for a specific simulation vector
        /// </summary>
        /// <param name="VectorName">Name of vector</param>
        /// <returns>List of time-value pairs</returns>
        public List<SimDataPoint> GetData
            (
            string VectorName
            )
        {
            if (VectorName == "time")
            {
                throw new Exception("Cannot get data for time vector");
            }

            List<SimDataPoint> Data = new List<SimDataPoint>();
            string VName = VectorName.ToLower();

            foreach (vecValue[] ValArr in SimulationData)
            {
                double? Time = null;
                double? Value = null;

                foreach (vecValue Val in ValArr)
                {
                    if (Val.vecName == "time")
                    {
                        Time = Val.cReal;
                    }
                    else if (Val.vecName == VName)
                    {
                        Value = Val.cReal;
                    }

                    if (Time.HasValue && Value.HasValue)
                    {
                        Data.Add(new SimDataPoint(Time.Value, Value.Value));
                        break;
                    }
                }
            }

            return Data;
        }

        private int SendInitDataReceive(IntPtr pvecinfoall, int idNum, IntPtr pointer)
        {
            return 0;
        }

        private int BGThreadRunningReceive(bool backgroundThreadRunning, int idNum, IntPtr pointer)
        {
            return 0;
        }
        #endregion

        #region Helper function
        /// <summary>
        /// Converts an array of structures from unmanaged memory to a managed variable
        /// https://stackoverflow.com/a/40376326/9112181
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="unmanagedArray"></param>
        /// <param name="length"></param>
        /// <param name="mangagedArray"></param>
        private void MarshalUnmananagedArray2Struct<T>(IntPtr unmanagedArray, int length, out T[] mangagedArray)
        {                                                                                                                   
            var size = Marshal.SizeOf(typeof(T));
            mangagedArray = new T[length];

            for (int i = 0; i < length; i++)
            {
                // increments pointer memory location to position of next struct in array
                IntPtr nextStructureMemBlock = IntPtr.Add(unmanagedArray, i * size);
                mangagedArray[i] = Marshal.PtrToStructure<T>(nextStructureMemBlock);
            }
        }
        #endregion
    }
}
