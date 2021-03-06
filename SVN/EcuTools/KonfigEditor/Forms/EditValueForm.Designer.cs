﻿namespace KonfigEditor.Forms
{
    partial class EditValueForm
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
            this.buttonOkay = new System.Windows.Forms.Button();
            this.buttonAbbrechen = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.labelPreviousValue = new System.Windows.Forms.Label();
            this.comboBoxValue = new System.Windows.Forms.ComboBox();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonOkay
            // 
            this.buttonOkay.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOkay.Location = new System.Drawing.Point(12, 91);
            this.buttonOkay.Name = "buttonOkay";
            this.buttonOkay.Size = new System.Drawing.Size(75, 23);
            this.buttonOkay.TabIndex = 0;
            this.buttonOkay.Text = "OK";
            this.buttonOkay.UseVisualStyleBackColor = true;
            this.buttonOkay.Click += new System.EventHandler(this.buttonOkay_Click);
            // 
            // buttonAbbrechen
            // 
            this.buttonAbbrechen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonAbbrechen.Location = new System.Drawing.Point(117, 91);
            this.buttonAbbrechen.Name = "buttonAbbrechen";
            this.buttonAbbrechen.Size = new System.Drawing.Size(75, 23);
            this.buttonAbbrechen.TabIndex = 1;
            this.buttonAbbrechen.Text = "Abbrechen";
            this.buttonAbbrechen.UseVisualStyleBackColor = true;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(58, 13);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Item Name";
            // 
            // labelPreviousValue
            // 
            this.labelPreviousValue.AutoSize = true;
            this.labelPreviousValue.Location = new System.Drawing.Point(12, 22);
            this.labelPreviousValue.Name = "labelPreviousValue";
            this.labelPreviousValue.Size = new System.Drawing.Size(78, 13);
            this.labelPreviousValue.TabIndex = 3;
            this.labelPreviousValue.Text = "Previous Value";
            // 
            // comboBoxValue
            // 
            this.comboBoxValue.FormattingEnabled = true;
            this.comboBoxValue.Location = new System.Drawing.Point(12, 38);
            this.comboBoxValue.Name = "comboBoxValue";
            this.comboBoxValue.Size = new System.Drawing.Size(180, 21);
            this.comboBoxValue.TabIndex = 4;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(12, 65);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(180, 20);
            this.textBoxValue.TabIndex = 5;
            this.textBoxValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxValue_KeyDown);
            // 
            // EditValueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 124);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.comboBoxValue);
            this.Controls.Add(this.labelPreviousValue);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonAbbrechen);
            this.Controls.Add(this.buttonOkay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditValueForm";
            this.ShowIcon = false;
            this.Text = "Direkte Änderung";
            this.Load += new System.EventHandler(this.EditValueForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOkay;
        private System.Windows.Forms.Button buttonAbbrechen;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelPreviousValue;
        private System.Windows.Forms.ComboBox comboBoxValue;
        private System.Windows.Forms.TextBox textBoxValue;

    }
}