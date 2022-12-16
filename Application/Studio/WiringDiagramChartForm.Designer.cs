namespace DJetronicStudio
{
    partial class WiringDiagramChartForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CloseBtn = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ExportImageBtn = new System.Windows.Forms.ToolStripButton();
            this.Chart = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            this.ExportImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseBtn
            // 
            this.CloseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CloseBtn.Location = new System.Drawing.Point(760, 397);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(75, 23);
            this.CloseBtn.TabIndex = 0;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportImageBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(847, 39);
            this.toolStrip1.TabIndex = 13;
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
            this.Chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Chart.BackColor = System.Drawing.Color.White;
            this.Chart.Location = new System.Drawing.Point(0, 42);
            this.Chart.Name = "Chart";
            this.Chart.Size = new System.Drawing.Size(847, 349);
            this.Chart.TabIndex = 14;
            // 
            // ExportImageDialog
            // 
            this.ExportImageDialog.DefaultExt = "png";
            this.ExportImageDialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            this.ExportImageDialog.Title = "Export Chart as Image";
            // 
            // WiringDiagramChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 432);
            this.Controls.Add(this.Chart);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.CloseBtn);
            this.Name = "WiringDiagramChartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wiring Diagram Chart";
            this.Shown += new System.EventHandler(this.WiringDiagramChartForm_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ExportImageBtn;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart Chart;
        private System.Windows.Forms.SaveFileDialog ExportImageDialog;
    }
}