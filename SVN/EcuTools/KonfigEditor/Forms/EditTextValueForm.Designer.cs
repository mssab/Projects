namespace KonfigEditor.Forms
{
    partial class EditTextValueForm
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
            this.labelMrwId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownPosition = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownDezStellen = new System.Windows.Forms.NumericUpDown();
            this.checkBoxHex = new System.Windows.Forms.CheckBox();
            this.checkBoxSigned = new System.Windows.Forms.CheckBox();
            this.checkBoxHidden = new System.Windows.Forms.CheckBox();
            this.checkBoxCalculated = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplay = new System.Windows.Forms.CheckBox();
            this.tabControlText = new System.Windows.Forms.TabControl();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosition)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDezStellen)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMrwId
            // 
            this.labelMrwId.AutoSize = true;
            this.labelMrwId.Location = new System.Drawing.Point(12, 9);
            this.labelMrwId.Name = "labelMrwId";
            this.labelMrwId.Size = new System.Drawing.Size(141, 13);
            this.labelMrwId.TabIndex = 0;
            this.labelMrwId.Text = "Mess-/Rechenwert Identifier";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Position";
            // 
            // numericUpDownPosition
            // 
            this.numericUpDownPosition.Location = new System.Drawing.Point(62, 25);
            this.numericUpDownPosition.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownPosition.Name = "numericUpDownPosition";
            this.numericUpDownPosition.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownPosition.TabIndex = 2;
            this.numericUpDownPosition.ValueChanged += new System.EventHandler(this.numericUpDownPosition_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDownDezStellen);
            this.groupBox1.Controls.Add(this.checkBoxHex);
            this.groupBox1.Controls.Add(this.checkBoxSigned);
            this.groupBox1.Controls.Add(this.checkBoxHidden);
            this.groupBox1.Controls.Add(this.checkBoxCalculated);
            this.groupBox1.Controls.Add(this.checkBoxDisplay);
            this.groupBox1.Location = new System.Drawing.Point(12, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 95);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Flags";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Dez. Stellen";
            // 
            // numericUpDownDezStellen
            // 
            this.numericUpDownDezStellen.Location = new System.Drawing.Point(100, 67);
            this.numericUpDownDezStellen.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericUpDownDezStellen.Name = "numericUpDownDezStellen";
            this.numericUpDownDezStellen.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownDezStellen.TabIndex = 5;
            this.numericUpDownDezStellen.ValueChanged += new System.EventHandler(this.numericUpDownDezStellen_ValueChanged);
            // 
            // checkBoxHex
            // 
            this.checkBoxHex.AutoSize = true;
            this.checkBoxHex.Location = new System.Drawing.Point(7, 68);
            this.checkBoxHex.Name = "checkBoxHex";
            this.checkBoxHex.Size = new System.Drawing.Size(45, 17);
            this.checkBoxHex.TabIndex = 4;
            this.checkBoxHex.Text = "Hex";
            this.checkBoxHex.UseVisualStyleBackColor = true;
            this.checkBoxHex.CheckedChanged += new System.EventHandler(this.checkBoxHex_CheckedChanged);
            // 
            // checkBoxSigned
            // 
            this.checkBoxSigned.AutoSize = true;
            this.checkBoxSigned.Location = new System.Drawing.Point(100, 44);
            this.checkBoxSigned.Name = "checkBoxSigned";
            this.checkBoxSigned.Size = new System.Drawing.Size(79, 17);
            this.checkBoxSigned.TabIndex = 3;
            this.checkBoxSigned.Text = "Vorzeichen";
            this.checkBoxSigned.UseVisualStyleBackColor = true;
            // 
            // checkBoxHidden
            // 
            this.checkBoxHidden.AutoSize = true;
            this.checkBoxHidden.Location = new System.Drawing.Point(100, 20);
            this.checkBoxHidden.Name = "checkBoxHidden";
            this.checkBoxHidden.Size = new System.Drawing.Size(60, 17);
            this.checkBoxHidden.TabIndex = 2;
            this.checkBoxHidden.Text = "Hidden";
            this.checkBoxHidden.UseVisualStyleBackColor = true;
            // 
            // checkBoxCalculated
            // 
            this.checkBoxCalculated.AutoSize = true;
            this.checkBoxCalculated.Location = new System.Drawing.Point(7, 44);
            this.checkBoxCalculated.Name = "checkBoxCalculated";
            this.checkBoxCalculated.Size = new System.Drawing.Size(84, 17);
            this.checkBoxCalculated.TabIndex = 1;
            this.checkBoxCalculated.Text = "Rechenwert";
            this.checkBoxCalculated.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplay
            // 
            this.checkBoxDisplay.AutoSize = true;
            this.checkBoxDisplay.Location = new System.Drawing.Point(7, 20);
            this.checkBoxDisplay.Name = "checkBoxDisplay";
            this.checkBoxDisplay.Size = new System.Drawing.Size(60, 17);
            this.checkBoxDisplay.TabIndex = 0;
            this.checkBoxDisplay.Text = "Display";
            this.checkBoxDisplay.UseVisualStyleBackColor = true;
            // 
            // tabControlText
            // 
            this.tabControlText.Location = new System.Drawing.Point(12, 152);
            this.tabControlText.Multiline = true;
            this.tabControlText.Name = "tabControlText";
            this.tabControlText.SelectedIndex = 0;
            this.tabControlText.Size = new System.Drawing.Size(308, 236);
            this.tabControlText.TabIndex = 4;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(236, 12);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(81, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(235, 41);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(82, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Abbrechen";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(236, 123);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(81, 23);
            this.buttonApply.TabIndex = 7;
            this.buttonApply.Text = "Übernehmen";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // EditTextValueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 396);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControlText);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numericUpDownPosition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelMrwId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditTextValueForm";
            this.ShowIcon = false;
            this.Text = "Text für Messwert ändern";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosition)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDezStellen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMrwId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownPosition;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownDezStellen;
        private System.Windows.Forms.CheckBox checkBoxHex;
        private System.Windows.Forms.CheckBox checkBoxSigned;
        private System.Windows.Forms.CheckBox checkBoxHidden;
        private System.Windows.Forms.CheckBox checkBoxCalculated;
        private System.Windows.Forms.CheckBox checkBoxDisplay;
        private System.Windows.Forms.TabControl tabControlText;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonApply;
    }
}