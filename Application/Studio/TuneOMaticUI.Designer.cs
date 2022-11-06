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
            // TuneOMaticUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.StopContBtn);
            this.Controls.Add(this.StartContBtn);
            this.Controls.Add(this.PulseWidthValue);
            this.Controls.Add(this.PressureValue);
            this.Controls.Add(this.GetPulseWidthBtn);
            this.Controls.Add(this.GetPressureBtn);
            this.Name = "TuneOMaticUI";
            this.Size = new System.Drawing.Size(906, 502);
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
    }
}
