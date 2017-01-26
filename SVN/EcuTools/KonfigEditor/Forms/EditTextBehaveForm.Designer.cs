namespace KonfigEditor.Forms
{
    partial class EditTextBehaveForm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.numericUpDownPosition = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanelStrings = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(187, 9);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(187, 41);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Abbrechen";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(99, 41);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(82, 23);
            this.buttonApply.TabIndex = 2;
            this.buttonApply.Text = "Übernehmen";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // numericUpDownPosition
            // 
            this.numericUpDownPosition.Location = new System.Drawing.Point(62, 12);
            this.numericUpDownPosition.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownPosition.Name = "numericUpDownPosition";
            this.numericUpDownPosition.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownPosition.TabIndex = 3;
            this.numericUpDownPosition.ValueChanged += new System.EventHandler(this.numericUpDownPosition_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Position";
            // 
            // flowLayoutPanelStrings
            // 
            this.flowLayoutPanelStrings.AutoScroll = true;
            this.flowLayoutPanelStrings.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelStrings.Location = new System.Drawing.Point(12, 70);
            this.flowLayoutPanelStrings.Name = "flowLayoutPanelStrings";
            this.flowLayoutPanelStrings.Size = new System.Drawing.Size(250, 259);
            this.flowLayoutPanelStrings.TabIndex = 5;
            // 
            // EditTextBehaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 341);
            this.Controls.Add(this.flowLayoutPanelStrings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownPosition);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditTextBehaveForm";
            this.ShowIcon = false;
            this.Text = "Text für Verhalten ändern";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.NumericUpDown numericUpDownPosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelStrings;
    }
}