using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DJetronicStudio
{
    public class Simulator
    {
        public delegate void OnConnectedHandler(object sender);
        public event OnConnectedHandler OnConnected = null;

        public delegate void OnDisconnectedHandler(object sender);
        public event OnDisconnectedHandler OnDisconnected = null;

        public delegate void OnShowMessageHandler(object sender, string Message);
        public event OnShowMessageHandler OnShowMessage = null;

        public delegate void OnSimulationStartedHandler(object sender);
        public event OnSimulationStartedHandler OnSimulationStarted = null;

        public delegate void OnSimulationEndedHandler(object sender);
        public event OnSimulationEndedHandler OnSimulationEnded = null;

        private bool Connected = false;
        private string SpiceFolder;
        private int StartTimeMs;
        private int EndTimeMs;
        private int StepUs;
        private Thread SimThread;

        private NGSpice _Spice;
        public NGSpice Spice
        {
            get { return _Spice; }
            set { _Spice = value; }
        }

        public Simulator
            (
            )
        {
#if DEBUG
            SpiceFolder = Path.GetDirectoryName(Application.ExecutablePath) + @"\..\..\..\Spice";
#else
            SpiceFolder = Path.GetDirectoryName(Application.ExecutablePath) + @"\Spice";
#endif

            SimThread = null;
        }

        /// <summary>
        /// true if connected to the simulation
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return Connected;
            }
        }

        /// <summary>
        /// Finds and connects to the simulation
        /// </summary>
        public void Connect
            (
            )
        {
            if (Connected)
            {
                return;
            }

            Connected = true;

            if (OnConnected != null)
            {
                OnConnected(this);
            }
        }

        /// <summary>
        /// Disconnects from the tester
        /// </summary>
        public void Disconnect
            (
            )
        {
            if (!Connected)
            {
                return;
            }

            Stop();

            Connected = false;

            if (OnDisconnected != null)
            {
                OnDisconnected(this);
            }
        }

        /// <summary>
        /// Runs a simulation
        /// </summary>
        /// <param name="StartTimeMs">Time of simulation start in ms</param>
        /// <param name="EndTimeMs">Time of simulation end in ms</param>
        /// <param name="StepUs">Simulation step time in us</param>
        public void Run
            (
            int StartTimeMs,
            int EndTimeMs,
            int StepUs
            )
        {
            if (SimThread != null)
            {
                Stop();
                SimThread = null;
            }

            this.StartTimeMs = StartTimeMs;
            this.EndTimeMs = EndTimeMs;
            this.StepUs = StepUs;

            SimThread = new Thread(new ThreadStart(RunSim));
            SimThread.Name = "Simulation";
            SimThread.Start();
        }

        /// <summary>
        /// Stops the current simulation
        /// </summary>
        public void Stop
            (
            )
        {
            if (SimThread != null)
            {
                SimThread.Abort();
                while (SimThread != null)
                {
                    Thread.Sleep(10);
                }
                Spice.ReInit();
            }

            SimThread = null;
            if (OnSimulationEnded != null) OnSimulationEnded(this);
        }

        /// <summary>
        /// Runs the simulation
        /// Executes as a background thread
        /// </summary>
        private void RunSim
            (
            )
        {
            if (OnSimulationStarted != null) OnSimulationStarted(this);

            try
            {
                Spice.Reset();

                if (!Spice.RunCommand("set ngbehavior=ps"))
                {
                    return;
                }
                if (!Spice.RunCommand("cd \"" + SpiceFolder.Replace(@"\", @"/") + "\""))
                {
                    return;
                }

                string Netlist = SpiceFolder + Path.DirectorySeparatorChar + "Bosch ECU 0 280 002 005.cir";

                string[] NetListLines = File.ReadAllLines(Netlist);

                List<string> ProcessedNetList = new List<string>();
                foreach (string Line in NetListLines)
                {
                    string TrimmedLine = Line.Trim();

                    if (TrimmedLine.Length > 0)
                    {
                        if (TrimmedLine.StartsWith(".tran"))
                        {
                            TrimmedLine = string.Format(".tran {0}us {1}ms {2}ms", StepUs, EndTimeMs, StartTimeMs);
                        }

                        ProcessedNetList.Add(TrimmedLine);
                    }
                }

                Spice.SpecifyCircuit(ProcessedNetList.ToArray());

                /*foreach (string Line in NetListLines)
                {
                    string TrimmedLine = Line.Trim();

                    if (TrimmedLine.Length > 0)
                    {
                        if (TrimmedLine.StartsWith(".tran"))
                        {
                            TrimmedLine = string.Format(".tran {0}us {1}ms {2}ms", StepUs, EndTimeMs, StartTimeMs);
                        }

                        if (!Spice.RunCommand("circbyline " + TrimmedLine))
                        {
                            return;
                        }
                    }
                }*/
            }
            finally
            {
                SimThread = null;
                if (OnSimulationEnded != null) OnSimulationEnded(this);
            }
        }
    }
}
