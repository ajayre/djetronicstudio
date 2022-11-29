using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private SimSettings Settings;

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
        /// <param name="Settings">Settings to use for simulation</param>
        public void Run
            (
            SimSettings Settings
            )
        {
            if (SimThread != null)
            {
                Stop();
                SimThread = null;
            }

            this.Settings = Settings;

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

            uint StepUs = Settings.Resolution;
            if (Settings.ResolutionUnit == SimSettings.ResolutionUnits.Milliseconds)
                StepUs = StepUs * 1000;

            this.StartTimeMs = 0;
            this.EndTimeMs = (int)Settings.TotalTimeMs;
            this.StepUs = (int)StepUs;

            try
            {
                Spice.Reset();
                Spice.TotalTimeMs = EndTimeMs - StartTimeMs;

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

                ApplySettings(ProcessedNetList);

                Spice.SpecifyCircuit(ProcessedNetList.ToArray());
            }
            finally
            {
                SimThread = null;
                if (OnSimulationEnded != null) OnSimulationEnded(this);
            }
        }

        private const string THROTTLE_VOLTAGE_SOURCE = "V4";
        private const string ENGINE_VACUUM_VOLTAGE_SOURCE = "V5";
        private const string RHEOSTAT_POSITION_VOLTAGE_SOURCE = "V6";
        private const string START_VOLTAGE_SOURCE = "V7";
        private const string BATTERY_VOLTAGE_SOURCE_1 = "V16";
        private const string BATTERY_VOLTAGE_SOURCE_2 = "V24";

        /// <summary>
        /// Applies the current settings to a netlist
        /// </summary>
        /// <param name="NetList">Netlist to change</param>
        private void ApplySettings
            (
            List<string> NetList
            )
        {
            // V16 AUX7-12V 0 dc 13.5
            Regex BatteryVoltage1Rx = new Regex(@"^(" + BATTERY_VOLTAGE_SOURCE_1 + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex BatteryVoltage2Rx = new Regex(@"^(" + BATTERY_VOLTAGE_SOURCE_2 + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex EngineVacuumRx = new Regex(@"^(" + ENGINE_VACUUM_VOLTAGE_SOURCE + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex RheostatRx = new Regex(@"^(" + RHEOSTAT_POSITION_VOLTAGE_SOURCE + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex StartRx = new Regex(@"^(" + START_VOLTAGE_SOURCE + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex ThrottleRx = new Regex(@"^(" + THROTTLE_VOLTAGE_SOURCE + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);

            for (int i = 0; i < NetList.Count; i++)
            {
                Match BatteryVoltage1Ma = BatteryVoltage1Rx.Match(NetList[i]);
                if (BatteryVoltage1Ma.Success)
                {
                    string Spec;
                    if (Settings.StartBatteryVoltage != Settings.EndBatteryVoltage)
                    {
                        Spec = string.Format("pwl(0m {0} {1}m {2})", Settings.StartBatteryVoltage, Settings.TotalTimeMs, Settings.EndBatteryVoltage);
                    }
                    else
                    {
                        Spec = string.Format("dc({0})", Settings.StartBatteryVoltage);
                    }
                    NetList[i] = string.Format("{0} {1}", BatteryVoltage1Ma.Groups[1], Spec);
                    continue;
                }

                Match BatteryVoltage2Ma = BatteryVoltage2Rx.Match(NetList[i]);
                if (BatteryVoltage2Ma.Success)
                {
                    string Spec;
                    if (Settings.StartBatteryVoltage != Settings.EndBatteryVoltage)
                    {
                        Spec = string.Format("pwl(0m {0} {1}m {2})", Settings.StartBatteryVoltage, Settings.TotalTimeMs, Settings.EndBatteryVoltage);
                    }
                    else
                    {
                        Spec = string.Format("dc({0})", Settings.StartBatteryVoltage);
                    }
                    NetList[i] = string.Format("{0} {1}", BatteryVoltage2Ma.Groups[1], Spec);
                    continue;
                }

                Match EngineVacuumMa = EngineVacuumRx.Match(NetList[i]);
                if (EngineVacuumMa.Success)
                {
                    string Spec;
                    if (Settings.StartManifoldVacuumInhg != Settings.EndManifoldVacuumInhg)
                    {
                        Spec = string.Format("pwl(0m {0} {1}m {2})", Settings.StartManifoldVacuumInhg, Settings.TotalTimeMs, Settings.EndManifoldVacuumInhg);
                    }
                    else
                    {
                        Spec = string.Format("dc({0})", Settings.StartManifoldVacuumInhg);
                    }
                    NetList[i] = string.Format("{0} {1}", EngineVacuumMa.Groups[1], Spec);
                    continue;
                }

                Match RheostatMa = RheostatRx.Match(NetList[i]);
                if (RheostatMa.Success)
                {
                    string Spec;
                    if (Settings.StartRheostat != Settings.EndRheostat)
                    {
                        Spec = string.Format("pwl(0m {0} {1}m {2})", Settings.StartRheostat, Settings.TotalTimeMs, Settings.EndRheostat);
                    }
                    else
                    {
                        Spec = string.Format("dc({0})", Settings.StartRheostat);
                    }
                    NetList[i] = string.Format("{0} {1}", RheostatMa.Groups[1], Spec);
                    continue;
                }

                Match StartMa = StartRx.Match(NetList[i]);
                if (StartMa.Success)
                {
                    string Spec;
                    if (Settings.StarterMotorOffMs > 0)
                    {
                        Spec = string.Format("pwl({0}m 12 {1}m 12 {2}m 0)", Settings.StarterMotorOnMs, Settings.StarterMotorOffMs - 1, Settings.StarterMotorOffMs);
                    }
                    else
                    {
                        Spec = "dc(0)";
                    }
                    NetList[i] = string.Format("{0} {1}", StartMa.Groups[1], Spec);
                    continue;
                }

                Match ThrottleMa = ThrottleRx.Match(NetList[i]);
                if (ThrottleMa.Success)
                {
                    string Spec;
                    if (Settings.StartThrottlePcent != Settings.EndThrottlePcent)
                    {
                        Spec = string.Format("pwl(0m {0:N2} {1}m {2:N2})", Settings.StartThrottlePcent / 100.0, Settings.TotalTimeMs, Settings.EndThrottlePcent / 100.0);
                    }
                    else
                    {
                        Spec = string.Format("dc({0:N2})", Settings.StartThrottlePcent / 100.0);
                    }
                    NetList[i] = string.Format("{0} {1}", ThrottleMa.Groups[1], Spec);
                    continue;
                }

            }
        }

        /// <summary>
        /// Constructs a spice voltage source that ramps the voltage over time
        /// </summary>
        /// <param name="StartTime">Start time in ms</param>
        /// <param name="StartValue">Start value in volts</param>
        /// <param name="EndTime">End time in ms</param>
        /// <param name="EndValue">End value in volts</param>
        /// <returns></returns>
        private string ConstructRamp
            (
            uint StartTime,
            double StartValue,
            uint EndTime,
            double EndValue
            )
        {
            return string.Format("pwl({0}m {1} {2}m {3})", StartTime, StartValue, EndTime, EndValue);
        }
    }
}
