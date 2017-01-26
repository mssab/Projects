namespace KonfigEditor.Forms
{
    partial class EditDatamapForm
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
            this.textBoxIdentifier = new System.Windows.Forms.TextBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownDimension = new System.Windows.Forms.NumericUpDown();
            this.groupBox_X = new System.Windows.Forms.GroupBox();
            this.numericUpDown_X_Steps = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown_X_Stepsize = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown_X_Start = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox_Z = new System.Windows.Forms.GroupBox();
            this.numericUpDown_Z1_Stepsize = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.numericUpDown_Z_Stepsize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Z_Start = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1y = new System.Windows.Forms.GroupBox();
            this.numericUpDown_Y_Steps = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_Y_Stepsize = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDown_Y_Start = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDimension)).BeginInit();
            this.groupBox_X.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X_Steps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X_Stepsize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X_Start)).BeginInit();
            this.groupBox_Z.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z1_Stepsize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z_Stepsize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z_Start)).BeginInit();
            this.groupBox1y.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y_Steps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y_Stepsize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y_Start)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxIdentifier
            // 
            this.textBoxIdentifier.Location = new System.Drawing.Point(76, 12);
            this.textBoxIdentifier.Name = "textBoxIdentifier";
            this.textBoxIdentifier.Size = new System.Drawing.Size(170, 20);
            this.textBoxIdentifier.TabIndex = 0;
            this.textBoxIdentifier.TextChanged += new System.EventHandler(this.textBoxIdentifier_TextChanged);
            this.textBoxIdentifier.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxIdentifier_KeyPress);
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(76, 38);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(170, 21);
            this.comboBoxType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Identifier";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Dimension";
            // 
            // numericUpDownDimension
            // 
            this.numericUpDownDimension.Location = new System.Drawing.Point(76, 65);
            this.numericUpDownDimension.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownDimension.Name = "numericUpDownDimension";
            this.numericUpDownDimension.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownDimension.TabIndex = 5;
            this.numericUpDownDimension.ValueChanged += new System.EventHandler(this.numericUpDownDimension_ValueChanged);
            // 
            // groupBox_X
            // 
            this.groupBox_X.Controls.Add(this.numericUpDown_X_Steps);
            this.groupBox_X.Controls.Add(this.label6);
            this.groupBox_X.Controls.Add(this.numericUpDown_X_Stepsize);
            this.groupBox_X.Controls.Add(this.label5);
            this.groupBox_X.Controls.Add(this.numericUpDown_X_Start);
            this.groupBox_X.Controls.Add(this.label4);
            this.groupBox_X.Location = new System.Drawing.Point(12, 91);
            this.groupBox_X.Name = "groupBox_X";
            this.groupBox_X.Size = new System.Drawing.Size(74, 143);
            this.groupBox_X.TabIndex = 6;
            this.groupBox_X.TabStop = false;
            this.groupBox_X.Text = "X-Achse";
            // 
            // numericUpDown_X_Steps
            // 
            this.numericUpDown_X_Steps.Location = new System.Drawing.Point(6, 110);
            this.numericUpDown_X_Steps.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown_X_Steps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_X_Steps.Name = "numericUpDown_X_Steps";
            this.numericUpDown_X_Steps.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown_X_Steps.TabIndex = 5;
            this.numericUpDown_X_Steps.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_X_Steps.ValueChanged += new System.EventHandler(this.numericUpDown_X_Steps_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Steps";
            // 
            // numericUpDown_X_Stepsize
            // 
            this.numericUpDown_X_Stepsize.Location = new System.Drawing.Point(6, 71);
            this.numericUpDown_X_Stepsize.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown_X_Stepsize.Name = "numericUpDown_X_Stepsize";
            this.numericUpDown_X_Stepsize.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown_X_Stepsize.TabIndex = 3;
            this.numericUpDown_X_Stepsize.ValueChanged += new System.EventHandler(this.numericUpDown_X_Stepsize_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Stepsize";
            // 
            // numericUpDown_X_Start
            // 
            this.numericUpDown_X_Start.Location = new System.Drawing.Point(6, 32);
            this.numericUpDown_X_Start.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown_X_Start.Name = "numericUpDown_X_Start";
            this.numericUpDown_X_Start.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown_X_Start.TabIndex = 1;
            this.numericUpDown_X_Start.ValueChanged += new System.EventHandler(this.numericUpDown_X_Start_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Start";
            // 
            // groupBox_Z
            // 
            this.groupBox_Z.Controls.Add(this.numericUpDown_Z1_Stepsize);
            this.groupBox_Z.Controls.Add(this.label10);
            this.groupBox_Z.Controls.Add(this.label11);
            this.groupBox_Z.Controls.Add(this.label12);
            this.groupBox_Z.Controls.Add(this.numericUpDown_Z_Stepsize);
            this.groupBox_Z.Controls.Add(this.numericUpDown_Z_Start);
            this.groupBox_Z.Location = new System.Drawing.Point(172, 91);
            this.groupBox_Z.Name = "groupBox_Z";
            this.groupBox_Z.Size = new System.Drawing.Size(74, 143);
            this.groupBox_Z.TabIndex = 7;
            this.groupBox_Z.TabStop = false;
            this.groupBox_Z.Text = "Z-Achse";
            this.groupBox_Z.Enter += new System.EventHandler(this.groupBox_Z_Enter);
            // 
            // numericUpDown_Z1_Stepsize
            // 
            this.numericUpDown_Z1_Stepsize.Location = new System.Drawing.Point(6, 110);
            this.numericUpDown_Z1_Stepsize.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown_Z1_Stepsize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Z1_Stepsize.Name = "numericUpDown_Z1_Stepsize";
            this.numericUpDown_Z1_Stepsize.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown_Z1_Stepsize.TabIndex = 5;
            this.numericUpDown_Z1_Stepsize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Z1_Stepsize.ValueChanged += new System.EventHandler(this.numericUpDown_X_Steps_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 94);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Steps";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Stepsize";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Start";
            // 
            // numericUpDown_Z_Stepsize
            // 
            this.numericUpDown_Z_Stepsize.Cursor = System.Windows.Forms.Cursors.Default;
            this.numericUpDown_Z_Stepsize.Location = new System.Drawing.Point(6, 71);
            this.numericUpDown_Z_Stepsize.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown_Z_Stepsize.Name = "numericUpDown_Z_Stepsize";
            this.numericUpDown_Z_Stepsize.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown_Z_Stepsize.TabIndex = 3;
            this.numericUpDown_Z_Stepsize.ValueChanged += new System.EventHandler(this.numericUpDown_Z_Stepsize_ValueChanged);
            // 
            // numericUpDown_Z_Start
            // 
            this.numericUpDown_Z_Start.Location = new System.Drawing.Point(6, 32);
            this.numericUpDown_Z_Start.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown_Z_Start.Name = "numericUpDown_Z_Start";
            this.numericUpDown_Z_Start.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown_Z_Start.TabIndex = 1;
            this.numericUpDown_Z_Start.ValueChanged += new System.EventHandler(this.numericUpDown_X_Start_ValueChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(12, 240);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(171, 240);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Abbrechen";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox1y
            // 
            this.groupBox1y.Controls.Add(this.numericUpDown_Y_Steps);
            this.groupBox1y.Controls.Add(this.label7);
            this.groupBox1y.Controls.Add(this.label8);
            this.groupBox1y.Controls.Add(this.numericUpDown_Y_Stepsize);
            this.groupBox1y.Controls.Add(this.label13);
            this.groupBox1y.Controls.Add(this.numericUpDown_Y_Start);
            this.groupBox1y.Location = new System.Drawing.Point(92, 91);
            this.groupBox1y.Name = "groupBox1y";
            this.groupBox1y.Size = new System.Drawing.Size(74, 143);
            this.groupBox1y.TabIndex = 6;
            this.groupBox1y.TabStop = false;
            this.groupBox1y.Text = "Y-Achse";
            this.groupBox1y.Enter += new System.EventHandler(this.groupBox1y_Enter);
            // 
            // numericUpDown_Y_Steps
            // 
            this.numericUpDown_Y_Steps.Location = new System.Drawing.Point(6, 110);
            this.numericUpDown_Y_Steps.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown_Y_Steps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Y_Steps.Name = "numericUpDown_Y_Steps";
            this.numericUpDown_Y_Steps.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown_Y_Steps.TabIndex = 5;
            this.numericUpDown_Y_Steps.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Y_Steps.ValueChanged += new System.EventHandler(this.numericUpDown_X_Steps_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Steps";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Stepsize";
            // 
            // numericUpDown_Y_Stepsize
            // 
            this.numericUpDown_Y_Stepsize.Location = new System.Drawing.Point(6, 71);
            this.numericUpDown_Y_Stepsize.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown_Y_Stepsize.Name = "numericUpDown_Y_Stepsize";
            this.numericUpDown_Y_Stepsize.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown_Y_Stepsize.TabIndex = 3;
            this.numericUpDown_Y_Stepsize.ValueChanged += new System.EventHandler(this.numericUpDown_Y_Stepsize_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Start";
            // 
            // numericUpDown_Y_Start
            // 
            this.numericUpDown_Y_Start.Location = new System.Drawing.Point(6, 32);
            this.numericUpDown_Y_Start.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown_Y_Start.Name = "numericUpDown_Y_Start";
            this.numericUpDown_Y_Start.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown_Y_Start.TabIndex = 1;
            this.numericUpDown_Y_Start.ValueChanged += new System.EventHandler(this.numericUpDown_X_Start_ValueChanged);
            // 
            // EditDatamapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 296);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox_Z);
            this.Controls.Add(this.groupBox1y);
            this.Controls.Add(this.groupBox_X);
            this.Controls.Add(this.numericUpDownDimension);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.textBoxIdentifier);
            this.Name = "EditDatamapForm";
            this.Text = "Kennfeld ändern";
            this.Load += new System.EventHandler(this.EditDatamapForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDimension)).EndInit();
            this.groupBox_X.ResumeLayout(false);
            this.groupBox_X.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X_Steps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X_Stepsize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_X_Start)).EndInit();
            this.groupBox_Z.ResumeLayout(false);
            this.groupBox_Z.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z1_Stepsize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z_Stepsize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Z_Start)).EndInit();
            this.groupBox1y.ResumeLayout(false);
            this.groupBox1y.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y_Steps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y_Stepsize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Y_Start)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxIdentifier;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownDimension;
        private System.Windows.Forms.GroupBox groupBox_X;
        private System.Windows.Forms.NumericUpDown numericUpDown_X_Steps;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown_X_Stepsize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown_X_Start;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox_Z;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1y;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numericUpDown_Z1_Stepsize;
        private System.Windows.Forms.NumericUpDown numericUpDown_Z_Stepsize;
        private System.Windows.Forms.NumericUpDown numericUpDown_Z_Start;
        private System.Windows.Forms.NumericUpDown numericUpDown_Y_Steps;
        private System.Windows.Forms.NumericUpDown numericUpDown_Y_Stepsize;
        private System.Windows.Forms.NumericUpDown numericUpDown_Y_Start;
    }
}