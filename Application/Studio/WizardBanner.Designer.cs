namespace DJetronicStudio
{
    partial class WizardBanner
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
            this.TitleText = new System.Windows.Forms.Label();
            this.IconBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleText
            // 
            this.TitleText.AutoSize = true;
            this.TitleText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleText.ForeColor = System.Drawing.Color.White;
            this.TitleText.Location = new System.Drawing.Point(63, 22);
            this.TitleText.Name = "TitleText";
            this.TitleText.Size = new System.Drawing.Size(183, 26);
            this.TitleText.TabIndex = 1;
            this.TitleText.Text = "Tune MPS Step 1";
            // 
            // IconBox
            // 
            this.IconBox.Image = global::DJetronicStudio.Properties.Resources.tuneomatic_48;
            this.IconBox.Location = new System.Drawing.Point(9, 9);
            this.IconBox.Name = "IconBox";
            this.IconBox.Size = new System.Drawing.Size(48, 48);
            this.IconBox.TabIndex = 0;
            this.IconBox.TabStop = false;
            // 
            // WizardBanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Controls.Add(this.TitleText);
            this.Controls.Add(this.IconBox);
            this.Name = "WizardBanner";
            this.Size = new System.Drawing.Size(876, 68);
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox IconBox;
        private System.Windows.Forms.Label TitleText;
    }
}
