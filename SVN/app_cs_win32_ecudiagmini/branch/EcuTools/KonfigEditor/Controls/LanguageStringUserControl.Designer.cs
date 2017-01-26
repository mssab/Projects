namespace KonfigEditor.Controls
{
    partial class LanguageStringUserControl
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
            this.labelLanguageID = new System.Windows.Forms.Label();
            this.textBoxLanguageString = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelLanguageID
            // 
            this.labelLanguageID.AutoSize = true;
            this.labelLanguageID.Location = new System.Drawing.Point(3, 6);
            this.labelLanguageID.Name = "labelLanguageID";
            this.labelLanguageID.Size = new System.Drawing.Size(48, 13);
            this.labelLanguageID.TabIndex = 0;
            this.labelLanguageID.Text = "IdLngXX";
            // 
            // textBoxLanguageString
            // 
            this.textBoxLanguageString.Location = new System.Drawing.Point(57, 3);
            this.textBoxLanguageString.Name = "textBoxLanguageString";
            this.textBoxLanguageString.Size = new System.Drawing.Size(175, 20);
            this.textBoxLanguageString.TabIndex = 1;
            // 
            // LanguageStringUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.textBoxLanguageString);
            this.Controls.Add(this.labelLanguageID);
            this.Name = "LanguageStringUserControl";
            this.Size = new System.Drawing.Size(239, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLanguageID;
        private System.Windows.Forms.TextBox textBoxLanguageString;
    }
}
