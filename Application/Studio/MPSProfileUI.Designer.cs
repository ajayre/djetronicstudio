namespace DJetronicStudio
{
    partial class MPSProfileUI
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
            this.components = new System.ComponentModel.Container();
            this.MPSNamelabel = new System.Windows.Forms.Label();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.CalibrationIcon = new System.Windows.Forms.PictureBox();
            this.ExportBtn = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.TuneUsingBtn = new System.Windows.Forms.Button();
            this.RenameBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CalibrationIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // MPSNamelabel
            // 
            this.MPSNamelabel.Location = new System.Drawing.Point(0, 1);
            this.MPSNamelabel.Name = "MPSNamelabel";
            this.MPSNamelabel.Size = new System.Drawing.Size(128, 12);
            this.MPSNamelabel.TabIndex = 5;
            this.MPSNamelabel.Text = "Name of the MPS";
            // 
            // CalibrationIcon
            // 
            this.CalibrationIcon.BackColor = System.Drawing.Color.White;
            this.CalibrationIcon.Image = global::DJetronicStudio.Properties.Resources.star_24;
            this.CalibrationIcon.Location = new System.Drawing.Point(103, 17);
            this.CalibrationIcon.Name = "CalibrationIcon";
            this.CalibrationIcon.Size = new System.Drawing.Size(24, 24);
            this.CalibrationIcon.TabIndex = 6;
            this.CalibrationIcon.TabStop = false;
            this.ToolTips.SetToolTip(this.CalibrationIcon, "Factory calibrated");
            // 
            // ExportBtn
            // 
            this.ExportBtn.Image = global::DJetronicStudio.Properties.Resources.export_mps_24;
            this.ExportBtn.Location = new System.Drawing.Point(67, 147);
            this.ExportBtn.Name = "ExportBtn";
            this.ExportBtn.Size = new System.Drawing.Size(28, 28);
            this.ExportBtn.TabIndex = 4;
            this.ToolTips.SetToolTip(this.ExportBtn, "Export profile");
            this.ExportBtn.UseVisualStyleBackColor = true;
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Image = global::DJetronicStudio.Properties.Resources.delete_24;
            this.DeleteBtn.Location = new System.Drawing.Point(101, 147);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(28, 28);
            this.DeleteBtn.TabIndex = 3;
            this.ToolTips.SetToolTip(this.DeleteBtn, "Delete profile");
            this.DeleteBtn.UseVisualStyleBackColor = true;
            // 
            // TuneUsingBtn
            // 
            this.TuneUsingBtn.Image = global::DJetronicStudio.Properties.Resources.tuneomatic_24;
            this.TuneUsingBtn.Location = new System.Drawing.Point(33, 147);
            this.TuneUsingBtn.Name = "TuneUsingBtn";
            this.TuneUsingBtn.Size = new System.Drawing.Size(28, 28);
            this.TuneUsingBtn.TabIndex = 2;
            this.ToolTips.SetToolTip(this.TuneUsingBtn, "Tune using this profile as a reference");
            this.TuneUsingBtn.UseVisualStyleBackColor = true;
            // 
            // RenameBtn
            // 
            this.RenameBtn.Image = global::DJetronicStudio.Properties.Resources.edit_24;
            this.RenameBtn.Location = new System.Drawing.Point(-1, 147);
            this.RenameBtn.Name = "RenameBtn";
            this.RenameBtn.Size = new System.Drawing.Size(28, 28);
            this.RenameBtn.TabIndex = 1;
            this.ToolTips.SetToolTip(this.RenameBtn, "Rename");
            this.RenameBtn.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DJetronicStudio.Properties.Resources.mps_128;
            this.pictureBox1.Location = new System.Drawing.Point(0, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // MPSProfileUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CalibrationIcon);
            this.Controls.Add(this.MPSNamelabel);
            this.Controls.Add(this.ExportBtn);
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.TuneUsingBtn);
            this.Controls.Add(this.RenameBtn);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MPSProfileUI";
            this.Size = new System.Drawing.Size(128, 177);
            ((System.ComponentModel.ISupportInitialize)(this.CalibrationIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button RenameBtn;
        private System.Windows.Forms.Button TuneUsingBtn;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.Button ExportBtn;
        private System.Windows.Forms.Label MPSNamelabel;
        private System.Windows.Forms.PictureBox CalibrationIcon;
        private System.Windows.Forms.ToolTip ToolTips;
    }
}
