using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private bool Connected = false;
        private string SpiceFolder;

        public NGSpice Spice;

        public Simulator
            (
            )
        {
#if DEBUG
            SpiceFolder = Path.GetDirectoryName(Application.ExecutablePath) + @"\..\..\..\Spice";
#else
            SpiceFolder = Path.GetDirectoryName(Application.ExecutablePath) + @"\Spice";
#endif
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

            foreach (string Line in NetListLines)
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
            }

            int c = Spice.counter;
        }
    }
}
