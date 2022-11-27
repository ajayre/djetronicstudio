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

        private List<ToolbarButton> ToolbarButtons = new List<ToolbarButton>();
        private List<StatusLabel> StatusLabels = new List<StatusLabel>();
        private Simulator Sim;
        private NGSpice Spice = new NGSpice();
        private ToolbarButton RunSimButton;
        private ToolbarButton StopSimButton;
        private bool Running = false;

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

            UpdateUI();
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
                Sim.Run(0, 30, 8);
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
            SimChart.SetTimeRange(0, 30);

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
            SimChart.Clear();
        }
    }
}
