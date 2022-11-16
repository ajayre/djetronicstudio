using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public delegate int SendChar(string callerOut, int idNum, IntPtr pointer);                              //Define the delegates required to interface with Ngspice.dll
        public delegate int SendStat(string simStatus, int idNum, IntPtr pointer);                              //Note: char* data type in C++ seems to translate to string type in C#
        public delegate int ControlledExit(int exitStatus, bool unloadStatus, bool exitType, int idNum, IntPtr pointer);    //IntPtr type stores a pointer as an integer
        public delegate int SendData(IntPtr pvecvaluesall, int structNum, int idNum, IntPtr pointer);
        public delegate int SendInitData(IntPtr pvecinfoall, int idNum, IntPtr pointer);
        public delegate int BGThreadRunning(bool backgroundThreadRunning, int idNum, IntPtr pointer);

        [DllImport("ngspice.dll")] private static extern int ngSpice_Init(SendChar aa, SendStat bb, ControlledExit cc, SendData dd, SendInitData ee, BGThreadRunning ff, IntPtr pointer);    //define external dll functions (aa, bb, cc, dd, ee, ff are random variable names)
        [DllImport("ngspice.dll")] private static extern int ngSpice_Command(string commandString);
        //Other required ngspice functions can be defined as above

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

        // references to keep callbacks in memory
        private SendChar SendCharCallback;
        private SendStat SendStatCallback;
        private ControlledExit ControlledExitCallback;
        private SendData SendDataCallback;
        private SendInitData SendInitDataCallback;
        private BGThreadRunning BGThreadRunningCallback;

        public int counter = 0;

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

            ngSpice_Init(SendCharCallback, SendStatCallback, ControlledExitCallback, SendDataCallback, SendInitDataCallback, BGThreadRunningCallback, dummyIntPtr);

            CurrentCommand = Commands.None;

            SimulationData.Clear();
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
            CommandError = false;
            ngSpice_Command(Command);
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

        //WARNING: The boolean variables of the structure below (isScale and isComplex) may not be memory mapped correctly between C and C#
        //and as such, may not yield correct values. (I did no testing on these two variables) 

        [StructLayout(LayoutKind.Sequential)]
        private struct vecValue
        {
            public string vecName;              // name of a specific vector (as char*, this pointer can be turned into a string later)
            public double cReal;                    // actual data value (real)
            public double cImag;                    // actual data value (imaginary)
            public bool isScale;                    // if ’name ’ is the scale vector
            public bool isComplex;                  // if the data are complex numbers
        };

        private struct vecValuePtrStruct             // this structure purely makes parsing easier using C# Marshalling
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

            //Debug.Log("SendStatReceive called: " + simStatus);
            return 0;
        }

        private int ControlledExitReceive(int exitStatus, bool unloadStatus, bool exitType, int idNum, IntPtr pointer)
        {
            //Debug.Log("ControlledExitReceive called: " + exitStatus);
            return 0;
        }

        private int SendDataReceive(IntPtr pvecvaluesall, int structNum, int idNum, IntPtr pointer)
        {
            counter++;

            //Debug.Log("SendDataReceive called");

            vecValuesAll allValues = (vecValuesAll)Marshal.PtrToStructure(pvecvaluesall, typeof(vecValuesAll));         // get allValues struct from unmanaged memory

            vecValuePtrStruct[] vecValuePtrs = new vecValuePtrStruct[allValues.vecCount];       // define array of IntPtrs
            vecValue[] values = new vecValue[allValues.vecCount];                               // define array of vector values

            // marshal array of pointers from location of allValues.vecArray, and paste into vecValuePtrs array
            MarshalUnmananagedArray2Struct(allValues.vecArray, allValues.vecCount, out vecValuePtrs);

            for (int i = 0; i < allValues.vecCount; i++)
            {                                                        
                // iterate through each vector
                // marshal each vector value structure from each pointer value in vecValuePtrs array
                values[i] = (vecValue)Marshal.PtrToStructure(vecValuePtrs[i].vecValuePtr, typeof(vecValue)); 

                //if (values[i].vecName == "time")
                //{
                //    // convert from s to x10ns
                //    UInt32 Timestamp = (UInt32)(values[i].cReal * 1000 * 1000 * 100);
                //}
            }

            SimulationData.Add(values);

            return 0;
        }

        private int SendInitDataReceive(IntPtr pvecinfoall, int idNum, IntPtr pointer)
        {
            //Debug.Log("SendInitDataReceive called");
            return 0;
        }

        private int BGThreadRunningReceive(bool backgroundThreadRunning, int idNum, IntPtr pointer)
        {
            //Debug.Log("BGThreadRunningReceive called");
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
