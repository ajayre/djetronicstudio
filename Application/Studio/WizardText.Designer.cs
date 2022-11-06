namespace DJetronicStudio
{
    partial class WizardText
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
            this.BodyText = new System.Windows.Forms.Label();
            this.TitleText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BodyText
            // 
            this.BodyText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BodyText.Location = new System.Drawing.Point(53, 43);
            this.BodyText.Name = "BodyText";
            this.BodyText.Size = new System.Drawing.Size(446, 42);
            this.BodyText.TabIndex = 5;
            this.BodyText.Text = "BodyText";
            // 
            // TitleText
            // 
            this.TitleText.AutoSize = true;
            this.TitleText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleText.Location = new System.Drawing.Point(12, 12);
            this.TitleText.Name = "TitleText";
            this.TitleText.Size = new System.Drawing.Size(68, 20);
            this.TitleText.TabIndex = 4;
            this.TitleText.Text = "TitleText";
            // 
            // WizardText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BodyText);
            this.Controls.Add(this.TitleText);
            this.Name = "WizardText";
            this.Size = new System.Drawing.Size(560, 85);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BodyText;
        private System.Windows.Forms.Label TitleText;
    }
}
