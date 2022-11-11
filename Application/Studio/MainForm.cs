using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DJetronicStudio
{
    public partial class MainForm : Form
    {
        private ECUTester Tester = new ECUTester();
        private Simulator Sim = new Simulator();
        private TuneOMatic Tuner = new TuneOMatic();

        private const byte PRODUCT_UID_ECUTESTER = 0x01;
        private const byte PRODUCT_UID_TUNEOMATIC = 0x02;

        private Color Orange = Color.FromArgb(202, 81, 0);
        private List<ToolStripItem> UIToolStripItems = new List<ToolStripItem>();

        private bool IsConnected
        {
            get
            {
                if (Tester.IsConnected || Sim.IsConnected || Tuner.IsConnected)
                    return true;
                else
                    return false;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            Tester.OnConnected += Tester_OnConnected;
            Tester.OnDisconnected += Tester_OnDisconnected;

            Sim.OnConnected += Sim_OnConnected;
            Sim.OnDisconnected += Sim_OnDisconnected;

            Tuner.OnConnected += Tuner_OnConnected;
            Tuner.OnDisconnected += Tuner_OnDisconnected;

            ConnectionStatus.Text = "";
            ConnectionStatus.Visible = false;

            UpdateUI();
        }

        /// <summary>
        /// Adds a toolstrip separator
        /// </summary>
        /// <returns>Added separator</returns>
        private ToolStripItem AddToolStripSeparator
            (
            )
        {
            ToolStripSeparator Sep = new ToolStripSeparator();

            Sep.Name = "";
            Sep.Size = new System.Drawing.Size(6, 39);

            MainToolStrip.Items.Add(Sep);

            return Sep;
        }

        /// <summary>
        /// Adds a toolbar button
        /// </summary>
        /// <param name="Btn">Description of button to add</param>
        /// <returns>Added button</returns>
        private ToolStripItem AddToolbarButton
            (
            ToolbarButton Btn
            )
        {
            ToolStripButton NewButton = new ToolStripButton();

            NewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            NewButton.Image = Btn.ButtonImage;
            NewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            NewButton.Name = "";
            NewButton.Size = new System.Drawing.Size(36, 36);
            NewButton.Text = Btn.Description;
            NewButton.Click += new System.EventHandler(Btn.ButtonAction);
            NewButton.Enabled = Btn.Enabled;
            NewButton.Tag = Btn;

            MainToolStrip.Items.Add(NewButton);

            return NewButton;
        }
       
        /// <summary>
        /// Adds a label to the status strip
        /// </summary>
        /// <param name="Label">Description of label to add</param>
        /// <returns>The added label</returns>
        private ToolStripItem AddStatusLabel
            (
            StatusLabel Label
            )
        {
            ToolStripStatusLabel NewLabel = new ToolStripStatusLabel();
            NewLabel.Image = Label.StatusImage;
            NewLabel.Name = "";
            NewLabel.Text = Label.Text;
            NewLabel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            NewLabel.Size = new System.Drawing.Size(Label.Width, 24);
            NewLabel.ForeColor = Color.White;
            NewLabel.Tag = Label;

            StatusStrip.Items.Add(NewLabel);

            return NewLabel;
        }

        /// <summary>
        /// Called when a user interface control wants to change the enabled/disabled
        /// state of a toolbar button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Button"></param>
        /// <param name="Enabled"></param>
        private void UI_OnSetToolbarButtonState(object sender, ToolbarButton Button, bool Enabled)
        {
            foreach (ToolStripItem StripItem in UIToolStripItems)
            {
                if (StripItem.Tag == Button)
                {
                    StripItem.Enabled = Enabled;
                    return;
                }
            }
        }

        /// <summary>
        /// Called when a user interface control wants to change the text on a status strip label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Label"></param>
        /// <param name="Text"></param>
        private void UI_OnSetStatusLabelText(object sender, StatusLabel Label, string Text)
        {
            foreach (ToolStripItem StripItem in UIToolStripItems)
            {
                if (StripItem.Tag == Label)
                {
                    StripItem.Text = Text;
                    return;
                }
            }
        }

        /// <summary>
        /// Removes the current user interface
        /// </summary>
        private void RemoveUI
            (
            )
        {
            if (UIPanel.Controls.Count > 0)
            {
                UIPanel.Controls.RemoveAt(0);
            }

            foreach (ToolStripItem StripItem in UIToolStripItems)
            {
                StripItem.GetCurrentParent().Items.Remove(StripItem);
            }
            UIToolStripItems.Clear();
        }

        /// <summary>
        /// Builds the user interface
        /// </summary>
        /// <param name="UI">User control/UI description</param>
        private void BuildUI
            (
            IUI UI
            )
        {
            ((UserControl)UI).Parent = UIPanel;
            ((UserControl)UI).Dock = DockStyle.Fill;
            ((UserControl)UI).Visible = true;

            UI.OnSetToolbarButtonState += UI_OnSetToolbarButtonState;
            UI.OnSetStatusLabelText += UI_OnSetStatusLabelText;

            bool First = true;
            foreach (ToolbarButton Btn in UI.GetToolbarButtons())
            {
                if (First)
                {
                    UIToolStripItems.Add(AddToolStripSeparator());
                    First = false;
                }
                UIToolStripItems.Add(AddToolbarButton(Btn));
            }
            foreach (StatusLabel Label in UI.GetStatusLabels())
            {
                UIToolStripItems.Add(AddStatusLabel(Label));
            }

            UI.UIReady();
        }

        /// <summary>
        /// Called when disconnected from the tester
        /// </summary>
        /// <param name="sender">Tester object</param>
        private void Tester_OnDisconnected(object sender)
        {
            ConnectionStatus.Text = "";
            ConnectionStatus.Visible = false;
            RemoveUI();
            UpdateUI();
        }

        /// <summary>
        /// Called when connected to the tester
        /// </summary>
        /// <param name="sender">Tester object</param>
        /// <param name="PortName">Name of COM port</param>
        /// <param name="Baudrate">Connection speed in bps</param>
        private void Tester_OnConnected(object sender, string PortName, int Baudrate, byte FirmwareMajorVersion, byte FirmwareMinorVersion)
        {
            ConnectionStatus.Text = string.Format("Connected to ECU tester V{0}.{1} on {2}", FirmwareMajorVersion, FirmwareMinorVersion, PortName);
            ConnectionStatus.Image = Properties.Resources.tester_24;
            ConnectionStatus.Visible = true;

            ECUTesterUI UI = new ECUTesterUI(Tester);
            BuildUI(UI);

            UpdateUI();
        }

        /// <summary>
        /// Called when connected to the simulator
        /// </summary>
        /// <param name="sender">Simulation object</param>
        private void Sim_OnDisconnected(object sender)
        {
            ConnectionStatus.Text = "";
            ConnectionStatus.Visible = false;
            RemoveUI();
            UpdateUI();
        }

        /// <summary>
        /// Called when connected to the simulator
        /// </summary>
        /// <param name="sender">Simulation object</param>
        private void Sim_OnConnected(object sender)
        {
            ConnectionStatus.Text = string.Format("Connected to ECU simulator");
            ConnectionStatus.Image = Properties.Resources.simulation_24;
            ConnectionStatus.Visible = true;

            SimulationUI UI = new SimulationUI(Sim);
            BuildUI(UI);

            UpdateUI();
        }

        /// <summary>
        /// Called when tuneomatic is disconnected
        /// </summary>
        /// <param name="sender"></param>
        private void Tuner_OnDisconnected(object sender)
        {
            ConnectionStatus.Text = "";
            ConnectionStatus.Visible = false;
            RemoveUI();
            UpdateUI();
        }

        /// <summary>
        /// Called when connected to the tune-o-matic
        /// </summary>
        /// <param name="sender">Tune-o-matic object</param>
        /// <param name="PortName">Name of COM port</param>
        /// <param name="Baudrate">Connection speed in bps</param>
        private void Tuner_OnConnected(object sender, string PortName, int Baudrate, byte FirmwareMajorVersion, byte FirmwareMinorVersion)
        {
            ConnectionStatus.Text = string.Format("Connected to MPS Tune-o-Matic V{0}.{1} on {2}", FirmwareMajorVersion, FirmwareMinorVersion, PortName);
            ConnectionStatus.Image = Properties.Resources.tuneomatic_24;
            ConnectionStatus.Visible = true;

            TuneOMaticUI UI = new TuneOMaticUI(Tuner);
            BuildUI(UI);

            UpdateUI();
        }

        /// <summary>
        /// Called when user clicks on connect button
        /// Finds and connects to tester or simulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            Connect();
        }

        /// <summary>
        /// Prompts the user to choose what to connect to
        /// </summary>
        private void Connect
            (
            )
        {
            ConnectForm CForm = new ConnectForm();
            if (CForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (CForm.UseTester == true)
                    {
                        Tester.Connect(PRODUCT_UID_ECUTESTER);
                    }
                    else if (CForm.UseSimulation == true)
                    {
                        Sim.Connect();
                    }
                    else if (CForm.UseTuneOMatic == true)
                    {
                        Tuner.Connect(PRODUCT_UID_TUNEOMATIC);
                    }
                }
                catch (Exception Exc)
                {
                    MessageBox.Show(Exc.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// Disconnects from hardware/simulator
        /// </summary>
        private void Disconnect
            (
            )
        {
            Tester.Disconnect();
            Sim.Disconnect();
            Tuner.Disconnect();
            UpdateUI();
        }

        /// <summary>
        /// Called when user clicks on the disconnect button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisconnectBtn_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        /// <summary>
        /// Updates the user interface with the current settings
        /// </summary>
        private void UpdateUI
            (
            )
        {
            ConnectBtn.Enabled = !IsConnected;
            connectToolStripMenuItem.Enabled = !IsConnected;
            DisconnectBtn.Enabled = IsConnected;
            disconnectToolStripMenuItem.Enabled = IsConnected;

            if (IsConnected)
            {
                if (Tester.IsConnected)
                {
                    StatusStrip.BackColor = Orange;
                    ConnectionStatus.ForeColor = Color.White;
                }
                else if (Sim.IsConnected)
                {
                    StatusStrip.BackColor = Color.Green;
                    ConnectionStatus.ForeColor = Color.White;
                }
                else if (Tuner.IsConnected)
                {
                    StatusStrip.BackColor = Color.CornflowerBlue;
                    ConnectionStatus.ForeColor = Color.White;
                }
            }
            else
            {
                StatusStrip.BackColor = SystemColors.Control;
                ConnectionStatus.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Called when user chooses File->Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Disconnect();
            Close();
        }

        /// <summary>
        /// Called when user chooses Help->About
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm AForm = new AboutForm();
            AForm.Version = string.Format("Version {0}", Application.ProductVersion);
            AForm.ShowDialog();
        }

        /// <summary>
        /// Called when user chooses Hardware->Connect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Connect();
        }

        /// <summary>
        /// Called when user chooses Hardware->Disconnect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        /// <summary>
        /// Called when form is closing
        /// Make sure we cleanly disconnect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }
    }
}
