using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DJetronicStudio
{
    public partial class SimulationUI : UserControl, IUI
    {
        public event Action<object, ToolbarButton, bool> OnSetToolbarButtonState = null;
        public event Action<object, StatusLabel, string> OnSetStatusLabelText = null;
        public event Action<object, double> OnPercentageCompleted = null;

        private List<ToolbarButton> ToolbarButtons = new List<ToolbarButton>();
        private List<StatusLabel> StatusLabels = new List<StatusLabel>();
        private Simulator Sim;
        private NGSpice Spice = new NGSpice();
        private ToolbarButton RunSimButton;
        private ToolbarButton StopSimButton;
        private bool Running = false;
        private SimSettings SimulationSettings = new SimSettings();

        public SimulationUI
            (
            Simulator Sim
            )
        {
            this.Sim = Sim;

            InitializeComponent();

            RunSimButton = new ToolbarButton("Run Simulation", Properties.Resources.run_32, RunSimulation, true);
            ToolbarButtons.Add(RunSimButton);
            StopSimButton = new ToolbarButton("Stop Simulation", Properties.Resources.stop, StopSimulation, false);
            ToolbarButtons.Add(StopSimButton);

            Sim.Spice = Spice;
            Spice.OnShowMessage += Spice_OnShowMessage;
            Spice.OnPercentageCompleted += Sim_OnPercentageCompleted;

            Sim.OnSimulationStarted += Sim_OnSimulationStarted;
            Sim.OnSimulationEnded += Sim_OnSimulationEnded;

            SimChart.OnRequestAddCustomData += SimChart_OnRequestAddCustomData;

            string[] VersionInfo = Spice.GetVersion();
            foreach (string Line in VersionInfo)
            {
                OutputBox.AppendText(Line + Environment.NewLine);
            }

            Running = false;

            ShowInitialSettings();

            ShowSimSettings();

            UpdateUI();
        }

        /// <summary>
        /// Called during simulation to show progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="PercentageCompleted"></param>
        private void Sim_OnPercentageCompleted(object sender, double PercentageCompleted)
        {
            if (OnPercentageCompleted != null) OnPercentageCompleted(this, PercentageCompleted);
        }

        /// <summary>
        /// Shows the current simulation settings
        /// </summary>
        private void ShowSimSettings
            (
            )
        {
            TimePeriod.Text = SimulationSettings.TotalTimeMs.ToString();
            ResolutionInput.Text = SimulationSettings.Resolution.ToString();
            if (SimulationSettings.ResolutionUnit == SimSettings.ResolutionUnits.Milliseconds)
                ResolutionUnitsSelector.SelectedItem = "ms";
            else
                ResolutionUnitsSelector.SelectedItem = "us";
            EngineSpeedInput.Text = SimulationSettings.EngineSpeedRpm.ToString();
            StartAirTempInput.Text = SimulationSettings.StartAirTemperatureF.ToString();
            EndAirTempInput.Text = SimulationSettings.EndAirTemperatureF.ToString();
            StartCoolantTempInput.Text = SimulationSettings.StartCoolantTemperatureF.ToString();
            EndCoolantTempInput.Text = SimulationSettings.EndCoolantTemperatureF.ToString();
            StartStarterInput.Text = SimulationSettings.StarterMotorOnMs.ToString();
            EndStarterInput.Text = SimulationSettings.StarterMotorOffMs.ToString();
            StartThrottleInput.Text = SimulationSettings.StartThrottlePcent.ToString();
            EndThrottleInput.Text = SimulationSettings.EndThrottlePcent.ToString();
            StartManifoldVacuumInput.Text = SimulationSettings.StartManifoldVacuumInhg.ToString();
            EndManifoldVacuumInput.Text = SimulationSettings.EndManifoldVacuumInhg.ToString();
            StartRheostatInput.Text = SimulationSettings.StartRheostat.ToString();
            EndRheostatInput.Text = SimulationSettings.EndRheostat.ToString();
            StartBatteryVoltageInput.Text = SimulationSettings.StartBatteryVoltage.ToString();
            EndBatteryVoltageInput.Text = SimulationSettings.EndBatteryVoltage.ToString();
            DwellAngleInput.Text = SimulationSettings.DwellAngle.ToString();
        }

        /// <summary>
        /// Gets the current simulation settings
        /// </summary>
        private void GetSimSettings
            (
            )
        {
            if (!uint.TryParse(TimePeriod.Text, out SimulationSettings.TotalTimeMs))
            {
                throw new Exception("Invalid total time for simulation");
            }

            if (SimulationSettings.TotalTimeMs <= 0) throw new Exception("Simulation total time must be greater than zero");

            if (!uint.TryParse(ResolutionInput.Text, out SimulationSettings.Resolution))
            {
                throw new Exception("Invalid resolution for simulation");
            }

            if (SimulationSettings.Resolution <= 0) throw new Exception("Simulation resolution must be greater than zero");

            if ((string)ResolutionUnitsSelector.SelectedItem == "ms")
                SimulationSettings.ResolutionUnit = SimSettings.ResolutionUnits.Milliseconds;
            else
                SimulationSettings.ResolutionUnit = SimSettings.ResolutionUnits.Microseconds;

            if (!uint.TryParse(EngineSpeedInput.Text, out SimulationSettings.EngineSpeedRpm)) throw new Exception("Invalid value for engine speed");

            if ((SimulationSettings.EngineSpeedRpm < 0) || (SimulationSettings.EngineSpeedRpm > 6500)) throw new Exception("Engine speed must be in the range 0 to 6500");

            if (!double.TryParse(StartAirTempInput.Text, out SimulationSettings.StartAirTemperatureF)) throw new Exception("Invalid start value for air temperature");
            if (!double.TryParse(EndAirTempInput.Text, out SimulationSettings.EndAirTemperatureF)) throw new Exception("Invalid end value for air temperature");

            if (!double.TryParse(StartCoolantTempInput.Text, out SimulationSettings.StartCoolantTemperatureF)) throw new Exception("Invalid start value for coolant temperature");
            if (!double.TryParse(EndCoolantTempInput.Text, out SimulationSettings.EndCoolantTemperatureF)) throw new Exception("Invalid end value for coolant temperature");

            if (!uint.TryParse(StartStarterInput.Text, out SimulationSettings.StarterMotorOnMs)) throw new Exception("Invalid start value for starter motor");
            if (!uint.TryParse(EndStarterInput.Text, out SimulationSettings.StarterMotorOffMs)) throw new Exception("Invalid end value for starter motor");

            if (SimulationSettings.StarterMotorOffMs < SimulationSettings.StarterMotorOnMs)
            {
                throw new Exception("Starter motor off time must be later than the on time");
            }
            if ((SimulationSettings.StarterMotorOffMs == SimulationSettings.StarterMotorOnMs) && (SimulationSettings.StarterMotorOffMs > 0))
            {
                throw new Exception("Starter motor on and off times cannot be the same (unless they are both zero)");
            }

            if ((SimulationSettings.StarterMotorOnMs < 0) || (SimulationSettings.StarterMotorOnMs > SimulationSettings.TotalTimeMs)) throw new Exception("Starter motor on time must be in the range 0 to Total Time");
            if ((SimulationSettings.StarterMotorOffMs < 0) || (SimulationSettings.StarterMotorOffMs > SimulationSettings.TotalTimeMs)) throw new Exception("Starter motor off time must be in the range 0 to Total Time");

            if (!uint.TryParse(StartThrottleInput.Text, out SimulationSettings.StartThrottlePcent)) throw new Exception("Invalid start value for throttle");
            if (!uint.TryParse(EndThrottleInput.Text, out SimulationSettings.EndThrottlePcent)) throw new Exception("Invalid end value for throttle");

            if ((SimulationSettings.StartThrottlePcent < 0) || (SimulationSettings.StartThrottlePcent > 100)) throw new Exception("Start throttle must be in the range 0 to 100");
            if ((SimulationSettings.EndThrottlePcent < 0) || (SimulationSettings.EndThrottlePcent > 100)) throw new Exception("End throttle must be in the range 0 to 100");

            if (!uint.TryParse(StartManifoldVacuumInput.Text, out SimulationSettings.StartManifoldVacuumInhg)) throw new Exception("Invalid start value for manifold vacuum");
            if (!uint.TryParse(EndManifoldVacuumInput.Text, out SimulationSettings.EndManifoldVacuumInhg)) throw new Exception("Invalid end value for manifold vacuum");

            if ((SimulationSettings.StartManifoldVacuumInhg < 0) || (SimulationSettings.StartManifoldVacuumInhg > 15)) throw new Exception("Start manifold vacuum must be in the range 0 to 15");
            if ((SimulationSettings.EndManifoldVacuumInhg < 0) || (SimulationSettings.EndManifoldVacuumInhg > 15)) throw new Exception("End manifold vacuum must be in the range 0 to 15");

            if (!uint.TryParse(StartRheostatInput.Text, out SimulationSettings.StartRheostat)) throw new Exception("Invalid start value for rheostat");
            if (!uint.TryParse(EndRheostatInput.Text, out SimulationSettings.EndRheostat)) throw new Exception("Invalid end value for rheostat");

            if ((SimulationSettings.StartRheostat < 0) || (SimulationSettings.StartRheostat > 22)) throw new Exception("Start rheostat value must be in the range 0 to 22");
            if ((SimulationSettings.EndRheostat < 0) || (SimulationSettings.EndRheostat > 22)) throw new Exception("End rheostat value must be in the range 0 to 22");

            if (!double.TryParse(StartBatteryVoltageInput.Text, out SimulationSettings.StartBatteryVoltage)) throw new Exception("Invalid start value for battery voltage");
            if (!double.TryParse(EndBatteryVoltageInput.Text, out SimulationSettings.EndBatteryVoltage)) throw new Exception("Invalid end value for battery voltage");

            if (SimulationSettings.StartBatteryVoltage < 0) throw new Exception("Start battery voltage cannot be less than zero");
            if (SimulationSettings.EndBatteryVoltage < 0) throw new Exception("End battery voltage cannot be less than zero");

            if (!uint.TryParse(DwellAngleInput.Text, out SimulationSettings.DwellAngle)) throw new Exception("Invalid value for dwell angle");

            if ((SimulationSettings.DwellAngle < 100) || (SimulationSettings.DwellAngle > 180)) throw new Exception("Dwell angle must be in the range 100 to 180");
        }

        /// <summary>
        /// Called when the simulation has started
        /// </summary>
        /// <param name="sender"></param>
        private void Sim_OnSimulationEnded(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<object>(Sim_OnSimulationEnded), sender);
                return;
            }

            Running = false;
            UpdateUI();

            ShowData();
        }

        /// <summary>
        /// Called when the simulation has ended
        /// </summary>
        /// <param name="sender"></param>
        private void Sim_OnSimulationStarted(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<object>(Sim_OnSimulationStarted), sender);
                return;
            }

            Running = true;
            UpdateUI();
        }

        /// <summary>
        /// Called when simulator wants to output a message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Message"></param>
        private void Spice_OnShowMessage(object sender, string Message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<object, string>(Spice_OnShowMessage), sender, Message);
                return;
            }

            OutputBox.AppendText(Message + Environment.NewLine);
        }

        /// <summary>
        /// Gets the toolbar buttons
        /// </summary>
        /// <returns>Enumeration of buttons</returns>
        public IEnumerable<ToolbarButton> GetToolbarButtons
            (
            )
        {
            foreach (ToolbarButton Button in ToolbarButtons)
            {
                yield return Button;
            }
        }

        /// <summary>
        /// Gets the status labels
        /// </summary>
        /// <returns>Enumeration of status labels</returns>
        public IEnumerable<StatusLabel> GetStatusLabels
            (
            )
        {
            foreach (StatusLabel Label in StatusLabels)
            {
                yield return Label;
            }
        }

        /// <summary>
        /// Called when the user interface has been constructed
        /// </summary>
        public void UIReady
            (
            )
        {
        }

        /// <summary>
        /// Shows the initial settings
        /// </summary>
        private void ShowInitialSettings
            (
            )
        {
        }

        /// <summary>
        /// Updates the user interface with the current settings
        /// </summary>
        private void UpdateUI
            (
            )
        {
            if (OnSetToolbarButtonState != null) OnSetToolbarButtonState(this, RunSimButton, !Running);
            if (OnSetToolbarButtonState != null) OnSetToolbarButtonState(this, StopSimButton, Running);
        }

        /// <summary>
        /// Stops the simulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopSimulation
            (
            object sender,
            EventArgs e
            )
        {
            Sim.Stop();
        }

        /// <summary>
        /// Runs the simulation
        /// </summary>
        private void RunSimulation
            (
            object sender,
            EventArgs e
            )
        {
            try
            {
                OutputBox.Clear();
                ClearData();
                GetSimSettings();
                uint StepUs = SimulationSettings.Resolution;
                if (SimulationSettings.ResolutionUnit == SimSettings.ResolutionUnits.Milliseconds)
                    StepUs = StepUs * 1000;
                Sim.Run(SimulationSettings);
            }
            catch (Exception Exc)
            {
                MessageBox.Show("Failed to start simulation. " + Exc.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Running = false;
                UpdateUI();
            }
        }

        /// <summary>
        /// Called when chart wants to add custom simulation data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="VectorName">Name of simulation vector for data</param>
        private void SimChart_OnRequestAddCustomData(object sender, string VectorName)
        {
            SimChart.AddData(VectorName, VectorName, SimChart.SimData.DataSources.Custom, Spice.GetData(VectorName));
        }

        /// <summary>
        /// Shows simulation data in the UI
        /// </summary>
        private void ShowData
            (
            )
        {
            SimChart.SetTimeRange(0, SimulationSettings.TotalTimeMs);

            SimChart.AddData("Injector group I", "E3-INJ1-5", SimChart.SimData.DataSources.InjectorGroupI, Spice.GetData("E3-INJ1-5"));
            SimChart.AddData("Injector group II", "E5-INJ6-3", SimChart.SimData.DataSources.InjectorGroupII, Spice.GetData("E5-INJ6-3"));
            SimChart.AddData("Injector group III", "E4-INJ4-8", SimChart.SimData.DataSources.InjectorGroupIII, Spice.GetData("E4-INJ4-8"));
            SimChart.AddData("Injector group IV", "E6-INJ7-2", SimChart.SimData.DataSources.InjectorGroupIV, Spice.GetData("E6-INJ7-2"));

            SimChart.AddData("MPS pin 7", "AUX9-BP", SimChart.SimData.DataSources.MPSPin7, Spice.GetData("AUX9-BP"));
            SimChart.AddData("MPS pin 8", "E8-MPS", SimChart.SimData.DataSources.MPSPin8, Spice.GetData("E8-MPS"));
            SimChart.AddData("MPS pin 10", "E10-MPS", SimChart.SimData.DataSources.MPSPin10, Spice.GetData("E10-MPS"));
            SimChart.AddData("MPS pin 15", "E15-MPS", SimChart.SimData.DataSources.MPSPin15, Spice.GetData("E15-MPS"));

            SimChart.AddData("Air temperature", "E1-T1", SimChart.SimData.DataSources.AirTemperature, Spice.GetData("E1-T1"));
            SimChart.AddData("Coolant temperature", "E23-T2", SimChart.SimData.DataSources.CoolantTemperature, Spice.GetData("E23-T2"));

            SimChart.AddData("TPS full throttle (diag II and IV)", "E2-TPS", SimChart.SimData.DataSources.TPSFullThrottle_DiagII_IV, Spice.GetData("E2-TPS"));
            SimChart.AddData("TPS idle", "AUX2-IDL", SimChart.SimData.DataSources.TPSIdle, Spice.GetData("AUX2-IDL"));
            SimChart.AddData("TPS acceleration 1", "AUX3-AE9", SimChart.SimData.DataSources.TPSAcceleration1, Spice.GetData("AUX3-AE9"));
            SimChart.AddData("TPS acceleration 2", "AUX5-AE20", SimChart.SimData.DataSources.TPSAcceleration2, Spice.GetData("AUX5-AE20"));

            SimChart.AddData("Diag I and III", "E25-DIAG1", SimChart.SimData.DataSources.DiagI_III, Spice.GetData("E25-DIAG1"));

            SimChart.AddData("Pulse generator group I", "E21-PG", SimChart.SimData.DataSources.PulseGeneratorGroupI, Spice.GetData("E21-PG"));
            SimChart.AddData("Pulse generator group II", "E13-PG", SimChart.SimData.DataSources.PulseGeneratorGroupII, Spice.GetData("E13-PG"));
            SimChart.AddData("Pulse generator group III", "E22-PG", SimChart.SimData.DataSources.PulseGeneratorGroupIII, Spice.GetData("E22-PG"));
            SimChart.AddData("Pulse generator group IV", "E14-PG", SimChart.SimData.DataSources.PulseGeneratorGroupIV, Spice.GetData("E14-PG"));

            SimChart.AddData("Start", "E18-START", SimChart.SimData.DataSources.Start, Spice.GetData("E18-START"));
            SimChart.AddData("Fuel pump relay", "E19-FPR", SimChart.SimData.DataSources.FuelPumpRelay, Spice.GetData("E19-FPR"));
        }

        /// <summary>
        /// Clears all of the simulation data from the UI
        /// </summary>
        private void ClearData
            (
            )
        {
            SimChart.ClearAll();
        }
    }
}
