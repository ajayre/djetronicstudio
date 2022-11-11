namespace DJetronicStudio
{
    partial class ReadPressureButton
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
            this.Btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn
            // 
            this.Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn.Location = new System.Drawing.Point(0, 0);
            this.Btn.Name = "Btn";
            this.Btn.Size = new System.Drawing.Size(154, 32);
            this.Btn.TabIndex = 0;
            this.Btn.Text = "Vacuum set to 0 inHg";
            this.Btn.UseVisualStyleBackColor = true;
            this.Btn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Btn_MouseClick);
            // 
            // ReadPressureButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Btn);
            this.Name = "ReadPressureButton";
            this.Size = new System.Drawing.Size(154, 32);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn;
    }
}
