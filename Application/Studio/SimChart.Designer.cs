namespace DJetronicStudio
{
    partial class SimChart
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ExportImageBtn = new System.Windows.Forms.ToolStripButton();
            this.Chart = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            this.ExportImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.EditBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportImageBtn,
            this.toolStripSeparator1,
            this.EditBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(911, 39);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ExportImageBtn
            // 
            this.ExportImageBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExportImageBtn.Image = global::DJetronicStudio.Properties.Resources.exportimage_32;
            this.ExportImageBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExportImageBtn.Name = "ExportImageBtn";
            this.ExportImageBtn.Size = new System.Drawing.Size(36, 36);
            this.ExportImageBtn.Text = "Export image";
            this.ExportImageBtn.Click += new System.EventHandler(this.ExportImageBtn_Click);
            // 
            // Chart
            // 
            this.Chart.BackColor = System.Drawing.Color.White;
            this.Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Chart.Location = new System.Drawing.Point(0, 39);
            this.Chart.Name = "Chart";
            this.Chart.Size = new System.Drawing.Size(911, 488);
            this.Chart.TabIndex = 0;
            // 
            // ExportImageDialog
            // 
            this.ExportImageDialog.DefaultExt = "png";
            this.ExportImageDialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            this.ExportImageDialog.Title = "Export Chart as Image";
            // 
            // EditBtn
            // 
            this.EditBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditBtn.Image = global::DJetronicStudio.Properties.Resources.edit_32;
            this.EditBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditBtn.Name = "EditBtn";
            this.EditBtn.Size = new System.Drawing.Size(36, 36);
            this.EditBtn.Text = "Edit";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // SimChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Chart);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SimChart";
            this.Size = new System.Drawing.Size(911, 527);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart Chart;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ExportImageBtn;
        private System.Windows.Forms.SaveFileDialog ExportImageDialog;
        private System.Windows.Forms.ToolStripButton EditBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
