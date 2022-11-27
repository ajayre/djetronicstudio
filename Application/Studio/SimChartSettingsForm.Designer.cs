namespace DJetronicStudio
{
    partial class SimChartSettingsForm
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
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.DataBox = new System.Windows.Forms.GroupBox();
            this.InjectorGroupIVColorInput = new System.Windows.Forms.Panel();
            this.InjectorGroupIIIColorInput = new System.Windows.Forms.Panel();
            this.InjectorGroupIIColorInput = new System.Windows.Forms.Panel();
            this.InjectorGroupIColorInput = new System.Windows.Forms.Panel();
            this.InjectorGroupIVInput = new System.Windows.Forms.CheckBox();
            this.InjectorGroupIIIInput = new System.Windows.Forms.CheckBox();
            this.InjectorGroupIIInput = new System.Windows.Forms.CheckBox();
            this.InjectorGroupIInput = new System.Windows.Forms.CheckBox();
            this.ColorDialog = new System.Windows.Forms.ColorDialog();
            this.MPSPin7ColorInput = new System.Windows.Forms.Panel();
            this.MPSPin7Input = new System.Windows.Forms.CheckBox();
            this.MPSPin8Input = new System.Windows.Forms.CheckBox();
            this.MPSPin10Input = new System.Windows.Forms.CheckBox();
            this.MPSPin15Input = new System.Windows.Forms.CheckBox();
            this.MPSPin8ColorInput = new System.Windows.Forms.Panel();
            this.MPSPin10ColorInput = new System.Windows.Forms.Panel();
            this.MPSPin15ColorInput = new System.Windows.Forms.Panel();
            this.CustomInput = new System.Windows.Forms.CheckBox();
            this.CustomColorInput = new System.Windows.Forms.Panel();
            this.CustomVectorNameInput = new System.Windows.Forms.TextBox();
            this.DataBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OKBtn
            // 
            this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Location = new System.Drawing.Point(669, 345);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 0;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(588, 345);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 1;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            // 
            // DataBox
            // 
            this.DataBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataBox.Controls.Add(this.CustomVectorNameInput);
            this.DataBox.Controls.Add(this.CustomColorInput);
            this.DataBox.Controls.Add(this.CustomInput);
            this.DataBox.Controls.Add(this.MPSPin15ColorInput);
            this.DataBox.Controls.Add(this.MPSPin10ColorInput);
            this.DataBox.Controls.Add(this.MPSPin8ColorInput);
            this.DataBox.Controls.Add(this.MPSPin15Input);
            this.DataBox.Controls.Add(this.MPSPin10Input);
            this.DataBox.Controls.Add(this.MPSPin8Input);
            this.DataBox.Controls.Add(this.MPSPin7ColorInput);
            this.DataBox.Controls.Add(this.MPSPin7Input);
            this.DataBox.Controls.Add(this.InjectorGroupIVColorInput);
            this.DataBox.Controls.Add(this.InjectorGroupIIIColorInput);
            this.DataBox.Controls.Add(this.InjectorGroupIIColorInput);
            this.DataBox.Controls.Add(this.InjectorGroupIColorInput);
            this.DataBox.Controls.Add(this.InjectorGroupIVInput);
            this.DataBox.Controls.Add(this.InjectorGroupIIIInput);
            this.DataBox.Controls.Add(this.InjectorGroupIIInput);
            this.DataBox.Controls.Add(this.InjectorGroupIInput);
            this.DataBox.Location = new System.Drawing.Point(12, 12);
            this.DataBox.Name = "DataBox";
            this.DataBox.Size = new System.Drawing.Size(732, 327);
            this.DataBox.TabIndex = 2;
            this.DataBox.TabStop = false;
            this.DataBox.Text = "Data";
            // 
            // InjectorGroupIVColorInput
            // 
            this.InjectorGroupIVColorInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InjectorGroupIVColorInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InjectorGroupIVColorInput.Location = new System.Drawing.Point(118, 88);
            this.InjectorGroupIVColorInput.Name = "InjectorGroupIVColorInput";
            this.InjectorGroupIVColorInput.Size = new System.Drawing.Size(49, 17);
            this.InjectorGroupIVColorInput.TabIndex = 7;
            this.InjectorGroupIVColorInput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorInput_MouseClick);
            // 
            // InjectorGroupIIIColorInput
            // 
            this.InjectorGroupIIIColorInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InjectorGroupIIIColorInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InjectorGroupIIIColorInput.Location = new System.Drawing.Point(118, 65);
            this.InjectorGroupIIIColorInput.Name = "InjectorGroupIIIColorInput";
            this.InjectorGroupIIIColorInput.Size = new System.Drawing.Size(49, 17);
            this.InjectorGroupIIIColorInput.TabIndex = 6;
            this.InjectorGroupIIIColorInput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorInput_MouseClick);
            // 
            // InjectorGroupIIColorInput
            // 
            this.InjectorGroupIIColorInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InjectorGroupIIColorInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InjectorGroupIIColorInput.Location = new System.Drawing.Point(118, 42);
            this.InjectorGroupIIColorInput.Name = "InjectorGroupIIColorInput";
            this.InjectorGroupIIColorInput.Size = new System.Drawing.Size(49, 17);
            this.InjectorGroupIIColorInput.TabIndex = 5;
            this.InjectorGroupIIColorInput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorInput_MouseClick);
            // 
            // InjectorGroupIColorInput
            // 
            this.InjectorGroupIColorInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InjectorGroupIColorInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InjectorGroupIColorInput.Location = new System.Drawing.Point(118, 19);
            this.InjectorGroupIColorInput.Name = "InjectorGroupIColorInput";
            this.InjectorGroupIColorInput.Size = new System.Drawing.Size(49, 17);
            this.InjectorGroupIColorInput.TabIndex = 4;
            this.InjectorGroupIColorInput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorInput_MouseClick);
            // 
            // InjectorGroupIVInput
            // 
            this.InjectorGroupIVInput.AutoSize = true;
            this.InjectorGroupIVInput.Location = new System.Drawing.Point(6, 88);
            this.InjectorGroupIVInput.Name = "InjectorGroupIVInput";
            this.InjectorGroupIVInput.Size = new System.Drawing.Size(106, 17);
            this.InjectorGroupIVInput.TabIndex = 3;
            this.InjectorGroupIVInput.Text = "Injector Group IV";
            this.InjectorGroupIVInput.UseVisualStyleBackColor = true;
            // 
            // InjectorGroupIIIInput
            // 
            this.InjectorGroupIIIInput.AutoSize = true;
            this.InjectorGroupIIIInput.Location = new System.Drawing.Point(6, 65);
            this.InjectorGroupIIIInput.Name = "InjectorGroupIIIInput";
            this.InjectorGroupIIIInput.Size = new System.Drawing.Size(105, 17);
            this.InjectorGroupIIIInput.TabIndex = 2;
            this.InjectorGroupIIIInput.Text = "Injector Group III";
            this.InjectorGroupIIIInput.UseVisualStyleBackColor = true;
            // 
            // InjectorGroupIIInput
            // 
            this.InjectorGroupIIInput.AutoSize = true;
            this.InjectorGroupIIInput.Location = new System.Drawing.Point(6, 42);
            this.InjectorGroupIIInput.Name = "InjectorGroupIIInput";
            this.InjectorGroupIIInput.Size = new System.Drawing.Size(102, 17);
            this.InjectorGroupIIInput.TabIndex = 1;
            this.InjectorGroupIIInput.Text = "Injector Group II";
            this.InjectorGroupIIInput.UseVisualStyleBackColor = true;
            // 
            // InjectorGroupIInput
            // 
            this.InjectorGroupIInput.AutoSize = true;
            this.InjectorGroupIInput.Location = new System.Drawing.Point(6, 19);
            this.InjectorGroupIInput.Name = "InjectorGroupIInput";
            this.InjectorGroupIInput.Size = new System.Drawing.Size(99, 17);
            this.InjectorGroupIInput.TabIndex = 0;
            this.InjectorGroupIInput.Text = "Injector Group I";
            this.InjectorGroupIInput.UseVisualStyleBackColor = true;
            // 
            // MPSPin7ColorInput
            // 
            this.MPSPin7ColorInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MPSPin7ColorInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MPSPin7ColorInput.Location = new System.Drawing.Point(278, 19);
            this.MPSPin7ColorInput.Name = "MPSPin7ColorInput";
            this.MPSPin7ColorInput.Size = new System.Drawing.Size(49, 17);
            this.MPSPin7ColorInput.TabIndex = 9;
            this.MPSPin7ColorInput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorInput_MouseClick);
            // 
            // MPSPin7Input
            // 
            this.MPSPin7Input.AutoSize = true;
            this.MPSPin7Input.Location = new System.Drawing.Point(190, 19);
            this.MPSPin7Input.Name = "MPSPin7Input";
            this.MPSPin7Input.Size = new System.Drawing.Size(76, 17);
            this.MPSPin7Input.TabIndex = 8;
            this.MPSPin7Input.Text = "MPS Pin 7";
            this.MPSPin7Input.UseVisualStyleBackColor = true;
            // 
            // MPSPin8Input
            // 
            this.MPSPin8Input.AutoSize = true;
            this.MPSPin8Input.Location = new System.Drawing.Point(190, 42);
            this.MPSPin8Input.Name = "MPSPin8Input";
            this.MPSPin8Input.Size = new System.Drawing.Size(76, 17);
            this.MPSPin8Input.TabIndex = 10;
            this.MPSPin8Input.Text = "MPS Pin 8";
            this.MPSPin8Input.UseVisualStyleBackColor = true;
            // 
            // MPSPin10Input
            // 
            this.MPSPin10Input.AutoSize = true;
            this.MPSPin10Input.Location = new System.Drawing.Point(190, 65);
            this.MPSPin10Input.Name = "MPSPin10Input";
            this.MPSPin10Input.Size = new System.Drawing.Size(82, 17);
            this.MPSPin10Input.TabIndex = 11;
            this.MPSPin10Input.Text = "MPS Pin 10";
            this.MPSPin10Input.UseVisualStyleBackColor = true;
            // 
            // MPSPin15Input
            // 
            this.MPSPin15Input.AutoSize = true;
            this.MPSPin15Input.Location = new System.Drawing.Point(190, 88);
            this.MPSPin15Input.Name = "MPSPin15Input";
            this.MPSPin15Input.Size = new System.Drawing.Size(82, 17);
            this.MPSPin15Input.TabIndex = 12;
            this.MPSPin15Input.Text = "MPS Pin 15";
            this.MPSPin15Input.UseVisualStyleBackColor = true;
            // 
            // MPSPin8ColorInput
            // 
            this.MPSPin8ColorInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MPSPin8ColorInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MPSPin8ColorInput.Location = new System.Drawing.Point(278, 42);
            this.MPSPin8ColorInput.Name = "MPSPin8ColorInput";
            this.MPSPin8ColorInput.Size = new System.Drawing.Size(49, 17);
            this.MPSPin8ColorInput.TabIndex = 10;
            this.MPSPin8ColorInput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorInput_MouseClick);
            // 
            // MPSPin10ColorInput
            // 
            this.MPSPin10ColorInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MPSPin10ColorInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MPSPin10ColorInput.Location = new System.Drawing.Point(278, 65);
            this.MPSPin10ColorInput.Name = "MPSPin10ColorInput";
            this.MPSPin10ColorInput.Size = new System.Drawing.Size(49, 17);
            this.MPSPin10ColorInput.TabIndex = 13;
            this.MPSPin10ColorInput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorInput_MouseClick);
            // 
            // MPSPin15ColorInput
            // 
            this.MPSPin15ColorInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MPSPin15ColorInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MPSPin15ColorInput.Location = new System.Drawing.Point(278, 88);
            this.MPSPin15ColorInput.Name = "MPSPin15ColorInput";
            this.MPSPin15ColorInput.Size = new System.Drawing.Size(49, 17);
            this.MPSPin15ColorInput.TabIndex = 14;
            this.MPSPin15ColorInput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorInput_MouseClick);
            // 
            // CustomInput
            // 
            this.CustomInput.AutoSize = true;
            this.CustomInput.Location = new System.Drawing.Point(6, 136);
            this.CustomInput.Name = "CustomInput";
            this.CustomInput.Size = new System.Drawing.Size(61, 17);
            this.CustomInput.TabIndex = 15;
            this.CustomInput.Text = "Custom";
            this.CustomInput.UseVisualStyleBackColor = true;
            this.CustomInput.CheckedChanged += new System.EventHandler(this.Input_CheckedChanged);
            // 
            // CustomColorInput
            // 
            this.CustomColorInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CustomColorInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CustomColorInput.Location = new System.Drawing.Point(278, 135);
            this.CustomColorInput.Name = "CustomColorInput";
            this.CustomColorInput.Size = new System.Drawing.Size(49, 17);
            this.CustomColorInput.TabIndex = 8;
            this.CustomColorInput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorInput_MouseClick);
            // 
            // CustomVectorNameInput
            // 
            this.CustomVectorNameInput.Location = new System.Drawing.Point(73, 133);
            this.CustomVectorNameInput.Name = "CustomVectorNameInput";
            this.CustomVectorNameInput.Size = new System.Drawing.Size(199, 20);
            this.CustomVectorNameInput.TabIndex = 16;
            // 
            // SimChartSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 380);
            this.Controls.Add(this.DataBox);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SimChartSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SimChartSettingsForm_FormClosing);
            this.Shown += new System.EventHandler(this.SimChartSettingsForm_Shown);
            this.DataBox.ResumeLayout(false);
            this.DataBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.GroupBox DataBox;
        private System.Windows.Forms.CheckBox InjectorGroupIVInput;
        private System.Windows.Forms.CheckBox InjectorGroupIIIInput;
        private System.Windows.Forms.CheckBox InjectorGroupIIInput;
        private System.Windows.Forms.CheckBox InjectorGroupIInput;
        private System.Windows.Forms.Panel InjectorGroupIVColorInput;
        private System.Windows.Forms.Panel InjectorGroupIIIColorInput;
        private System.Windows.Forms.Panel InjectorGroupIIColorInput;
        private System.Windows.Forms.Panel InjectorGroupIColorInput;
        private System.Windows.Forms.ColorDialog ColorDialog;
        private System.Windows.Forms.Panel MPSPin15ColorInput;
        private System.Windows.Forms.Panel MPSPin10ColorInput;
        private System.Windows.Forms.Panel MPSPin8ColorInput;
        private System.Windows.Forms.CheckBox MPSPin15Input;
        private System.Windows.Forms.CheckBox MPSPin10Input;
        private System.Windows.Forms.CheckBox MPSPin8Input;
        private System.Windows.Forms.Panel MPSPin7ColorInput;
        private System.Windows.Forms.CheckBox MPSPin7Input;
        private System.Windows.Forms.TextBox CustomVectorNameInput;
        private System.Windows.Forms.Panel CustomColorInput;
        private System.Windows.Forms.CheckBox CustomInput;
    }
}