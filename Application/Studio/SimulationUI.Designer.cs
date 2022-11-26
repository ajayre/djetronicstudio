namespace DJetronicStudio
{
    partial class SimulationUI
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
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SettingsPage = new System.Windows.Forms.TabPage();
            this.ChartPage = new System.Windows.Forms.TabPage();
            this.SimChart = new DJetronicStudio.SimChart();
            this.tabControl1.SuspendLayout();
            this.ChartPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // OutputBox
            // 
            this.OutputBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.OutputBox.Location = new System.Drawing.Point(0, 307);
            this.OutputBox.Multiline = true;
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputBox.Size = new System.Drawing.Size(906, 195);
            this.OutputBox.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.SettingsPage);
            this.tabControl1.Controls.Add(this.ChartPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(906, 307);
            this.tabControl1.TabIndex = 2;
            // 
            // SettingsPage
            // 
            this.SettingsPage.Location = new System.Drawing.Point(4, 22);
            this.SettingsPage.Name = "SettingsPage";
            this.SettingsPage.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsPage.Size = new System.Drawing.Size(898, 281);
            this.SettingsPage.TabIndex = 0;
            this.SettingsPage.Text = "Settings";
            this.SettingsPage.UseVisualStyleBackColor = true;
            // 
            // ChartPage
            // 
            this.ChartPage.Controls.Add(this.SimChart);
            this.ChartPage.Location = new System.Drawing.Point(4, 22);
            this.ChartPage.Name = "ChartPage";
            this.ChartPage.Padding = new System.Windows.Forms.Padding(3);
            this.ChartPage.Size = new System.Drawing.Size(898, 281);
            this.ChartPage.TabIndex = 1;
            this.ChartPage.Text = "Chart";
            this.ChartPage.UseVisualStyleBackColor = true;
            // 
            // SimChart
            // 
            this.SimChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SimChart.Location = new System.Drawing.Point(3, 3);
            this.SimChart.Name = "SimChart";
            this.SimChart.Size = new System.Drawing.Size(892, 275);
            this.SimChart.TabIndex = 0;
            // 
            // SimulationUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.OutputBox);
            this.Name = "SimulationUI";
            this.Size = new System.Drawing.Size(906, 502);
            this.tabControl1.ResumeLayout(false);
            this.ChartPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox OutputBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SettingsPage;
        private System.Windows.Forms.TabPage ChartPage;
        private SimChart SimChart;
    }
}
