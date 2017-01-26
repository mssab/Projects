namespace KonfigEditor.Forms
{
    partial class KonfigEditorMainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KonfigEditorMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportNachCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.upgradeNachV9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upgradeNachV10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mRU0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mRU1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mRU2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mRU3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mRU4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorMRU = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewParam = new System.Windows.Forms.TreeView();
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.upbutton = new System.Windows.Forms.Button();
            this.dnbutton = new System.Windows.Forms.Button();
            this.listViewParam = new HJS.ListViewNF();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuStrip1.Size = new System.Drawing.Size(883, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFileOpen,
            this.downloadToolStripMenuItem,
            this.speichernToolStripMenuItem,
            this.toolStripMenuItemFileSave,
            this.toolStripSeparator1,
            this.exportNachCSVToolStripMenuItem,
            this.toolStripSeparator3,
            this.upgradeNachV9ToolStripMenuItem,
            this.upgradeNachV10ToolStripMenuItem,
            this.toolStripSeparator2,
            this.mRU0ToolStripMenuItem,
            this.mRU1ToolStripMenuItem,
            this.mRU2ToolStripMenuItem,
            this.mRU3ToolStripMenuItem,
            this.mRU4ToolStripMenuItem,
            this.toolStripSeparatorMRU,
            this.toolStripMenuItemClose});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // toolStripMenuItemFileOpen
            // 
            this.toolStripMenuItemFileOpen.Name = "toolStripMenuItemFileOpen";
            this.toolStripMenuItemFileOpen.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemFileOpen.Text = "Öffnen...";
            this.toolStripMenuItemFileOpen.Click += new System.EventHandler(this.toolStripMenuItemFileOpen_Click);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.downloadToolStripMenuItem.Text = "Download...";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // speichernToolStripMenuItem
            // 
            this.speichernToolStripMenuItem.Enabled = false;
            this.speichernToolStripMenuItem.Name = "speichernToolStripMenuItem";
            this.speichernToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.speichernToolStripMenuItem.Text = "Speichern";
            this.speichernToolStripMenuItem.Click += new System.EventHandler(this.speichernToolStripMenuItem_Click);
            // 
            // toolStripMenuItemFileSave
            // 
            this.toolStripMenuItemFileSave.Name = "toolStripMenuItemFileSave";
            this.toolStripMenuItemFileSave.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemFileSave.Text = "Speichern unter...";
            this.toolStripMenuItemFileSave.Click += new System.EventHandler(this.toolStripMenuItemFileSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(182, 6);
            // 
            // exportNachCSVToolStripMenuItem
            // 
            this.exportNachCSVToolStripMenuItem.Name = "exportNachCSVToolStripMenuItem";
            this.exportNachCSVToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.exportNachCSVToolStripMenuItem.Text = "Export nach CSV...";
            this.exportNachCSVToolStripMenuItem.Click += new System.EventHandler(this.exportNachCSVToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(182, 6);
            // 
            // upgradeNachV9ToolStripMenuItem
            // 
            this.upgradeNachV9ToolStripMenuItem.Name = "upgradeNachV9ToolStripMenuItem";
            this.upgradeNachV9ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.upgradeNachV9ToolStripMenuItem.Text = "Upgrade nach V9...";
            this.upgradeNachV9ToolStripMenuItem.Click += new System.EventHandler(this.upgradeNachV9ToolStripMenuItem_Click);
            // 
            // upgradeNachV10ToolStripMenuItem
            // 
            this.upgradeNachV10ToolStripMenuItem.Name = "upgradeNachV10ToolStripMenuItem";
            this.upgradeNachV10ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.upgradeNachV10ToolStripMenuItem.Text = "Upgrade nach V10...";
            this.upgradeNachV10ToolStripMenuItem.Click += new System.EventHandler(this.upgradeNachV10ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(182, 6);
            // 
            // mRU0ToolStripMenuItem
            // 
            this.mRU0ToolStripMenuItem.Name = "mRU0ToolStripMenuItem";
            this.mRU0ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.mRU0ToolStripMenuItem.Text = "MRU0";
            this.mRU0ToolStripMenuItem.Click += new System.EventHandler(this.loadMostRecentlyUsed);
            // 
            // mRU1ToolStripMenuItem
            // 
            this.mRU1ToolStripMenuItem.Name = "mRU1ToolStripMenuItem";
            this.mRU1ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.mRU1ToolStripMenuItem.Text = "MRU1";
            this.mRU1ToolStripMenuItem.Click += new System.EventHandler(this.loadMostRecentlyUsed);
            // 
            // mRU2ToolStripMenuItem
            // 
            this.mRU2ToolStripMenuItem.Name = "mRU2ToolStripMenuItem";
            this.mRU2ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.mRU2ToolStripMenuItem.Text = "MRU2";
            this.mRU2ToolStripMenuItem.Click += new System.EventHandler(this.loadMostRecentlyUsed);
            // 
            // mRU3ToolStripMenuItem
            // 
            this.mRU3ToolStripMenuItem.Name = "mRU3ToolStripMenuItem";
            this.mRU3ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.mRU3ToolStripMenuItem.Text = "MRU3";
            this.mRU3ToolStripMenuItem.Click += new System.EventHandler(this.loadMostRecentlyUsed);
            // 
            // mRU4ToolStripMenuItem
            // 
            this.mRU4ToolStripMenuItem.Name = "mRU4ToolStripMenuItem";
            this.mRU4ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.mRU4ToolStripMenuItem.Text = "MRU4";
            this.mRU4ToolStripMenuItem.Click += new System.EventHandler(this.loadMostRecentlyUsed);
            // 
            // toolStripSeparatorMRU
            // 
            this.toolStripSeparatorMRU.Name = "toolStripSeparatorMRU";
            this.toolStripSeparatorMRU.Size = new System.Drawing.Size(182, 6);
            // 
            // toolStripMenuItemClose
            // 
            this.toolStripMenuItemClose.Name = "toolStripMenuItemClose";
            this.toolStripMenuItemClose.Size = new System.Drawing.Size(185, 22);
            this.toolStripMenuItemClose.Text = "Beenden";
            this.toolStripMenuItemClose.Click += new System.EventHandler(this.toolStripMenuItemClose_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "?";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // treeViewParam
            // 
            this.treeViewParam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewParam.BackColor = System.Drawing.Color.Black;
            this.treeViewParam.ForeColor = System.Drawing.Color.Yellow;
            this.treeViewParam.ImageIndex = 0;
            this.treeViewParam.ImageList = this.imageListTree;
            this.treeViewParam.Location = new System.Drawing.Point(12, 27);
            this.treeViewParam.Name = "treeViewParam";
            this.treeViewParam.SelectedImageIndex = 0;
            this.treeViewParam.ShowRootLines = false;
            this.treeViewParam.Size = new System.Drawing.Size(215, 452);
            this.treeViewParam.TabIndex = 1;
            this.treeViewParam.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewParam_AfterSelect);
            // 
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "database.png");
            this.imageListTree.Images.SetKeyName(1, "book-2.png");
            this.imageListTree.Images.SetKeyName(2, "document-new-2.png");
            this.imageListTree.Images.SetKeyName(3, "insert-table-2.png");
            this.imageListTree.Images.SetKeyName(4, "edit-user.png");
            this.imageListTree.Images.SetKeyName(5, "internet-chat-2.png");
            this.imageListTree.Images.SetKeyName(6, "file-doc.png");
            this.imageListTree.Images.SetKeyName(7, "page-edit.png");
            // 
            // upbutton
            // 
            this.upbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upbutton.Location = new System.Drawing.Point(233, 122);
            this.upbutton.Name = "upbutton";
            this.upbutton.Size = new System.Drawing.Size(27, 37);
            this.upbutton.TabIndex = 3;
            this.upbutton.Text = "↑";
            this.upbutton.UseVisualStyleBackColor = true;
            this.upbutton.Click += new System.EventHandler(this.button1_Click);
            this.upbutton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.button1_KeyPress);
            // 
            // dnbutton
            // 
            this.dnbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dnbutton.Location = new System.Drawing.Point(233, 165);
            this.dnbutton.Name = "dnbutton";
            this.dnbutton.Size = new System.Drawing.Size(27, 37);
            this.dnbutton.TabIndex = 3;
            this.dnbutton.Text = "↓";
            this.dnbutton.UseVisualStyleBackColor = true;
            this.dnbutton.Click += new System.EventHandler(this.dnbutton_Click);
            // 
            // listViewParam
            // 
            this.listViewParam.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewParam.AllowDrop = true;
            this.listViewParam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewParam.BackColor = System.Drawing.SystemColors.Window;
            this.listViewParam.FullRowSelect = true;
            this.listViewParam.HideSelection = false;
            this.listViewParam.HoverSelection = true;
            this.listViewParam.Location = new System.Drawing.Point(266, 27);
            this.listViewParam.Name = "listViewParam";
            this.listViewParam.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listViewParam.RightToLeftLayout = true;
            this.listViewParam.Size = new System.Drawing.Size(605, 449);
            this.listViewParam.TabIndex = 2;
            this.listViewParam.UseCompatibleStateImageBehavior = false;
            this.listViewParam.View = System.Windows.Forms.View.Details;
            this.listViewParam.SelectedIndexChanged += new System.EventHandler(this.listViewParam_SelectedIndexChanged);
            this.listViewParam.Click += new System.EventHandler(this.listViewParam_Click);
            this.listViewParam.DoubleClick += new System.EventHandler(this.listViewParam_DoubleClick);
            this.listViewParam.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewParam_KeyDown);
            this.listViewParam.Move += new System.EventHandler(this.listViewParam_Move);
            this.listViewParam.Resize += new System.EventHandler(this.KonfigEditorMainForm_Load);
            // 
            // KonfigEditorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 491);
            this.Controls.Add(this.dnbutton);
            this.Controls.Add(this.upbutton);
            this.Controls.Add(this.listViewParam);
            this.Controls.Add(this.treeViewParam);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "KonfigEditorMainForm";
            this.Text = "Parametrierungseditor Direkt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KonfigEditorMainForm_FormClosing);
            this.Load += new System.EventHandler(this.KonfigEditorMainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFileSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemClose;
        private System.Windows.Forms.TreeView treeViewParam;
        public HJS.ListViewNF listViewParam;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ImageList imageListTree;
        private System.Windows.Forms.ToolStripMenuItem exportNachCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem upgradeNachV10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upgradeNachV9ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mRU0ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mRU1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mRU2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mRU3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mRU4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorMRU;
        private System.Windows.Forms.ToolStripMenuItem speichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.Button upbutton;
        private System.Windows.Forms.Button dnbutton;
    }
}

