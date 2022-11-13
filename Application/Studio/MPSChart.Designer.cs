namespace DJetronicStudio
{
    partial class MPSChart
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DatabaseTree = new System.Windows.Forms.TreeView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportImageBtn});
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
            this.Chart.Location = new System.Drawing.Point(0, 0);
            this.Chart.Name = "Chart";
            this.Chart.Size = new System.Drawing.Size(678, 488);
            this.Chart.TabIndex = 0;
            // 
            // ExportImageDialog
            // 
            this.ExportImageDialog.DefaultExt = "png";
            this.ExportImageDialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            this.ExportImageDialog.Title = "Export Chart as Image";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DatabaseTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Chart);
            this.splitContainer1.Size = new System.Drawing.Size(911, 488);
            this.splitContainer1.SplitterDistance = 229;
            this.splitContainer1.TabIndex = 13;
            // 
            // DatabaseTree
            // 
            this.DatabaseTree.CheckBoxes = true;
            this.DatabaseTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DatabaseTree.Location = new System.Drawing.Point(0, 0);
            this.DatabaseTree.Name = "DatabaseTree";
            this.DatabaseTree.Size = new System.Drawing.Size(229, 488);
            this.DatabaseTree.TabIndex = 0;
            this.DatabaseTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.DatabaseTree_AfterCheck);
            // 
            // MPSChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MPSChart";
            this.Size = new System.Drawing.Size(911, 527);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart Chart;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ExportImageBtn;
        private System.Windows.Forms.SaveFileDialog ExportImageDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView DatabaseTree;
    }
}
