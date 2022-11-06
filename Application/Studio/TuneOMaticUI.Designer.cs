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
            this.Tabs = new DJetronicStudio.TablessControl();
            this.DbPage = new System.Windows.Forms.TabPage();
            this.TunePage1 = new System.Windows.Forms.TabPage();
            this.TunePage1Banner = new DJetronicStudio.WizardBanner();
            this.TunePage1Nav = new DJetronicStudio.WizardNavigation();
            this.TunePage2 = new System.Windows.Forms.TabPage();
            this.TunePage2Banner = new DJetronicStudio.WizardBanner();
            this.TunePage2Nav = new DJetronicStudio.WizardNavigation();
            this.Tabs.SuspendLayout();
            this.TunePage1.SuspendLayout();
            this.TunePage2.SuspendLayout();
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
            // Tabs
            // 
            this.Tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tabs.Controls.Add(this.DbPage);
            this.Tabs.Controls.Add(this.TunePage1);
            this.Tabs.Controls.Add(this.TunePage2);
            this.Tabs.Location = new System.Drawing.Point(3, 61);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(900, 438);
            this.Tabs.TabIndex = 6;
            // 
            // DbPage
            // 
            this.DbPage.BackColor = System.Drawing.SystemColors.Control;
            this.DbPage.Location = new System.Drawing.Point(4, 22);
            this.DbPage.Name = "DbPage";
            this.DbPage.Padding = new System.Windows.Forms.Padding(3);
            this.DbPage.Size = new System.Drawing.Size(892, 412);
            this.DbPage.TabIndex = 0;
            this.DbPage.Text = "DbPage";
            // 
            // TunePage1
            // 
            this.TunePage1.BackColor = System.Drawing.SystemColors.Control;
            this.TunePage1.Controls.Add(this.TunePage1Nav);
            this.TunePage1.Controls.Add(this.TunePage1Banner);
            this.TunePage1.Location = new System.Drawing.Point(4, 22);
            this.TunePage1.Name = "TunePage1";
            this.TunePage1.Padding = new System.Windows.Forms.Padding(3);
            this.TunePage1.Size = new System.Drawing.Size(892, 412);
            this.TunePage1.TabIndex = 1;
            this.TunePage1.Text = "TunePage1";
            // 
            // TunePage1Banner
            // 
            this.TunePage1Banner.BackColor = System.Drawing.Color.CornflowerBlue;
            this.TunePage1Banner.Dock = System.Windows.Forms.DockStyle.Top;
            this.TunePage1Banner.Location = new System.Drawing.Point(3, 3);
            this.TunePage1Banner.Name = "TunePage1Banner";
            this.TunePage1Banner.Size = new System.Drawing.Size(886, 68);
            this.TunePage1Banner.TabIndex = 0;
            // 
            // TunePage1Nav
            // 
            this.TunePage1Nav.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TunePage1Nav.Location = new System.Drawing.Point(3, 373);
            this.TunePage1Nav.Name = "TunePage1Nav";
            this.TunePage1Nav.Size = new System.Drawing.Size(886, 36);
            this.TunePage1Nav.TabIndex = 1;
            // 
            // TunePage2
            // 
            this.TunePage2.BackColor = System.Drawing.SystemColors.Control;
            this.TunePage2.Controls.Add(this.TunePage2Nav);
            this.TunePage2.Controls.Add(this.TunePage2Banner);
            this.TunePage2.Location = new System.Drawing.Point(4, 22);
            this.TunePage2.Name = "TunePage2";
            this.TunePage2.Padding = new System.Windows.Forms.Padding(3);
            this.TunePage2.Size = new System.Drawing.Size(892, 412);
            this.TunePage2.TabIndex = 2;
            this.TunePage2.Text = "TunePage2";
            // 
            // TunePage2Banner
            // 
            this.TunePage2Banner.BackColor = System.Drawing.Color.CornflowerBlue;
            this.TunePage2Banner.Dock = System.Windows.Forms.DockStyle.Top;
            this.TunePage2Banner.Location = new System.Drawing.Point(3, 3);
            this.TunePage2Banner.Name = "TunePage2Banner";
            this.TunePage2Banner.Size = new System.Drawing.Size(886, 68);
            this.TunePage2Banner.TabIndex = 0;
            // 
            // TunePage2Nav
            // 
            this.TunePage2Nav.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TunePage2Nav.FirstPage = false;
            this.TunePage2Nav.Location = new System.Drawing.Point(3, 373);
            this.TunePage2Nav.Name = "TunePage2Nav";
            this.TunePage2Nav.Size = new System.Drawing.Size(886, 36);
            this.TunePage2Nav.TabIndex = 1;
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
            this.TunePage2.ResumeLayout(false);
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
    }
}
