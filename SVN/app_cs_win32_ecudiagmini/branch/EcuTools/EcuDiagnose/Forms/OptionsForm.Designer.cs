namespace EcuDiagnose.Forms
{
    partial class OptionsForm
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxLocalTime = new System.Windows.Forms.CheckBox();
            this.comboBoxServerId = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxOptionLanguage = new System.Windows.Forms.ComboBox();
            this.textBoxTimerInterval = new System.Windows.Forms.TextBox();
            this.labelTimer = new System.Windows.Forms.Label();
            this.labelComPort = new System.Windows.Forms.Label();
            this.textBoxComPort = new System.Windows.Forms.TextBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBoxLocalTime
            // 
            this.checkBoxLocalTime.AutoSize = true;
            this.checkBoxLocalTime.Location = new System.Drawing.Point(77, 145);
            this.checkBoxLocalTime.Name = "checkBoxLocalTime";
            this.checkBoxLocalTime.Size = new System.Drawing.Size(133, 17);
            this.checkBoxLocalTime.TabIndex = 23;
            this.checkBoxLocalTime.Text = "Lokale Zeit (statt UTC)";
            this.checkBoxLocalTime.UseVisualStyleBackColor = true;
            // 
            // comboBoxServerId
            // 
            this.comboBoxServerId.FormattingEnabled = true;
            this.comboBoxServerId.Location = new System.Drawing.Point(99, 118);
            this.comboBoxServerId.Name = "comboBoxServerId";
            this.comboBoxServerId.Size = new System.Drawing.Size(82, 21);
            this.comboBoxServerId.TabIndex = 22;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(46, 121);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "ServerId";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 67);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Passwort (HEX)";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(99, 64);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(121, 20);
            this.textBoxPassword.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Sprache";
            // 
            // comboBoxOptionLanguage
            // 
            this.comboBoxOptionLanguage.FormattingEnabled = true;
            this.comboBoxOptionLanguage.Items.AddRange(new object[] {
            "Deutsch",
            "Englisch",
            "Französisch",
            "Italienisch",
            "Spanisch",
            "Polnisch",
            "Niederländisch"});
            this.comboBoxOptionLanguage.Location = new System.Drawing.Point(99, 90);
            this.comboBoxOptionLanguage.Name = "comboBoxOptionLanguage";
            this.comboBoxOptionLanguage.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOptionLanguage.TabIndex = 17;
            this.comboBoxOptionLanguage.Text = "Deutsch";
            // 
            // textBoxTimerInterval
            // 
            this.textBoxTimerInterval.Location = new System.Drawing.Point(99, 38);
            this.textBoxTimerInterval.Name = "textBoxTimerInterval";
            this.textBoxTimerInterval.Size = new System.Drawing.Size(43, 20);
            this.textBoxTimerInterval.TabIndex = 16;
            this.textBoxTimerInterval.Text = "500";
            this.textBoxTimerInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelTimer
            // 
            this.labelTimer.AutoSize = true;
            this.labelTimer.Location = new System.Drawing.Point(38, 41);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(55, 13);
            this.labelTimer.TabIndex = 15;
            this.labelTimer.Text = "Timer [ms]";
            // 
            // labelComPort
            // 
            this.labelComPort.AutoSize = true;
            this.labelComPort.Location = new System.Drawing.Point(40, 15);
            this.labelComPort.Name = "labelComPort";
            this.labelComPort.Size = new System.Drawing.Size(53, 13);
            this.labelComPort.TabIndex = 13;
            this.labelComPort.Text = "COM Port";
            // 
            // textBoxComPort
            // 
            this.textBoxComPort.Location = new System.Drawing.Point(99, 12);
            this.textBoxComPort.Name = "textBoxComPort";
            this.textBoxComPort.Size = new System.Drawing.Size(43, 20);
            this.textBoxComPort.TabIndex = 14;
            this.textBoxComPort.Text = "1";
            this.textBoxComPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonApply
            // 
            this.buttonApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonApply.Location = new System.Drawing.Point(22, 184);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 24;
            this.buttonApply.Text = "OK";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(136, 183);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 25;
            this.buttonCancel.Text = "Abbrechen";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 216);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.checkBoxLocalTime);
            this.Controls.Add(this.comboBoxServerId);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxOptionLanguage);
            this.Controls.Add(this.textBoxTimerInterval);
            this.Controls.Add(this.labelTimer);
            this.Controls.Add(this.labelComPort);
            this.Controls.Add(this.textBoxComPort);
            this.Name = "OptionsForm";
            this.Text = "Verbindungs Einstellungen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxLocalTime;
        private System.Windows.Forms.ComboBox comboBoxServerId;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxOptionLanguage;
        private System.Windows.Forms.TextBox textBoxTimerInterval;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Label labelComPort;
        private System.Windows.Forms.TextBox textBoxComPort;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonCancel;
    }
}