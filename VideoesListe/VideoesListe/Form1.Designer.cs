namespace VideoesListe
{
    partial class Form1
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
            this.btnew = new System.Windows.Forms.Button();
            this.listViewVideos = new System.Windows.Forms.ListView();
            this.spichbtn = new System.Windows.Forms.Button();
            this.ladbtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.öffenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnew
            // 
            this.btnew.Location = new System.Drawing.Point(243, 26);
            this.btnew.Name = "btnew";
            this.btnew.Size = new System.Drawing.Size(75, 23);
            this.btnew.TabIndex = 0;
            this.btnew.Text = "Neu Video";
            this.btnew.UseVisualStyleBackColor = true;
            this.btnew.Click += new System.EventHandler(this.button1_Click);
            // 
            // listViewVideos
            // 
            this.listViewVideos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewVideos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewVideos.Location = new System.Drawing.Point(0, 87);
            this.listViewVideos.Name = "listViewVideos";
            this.listViewVideos.Size = new System.Drawing.Size(818, 367);
            this.listViewVideos.TabIndex = 1;
            this.listViewVideos.UseCompatibleStateImageBehavior = false;
            this.listViewVideos.View = System.Windows.Forms.View.Details;
            this.listViewVideos.DoubleClick += new System.EventHandler(this.listViewVideos_DoubleClick);
            this.listViewVideos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewVideos_KeyDown);
            this.listViewVideos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewVideos_MouseDoubleClick);
            // 
            // spichbtn
            // 
            this.spichbtn.Location = new System.Drawing.Point(324, 26);
            this.spichbtn.Name = "spichbtn";
            this.spichbtn.Size = new System.Drawing.Size(75, 23);
            this.spichbtn.TabIndex = 2;
            this.spichbtn.Text = "Speichern";
            this.spichbtn.UseVisualStyleBackColor = true;
            this.spichbtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // ladbtn
            // 
            this.ladbtn.Location = new System.Drawing.Point(405, 26);
            this.ladbtn.Name = "ladbtn";
            this.ladbtn.Size = new System.Drawing.Size(75, 23);
            this.ladbtn.TabIndex = 3;
            this.ladbtn.Text = "Laden";
            this.ladbtn.UseVisualStyleBackColor = true;
            this.ladbtn.Click += new System.EventHandler(this.ladbtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(486, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Löschen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(818, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.neuToolStripMenuItem,
            this.öffenToolStripMenuItem,
            this.speichernToolStripMenuItem,
            this.speichernToolStripMenuItem1});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // neuToolStripMenuItem
            // 
            this.neuToolStripMenuItem.Name = "neuToolStripMenuItem";
            this.neuToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.neuToolStripMenuItem.Text = "Neu Video";
            this.neuToolStripMenuItem.Click += new System.EventHandler(this.neuToolStripMenuItem_Click);
            // 
            // öffenToolStripMenuItem
            // 
            this.öffenToolStripMenuItem.Name = "öffenToolStripMenuItem";
            this.öffenToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.öffenToolStripMenuItem.Text = "Öffen";
            this.öffenToolStripMenuItem.Click += new System.EventHandler(this.öffenToolStripMenuItem_Click);
            // 
            // speichernToolStripMenuItem
            // 
            this.speichernToolStripMenuItem.Name = "speichernToolStripMenuItem";
            this.speichernToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.speichernToolStripMenuItem.Text = "Speichern Unter";
            this.speichernToolStripMenuItem.Click += new System.EventHandler(this.speichernToolStripMenuItem_Click);
            // 
            // speichernToolStripMenuItem1
            // 
            this.speichernToolStripMenuItem1.Name = "speichernToolStripMenuItem1";
            this.speichernToolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.speichernToolStripMenuItem1.Text = "Speichern";
            this.speichernToolStripMenuItem1.Click += new System.EventHandler(this.speichernToolStripMenuItem1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(762, 58);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(44, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(656, 58);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(621, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Filter";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 455);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ladbtn);
            this.Controls.Add(this.spichbtn);
            this.Controls.Add(this.listViewVideos);
            this.Controls.Add(this.btnew);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnew;
        private System.Windows.Forms.Button ladbtn;
        public System.Windows.Forms.Button spichbtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem neuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem öffenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speichernToolStripMenuItem1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListView listViewVideos;
    }
}

