namespace KonfigEditor.Forms
{
    partial class EditVersionForm
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
            this.numericUpDownMain = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownNeben = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownRevision = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNeben)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRevision)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownMain
            // 
            this.numericUpDownMain.Location = new System.Drawing.Point(12, 12);
            this.numericUpDownMain.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownMain.Name = "numericUpDownMain";
            this.numericUpDownMain.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownMain.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = ".";
            // 
            // numericUpDownNeben
            // 
            this.numericUpDownNeben.Location = new System.Drawing.Point(98, 12);
            this.numericUpDownNeben.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownNeben.Name = "numericUpDownNeben";
            this.numericUpDownNeben.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownNeben.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = ".";
            // 
            // numericUpDownRevision
            // 
            this.numericUpDownRevision.Enabled = false;
            this.numericUpDownRevision.Location = new System.Drawing.Point(170, 12);
            this.numericUpDownRevision.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownRevision.Name = "numericUpDownRevision";
            this.numericUpDownRevision.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownRevision.TabIndex = 4;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(12, 47);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(145, 47);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Abbrechen";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // EditVersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 82);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.numericUpDownRevision);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownNeben);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditVersionForm";
            this.ShowIcon = false;
            this.Text = "Versionsnummer ändern";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNeben)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRevision)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownNeben;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownRevision;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}