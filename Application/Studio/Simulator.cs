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

        private const string AIR_TEMPERATURE_VOLTAGE_SOURCE = "V1";
        private const string THROTTLE_ENRICHMENT_VOLTAGE_SOURCE = "V3";
        private const string THROTTLE_VOLTAGE_SOURCE = "V4";
        private const string ENGINE_VACUUM_VOLTAGE_SOURCE = "V5";
        private const string RHEOSTAT_POSITION_VOLTAGE_SOURCE = "V6";
        private const string START_VOLTAGE_SOURCE = "V7";
        private const string PG13_VOLTAGE_SOURCE = "V13";
        private const string PG14_VOLTAGE_SOURCE = "V14";
        private const string BATTERY_VOLTAGE_SOURCE_1 = "V16";
        private const string PG21_VOLTAGE_SOURCE = "V21";
        private const string PG22_VOLTAGE_SOURCE = "V22";
        private const string COOLANT_TEMPERATURE_VOLTAGE_SOURCE = "V23";
        private const string BATTERY_VOLTAGE_SOURCE_2 = "V24";

        private const int PG_SPEC_INDEX_E13 = 0;
        private const int PG_SPEC_INDEX_E14 = 1;
        private const int PG_SPEC_INDEX_E21 = 2;
        private const int PG_SPEC_INDEX_E22 = 3;

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

                // fixme - remove
                //File.WriteAllLines(@"D:\dump.txt", ProcessedNetList);

                Spice.SpecifyCircuit(ProcessedNetList.ToArray());
            }
            finally
            {
                SimThread = null;
                if (OnSimulationEnded != null) OnSimulationEnded(this);
            }
        }

        /// <summary>
        /// Applies the current settings to a netlist
        /// </summary>
        /// <param name="NetList">Netlist to change</param>
        private void ApplySettings
            (
            List<string> NetList
            )
        {
            Regex BatteryVoltage1Rx = new Regex(@"^(" + BATTERY_VOLTAGE_SOURCE_1 + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex BatteryVoltage2Rx = new Regex(@"^(" + BATTERY_VOLTAGE_SOURCE_2 + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex EngineVacuumRx = new Regex(@"^(" + ENGINE_VACUUM_VOLTAGE_SOURCE + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex RheostatRx = new Regex(@"^(" + RHEOSTAT_POSITION_VOLTAGE_SOURCE + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex StartRx = new Regex(@"^(" + START_VOLTAGE_SOURCE + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex ThrottleRx = new Regex(@"^(" + THROTTLE_VOLTAGE_SOURCE + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex PG13Rx = new Regex(@"^(" + PG13_VOLTAGE_SOURCE + @"\s.+\s+)dc\s+[0-9]+\s+(pulse\s*[mu\ \(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex PG14Rx = new Regex(@"^(" + PG14_VOLTAGE_SOURCE + @"\s.+\s+)dc\s+[0-9]+\s+(pulse\s*[mu\ \(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex PG21Rx = new Regex(@"^(" + PG21_VOLTAGE_SOURCE + @"\s.+\s+)dc\s+[0-9]+\s+(pulse\s*[mu\ \(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex PG22Rx = new Regex(@"^(" + PG22_VOLTAGE_SOURCE + @"\s.+\s+)dc\s+[0-9]+\s+(pulse\s*[mu\ \(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex ThrottleEnrichmentRx = new Regex(@"^(" + THROTTLE_ENRICHMENT_VOLTAGE_SOURCE + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex AirTempRx = new Regex(@"^(" + AIR_TEMPERATURE_VOLTAGE_SOURCE + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);
            Regex CoolantTempRx = new Regex(@"^(" + COOLANT_TEMPERATURE_VOLTAGE_SOURCE + @"\s.+\s+)(dc\s*[\(\)0-9\.]+)\s*$", RegexOptions.IgnoreCase);

            string[] PGSpecs = ConstructPulseGenerator(Settings.EngineSpeedRpm, Settings.DwellAngle);

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

                Match ThrottleEnrichmentMa = ThrottleEnrichmentRx.Match(NetList[i]);
                if (ThrottleEnrichmentMa.Success)
                {
                    string Spec;
                    if (Settings.StartThrottlePcent < Settings.EndThrottlePcent)
                    {
                        Spec = "dc(4)";
                    }
                    else
                    {
                        Spec = "dc(0)";
                    }
                    NetList[i] = string.Format("{0} {1}", ThrottleEnrichmentMa.Groups[1], Spec);
                    continue;
                }

                Match PG13Ma = PG13Rx.Match(NetList[i]);
                if (PG13Ma.Success)
                {
                    NetList[i] = string.Format("{0} {1}", PG13Ma.Groups[1], PGSpecs[PG_SPEC_INDEX_E13]);
                    continue;
                }
                Match PG14Ma = PG14Rx.Match(NetList[i]);
                if (PG14Ma.Success)
                {
                    NetList[i] = string.Format("{0} {1}", PG14Ma.Groups[1], PGSpecs[PG_SPEC_INDEX_E14]);
                    continue;
                }
                Match PG21Ma = PG21Rx.Match(NetList[i]);
                if (PG21Ma.Success)
                {
                    NetList[i] = string.Format("{0} {1}", PG21Ma.Groups[1], PGSpecs[PG_SPEC_INDEX_E21]);
                    continue;
                }
                Match PG22Ma = PG22Rx.Match(NetList[i]);
                if (PG22Ma.Success)
                {
                    NetList[i] = string.Format("{0} {1}", PG22Ma.Groups[1], PGSpecs[PG_SPEC_INDEX_E22]);
                    continue;
                }

                Match AirTempMa = AirTempRx.Match(NetList[i]);
                if (AirTempMa.Success)
                {
                    string Spec;
                    if (Settings.StartAirTemperatureF != Settings.EndAirTemperatureF)
                    {
                        Spec = string.Format("pwl(0m {0} {1}m {2})", (int)((Settings.StartAirTemperatureF - 32.0) * (5.0 / 9.0)), Settings.TotalTimeMs, (int)((Settings.EndAirTemperatureF - 32.0) * (5.0 / 9.0)));
                    }
                    else
                    {
                        Spec = string.Format("dc({0})", (int)((Settings.StartAirTemperatureF - 32.0) * (5.0 / 9.0)));
                    }
                    NetList[i] = string.Format("{0} {1}", AirTempMa.Groups[1], Spec);
                    continue;
                }

                Match CoolantTempMa = CoolantTempRx.Match(NetList[i]);
                if (CoolantTempMa.Success)
                {
                    string Spec;
                    if (Settings.StartCoolantTemperatureF != Settings.EndCoolantTemperatureF)
                    {
                        Spec = string.Format("pwl(0m {0} {1}m {2})", (int)((Settings.StartCoolantTemperatureF - 32.0) * (5.0 / 9.0)), Settings.TotalTimeMs, (int)((Settings.EndCoolantTemperatureF - 32.0) * (5.0 / 9.0)));
                    }
                    else
                    {
                        Spec = string.Format("dc({0})", (int)((Settings.StartCoolantTemperatureF - 32.0) * (5.0 / 9.0)));
                    }
                    NetList[i] = string.Format("{0} {1}", CoolantTempMa.Groups[1], Spec);
                    continue;
                }
            }
        }

        /// <summary>
        /// Creates the pulse generator spice statements
        /// </summary>
        /// <param name="EngineSpeedRpm">Speed of engine in RPM</param>
        /// <param name="DwellAngle">Dwell angle of pulse generator</param>
        /// <returns>Set of four spice statements E13, E14, E21, E22</returns>
        private string[] ConstructPulseGenerator
            (
            uint EngineSpeedRpm,
            uint DwellAngle
            )
        {
            string[] Signals = new string[4];

            string RiseFallTime = "100u";
            double Distributor_RPM = EngineSpeedRpm / 2.0;
            double RotationsPerSec = Distributor_RPM / 60.0;
            double MillisecondsPerRotation = 1.0 / RotationsPerSec * 1000.0;
            double NegDwellPcentPeriod = DwellAngle / 360.0;
            double NegDwell_ms = MillisecondsPerRotation * NegDwellPcentPeriod;
            double DwellPcentPeriod = (360.0 - DwellAngle) / 360.0;
            double Dwell_ms = MillisecondsPerRotation * DwellPcentPeriod;
            double PG1StartOffset = MillisecondsPerRotation * 0.5;
            double PG2StartOffset = MillisecondsPerRotation * 0.25;
            double PG3StartOffset = 0;
            double PG4StartOffset = NegDwell_ms - PG2StartOffset;

            Signals[PG_SPEC_INDEX_E22] = "dc 5 pulse(0 5 " + PG3StartOffset.ToString() + "m " + RiseFallTime + " " + RiseFallTime + " " + NegDwell_ms.ToString() + "m " + MillisecondsPerRotation.ToString() + "m)";
            Signals[PG_SPEC_INDEX_E21] = "dc 5 pulse(0 5 " + PG1StartOffset.ToString() + "m " + RiseFallTime + " " + RiseFallTime + " " + NegDwell_ms.ToString() + "m " + MillisecondsPerRotation.ToString() + "m)";
            Signals[PG_SPEC_INDEX_E14] = "dc 5 pulse(0 5 " + PG4StartOffset.ToString() + "m " + RiseFallTime + " " + RiseFallTime + " " + Dwell_ms.ToString() + "m " + MillisecondsPerRotation.ToString() + "m)";
            Signals[PG_SPEC_INDEX_E13] = "dc 5 pulse(5 0 " + PG2StartOffset.ToString() + "m " + RiseFallTime + " " + RiseFallTime + " " + NegDwell_ms.ToString() + "m " + MillisecondsPerRotation.ToString() + "m)";

            return Signals;
        }
    }
}
