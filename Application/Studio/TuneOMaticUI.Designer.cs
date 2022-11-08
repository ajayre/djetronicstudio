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
            this.ExportCSVDialog = new System.Windows.Forms.SaveFileDialog();
            this.GetPressureBtn = new System.Windows.Forms.Button();
            this.GetPulseWidthBtn = new System.Windows.Forms.Button();
            this.PressureValue = new System.Windows.Forms.Label();
            this.PulseWidthValue = new System.Windows.Forms.Label();
            this.StartContBtn = new System.Windows.Forms.Button();
            this.StopContBtn = new System.Windows.Forms.Button();
            this.ImportMPSProfileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Tabs = new DJetronicStudio.TablessControl();
            this.DbPage = new System.Windows.Forms.TabPage();
            this.TunePage1 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.wizardText1 = new DJetronicStudio.WizardText();
            this.TunePage1Nav = new DJetronicStudio.WizardNavigation();
            this.TunePage1Banner = new DJetronicStudio.WizardBanner();
            this.TunePage2 = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.wizardText2 = new DJetronicStudio.WizardText();
            this.TunePage2Nav = new DJetronicStudio.WizardNavigation();
            this.TunePage2Banner = new DJetronicStudio.WizardBanner();
            this.TunePage3 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.wizardText3 = new DJetronicStudio.WizardText();
            this.wizardNavigation1 = new DJetronicStudio.WizardNavigation();
            this.wizardBanner1 = new DJetronicStudio.WizardBanner();
            this.TunePage4 = new System.Windows.Forms.TabPage();
            this.Gauge = new DJetronicStudio.TunerGauge();
            this.wizardText4 = new DJetronicStudio.WizardText();
            this.wizardNavigation2 = new DJetronicStudio.WizardNavigation();
            this.wizardBanner2 = new DJetronicStudio.WizardBanner();
            this.ExportMPSProfileDialog = new System.Windows.Forms.SaveFileDialog();
            this.Tabs.SuspendLayout();
            this.TunePage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.TunePage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.TunePage3.SuspendLayout();
            this.TunePage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExportCSVDialog
            // 
            this.ExportCSVDialog.DefaultExt = "csv";
            this.ExportCSVDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            this.ExportCSVDialog.Title = "Export Data to CSV";
            // 
            // GetPressureBtn
            // 
            this.GetPressureBtn.Location = new System.Drawing.Point(3, 3);
            this.GetPressureBtn.Name = "GetPressureBtn";
            this.GetPressureBtn.Size = new System.Drawing.Size(104, 23);
            this.GetPressureBtn.TabIndex = 0;
            this.GetPressureBtn.Text = "Get Pressure";
            this.GetPressureBtn.UseVisualStyleBackColor = true;
            this.GetPressureBtn.Click += new System.EventHandler(this.GetPressureBtn_Click);
            // 
            // GetPulseWidthBtn
            // 
            this.GetPulseWidthBtn.Location = new System.Drawing.Point(3, 32);
            this.GetPulseWidthBtn.Name = "GetPulseWidthBtn";
            this.GetPulseWidthBtn.Size = new System.Drawing.Size(104, 23);
            this.GetPulseWidthBtn.TabIndex = 1;
            this.GetPulseWidthBtn.Text = "Get Pulse Width";
            this.GetPulseWidthBtn.UseVisualStyleBackColor = true;
            this.GetPulseWidthBtn.Click += new System.EventHandler(this.GetPulseWidthBtn_Click);
            // 
            // PressureValue
            // 
            this.PressureValue.AutoSize = true;
            this.PressureValue.Location = new System.Drawing.Point(113, 8);
            this.PressureValue.Name = "PressureValue";
            this.PressureValue.Size = new System.Drawing.Size(35, 13);
            this.PressureValue.TabIndex = 2;
            this.PressureValue.Text = "label1";
            // 
            // PulseWidthValue
            // 
            this.PulseWidthValue.AutoSize = true;
            this.PulseWidthValue.Location = new System.Drawing.Point(113, 37);
            this.PulseWidthValue.Name = "PulseWidthValue";
            this.PulseWidthValue.Size = new System.Drawing.Size(35, 13);
            this.PulseWidthValue.TabIndex = 3;
            this.PulseWidthValue.Text = "label2";
            // 
            // StartContBtn
            // 
            this.StartContBtn.Location = new System.Drawing.Point(217, 32);
            this.StartContBtn.Name = "StartContBtn";
            this.StartContBtn.Size = new System.Drawing.Size(104, 23);
            this.StartContBtn.TabIndex = 4;
            this.StartContBtn.Text = "Start Cont";
            this.StartContBtn.UseVisualStyleBackColor = true;
            this.StartContBtn.Click += new System.EventHandler(this.StartContBtn_Click);
            // 
            // StopContBtn
            // 
            this.StopContBtn.Location = new System.Drawing.Point(327, 32);
            this.StopContBtn.Name = "StopContBtn";
            this.StopContBtn.Size = new System.Drawing.Size(104, 23);
            this.StopContBtn.TabIndex = 5;
            this.StopContBtn.Text = "Stop Cont";
            this.StopContBtn.UseVisualStyleBackColor = true;
            this.StopContBtn.Click += new System.EventHandler(this.StopContBtn_Click);
            // 
            // ImportMPSProfileDialog
            // 
            this.ImportMPSProfileDialog.DefaultExt = "mps";
            this.ImportMPSProfileDialog.Filter = "MPS Profiles (*.mps)|*.mps|All Files (*.*)|*.*";
            this.ImportMPSProfileDialog.Title = "Import MPS Profile";
            // 
            // Tabs
            // 
            this.Tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tabs.Controls.Add(this.DbPage);
            this.Tabs.Controls.Add(this.TunePage1);
            this.Tabs.Controls.Add(this.TunePage2);
            this.Tabs.Controls.Add(this.TunePage3);
            this.Tabs.Controls.Add(this.TunePage4);
            this.Tabs.Location = new System.Drawing.Point(3, 61);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(900, 438);
            this.Tabs.TabIndex = 6;
            // 
            // DbPage
            // 
            this.DbPage.AutoScroll = true;
            this.DbPage.BackColor = System.Drawing.SystemColors.Control;
            this.DbPage.Location = new System.Drawing.Point(4, 22);
            this.DbPage.Name = "DbPage";
            this.DbPage.Padding = new System.Windows.Forms.Padding(3);
            this.DbPage.Size = new System.Drawing.Size(892, 412);
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
            this.TunePage1.Size = new System.Drawing.Size(892, 412);
            this.TunePage1.TabIndex = 1;
            this.TunePage1.Text = "TunePage1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::DJetronicStudio.Properties.Resources.Tune_o_Matic_V1_0_MPS_Connector;
            this.pictureBox1.Location = new System.Drawing.Point(3, 155);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(886, 130);
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
            this.wizardText1.Size = new System.Drawing.Size(886, 84);
            this.wizardText1.TabIndex = 2;
            this.wizardText1.Title = "Connect MPS";
            // 
            // TunePage1Nav
            // 
            this.TunePage1Nav.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TunePage1Nav.FirstPage = true;
            this.TunePage1Nav.LastPage = false;
            this.TunePage1Nav.Location = new System.Drawing.Point(3, 373);
            this.TunePage1Nav.Name = "TunePage1Nav";
            this.TunePage1Nav.Size = new System.Drawing.Size(886, 36);
            this.TunePage1Nav.TabIndex = 1;
            this.TunePage1Nav.OnCancel += new DJetronicStudio.WizardNavigation.OnCancelHandler(this.Nav_OnCancel);
            this.TunePage1Nav.OnPrevious += new DJetronicStudio.WizardNavigation.OnPreviousHandler(this.Nav_OnPrevious);
            this.TunePage1Nav.OnNext += new DJetronicStudio.WizardNavigation.OnNextHandler(this.Nav_OnNext);
            // 
            // TunePage1Banner
            // 
            this.TunePage1Banner.BackColor = System.Drawing.Color.CornflowerBlue;
            this.TunePage1Banner.Dock = System.Windows.Forms.DockStyle.Top;
            this.TunePage1Banner.Location = new System.Drawing.Point(3, 3);
            this.TunePage1Banner.Name = "TunePage1Banner";
            this.TunePage1Banner.Size = new System.Drawing.Size(886, 68);
            this.TunePage1Banner.TabIndex = 0;
            this.TunePage1Banner.Title = "Tune MPS Step 1";
            // 
            // TunePage2
            // 
            this.TunePage2.BackColor = System.Drawing.SystemColors.Control;
            this.TunePage2.Controls.Add(this.pictureBox2);
            this.TunePage2.Controls.Add(this.wizardText2);
            this.TunePage2.Controls.Add(this.TunePage2Nav);
            this.TunePage2.Controls.Add(this.TunePage2Banner);
            this.TunePage2.Location = new System.Drawing.Point(4, 22);
            this.TunePage2.Name = "TunePage2";
            this.TunePage2.Padding = new System.Windows.Forms.Padding(3);
            this.TunePage2.Size = new System.Drawing.Size(892, 412);
            this.TunePage2.TabIndex = 2;
            this.TunePage2.Text = "TunePage2";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox2.Image = global::DJetronicStudio.Properties.Resources.MPS_Vacuum_Port;
            this.pictureBox2.Location = new System.Drawing.Point(3, 145);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(886, 130);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // wizardText2
            // 
            this.wizardText2.Body = "Attach a MityVac (or similar) to the vacuum port on the back of the MPS. Set the " +
    "vacuum to 5 in Hg. When done click on Next.";
            this.wizardText2.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardText2.Location = new System.Drawing.Point(3, 71);
            this.wizardText2.Name = "wizardText2";
            this.wizardText2.Size = new System.Drawing.Size(886, 74);
            this.wizardText2.TabIndex = 2;
            this.wizardText2.Title = "Connect MityVac to MPS";
            // 
            // TunePage2Nav
            // 
            this.TunePage2Nav.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TunePage2Nav.FirstPage = false;
            this.TunePage2Nav.LastPage = false;
            this.TunePage2Nav.Location = new System.Drawing.Point(3, 373);
            this.TunePage2Nav.Name = "TunePage2Nav";
            this.TunePage2Nav.Size = new System.Drawing.Size(886, 36);
            this.TunePage2Nav.TabIndex = 1;
            this.TunePage2Nav.OnCancel += new DJetronicStudio.WizardNavigation.OnCancelHandler(this.Nav_OnCancel);
            this.TunePage2Nav.OnPrevious += new DJetronicStudio.WizardNavigation.OnPreviousHandler(this.Nav_OnPrevious);
            this.TunePage2Nav.OnNext += new DJetronicStudio.WizardNavigation.OnNextHandler(this.Nav_OnNext);
            // 
            // TunePage2Banner
            // 
            this.TunePage2Banner.BackColor = System.Drawing.Color.CornflowerBlue;
            this.TunePage2Banner.Dock = System.Windows.Forms.DockStyle.Top;
            this.TunePage2Banner.Location = new System.Drawing.Point(3, 3);
            this.TunePage2Banner.Name = "TunePage2Banner";
            this.TunePage2Banner.Size = new System.Drawing.Size(886, 68);
            this.TunePage2Banner.TabIndex = 0;
            this.TunePage2Banner.Title = "Tune MPS Step 2";
            // 
            // TunePage3
            // 
            this.TunePage3.BackColor = System.Drawing.SystemColors.Control;
            this.TunePage3.Controls.Add(this.label1);
            this.TunePage3.Controls.Add(this.comboBox1);
            this.TunePage3.Controls.Add(this.wizardText3);
            this.TunePage3.Controls.Add(this.wizardNavigation1);
            this.TunePage3.Controls.Add(this.wizardBanner1);
            this.TunePage3.Location = new System.Drawing.Point(4, 22);
            this.TunePage3.Name = "TunePage3";
            this.TunePage3.Padding = new System.Windows.Forms.Padding(3);
            this.TunePage3.Size = new System.Drawing.Size(892, 412);
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
            // comboBox1
            // 
            this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(265, 150);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(436, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // wizardText3
            // 
            this.wizardText3.Body = "Choose an MPS from the database to use as a reference. When done click on Next.";
            this.wizardText3.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardText3.Location = new System.Drawing.Point(3, 71);
            this.wizardText3.Name = "wizardText3";
            this.wizardText3.Size = new System.Drawing.Size(886, 73);
            this.wizardText3.TabIndex = 2;
            this.wizardText3.Title = "Choose Reference";
            // 
            // wizardNavigation1
            // 
            this.wizardNavigation1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wizardNavigation1.FirstPage = false;
            this.wizardNavigation1.LastPage = false;
            this.wizardNavigation1.Location = new System.Drawing.Point(3, 373);
            this.wizardNavigation1.Name = "wizardNavigation1";
            this.wizardNavigation1.Size = new System.Drawing.Size(886, 36);
            this.wizardNavigation1.TabIndex = 1;
            this.wizardNavigation1.OnCancel += new DJetronicStudio.WizardNavigation.OnCancelHandler(this.Nav_OnCancel);
            this.wizardNavigation1.OnPrevious += new DJetronicStudio.WizardNavigation.OnPreviousHandler(this.Nav_OnPrevious);
            this.wizardNavigation1.OnNext += new DJetronicStudio.WizardNavigation.OnNextHandler(this.Nav_OnNext);
            // 
            // wizardBanner1
            // 
            this.wizardBanner1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.wizardBanner1.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardBanner1.Location = new System.Drawing.Point(3, 3);
            this.wizardBanner1.Name = "wizardBanner1";
            this.wizardBanner1.Size = new System.Drawing.Size(886, 68);
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
            this.TunePage4.Size = new System.Drawing.Size(892, 412);
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
            this.Gauge.Size = new System.Drawing.Size(886, 215);
            this.Gauge.StartAngle = 150F;
            this.Gauge.StrokeWeight = 20F;
            this.Gauge.SweepAngle = 240F;
            this.Gauge.TabIndex = 3;
            this.Gauge.ThresholdNormal = 49.5F;
            this.Gauge.ThresholdOutOfRange = 60F;
            this.Gauge.ThresholdWarning1 = 40F;
            this.Gauge.ThresholdWarning2 = 50.5F;
            this.Gauge.Value = 10F;
            this.Gauge.ValueFont = new System.Drawing.Font("Calibri", 22F);
            this.Gauge.ValueMax = 15F;
            this.Gauge.ValueMin = 5F;
            this.Gauge.ValueUnit = "ms";
            this.Gauge.ValueUnitFont = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.Gauge.WarningColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(156)))), ((int)(((byte)(71)))));
            // 
            // wizardText4
            // 
            this.wizardText4.Body = "Turn the adjustment screw of the front of the MPS clockwise or counter-clockwise " +
    "to get the gauge below to turn green. When done click on Finish.";
            this.wizardText4.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardText4.Location = new System.Drawing.Point(3, 71);
            this.wizardText4.Name = "wizardText4";
            this.wizardText4.Size = new System.Drawing.Size(886, 87);
            this.wizardText4.TabIndex = 2;
            this.wizardText4.Title = "Perform Tuning";
            // 
            // wizardNavigation2
            // 
            this.wizardNavigation2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wizardNavigation2.FirstPage = false;
            this.wizardNavigation2.LastPage = true;
            this.wizardNavigation2.Location = new System.Drawing.Point(3, 373);
            this.wizardNavigation2.Name = "wizardNavigation2";
            this.wizardNavigation2.Size = new System.Drawing.Size(886, 36);
            this.wizardNavigation2.TabIndex = 1;
            this.wizardNavigation2.OnCancel += new DJetronicStudio.WizardNavigation.OnCancelHandler(this.Nav_OnCancel);
            this.wizardNavigation2.OnPrevious += new DJetronicStudio.WizardNavigation.OnPreviousHandler(this.Nav_OnPrevious);
            this.wizardNavigation2.OnNext += new DJetronicStudio.WizardNavigation.OnNextHandler(this.Nav_OnNext);
            // 
            // wizardBanner2
            // 
            this.wizardBanner2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.wizardBanner2.Dock = System.Windows.Forms.DockStyle.Top;
            this.wizardBanner2.Location = new System.Drawing.Point(3, 3);
            this.wizardBanner2.Name = "wizardBanner2";
            this.wizardBanner2.Size = new System.Drawing.Size(886, 68);
            this.wizardBanner2.TabIndex = 0;
            this.wizardBanner2.Title = "Tune MPS Step 4";
            // 
            // ExportMPSProfileDialog
            // 
            this.ExportMPSProfileDialog.DefaultExt = "mps";
            this.ExportMPSProfileDialog.Filter = "MPS Profiles (*.mps)|*.mps|All Files (*.*)|*.*";
            this.ExportMPSProfileDialog.Title = "Export MPS Profile";
            // 
            // TuneOMaticUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.StopContBtn);
            this.Controls.Add(this.StartContBtn);
            this.Controls.Add(this.PulseWidthValue);
            this.Controls.Add(this.PressureValue);
            this.Controls.Add(this.GetPulseWidthBtn);
            this.Controls.Add(this.GetPressureBtn);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog ExportCSVDialog;
        private System.Windows.Forms.Button GetPressureBtn;
        private System.Windows.Forms.Button GetPulseWidthBtn;
        private System.Windows.Forms.Label PressureValue;
        private System.Windows.Forms.Label PulseWidthValue;
        private System.Windows.Forms.Button StartContBtn;
        private System.Windows.Forms.Button StopContBtn;
        private TablessControl Tabs;
        private System.Windows.Forms.TabPage DbPage;
        private System.Windows.Forms.TabPage TunePage1;
        private WizardBanner TunePage1Banner;
        private WizardNavigation TunePage1Nav;
        private System.Windows.Forms.TabPage TunePage2;
        private WizardNavigation TunePage2Nav;
        private WizardBanner TunePage2Banner;
        private WizardText wizardText1;
        private WizardText wizardText2;
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
        private System.Windows.Forms.ComboBox comboBox1;
        private TunerGauge Gauge;
        private System.Windows.Forms.OpenFileDialog ImportMPSProfileDialog;
        private System.Windows.Forms.SaveFileDialog ExportMPSProfileDialog;
    }
}
