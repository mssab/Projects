namespace VideoesListe
{
    partial class VidListFm
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
            this.TextboxTitle = new System.Windows.Forms.TextBox();
            this.titellb = new System.Windows.Forms.Label();
            this.zeitlb = new System.Windows.Forms.Label();
            this.TextboxZeit = new System.Windows.Forms.TextBox();
            this.erstellDt = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.hochDtlb = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.Okbt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextboxTitle
            // 
            this.TextboxTitle.Location = new System.Drawing.Point(113, 12);
            this.TextboxTitle.Name = "TextboxTitle";
            this.TextboxTitle.Size = new System.Drawing.Size(200, 20);
            this.TextboxTitle.TabIndex = 0;
            this.TextboxTitle.TextChanged += new System.EventHandler(this.TextboxTitle_TextChanged);
            // 
            // titellb
            // 
            this.titellb.AutoSize = true;
            this.titellb.Location = new System.Drawing.Point(14, 15);
            this.titellb.Name = "titellb";
            this.titellb.Size = new System.Drawing.Size(57, 13);
            this.titellb.TabIndex = 1;
            this.titellb.Text = "Video Titel";
            // 
            // zeitlb
            // 
            this.zeitlb.AutoSize = true;
            this.zeitlb.Location = new System.Drawing.Point(14, 62);
            this.zeitlb.Name = "zeitlb";
            this.zeitlb.Size = new System.Drawing.Size(55, 13);
            this.zeitlb.TabIndex = 2;
            this.zeitlb.Text = "Video Zeit";
            // 
            // TextboxZeit
            // 
            this.TextboxZeit.Location = new System.Drawing.Point(113, 55);
            this.TextboxZeit.Name = "TextboxZeit";
            this.TextboxZeit.Size = new System.Drawing.Size(200, 20);
            this.TextboxZeit.TabIndex = 3;
            this.TextboxZeit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxZeit_KeyPress);
            // 
            // erstellDt
            // 
            this.erstellDt.AutoSize = true;
            this.erstellDt.Location = new System.Drawing.Point(14, 103);
            this.erstellDt.Name = "erstellDt";
            this.erstellDt.Size = new System.Drawing.Size(87, 13);
            this.erstellDt.TabIndex = 4;
            this.erstellDt.Text = "Erstellung Datum";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(113, 96);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // hochDtlb
            // 
            this.hochDtlb.AutoSize = true;
            this.hochDtlb.Location = new System.Drawing.Point(14, 151);
            this.hochDtlb.Name = "hochDtlb";
            this.hochDtlb.Size = new System.Drawing.Size(99, 13);
            this.hochDtlb.TabIndex = 4;
            this.hochDtlb.Text = "Hochladung Datum";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(113, 145);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(157, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Abbrechen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Okbt
            // 
            this.Okbt.Location = new System.Drawing.Point(238, 171);
            this.Okbt.Name = "Okbt";
            this.Okbt.Size = new System.Drawing.Size(75, 23);
            this.Okbt.TabIndex = 7;
            this.Okbt.Text = "OK";
            this.Okbt.UseVisualStyleBackColor = true;
            this.Okbt.Click += new System.EventHandler(this.Okbt_Click);
            this.Okbt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Okbt_KeyPress);
            // 
            // VidListFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 210);
            this.Controls.Add(this.Okbt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.hochDtlb);
            this.Controls.Add(this.erstellDt);
            this.Controls.Add(this.TextboxZeit);
            this.Controls.Add(this.zeitlb);
            this.Controls.Add(this.titellb);
            this.Controls.Add(this.TextboxTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VidListFm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Videos Liste ";
            this.Load += new System.EventHandler(this.VidListFm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titellb;
        private System.Windows.Forms.Label zeitlb;
        private System.Windows.Forms.Label erstellDt;
        private System.Windows.Forms.Label hochDtlb;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox TextboxTitle;
        public System.Windows.Forms.TextBox TextboxZeit;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.DateTimePicker dateTimePicker2;
        public System.Windows.Forms.Button Okbt;
    }
}