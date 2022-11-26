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
        /// Shows simulation data in the UI
        /// </summary>
        private void ShowData
            (
            )
        {
            List<NGSpice.SimDataPoint> Values = Spice.GetData("E4-INJ4-8");
            SimChart.AddData("Injector Group IV", "E4-INJ4-8", Values);
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
