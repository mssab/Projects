namespace KonfigEditor.Controls
{
    partial class LanguageValueUserControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxUnit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxOffset = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAltUnit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownDivisor = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownFactor = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDivisor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(44, 3);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(238, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // textBoxUnit
            // 
            this.textBoxUnit.Location = new System.Drawing.Point(54, 29);
            this.textBoxUnit.Name = "textBoxUnit";
            this.textBoxUnit.Size = new System.Drawing.Size(228, 20);
            this.textBoxUnit.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Einheit";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxOffset);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxAltUnit);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericUpDownDivisor);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDownFactor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(6, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 129);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Alternative Einheit";
            // 
            // textBoxOffset
            // 
            this.textBoxOffset.Location = new System.Drawing.Point(55, 72);
            this.textBoxOffset.Name = "textBoxOffset";
            this.textBoxOffset.Size = new System.Drawing.Size(100, 20);
            this.textBoxOffset.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Einheit";
            // 
            // textBoxAltUnit
            // 
            this.textBoxAltUnit.Location = new System.Drawing.Point(55, 99);
            this.textBoxAltUnit.Name = "textBoxAltUnit";
            this.textBoxAltUnit.Size = new System.Drawing.Size(100, 20);
            this.textBoxAltUnit.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Offset";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(106, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Faktor != 0";
            // 
            // numericUpDownDivisor
            // 
            this.numericUpDownDivisor.Location = new System.Drawing.Point(55, 46);
            this.numericUpDownDivisor.Maximum = new decimal(new int[] {
            242,
            0,
            0,
            0});
            this.numericUpDownDivisor.Name = "numericUpDownDivisor";
            this.numericUpDownDivisor.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownDivisor.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Divisor";
            // 
            // numericUpDownFactor
            // 
            this.numericUpDownFactor.Location = new System.Drawing.Point(55, 19);
            this.numericUpDownFactor.Maximum = new decimal(new int[] {
            242,
            0,
            0,
            0});
            this.numericUpDownFactor.Name = "numericUpDownFactor";
            this.numericUpDownFactor.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownFactor.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Faktor";
            // 
            // LanguageValueUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxUnit);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Name = "LanguageValueUserControl";
            this.Size = new System.Drawing.Size(293, 193);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDivisor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFactor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAltUnit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownDivisor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownFactor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxOffset;
    }
}
