namespace DJetronicStudio
{
    partial class TuneOMaticUI
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TuneOMaticUI));
            this.ExportCSVDialog = new System.Windows.Forms.SaveFileDialog();
            this.ImportMPSProfileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ExportMPSProfileDialog = new System.Windows.Forms.SaveFileDialog();
            this.Tabs = new DJetronicStudio.TablessControl();
            this.DbPage = new System.Windows.Forms.TabPage();
            this.TunePage1 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.wizardText1 = new DJetronicStudio.WizardText();
            this.TunePage1Nav = new DJetronicStudio.WizardNavigation();
            this.TunePage1Banner = new DJetronicStudio.WizardBanner();
            this.TunePage2 = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.TunePage2WizardText = new DJetronicStudio.WizardText();
            this.TunePage2Nav = new DJetronicStudio.WizardNavigation();
            this.TunePage2Banner = new DJetronicStudio.WizardBanner();
            this.TunePage3 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.ReferenceSelector = new System.Windows.Forms.ComboBox();
            this.wizardText3 = new DJetronicStudio.WizardText();
            this.wizardNavigation1 = new DJetronicStudio.WizardNavigation();
            this.wizardBanner1 = new DJetronicStudio.WizardBanner();
            this.TunePage4 = new System.Windows.Forms.TabPage();
            this.Gauge = new DJetronicStudio.TunerGauge();
            this.wizardText4 = new DJetronicStudio.WizardText();
            this.wizardNavigation2 = new DJetronicStudio.WizardNavigation();
            this.wizardBanner2 = new DJetronicStudio.WizardBanner();
            this.AddPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AddCalibrationSelector = new System.Windows.Forms.ComboBox();
            this.AddDescriptionInput = new System.Windows.Forms.TextBox();
            this.AddNameInput = new System.Windows.Forms.TextBox();
            this.wizardText2 = new DJetronicStudio.WizardText();
            this.wizardNavigation3 = new DJetronicStudio.WizardNavigation();
            this.wizardBanner3 = new DJetronicStudio.WizardBanner();
            this.AddPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.wizardText5 = new DJetronicStudio.WizardText();
            this.wizardNavigation4 = new DJetronicStudio.WizardNavigation();
            this.wizardBanner4 = new DJetronicStudio.WizardBanner();
            this.AddPage3 = new System.Windows.Forms.TabPage();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.wizardText6 = new DJetronicStudio.WizardText();
            this.wizardNavigation5 = new DJetronicStudio.WizardNavigation();
            this.wizardBanner5 = new DJetronicStudio.WizardBanner();
            this.AddPage4 = new System.Windows.Forms.TabPage();
            this.AddPressureButtonGrid = new DJetronicStudio.ReadPressureButtonGrid();
            this.wizardText7 = new DJetronicStudio.WizardText();
            this.wizardNavigation6 = new DJetronicStudio.WizardNavigation();
            this.wizardBanner6 = new DJetronicStudio.WizardBanner();
            this.ChartPage = new System.Windows.Forms.TabPage();
            this.Chart = new DJetronicStudio.MPSChart();
            this.Tabs.SuspendLayout();
            this.TunePage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.TunePage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.TunePage3.SuspendLayout();
            this.TunePage4.SuspendLayout();
            this.AddPage1.SuspendLayout();
            this.AddPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.AddPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.AddPage4.SuspendLayout();
            this.ChartPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExportCSVDialog
            // 
            this.ExportCSVDialog.DefaultExt = "csv";
            this.ExportCSVDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            this.ExportCSVDialog.Title = "Export Data to CSV";
            // 
            // ImportMPSProfileDialog
            // 
            this.ImportMPSProfileDialog.DefaultExt = "mps";
            this.ImportMPSProfileDialog.Filter = "MPS Profiles (*.mps)|*.mps|All Files (*.*)|*.*";
            this.ImportMPSProfileDialog.Title = "Import MPS Profile";
            // 
            // ExportMPSProfileDialog
            // 
            this.ExportMPSProfileDialog.DefaultExt = "mps";
            this.ExportMPSProfileDialog.Filter = "MPS Profiles (*.mps)|*.mps|All Files (*.*)|*.*";
            this.ExportMPSProfileDialog.Title = "Export MPS Profile";
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.DbPage);
            this.Tabs.Controls.Add(this.TunePage1);
            this.Tabs.Controls.Add(this.TunePage2);
            this.Tabs.Controls.Add(this.TunePage3);
            this.Tabs.Controls.Add(this.TunePage4);
            this.Tabs.Controls.Add(this.AddPage1);
            this.Tabs.Controls.Add(this.AddPage2);
            this.Tabs.Controls.Add(this.AddPage3);
            this.Tabs.Controls.Add(this.AddPage4);
            this.Tabs.Controls.Add(this.ChartPage);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(906, 502);
            this.Tabs.TabIndex = 6;
            // 
            // DbPage
            // 
            this.DbPage.AutoScroll = true;
            this.DbPage.BackColor = System.Drawing.SystemColors.Control;
            this.DbPage.Location = new System.Drawing.Point(4, 22);
            this.DbPage.Name = "DbPage";
            this.DbPage.Padding = new System.Windows.Forms.Padding(3);
            this.DbPage.Size = new System.Drawing.Size(898, 476);
            this.DbPage.TabIndex = 0;
            this.DbPage.Text = "DbPage";
            this.DbPage.Resize += new System.EventHandler(this.DbPage_Resize);
            // 
            // TunePage1
            // 
            this.TunePage1.BackColor = System.Drawing.SystemColors.Control;
            this.TunePage1.Controls.Add(this.pictureBox1);
            this.TunePage1.Controls.Add(this.wizardText1);
            this.TunePage1.Controls.Add(this.TunePage1Nav);
            this.TunePage1.Controls.Add(this.TunePage1Banner);
            this.TunePage1.Location = new System.Drawing.Point(4, 22);
            this.TunePage1.Name = "TunePage1";
            this.TunePage1.Padding = new System.Windows.Forms.Padding(3);
            this.TunePage1.Size = new System.Drawing.Size(898, 476);
            this.TunePage1.TabIndex = 1;
            this.TunePage1.Text = "TunePage1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::DJetronicStudio.Properties.Resources.Tune_o_Matic_V1_0_MPS_Connector;
            this.pictureBox1.Location = new System.Drawing.Point(3, 155);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(892, 130);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // wizardText1
            // 
            this.wizardText1.Body = "Plug the MPS into the Tune-o-Matic using the four pin screw terminal block. Match" +
    " up the pin numbers embossed on the MPS with the pin numbers for the Tune-o-Mati" +
    "c. When done click on Next.";
            this.wizardText1.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardText1.Location = new System.Drawing.Point(3, 71);
            this.wizardText1.Name = "wizardText1";
            this.wizardText1.Size = new System.Drawing.Size(892, 84);
            this.wizardText1.TabIndex = 2;
            this.wizardText1.Title = "Connect MPS";
            // 
            // TunePage1Nav
            // 
            this.TunePage1Nav.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TunePage1Nav.FirstPage = true;
            this.TunePage1Nav.LastPage = false;
            this.TunePage1Nav.Location = new System.Drawing.Point(3, 437);
            this.TunePage1Nav.Name = "TunePage1Nav";
            this.TunePage1Nav.Size = new System.Drawing.Size(892, 36);
            this.TunePage1Nav.TabIndex = 1;
            this.TunePage1Nav.OnCancel += new DJetronicStudio.WizardNavigation.OnCancelHandler(this.Nav_OnCancel);
            this.TunePage1Nav.OnPrevious += new DJetronicStudio.WizardNavigation.OnPreviousHandler(this.Nav_OnPrevious);
            this.TunePage1Nav.OnNext += new DJetronicStudio.WizardNavigation.OnNextHandler(this.Nav_OnNext);
            // 
            // TunePage1Banner
            // 
            this.TunePage1Banner.BackColor = System.Drawing.Color.CornflowerBlue;
            this.TunePage1Banner.Dock = System.Windows.Forms.DockStyle.Top;
            this.TunePage1Banner.Icon = ((System.Drawing.Image)(resources.GetObject("TunePage1Banner.Icon")));
            this.TunePage1Banner.Location = new System.Drawing.Point(3, 3);
            this.TunePage1Banner.Name = "TunePage1Banner";
            this.TunePage1Banner.Size = new System.Drawing.Size(892, 68);
            this.TunePage1Banner.TabIndex = 0;
            this.TunePage1Banner.Title = "Tune MPS Step 1";
            // 
            // TunePage2
            // 
            this.TunePage2.BackColor = System.Drawing.SystemColors.Control;
            this.TunePage2.Controls.Add(this.pictureBox2);
            this.TunePage2.Controls.Add(this.TunePage2WizardText);
            this.TunePage2.Controls.Add(this.TunePage2Nav);
            this.TunePage2.Controls.Add(this.TunePage2Banner);
            this.TunePage2.Location = new System.Drawing.Point(4, 22);
            this.TunePage2.Name = "TunePage2";
            this.TunePage2.Padding = new System.Windows.Forms.Padding(3);
            this.TunePage2.Size = new System.Drawing.Size(898, 476);
            this.TunePage2.TabIndex = 2;
            this.TunePage2.Text = "TunePage2";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox2.Image = global::DJetronicStudio.Properties.Resources.MPS_Vacuum_Port;
            this.pictureBox2.Location = new System.Drawing.Point(3, 145);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(892, 130);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // TunePage2WizardText
            // 
            this.TunePage2WizardText.Body = "Attach a MityVac (or similar) to the vacuum port on the back of the MPS. Set the " +
    "vacuum to 5 in Hg. When done click on Next.";
            this.TunePage2WizardText.Dock = System.Windows.Forms.DockStyle.Top;
            this.TunePage2WizardText.Location = new System.Drawing.Point(3, 71);
            this.TunePage2WizardText.Name = "TunePage2WizardText";
            this.TunePage2WizardText.Size = new System.Drawing.Size(892, 74);
            this.TunePage2WizardText.TabIndex = 2;
            this.TunePage2WizardText.Title = "Connect MityVac to MPS";
            // 
            // TunePage2Nav
            // 
            this.TunePage2Nav.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TunePage2Nav.FirstPage = false;
            this.TunePage2Nav.LastPage = false;
            this.TunePage2Nav.Location = new System.Drawing.Point(3, 437);
            this.TunePage2Nav.Name = "TunePage2Nav";
            this.TunePage2Nav.Size = new System.Drawing.Size(892, 36);
            this.TunePage2Nav.TabIndex = 1;
            this.TunePage2Nav.OnCancel += new DJetronicStudio.WizardNavigation.OnCancelHandler(this.Nav_OnCancel);
            this.TunePage2Nav.OnPrevious += new DJetronicStudio.WizardNavigation.OnPreviousHandler(this.Nav_OnPrevious);
            this.TunePage2Nav.OnNext += new DJetronicStudio.WizardNavigation.OnNextHandler(this.Nav_OnNext);
            // 
            // TunePage2Banner
            // 
            this.TunePage2Banner.BackColor = System.Drawing.Color.CornflowerBlue;
            this.TunePage2Banner.Dock = System.Windows.Forms.DockStyle.Top;
            this.TunePage2Banner.Icon = ((System.Drawing.Image)(resources.GetObject("TunePage2Banner.Icon")));
            this.TunePage2Banner.Location = new System.Drawing.Point(3, 3);
            this.TunePage2Banner.Name = "TunePage2Banner";
            this.TunePage2Banner.Size = new System.Drawing.Size(892, 68);
            this.TunePage2Banner.TabIndex = 0;
            this.TunePage2Banner.Title = "Tune MPS Step 2";
            // 
            // TunePage3
            // 
            this.TunePage3.BackColor = System.Drawing.SystemColors.Control;
            this.TunePage3.Controls.Add(this.label1);
            this.TunePage3.Controls.Add(this.ReferenceSelector);
            this.TunePage3.Controls.Add(this.wizardText3);
            this.TunePage3.Controls.Add(this.wizardNavigation1);
            this.TunePage3.Controls.Add(this.wizardBanner1);
            this.TunePage3.Location = new System.Drawing.Point(4, 22);
            this.TunePage3.Name = "TunePage3";
            this.TunePage3.Padding = new System.Windows.Forms.Padding(3);
            this.TunePage3.Size = new System.Drawing.Size(898, 476);
            this.TunePage3.TabIndex = 3;
            this.TunePage3.Text = "TunePage3";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(199, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Reference:";
            // 
            // ReferenceSelector
            // 
            this.ReferenceSelector.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ReferenceSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReferenceSelector.FormattingEnabled = true;
            this.ReferenceSelector.Location = new System.Drawing.Point(265, 150);
            this.ReferenceSelector.Name = "ReferenceSelector";
            this.ReferenceSelector.Size = new System.Drawing.Size(436, 21);
            this.ReferenceSelector.TabIndex = 3;
            // 
            // wizardText3
            // 
            this.wizardText3.Body = "Choose an MPS from the database to use as a reference. When done click on Next.";
            this.wizardText3.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardText3.Location = new System.Drawing.Point(3, 71);
            this.wizardText3.Name = "wizardText3";
            this.wizardText3.Size = new System.Drawing.Size(892, 73);
            this.wizardText3.TabIndex = 2;
            this.wizardText3.Title = "Choose Reference";
            // 
            // wizardNavigation1
            // 
            this.wizardNavigation1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wizardNavigation1.FirstPage = false;
            this.wizardNavigation1.LastPage = false;
            this.wizardNavigation1.Location = new System.Drawing.Point(3, 437);
            this.wizardNavigation1.Name = "wizardNavigation1";
            this.wizardNavigation1.Size = new System.Drawing.Size(892, 36);
            this.wizardNavigation1.TabIndex = 1;
            this.wizardNavigation1.OnCancel += new DJetronicStudio.WizardNavigation.OnCancelHandler(this.Nav_OnCancel);
            this.wizardNavigation1.OnPrevious += new DJetronicStudio.WizardNavigation.OnPreviousHandler(this.Nav_OnPrevious);
            this.wizardNavigation1.OnNext += new DJetronicStudio.WizardNavigation.OnNextHandler(this.Nav_OnNext);
            // 
            // wizardBanner1
            // 
            this.wizardBanner1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.wizardBanner1.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardBanner1.Icon = ((System.Drawing.Image)(resources.GetObject("wizardBanner1.Icon")));
            this.wizardBanner1.Location = new System.Drawing.Point(3, 3);
            this.wizardBanner1.Name = "wizardBanner1";
            this.wizardBanner1.Size = new System.Drawing.Size(892, 68);
            this.wizardBanner1.TabIndex = 0;
            this.wizardBanner1.Title = "Tune MPS Step 3";
            // 
            // TunePage4
            // 
            this.TunePage4.BackColor = System.Drawing.SystemColors.Control;
            this.TunePage4.Controls.Add(this.Gauge);
            this.TunePage4.Controls.Add(this.wizardText4);
            this.TunePage4.Controls.Add(this.wizardNavigation2);
            this.TunePage4.Controls.Add(this.wizardBanner2);
            this.TunePage4.Location = new System.Drawing.Point(4, 22);
            this.TunePage4.Name = "TunePage4";
            this.TunePage4.Padding = new System.Windows.Forms.Padding(3);
            this.TunePage4.Size = new System.Drawing.Size(898, 476);
            this.TunePage4.TabIndex = 4;
            this.TunePage4.Text = "TunePage4";
            // 
            // Gauge
            // 
            this.Gauge.AlarmColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(94)))), ((int)(((byte)(46)))));
            this.Gauge.BackColor = System.Drawing.SystemColors.Control;
            this.Gauge.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.Gauge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gauge.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(223)))), ((int)(((byte)(180)))));
            this.Gauge.GaugeHeight = 150F;
            this.Gauge.GaugeWidth = 150F;
            this.Gauge.InnerStrokeWeight = 4F;
            this.Gauge.Location = new System.Drawing.Point(3, 158);
            this.Gauge.Name = "Gauge";
            this.Gauge.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(223)))), ((int)(((byte)(180)))));
            this.Gauge.Size = new System.Drawing.Size(892, 279);
            this.Gauge.StartAngle = 150F;
            this.Gauge.StrokeWeight = 20F;
            this.Gauge.SweepAngle = 240F;
            this.Gauge.TabIndex = 3;
            this.Gauge.ThresholdNormal = 49.5F;
            this.Gauge.ThresholdOutOfRange = 60F;
            this.Gauge.ThresholdWarning1 = 40F;
            this.Gauge.ThresholdWarning2 = 50.5F;
            this.Gauge.Value = 16F;
            this.Gauge.ValueFont = new System.Drawing.Font("Calibri", 22F);
            this.Gauge.ValueMax = 20F;
            this.Gauge.ValueMin = 12F;
            this.Gauge.ValueUnit = "ms";
            this.Gauge.ValueUnitFont = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.Gauge.WarningColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(156)))), ((int)(((byte)(71)))));
            // 
            // wizardText4
            // 
            this.wizardText4.Body = "Turn the adjustment screw of the front of the MPS clockwise or counter-clockwise " +
    "to get the gauge below to turn green. Use a 4mm allen key. When done click on Fi" +
    "nish.";
            this.wizardText4.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardText4.Location = new System.Drawing.Point(3, 71);
            this.wizardText4.Name = "wizardText4";
            this.wizardText4.Size = new System.Drawing.Size(892, 87);
            this.wizardText4.TabIndex = 2;
            this.wizardText4.Title = "Perform Tuning";
            // 
            // wizardNavigation2
            // 
            this.wizardNavigation2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wizardNavigation2.FirstPage = false;
            this.wizardNavigation2.LastPage = true;
            this.wizardNavigation2.Location = new System.Drawing.Point(3, 437);
            this.wizardNavigation2.Name = "wizardNavigation2";
            this.wizardNavigation2.Size = new System.Drawing.Size(892, 36);
            this.wizardNavigation2.TabIndex = 1;
            this.wizardNavigation2.OnCancel += new DJetronicStudio.WizardNavigation.OnCancelHandler(this.Nav_OnCancel);
            this.wizardNavigation2.OnPrevious += new DJetronicStudio.WizardNavigation.OnPreviousHandler(this.Nav_OnPrevious);
            this.wizardNavigation2.OnNext += new DJetronicStudio.WizardNavigation.OnNextHandler(this.Nav_OnNext);
            // 
            // wizardBanner2
            // 
            this.wizardBanner2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.wizardBanner2.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardBanner2.Icon = ((System.Drawing.Image)(resources.GetObject("wizardBanner2.Icon")));
            this.wizardBanner2.Location = new System.Drawing.Point(3, 3);
            this.wizardBanner2.Name = "wizardBanner2";
            this.wizardBanner2.Size = new System.Drawing.Size(892, 68);
            this.wizardBanner2.TabIndex = 0;
            this.wizardBanner2.Title = "Tune MPS Step 4";
            // 
            // AddPage1
            // 
            this.AddPage1.BackColor = System.Drawing.SystemColors.Control;
            this.AddPage1.Controls.Add(this.label4);
            this.AddPage1.Controls.Add(this.label3);
            this.AddPage1.Controls.Add(this.label2);
            this.AddPage1.Controls.Add(this.AddCalibrationSelector);
            this.AddPage1.Controls.Add(this.AddDescriptionInput);
            this.AddPage1.Controls.Add(this.AddNameInput);
            this.AddPage1.Controls.Add(this.wizardText2);
            this.AddPage1.Controls.Add(this.wizardNavigation3);
            this.AddPage1.Controls.Add(this.wizardBanner3);
            this.AddPage1.Location = new System.Drawing.Point(4, 22);
            this.AddPage1.Name = "AddPage1";
            this.AddPage1.Padding = new System.Windows.Forms.Padding(3);
            this.AddPage1.Size = new System.Drawing.Size(898, 476);
            this.AddPage1.TabIndex = 5;
            this.AddPage1.Text = "AddPage1";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Calibration Method:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Description:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Name:";
            // 
            // AddCalibrationSelector
            // 
            this.AddCalibrationSelector.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.AddCalibrationSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AddCalibrationSelector.FormattingEnabled = true;
            this.AddCalibrationSelector.Items.AddRange(new object[] {
            "Factory (Bosch, cap is intact)",
            "Inductance",
            "Wideband O2 sensor",
            "Tune-o-Matic",
            "None or Unknown"});
            this.AddCalibrationSelector.Location = new System.Drawing.Point(140, 214);
            this.AddCalibrationSelector.Name = "AddCalibrationSelector";
            this.AddCalibrationSelector.Size = new System.Drawing.Size(221, 21);
            this.AddCalibrationSelector.TabIndex = 4;
            // 
            // AddDescriptionInput
            // 
            this.AddDescriptionInput.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.AddDescriptionInput.Location = new System.Drawing.Point(140, 188);
            this.AddDescriptionInput.Name = "AddDescriptionInput";
            this.AddDescriptionInput.Size = new System.Drawing.Size(705, 20);
            this.AddDescriptionInput.TabIndex = 3;
            // 
            // AddNameInput
            // 
            this.AddNameInput.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.AddNameInput.Location = new System.Drawing.Point(140, 162);
            this.AddNameInput.Name = "AddNameInput";
            this.AddNameInput.Size = new System.Drawing.Size(221, 20);
            this.AddNameInput.TabIndex = 2;
            // 
            // wizardText2
            // 
            this.wizardText2.Body = "Fill in a name, description and choose how this MPS was calibrated. Then click on" +
    " Next.";
            this.wizardText2.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardText2.Location = new System.Drawing.Point(3, 71);
            this.wizardText2.Name = "wizardText2";
            this.wizardText2.Size = new System.Drawing.Size(892, 85);
            this.wizardText2.TabIndex = 1;
            this.wizardText2.Title = "Enter MPS Details";
            // 
            // wizardNavigation3
            // 
            this.wizardNavigation3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wizardNavigation3.FirstPage = true;
            this.wizardNavigation3.LastPage = false;
            this.wizardNavigation3.Location = new System.Drawing.Point(3, 437);
            this.wizardNavigation3.Name = "wizardNavigation3";
            this.wizardNavigation3.Size = new System.Drawing.Size(892, 36);
            this.wizardNavigation3.TabIndex = 5;
            this.wizardNavigation3.OnCancel += new DJetronicStudio.WizardNavigation.OnCancelHandler(this.Nav_OnCancel);
            this.wizardNavigation3.OnPrevious += new DJetronicStudio.WizardNavigation.OnPreviousHandler(this.Nav_OnPrevious);
            this.wizardNavigation3.OnNext += new DJetronicStudio.WizardNavigation.OnNextHandler(this.Nav_OnNext);
            // 
            // wizardBanner3
            // 
            this.wizardBanner3.BackColor = System.Drawing.Color.CornflowerBlue;
            this.wizardBanner3.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardBanner3.Icon = global::DJetronicStudio.Properties.Resources.database_add_48;
            this.wizardBanner3.Location = new System.Drawing.Point(3, 3);
            this.wizardBanner3.Name = "wizardBanner3";
            this.wizardBanner3.Size = new System.Drawing.Size(892, 68);
            this.wizardBanner3.TabIndex = 0;
            this.wizardBanner3.Title = "Add MPS Step 1";
            // 
            // AddPage2
            // 
            this.AddPage2.BackColor = System.Drawing.SystemColors.Control;
            this.AddPage2.Controls.Add(this.pictureBox3);
            this.AddPage2.Controls.Add(this.wizardText5);
            this.AddPage2.Controls.Add(this.wizardNavigation4);
            this.AddPage2.Controls.Add(this.wizardBanner4);
            this.AddPage2.Location = new System.Drawing.Point(4, 22);
            this.AddPage2.Name = "AddPage2";
            this.AddPage2.Padding = new System.Windows.Forms.Padding(3);
            this.AddPage2.Size = new System.Drawing.Size(898, 476);
            this.AddPage2.TabIndex = 6;
            this.AddPage2.Text = "AddPage2";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox3.Image = global::DJetronicStudio.Properties.Resources.Tune_o_Matic_V1_0_MPS_Connector;
            this.pictureBox3.Location = new System.Drawing.Point(3, 156);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(892, 130);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // wizardText5
            // 
            this.wizardText5.Body = "Plug the MPS into the Tune-o-Matic using the four pin screw terminal block. Match" +
    " up the pin numbers embossed on the MPS with the pin numbers for the Tune-o-Mati" +
    "c. When done click on Next.";
            this.wizardText5.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardText5.Location = new System.Drawing.Point(3, 71);
            this.wizardText5.Name = "wizardText5";
            this.wizardText5.Size = new System.Drawing.Size(892, 85);
            this.wizardText5.TabIndex = 2;
            this.wizardText5.Title = "Connect MPS";
            // 
            // wizardNavigation4
            // 
            this.wizardNavigation4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wizardNavigation4.FirstPage = false;
            this.wizardNavigation4.LastPage = false;
            this.wizardNavigation4.Location = new System.Drawing.Point(3, 437);
            this.wizardNavigation4.Name = "wizardNavigation4";
            this.wizardNavigation4.Size = new System.Drawing.Size(892, 36);
            this.wizardNavigation4.TabIndex = 1;
            this.wizardNavigation4.OnCancel += new DJetronicStudio.WizardNavigation.OnCancelHandler(this.Nav_OnCancel);
            this.wizardNavigation4.OnPrevious += new DJetronicStudio.WizardNavigation.OnPreviousHandler(this.Nav_OnPrevious);
            this.wizardNavigation4.OnNext += new DJetronicStudio.WizardNavigation.OnNextHandler(this.Nav_OnNext);
            // 
            // wizardBanner4
            // 
            this.wizardBanner4.BackColor = System.Drawing.Color.CornflowerBlue;
            this.wizardBanner4.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardBanner4.Icon = global::DJetronicStudio.Properties.Resources.database_add_48;
            this.wizardBanner4.Location = new System.Drawing.Point(3, 3);
            this.wizardBanner4.Name = "wizardBanner4";
            this.wizardBanner4.Size = new System.Drawing.Size(892, 68);
            this.wizardBanner4.TabIndex = 0;
            this.wizardBanner4.Title = "Add MPS Step 2";
            // 
            // AddPage3
            // 
            this.AddPage3.BackColor = System.Drawing.SystemColors.Control;
            this.AddPage3.Controls.Add(this.pictureBox4);
            this.AddPage3.Controls.Add(this.wizardText6);
            this.AddPage3.Controls.Add(this.wizardNavigation5);
            this.AddPage3.Controls.Add(this.wizardBanner5);
            this.AddPage3.Location = new System.Drawing.Point(4, 22);
            this.AddPage3.Name = "AddPage3";
            this.AddPage3.Padding = new System.Windows.Forms.Padding(3);
            this.AddPage3.Size = new System.Drawing.Size(898, 476);
            this.AddPage3.TabIndex = 7;
            this.AddPage3.Text = "AddPage3";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox4.Image = global::DJetronicStudio.Properties.Resources.MPS_Vacuum_Port;
            this.pictureBox4.Location = new System.Drawing.Point(3, 156);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(892, 130);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            // 
            // wizardText6
            // 
            this.wizardText6.Body = "Attach a MityVac (or similar) to the vacuum port on the back of the MPS. Set the " +
    "vacuum to 0 in Hg. When done click on Next.";
            this.wizardText6.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardText6.Location = new System.Drawing.Point(3, 71);
            this.wizardText6.Name = "wizardText6";
            this.wizardText6.Size = new System.Drawing.Size(892, 85);
            this.wizardText6.TabIndex = 2;
            this.wizardText6.Title = "Connect MityVac to MPS";
            // 
            // wizardNavigation5
            // 
            this.wizardNavigation5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wizardNavigation5.FirstPage = false;
            this.wizardNavigation5.LastPage = false;
            this.wizardNavigation5.Location = new System.Drawing.Point(3, 437);
            this.wizardNavigation5.Name = "wizardNavigation5";
            this.wizardNavigation5.Size = new System.Drawing.Size(892, 36);
            this.wizardNavigation5.TabIndex = 1;
            this.wizardNavigation5.OnCancel += new DJetronicStudio.WizardNavigation.OnCancelHandler(this.Nav_OnCancel);
            this.wizardNavigation5.OnPrevious += new DJetronicStudio.WizardNavigation.OnPreviousHandler(this.Nav_OnPrevious);
            this.wizardNavigation5.OnNext += new DJetronicStudio.WizardNavigation.OnNextHandler(this.Nav_OnNext);
            // 
            // wizardBanner5
            // 
            this.wizardBanner5.BackColor = System.Drawing.Color.CornflowerBlue;
            this.wizardBanner5.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardBanner5.Icon = global::DJetronicStudio.Properties.Resources.database_add_48;
            this.wizardBanner5.Location = new System.Drawing.Point(3, 3);
            this.wizardBanner5.Name = "wizardBanner5";
            this.wizardBanner5.Size = new System.Drawing.Size(892, 68);
            this.wizardBanner5.TabIndex = 0;
            this.wizardBanner5.Title = "Add MPS Step 3";
            // 
            // AddPage4
            // 
            this.AddPage4.BackColor = System.Drawing.SystemColors.Control;
            this.AddPage4.Controls.Add(this.AddPressureButtonGrid);
            this.AddPage4.Controls.Add(this.wizardText7);
            this.AddPage4.Controls.Add(this.wizardNavigation6);
            this.AddPage4.Controls.Add(this.wizardBanner6);
            this.AddPage4.Location = new System.Drawing.Point(4, 22);
            this.AddPage4.Name = "AddPage4";
            this.AddPage4.Padding = new System.Windows.Forms.Padding(3);
            this.AddPage4.Size = new System.Drawing.Size(898, 476);
            this.AddPage4.TabIndex = 8;
            this.AddPage4.Text = "AddPage4";
            // 
            // AddPressureButtonGrid
            // 
            this.AddPressureButtonGrid.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.AddPressureButtonGrid.Location = new System.Drawing.Point(126, 162);
            this.AddPressureButtonGrid.Name = "AddPressureButtonGrid";
            this.AddPressureButtonGrid.Size = new System.Drawing.Size(641, 152);
            this.AddPressureButtonGrid.TabIndex = 3;
            // 
            // wizardText7
            // 
            this.wizardText7.Body = resources.GetString("wizardText7.Body");
            this.wizardText7.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardText7.Location = new System.Drawing.Point(3, 71);
            this.wizardText7.Name = "wizardText7";
            this.wizardText7.Size = new System.Drawing.Size(892, 85);
            this.wizardText7.TabIndex = 2;
            this.wizardText7.Title = "Set Vacuum";
            // 
            // wizardNavigation6
            // 
            this.wizardNavigation6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wizardNavigation6.FirstPage = false;
            this.wizardNavigation6.LastPage = true;
            this.wizardNavigation6.Location = new System.Drawing.Point(3, 437);
            this.wizardNavigation6.Name = "wizardNavigation6";
            this.wizardNavigation6.Size = new System.Drawing.Size(892, 36);
            this.wizardNavigation6.TabIndex = 1;
            this.wizardNavigation6.OnCancel += new DJetronicStudio.WizardNavigation.OnCancelHandler(this.Nav_OnCancel);
            this.wizardNavigation6.OnPrevious += new DJetronicStudio.WizardNavigation.OnPreviousHandler(this.Nav_OnPrevious);
            this.wizardNavigation6.OnNext += new DJetronicStudio.WizardNavigation.OnNextHandler(this.Nav_OnNext);
            // 
            // wizardBanner6
            // 
            this.wizardBanner6.BackColor = System.Drawing.Color.CornflowerBlue;
            this.wizardBanner6.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardBanner6.Icon = global::DJetronicStudio.Properties.Resources.database_add_48;
            this.wizardBanner6.Location = new System.Drawing.Point(3, 3);
            this.wizardBanner6.Name = "wizardBanner6";
            this.wizardBanner6.Size = new System.Drawing.Size(892, 68);
            this.wizardBanner6.TabIndex = 0;
            this.wizardBanner6.Title = "Add MPS Step 4";
            // 
            // ChartPage
            // 
            this.ChartPage.BackColor = System.Drawing.SystemColors.Control;
            this.ChartPage.Controls.Add(this.Chart);
            this.ChartPage.Location = new System.Drawing.Point(4, 22);
            this.ChartPage.Name = "ChartPage";
            this.ChartPage.Padding = new System.Windows.Forms.Padding(3);
            this.ChartPage.Size = new System.Drawing.Size(898, 476);
            this.ChartPage.TabIndex = 9;
            this.ChartPage.Text = "ChartPage";
            // 
            // Chart
            // 
            this.Chart.Database = null;
            this.Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Chart.Location = new System.Drawing.Point(3, 3);
            this.Chart.Name = "Chart";
            this.Chart.Size = new System.Drawing.Size(892, 470);
            this.Chart.TabIndex = 1;
            // 
            // TuneOMaticUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.Tabs);
            this.Name = "TuneOMaticUI";
            this.Size = new System.Drawing.Size(906, 502);
            this.Tabs.ResumeLayout(false);
            this.TunePage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.TunePage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.TunePage3.ResumeLayout(false);
            this.TunePage3.PerformLayout();
            this.TunePage4.ResumeLayout(false);
            this.AddPage1.ResumeLayout(false);
            this.AddPage1.PerformLayout();
            this.AddPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.AddPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.AddPage4.ResumeLayout(false);
            this.ChartPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog ExportCSVDialog;
        private TablessControl Tabs;
        private System.Windows.Forms.TabPage DbPage;
        private System.Windows.Forms.TabPage TunePage1;
        private WizardBanner TunePage1Banner;
        private WizardNavigation TunePage1Nav;
        private System.Windows.Forms.TabPage TunePage2;
        private WizardNavigation TunePage2Nav;
        private WizardBanner TunePage2Banner;
        private WizardText wizardText1;
        private WizardText TunePage2WizardText;
        private System.Windows.Forms.TabPage TunePage3;
        private WizardText wizardText3;
        private WizardNavigation wizardNavigation1;
        private WizardBanner wizardBanner1;
        private System.Windows.Forms.TabPage TunePage4;
        private WizardText wizardText4;
        private WizardNavigation wizardNavigation2;
        private WizardBanner wizardBanner2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ReferenceSelector;
        private TunerGauge Gauge;
        private System.Windows.Forms.OpenFileDialog ImportMPSProfileDialog;
        private System.Windows.Forms.SaveFileDialog ExportMPSProfileDialog;
        private System.Windows.Forms.TabPage AddPage1;
        private WizardText wizardText2;
        private WizardNavigation wizardNavigation3;
        private WizardBanner wizardBanner3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox AddCalibrationSelector;
        private System.Windows.Forms.TextBox AddDescriptionInput;
        private System.Windows.Forms.TextBox AddNameInput;
        private System.Windows.Forms.TabPage AddPage2;
        private WizardText wizardText5;
        private WizardNavigation wizardNavigation4;
        private WizardBanner wizardBanner4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TabPage AddPage3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private WizardText wizardText6;
        private WizardNavigation wizardNavigation5;
        private WizardBanner wizardBanner5;
        private System.Windows.Forms.TabPage AddPage4;
        private WizardText wizardText7;
        private WizardNavigation wizardNavigation6;
        private WizardBanner wizardBanner6;
        private ReadPressureButtonGrid AddPressureButtonGrid;
        private System.Windows.Forms.TabPage ChartPage;
        private MPSChart Chart;
    }
}
