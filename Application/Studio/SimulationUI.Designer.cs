﻿namespace DJetronicStudio
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
            this.ExportCSVDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // ExportCSVDialog
            // 
            this.ExportCSVDialog.DefaultExt = "csv";
            this.ExportCSVDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            this.ExportCSVDialog.Title = "Export Data to CSV";
            // 
            // SimulationUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Name = "SimulationUI";
            this.Size = new System.Drawing.Size(906, 502);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog ExportCSVDialog;
    }
}