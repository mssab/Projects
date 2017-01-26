/*
 * Object: KonfigEditor.Forms.KonfigEditorMainForm
 * Description:Config editor main form
 * 
 * $LastChangedDate: 2015-03-05 13:27:15 +0100 (Do, 05 Mrz 2015) $
 * $LastChangedRevision: 100 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/KonfigEditor/Forms/KonfigEditorMainForm.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Windows.Forms;
using HJS.ECU;
using System.Collections.Generic;
using HJS.ECU.Parameter;

namespace KonfigEditor.Forms
{
    /// <summary>Main form</summary>
    public partial class KonfigEditorMainForm : Form
    {
        private HJS.ECU.Parameter.ParameterSet mParam;
        private string lastFilename = string.Empty;
        private bool isLastFileModified;
        private bool isSaveDenied;

        private enum selectParamNode
        {
            Root = 0,
            DataMaps,
            TaskSequence
        }

        /// <summary>Default constructor</summary>
        public KonfigEditorMainForm(string[] args)
        {
            InitializeComponent();

            initDragDrop();
            initMostRecentlyUsed();

            isLastFileModified = false;
            isSaveDenied = false;

            // Open via filetype open
            if (args != null)
            {
                if (args.Length > 0)
                {
                    openFile(args[0]);
                }
            }
        }

        #region Open and Close
        private void KonfigEditorMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isLastFileModified)
            {
                DialogResult res = MessageBox.Show(
                    "Die Datei wurde modifiziert!\r\nSoll das Programm ohne speichern geschlossen werden?",
                    "Programm ohne speichern schliessen?", MessageBoxButtons.YesNo);
                if (res == System.Windows.Forms.DialogResult.No)
                {
                    toolStripMenuItemFileSaveAs_Click(sender, EventArgs.Empty);
                }
            }
        }

        private void openFile(string Filename)
        {
            mParam = new HJS.ECU.Parameter.ParameterSet();
            if (mParam.Open(Filename))
            {
                treeViewParam_FillFromFile();
                lastFilename = Filename;
                isLastFileModified = false;
                speichernToolStripMenuItem.Enabled = true;
                isSaveDenied = false;
                this.Text = lastFilename + " - Parametrierungseditor Direkt";
                updateMostRecentlyUsed(Filename);
                initMostRecentlyUsed();
            }
            else
            {
                MessageBox.Show(mParam.LastError);
            }
        }

        public void setFileModified()
        {
            if (!isLastFileModified)
            {
                isLastFileModified = true;
                speichernToolStripMenuItem.Enabled = true;
                this.Text = lastFilename + "* - Parametrierungseditor Direkt";
            }
        }
        #endregion

        #region Copy and Paste
        private void listViewParam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
            {
                if (mParam != null)
                {
                    if (treeViewParam.SelectedNode.Name.StartsWith("datamap_"))
                    {
                        PasteDatamap();
                    }
                }
            }


            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show(" Willst du letzte Task auslöchen ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    mParam.RemoveTask();
                    setFileModified();
                    treeViewParam_FillFromFile(selectParamNode.TaskSequence);
                    paramShowList_TaskSequence();
                    int c = listViewParam.Items.Count - 1; c--;
                    //MessageBox.Show("Nummer der Tasks : " + c.ToString() + " ", "Info über Tasks", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }


        }





        private void PasteDatamap()
        {
            var fmt_csv = DataFormats.CommaSeparatedValue;
            string data_string = Clipboard.GetText();
            int datamap_pos = int.Parse(treeViewParam.SelectedNode.Name.Substring(8));
            Int16[] start = new Int16[3];
            Int16[] stepsize = new Int16[3];
            UInt16[] steps = new UInt16[3];

            if (string.IsNullOrEmpty(data_string))
            {
                MessageBox.Show("Zwischenspeicher ist leer!");
                return;
            }
            mParam.GetDatamapAxis(datamap_pos, out start[0], out stepsize[0], out steps[0], out start[1],
                out stepsize[1], out steps[1], out start[2], out stepsize[2], out steps[2]);

            data_string = data_string.Replace("\r", "");
            data_string = data_string.Remove(data_string.Length - 1);   // ignore last line break
            string[] rows = data_string.Split('\n');

            string[] cells = rows[0].Split('\t');

            if (rows.Length > 1)
            {
                // 2D
                if ((steps[0] + 1 == cells.Length) && (steps[1] + 1 == rows.Length))
                {
                    UInt16 x = 0, y = 0;
                    foreach (string s in rows)
                    {
                        cells = s.Split('\t');
                        foreach (string c in cells)
                        {
                            try
                            {
                                mParam.SetDatamapValue(datamap_pos, x, y, UInt16.Parse(c));
                            }
                            catch (Exception e1)
                            {
                                MessageBox.Show(e1.Message);
                                return;
                            }
                            x++;
                        }
                        x = 0;
                        y++;
                    }
                    setFileModified();
                    paramShowList_Datamap(datamap_pos);
                }
                else
                {
                    MessageBox.Show("Die Anzahl der Zellen passt nicht überein!");
                }
            }
            else
            {
                // 1D
                if ((steps[0] + 1 == cells.Length))
                {
                    UInt16 x = 0;
                    cells = rows[0].Split('\t');
                    foreach (string c in cells)
                    {
                        try
                        {
                            mParam.SetDatamapValue(datamap_pos, x, 0, UInt16.Parse(c));
                        }
                        catch (Exception e1)
                        {
                            MessageBox.Show(e1.Message);
                            return;
                        }
                        x++;
                    }
                    setFileModified();
                    paramShowList_Datamap(datamap_pos);
                }
                else
                {
                    MessageBox.Show("Die Anzahl der Zellen passt nicht überein!");
                }
            }
        }
        #endregion

        #region Drag and drop
        private void initDragDrop()
        {
            this.treeViewParam.AllowDrop = true;
            this.treeViewParam.DragDrop += new System.Windows.Forms.DragEventHandler(this.anyDragDrop);
            //this.treeViewParam.DragEnter += new System.Windows.Forms.DragEventHandler(this.anyDragEnter);
            this.listViewParam.AllowDrop = true;
            //this.listViewParam.DragDrop += new System.Windows.Forms.DragEventHandler(this.anyDragDrop);
            //this.listViewParam.DragEnter += new System.Windows.Forms.DragEventHandler(this.anyDragEnter);
            this.AllowDrop = true;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.anyDragDrop);
            // this.DragEnter += new System.Windows.Forms.DragEventHandler(this.anyDragEnter);
        }
        private void anyDragDrop(object sender, DragEventArgs e)
        {
            string[] _filenames = (string[])e.Data.GetData(DataFormats.FileDrop);
            openFile(_filenames[0]);
        }
        #endregion

        #region Most recently used
        private void initMostRecentlyUsed()
        {
            Properties.Settings.Default.Reload();
            bool showMru = false;
            if (String.IsNullOrEmpty(Properties.Settings.Default.MRU0))
            {
                mRU0ToolStripMenuItem.Visible = false;
            }
            else
            {
                showMru = true;
                mRU0ToolStripMenuItem.Text = Properties.Settings.Default.MRU0;
            }
            if (String.IsNullOrEmpty(Properties.Settings.Default.MRU1))
            {
                mRU1ToolStripMenuItem.Visible = false;
            }
            else
            {
                showMru = true;
                mRU1ToolStripMenuItem.Text = Properties.Settings.Default.MRU1;
            }
            if (String.IsNullOrEmpty(Properties.Settings.Default.MRU2))
            {
                mRU2ToolStripMenuItem.Visible = false;
            }
            else
            {
                showMru = true;
                mRU2ToolStripMenuItem.Text = Properties.Settings.Default.MRU2;
            }
            if (String.IsNullOrEmpty(Properties.Settings.Default.MRU3))
            {
                mRU3ToolStripMenuItem.Visible = false;
            }
            else
            {
                showMru = true;
                mRU3ToolStripMenuItem.Text = Properties.Settings.Default.MRU3;
            }
            if (String.IsNullOrEmpty(Properties.Settings.Default.MRU4))
            {
                mRU4ToolStripMenuItem.Visible = false;
            }
            else
            {
                showMru = true;
                mRU4ToolStripMenuItem.Text = Properties.Settings.Default.MRU4;
            }
            toolStripSeparatorMRU.Visible = showMru;
        }
        private void updateMostRecentlyUsed(string RecentFilename)
        {
            // already in mru?
            if (String.Compare(Properties.Settings.Default.MRU0, RecentFilename) == 0)
                return;
            if (String.Compare(Properties.Settings.Default.MRU1, RecentFilename) == 0)
            {
                string s = Properties.Settings.Default.MRU1;
                Properties.Settings.Default.MRU1 = Properties.Settings.Default.MRU0;
                Properties.Settings.Default.MRU0 = s;
                Properties.Settings.Default.Save();
                return;
            }
            if (String.Compare(Properties.Settings.Default.MRU2, RecentFilename) == 0)
            {
                string s = Properties.Settings.Default.MRU2;
                Properties.Settings.Default.MRU2 = Properties.Settings.Default.MRU1;
                Properties.Settings.Default.MRU1 = Properties.Settings.Default.MRU0;
                Properties.Settings.Default.MRU0 = s;
                Properties.Settings.Default.Save();
                return;
            }
            if (String.Compare(Properties.Settings.Default.MRU3, RecentFilename) == 0)
            {
                string s = Properties.Settings.Default.MRU3;
                Properties.Settings.Default.MRU3 = Properties.Settings.Default.MRU2;
                Properties.Settings.Default.MRU2 = Properties.Settings.Default.MRU1;
                Properties.Settings.Default.MRU1 = Properties.Settings.Default.MRU0;
                Properties.Settings.Default.MRU0 = s;
                Properties.Settings.Default.Save();
                return;
            }
            if (String.Compare(Properties.Settings.Default.MRU4, RecentFilename) == 0)
            {
                string s = Properties.Settings.Default.MRU4;
                Properties.Settings.Default.MRU4 = Properties.Settings.Default.MRU3;
                Properties.Settings.Default.MRU3 = Properties.Settings.Default.MRU2;
                Properties.Settings.Default.MRU2 = Properties.Settings.Default.MRU1;
                Properties.Settings.Default.MRU1 = Properties.Settings.Default.MRU0;
                Properties.Settings.Default.MRU0 = s;
                Properties.Settings.Default.Save();
                return;
            }
            // insert in free
            if (String.IsNullOrEmpty(Properties.Settings.Default.MRU0))
            {
                Properties.Settings.Default.MRU0 = RecentFilename;
                Properties.Settings.Default.Save();
                return;
            }
            if (String.IsNullOrEmpty(Properties.Settings.Default.MRU1))
            {
                Properties.Settings.Default.MRU1 = Properties.Settings.Default.MRU0;
                Properties.Settings.Default.MRU0 = RecentFilename;
                Properties.Settings.Default.Save();
                return;
            }
            if (String.IsNullOrEmpty(Properties.Settings.Default.MRU2))
            {
                Properties.Settings.Default.MRU2 = Properties.Settings.Default.MRU1;
                Properties.Settings.Default.MRU1 = Properties.Settings.Default.MRU0;
                Properties.Settings.Default.MRU0 = RecentFilename;
                Properties.Settings.Default.Save();
                return;
            }
            if (String.IsNullOrEmpty(Properties.Settings.Default.MRU3))
            {
                Properties.Settings.Default.MRU3 = Properties.Settings.Default.MRU2;
                Properties.Settings.Default.MRU2 = Properties.Settings.Default.MRU1;
                Properties.Settings.Default.MRU1 = Properties.Settings.Default.MRU0;
                Properties.Settings.Default.MRU0 = RecentFilename;
                Properties.Settings.Default.Save();
                return;
            }
            // shift
            Properties.Settings.Default.MRU4 = Properties.Settings.Default.MRU3;
            Properties.Settings.Default.MRU3 = Properties.Settings.Default.MRU2;
            Properties.Settings.Default.MRU2 = Properties.Settings.Default.MRU1;
            Properties.Settings.Default.MRU1 = Properties.Settings.Default.MRU0;
            Properties.Settings.Default.MRU0 = RecentFilename;
            Properties.Settings.Default.Save();
        }
        private void loadMostRecentlyUsed(object sender, EventArgs e)
        {
            openFile(((System.Windows.Forms.ToolStripItem)sender).Text);
        }
        #endregion

        #region Menu item clicks
        private void toolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoForm inf = new InfoForm();
            inf.ShowDialog();
        }

        private void toolStripMenuItemFileOpen_Click(object sender, EventArgs e)
        {
            mParam = new HJS.ECU.Parameter.ParameterSet();

            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.DefaultExt = "kbf";
                fd.Filter = "Konfigurations block file (*.kbf)|*.kbf";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    openFile(fd.FileName);
                }
            }
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] buffer;
            string uri = "http://menden22/svn/devel/electronic/dir_dbink_ecu/";
            string path = @"c:\temp\";
            string file = "43_0_9.kbf";
            string user, pass;

            DownloadForm df = new DownloadForm();

            if (df.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file = df.Filename;
                user = df.User;
                pass = df.Password;
            }
            else { return; }

            try
            {
                wc.Credentials = new System.Net.NetworkCredential(user, pass);
                buffer = wc.DownloadData(string.Format("{0}{1}", uri, file));

                // Create File from downloaded byte array
                System.IO.FileStream hF = System.IO.File.Create(string.Format("{0}{1}", path, file));
                try
                {
                    hF.Write(buffer, 0, buffer.Length);
                }
                catch (Exception Ex1)
                {
                    MessageBox.Show(string.Format("Error creating temporary file:\r\n{0}{1}\r\n{2}",
                        path, file, Ex1.Message));
                }
                finally
                {
                    hF.Close();
                }
            }
            catch (Exception Ex2)
            {
                MessageBox.Show(string.Format("Error downloading file:\r\n{0}{1}\r\n{2}",
                    uri, file, Ex2.Message));
            }
            openFile(path + file);
        }

        private void toolStripMenuItemFileSaveAs_Click(object sender, EventArgs e)
        {
            if (mParam == null) return;
            using (SaveFileDialog fd = new SaveFileDialog())
            {
                fd.DefaultExt = "kbf";
                fd.Filter = "Konfigurations block file (*.kbf)|*.kbf";
                fd.FileName = mParam.GenerateFilename();
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    updateMostRecentlyUsed(fd.FileName);
                    isLastFileModified = false;
                    speichernToolStripMenuItem.Enabled = false;
                    isSaveDenied = false;
                    this.Text = fd.FileName + " - Parametrierungseditor Direkt";
                    lastFilename = fd.FileName;
                    if (!mParam.Save(fd.FileName))
                    {
                        MessageBox.Show(mParam.LastError);
                    }
                }
            }
        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isSaveDenied)
            {
                MessageBox.Show("Die Versionsnummer wurde geändert. Die Datei darf nicht unter gleichem Namen gespeichert werden!");
            }
            else
            {
                isLastFileModified = false;
                speichernToolStripMenuItem.Enabled = false;
                isSaveDenied = false;
                this.Text = lastFilename + " - Parametrierungseditor Direkt";
                if (!mParam.Save(lastFilename))
                {
                    MessageBox.Show(mParam.LastError);
                }
            }
        }

        private void exportNachCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mParam == null) return;
            using (SaveFileDialog fd = new SaveFileDialog())
            {
                fd.DefaultExt = "csv";
                fd.Filter = "Comma separated values file (*.csv)|*.csv";
                fd.FileName = mParam.GenerateFilename();
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (!mParam.CsvExport(fd.FileName))
                    {
                        MessageBox.Show(mParam.LastError);
                    }
                }
            }
        }

        private void upgradeNachV9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mParam = new HJS.ECU.Parameter.ParameterSet();

            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.DefaultExt = "kbf";
                fd.Filter = "Konfigurations block file (*.kbf)|*.kbf";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (mParam.Upgrade(fd.FileName, 9))
                    {
                        treeViewParam_FillFromFile();
                        this.Text = "Unbenannt - Parametrierungseditor Direkt";
                    }
                    else
                    {
                        MessageBox.Show(mParam.LastError);
                    }
                }
            }
        }

        private void upgradeNachV10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mParam = new HJS.ECU.Parameter.ParameterSet();

            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.DefaultExt = "kbf";
                fd.Filter = "Konfigurations block file (*.kbf)|*.kbf";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (mParam.Upgrade(fd.FileName, 10))
                    {
                        treeViewParam_FillFromFile();
                        this.Text = "Unbenannt - Parametrierungseditor Direkt";
                    }
                    else
                    {
                        MessageBox.Show(mParam.LastError);
                    }
                }
            }
        }
        #endregion

        #region Tree control
        private void treeViewParam_FillFromFile(selectParamNode SelectNode = selectParamNode.Root)
        {
            int merker;
            treeViewParam.Nodes.Clear();

            // Fill Config items
            // fill task array
            TreeNode[] taskNodes = new TreeNode[mParam.GetTaskNumber()];
            for (int i = 0; i < taskNodes.Length; i++)
            {
                taskNodes[i] = new TreeNode(mParam.GetTaskIdentifier(i), 2, 7);
                taskNodes[i].Name = String.Format("task_{0}", i);
            }
            // generate static config items
            TreeNode nodeSequence = new TreeNode("Task Startreihenfolge", 2, 7);
            nodeSequence.Name = "sequence";
            TreeNode node2ndCan = new TreeNode("Zweiter CAN-Bus", 2, 7);
            node2ndCan.Name = "can2";
            TreeNode nodeErrors = new TreeNode("Fehlerkonfiguration", 6, 7);
            nodeErrors.Name = "errors";
            TreeNode nodeInitVal = new TreeNode("Lernwerte Initialisierung", 2, 7);
            nodeInitVal.Name = "init";
            TreeNode[] arrayCfg;
            if (mParam.SecondCanGetNumber() != 0)
            {
                arrayCfg = new TreeNode[] { nodeSequence, nodeInitVal, nodeErrors, node2ndCan };
                merker = 4;
            }
            else
            {
                arrayCfg = new TreeNode[] { nodeSequence, nodeInitVal, nodeErrors };
                merker = 3;
            }
            // Vier Eintraege in cfg um tasks erweitern
            Array.Resize(ref arrayCfg, taskNodes.Length + merker);
            Array.Copy(taskNodes, 0, arrayCfg, merker, taskNodes.Length);
            // Konfiguration tree node erstellen
            TreeNode nodeCFG = new TreeNode("Konfiguration", 1, 7, arrayCfg);
            nodeCFG.Name = "cfg";

            // Fill datamaps
            TreeNode[] arrayKf = new TreeNode[mParam.GetStoredMaps()];
            for (int i = 0; i < mParam.GetStoredMaps(); i++)
            {
                arrayKf[i] = new TreeNode(String.Format("ID={0}", mParam.GetDatamapIdentifier(i)), 3, 7);
                arrayKf[i].Name = String.Format("datamap_{0}", i);
            }
            TreeNode nodeKF = new TreeNode("Kennfelder", 1, 7, arrayKf);
            nodeKF.Name = "kf";

            // Fill languages
            TreeNode[] nodeLngMrw = new TreeNode[mParam.GetUsedLanguages()];
            TreeNode[] nodeLngErr = new TreeNode[mParam.GetUsedLanguages()];
            TreeNode[] nodeLngBeh = new TreeNode[mParam.GetUsedLanguages()];
            TreeNode[] arrayLng = new TreeNode[mParam.GetUsedLanguages()];
            for (int i = 0; i < mParam.GetUsedLanguages(); i++)
            {
                nodeLngMrw[i] = new TreeNode("Mess-/Rechenwerte", 2, 7);
                nodeLngMrw[i].Name = "LngMrw";
                nodeLngErr[i] = new TreeNode("Events/Fehler", 2, 7);
                nodeLngErr[i].Name = "LngErr";
                nodeLngBeh[i] = new TreeNode("Verhalten", 2, 7);
                nodeLngBeh[i].Name = "LngBeh";
                TreeNode[] arraySubLng = new TreeNode[] { nodeLngMrw[i], nodeLngErr[i], nodeLngBeh[i] };
                arrayLng[i] = new TreeNode(mParam.GetLanguageId(i), 5, 7, arraySubLng);
                arrayLng[i].Name = String.Format("lng_{0}", i);
            }
            TreeNode nodeLNG = new TreeNode("Sprachen", 1, 7, arrayLng);
            nodeLNG.Name = "language";

            TreeNode[] arrayRoot;
            arrayRoot = new TreeNode[] { nodeCFG, nodeKF, nodeLNG };
            if (mParam.HasReportBlock())
            {
                TreeNode nodeReport = new TreeNode("Report", 6, 7);
                nodeReport.Name = "report";
                Array.Resize(ref arrayRoot, arrayRoot.Length + 1);
                arrayRoot[arrayRoot.Length - 1] = nodeReport;
            }
            if (mParam.HasAuthorBlock())
            {
                TreeNode nodeAuthor = new TreeNode("Author", 4, 7);
                nodeAuthor.Name = "author";
                Array.Resize(ref arrayRoot, arrayRoot.Length + 1);
                arrayRoot[arrayRoot.Length - 1] = nodeAuthor;
            }
            TreeNode nodeRoot = new TreeNode("Parameter Datei", 0, 7, arrayRoot);
            nodeRoot.Name = "file";

            // Add nodes to control, expand and scroll top most item
            treeViewParam.Nodes.Add(nodeRoot);
            treeViewParam.ExpandAll();
            treeViewParam.Nodes[0].EnsureVisible();

            switch (SelectNode)
            {
                case selectParamNode.DataMaps:
                    treeViewParam.SelectedNode = nodeKF;
                    break;
                case selectParamNode.Root:
                default:
                    // Select and show first item
                    treeViewParam.SelectedNode = nodeRoot;
                    break;
            }
            paramShowList_CfgChecks();
        }

        private void treeViewParam_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string dummy = "0";
            int dummy_i = 0;
            switch (e.Node.Name)
            {
                case "init":
                    paramShowList_Init();
                    break;
                case "can2":
                    paramShowList_SecondCan();
                    break;
                case "cfg":
                    paramShowList_Config();
                    break;
                case "file":
                    paramShowList_CfgChecks();
                    break;
                case "author":
                    paramShowList_Author();
                    break;
                case "sequence":
                    paramShowList_TaskSequence();
                    break;
                case "errors":
                    paramShowList_ErrorConfig();
                    break;
                case "language":
                    paramShowList_Languages();
                    break;
                case "kf":
                    paramShowList_Datamaps();
                    break;
                case "LngMrw":
                    dummy = e.Node.Parent.Name.Replace("lng_", "");
                    dummy_i = Convert.ToInt32(dummy);
                    paramShowList_LngValues(dummy_i);
                    break;
                case "LngErr":
                    dummy = e.Node.Parent.Name.Replace("lng_", "");
                    dummy_i = Convert.ToInt32(dummy);
                    paramShowList_LngErrors(dummy_i);
                    break;
                case "LngBeh":
                    dummy = e.Node.Parent.Name.Replace("lng_", "");
                    dummy_i = Convert.ToInt32(dummy);
                    paramShowList_LngBehave(dummy_i);
                    break;
                case "report":
                    paramShowList_Report();
                    break;
                default:
                    if (e.Node.Name.StartsWith("task_"))
                    {
                        dummy = e.Node.Name.Replace("task_", "");
                        dummy_i = Convert.ToInt32(dummy);
                        paramShowList_Task(dummy_i);
                    }
                    else if (e.Node.Name.StartsWith("lng_"))
                    {
                        dummy = e.Node.Name.Replace("lng_", "");
                        dummy_i = Convert.ToInt32(dummy);
                        paramShowList_Language(dummy_i);
                    }
                    else if (e.Node.Name.StartsWith("datamap_"))
                    {
                        dummy = e.Node.Name.Replace("datamap_", "");
                        dummy_i = Convert.ToInt32(dummy);
                        paramShowList_Datamap(dummy_i);
                    }
                    else
                    {
                        // ignore all other nodes
                        listViewParam.Items.Clear();
                    }
                    break;
            }
        }
        #endregion

        #region List contents
        private void paramShowList_Config()
        {
            ListViewItem li = new ListViewItem();

            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();
            listViewParam.Columns.Add("Name", 200);
            listViewParam.Columns.Add("Wert", 200);
            listViewParam.Columns.Add("Einheit", 200);

            li.Tag = 0;
            li.Name = "Version";
            li.Text = "Version";
            li.SubItems.Add(mParam.GetConfigVersion());
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 1;
            li.Name = "Abwaertsversion";
            li.Text = "Abwärtsversion";
            li.SubItems.Add(mParam.GetConfigDownwardVersion().ToString());
            listViewParam.Items.Add((ListViewItem)li.Clone());
            li.SubItems.Clear();

            li.SubItems.Clear();
            li.Tag = 2;
            li.Name = "Softwaretyp";
            li.Text = "Softwaretyp";
            li.SubItems.Add(mParam.GetConfigSoftwareType().ToString());
            listViewParam.Items.Add((ListViewItem)li.Clone());
            li.SubItems.Clear();

            li.SubItems.Clear();
            li.Tag = 3;
            li.Name = "Passwortlevel";
            li.Text = "Passwortlevel";
            li.SubItems.Add(mParam.GetConfigPasswordLevel().ToString());
            listViewParam.Items.Add((ListViewItem)li.Clone());
            li.SubItems.Clear();

            li.SubItems.Clear();
            li.Tag = 4;
            li.Name = "Kompatibilitaet";
            li.Text = "Kompatibilität";
            li.SubItems.Add(mParam.GetConfigCompatibility().ToString());
            listViewParam.Items.Add((ListViewItem)li.Clone());
            li.SubItems.Clear();

            li.SubItems.Clear();
            li.Tag = 5;
            li.Name = "Taskanzahl";
            li.Text = "Taskanzahl";
            li.SubItems.Add(mParam.GetTaskNumber().ToString());
            listViewParam.Items.Add((ListViewItem)li.Clone());
            li.SubItems.Clear();

            li.SubItems.Clear();
            li.Tag = 6;
            li.Name = "Kennfelderanzahl";
            li.Text = "Kennfelderanzahl";
            li.SubItems.Add(mParam.GetUsedMaps().ToString());
            listViewParam.Items.Add((ListViewItem)li.Clone());
            li.SubItems.Clear();
        }

        private void paramShowList_Init()
        {
            ListViewItem li = new ListViewItem();

            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();
            listViewParam.Columns.Add("Name", 200);
            listViewParam.Columns.Add("Wert", 200);
            listViewParam.Columns.Add("Einheit", 200);

            for (int i = 0; i < mParam.InitValueGetNumber(); i++)
            {
                li.SubItems.Clear();
                li.Tag = i;
                li.Name = mParam.InitValueGetItemName(i);
                li.Text = mParam.InitValueGetItemName(i);
                li.SubItems.Add(mParam.InitValueGetItemValueString(i));
                listViewParam.Items.Add((ListViewItem)li.Clone());
            }


        }

        private void paramShowList_SecondCan()
        {
            ListViewItem li = new ListViewItem();

            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();
            listViewParam.Columns.Add("Name", 200);
            listViewParam.Columns.Add("Wert", 200);
            listViewParam.Columns.Add("Einheit", 200);

            for (int i = 0; i < mParam.SecondCanGetNumber(); i++)
            {
                li.SubItems.Clear();
                li.Tag = i;
                li.Name = mParam.SecondCanGetItemName(i);
                li.Text = mParam.SecondCanGetItemName(i);
                li.SubItems.Add(mParam.SecondCanGetItemValueString(i));
                listViewParam.Items.Add((ListViewItem)li.Clone());
            }
        }

        private void paramShowList_Task(int taskPosition)
        {
            ListViewItem li = new ListViewItem();

            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();
            listViewParam.Columns.Add("Name", 200);
            listViewParam.Columns.Add("Wert", 200);
            listViewParam.Columns.Add("Einheit", 200);

            for (int i = 0; i < mParam.GetTaskItemNumber(taskPosition); i++)
            {
                li.SubItems.Clear();
                li.Tag = i;
                li.Name = mParam.GetTaskItemName(taskPosition, i);
                li.Text = mParam.GetTaskItemName(taskPosition, i);
                li.SubItems.Add(mParam.GetTaskItemValueString(taskPosition, i));
                listViewParam.Items.Add((ListViewItem)li.Clone());
            }
        }

        private void paramShowList_TaskSequence()
        {
            ListViewItem li = new ListViewItem();
            int i = 0;
            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();
            listViewParam.Columns.Add("Name", 150);
            listViewParam.Columns.Add("ID", 50);
            listViewParam.Columns.Add("Offset", 50);
            listViewParam.Columns.Add("Nr.0", 50);
            listViewParam.Columns.Add("Nr.1", 50);
            listViewParam.Columns.Add("Nr.2", 50);
            listViewParam.Columns.Add("Nr.3", 50);
            listViewParam.Columns.Add("Nr.4", 50);
            listViewParam.Columns.Add("Nr.5", 50);
            listViewParam.Columns.Add("Nr.6", 50);

            for (i = 0; i < mParam.GetTaskNumber(); i++)
            {
                li.SubItems.Clear();
                li.Tag = i;
                li.Name = mParam.GetTaskIdentifier(i);
                li.Text = mParam.GetTaskIdentifier(i);
                li.SubItems.Add(mParam.GetTaskIdentifierValue(i).ToString());
                li.SubItems.Add(mParam.GetTaskOffset(i).ToString());
                li.SubItems.Add(mParam.GetTaskErrorString(i, 0));
                li.SubItems.Add(mParam.GetTaskErrorString(i, 1));
                li.SubItems.Add(mParam.GetTaskErrorString(i, 2));
                li.SubItems.Add(mParam.GetTaskErrorString(i, 3));
                li.SubItems.Add(mParam.GetTaskErrorString(i, 4));
                li.SubItems.Add(mParam.GetTaskErrorString(i, 5));
                li.SubItems.Add(mParam.GetTaskErrorString(i, 6));
                listViewParam.Items.Add((ListViewItem)li.Clone());
            }
            if (mParam.GetTaskNumber() < 25)
            {
                i++;
                li.SubItems.Clear();
                li.Tag = i;
                li.Name = mParam.GetDatamapIdentifier(i).ToString();
                li.Text = "[+]";
                listViewParam.Items.Add((ListViewItem)li.Clone());

                //i++;
                //li.SubItems.Clear();
                //li.Tag = i;
                //li.Name = mParam.GetDatamapIdentifier(i).ToString();
                //li.Text = "[-]";
                //listViewParam.Items.Add((ListViewItem)li.Clone());


            }
        }



        private void paramShowList_ErrorConfig()
        {
            ListViewItem li = new ListViewItem();
            String _text = "";
            int row = 0;
            int muster_offset = 0;
            byte _flags8 = 0;
            UInt16 _flags16 = 0;

            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();
            listViewParam.Columns.Add("Pos", 30);
            listViewParam.Columns.Add("Name", 110);
            listViewParam.Columns.Add("Ursprung", 200);
            listViewParam.Columns.Add("Aktivierung", 80);
            listViewParam.Columns.Add("Deaktivierung", 85);
            listViewParam.Columns.Add("Sichtbar", 75);
            listViewParam.Columns.Add("Wert", 40);
            listViewParam.Columns.Add("m", 25);
            listViewParam.Columns.Add("Verhalten", 230);
            listViewParam.Columns.Add("Umwelt1", 125);
            listViewParam.Columns.Add("Umwelt2", 125);
            listViewParam.Columns.Add("Umwelt3", 125);

            for (int _err_abs = 1; _err_abs < 64; _err_abs++)
            {
                _text = "";
                // Generierung ausloesende task stati
                for (int _task = 0; _task < mParam.GetTaskNumber(); _task++)
                {
                    for (int _task_err = 0; _task_err < 7; _task_err++)
                    {
                        if (mParam.GetTaskErrorNumber(_task, _task_err) == _err_abs)
                        {
                            _text += String.Format("{0}({1})", mParam.GetTaskIdentifier(_task).ToString(), _task_err);
                        }
                    }
                }
                if (_err_abs == 39) _text = "Hardcoded!";  // Fehler 39 erzwingen !
                muster_offset = mParam.GetTaskItemPosition(2, "Cfg.ucError[0]"); // suche anfang der fehlerkonfig
                if ((!String.IsNullOrEmpty(_text)) && (muster_offset > 0))
                {
                    li.SubItems.Clear();
                    li.Tag = row;
                    li.Name = _err_abs.ToString();
                    li.Text = _err_abs.ToString();
                    li.SubItems.Add(mParam.GetErrorName(0, _err_abs));
                    li.SubItems.Add(_text);
                    // Aktivierung
                    _flags8 = (byte)mParam.GetTaskItemValue(2, muster_offset + _err_abs);
                    _text = "Nie";
                    if ((_flags8 & 0x90) == 0x10) _text = "Event";
                    if ((_flags8 & 0x90) == 0x80) _text = "Zeit";
                    if ((_flags8 & 0x90) == 0x90) _text = "Event, Zeit";
                    li.SubItems.Add(_text);
                    // Deaktivierung
                    _text = "Nie";
                    if ((_flags8 & 0x60) == 0x20) _text = "Dekrement";
                    if ((_flags8 & 0x60) == 0x40) _text = "Deaktivierung";
                    if ((_flags8 & 0x60) == 0x60) _text = "Löschen";
                    li.SubItems.Add(_text);
                    // Sichtbarkeit
                    li.SubItems.Add(mParam.IsEventHidden(0, _err_abs) ? "Versteckt" : "");
                    // Aktivierungsschwelle
                    li.SubItems.Add(mParam.GetTaskItemValue(2, muster_offset + 64 + (_flags8 & 0x0F)).ToString());
                    // Verhaltensmaske
                    li.SubItems.Add((_flags8 & 0x0F).ToString());
                    // Verhalten
                    _flags16 = (UInt16)mParam.GetTaskItemValue(2, muster_offset + 64 + 16 + (_flags8 & 0x0F));
                    _text = "";
                    for (UInt16 _verhalten = 0; _verhalten < 16; _verhalten++)
                    {
                        if ((_flags16 & (1 << _verhalten)) == (1 << _verhalten))
                        {
                            _text += mParam.GetBehaveName(0, _verhalten);
                            _text += " ";
                        }
                    }
                    li.SubItems.Add(_text);
                    // Umweltdaten
                    li.SubItems.Add(mParam.GetTaskItemValueString(2, muster_offset + 96 + _err_abs));
                    li.SubItems.Add(mParam.GetTaskItemValueString(2, muster_offset + 160 + _err_abs));
                    li.SubItems.Add(mParam.GetTaskItemValueString(2, muster_offset + 224 + _err_abs));
                    listViewParam.Items.Add((ListViewItem)li.Clone());
                    row++;
                }
            }
        }

        private void paramShowList_Author()
        {
            ListViewItem li = new ListViewItem();

            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();
            listViewParam.Columns.Add("Name", 150);
            listViewParam.Columns.Add("Wert", 250);

            li.SubItems.Clear();
            li.Tag = 0;
            li.Name = "Author date";
            li.Text = "Datum";
            //DateTime crt = new DateTime();
            //crt = DateTime.FromFileTime(mAuthor.CreationTime);
            //li.SubItems.Add(String.Format("{0} {1}",crt.ToShortDateString(), crt.ToShortTimeString()));
            //string.format("{0:x}", decValue);
            li.SubItems.Add(String.Format("{0} {1}", mParam.GetAuthorCreationTime().ToShortDateString(),
                //li.SubItems.Add(String.Format("{0:x}", mParam.GetAuthorCreationTime().ToShortDateString(),
            mParam.GetAuthorCreationTime().ToShortTimeString()));
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 1;
            li.Name = "Author computer";
            li.Text = "ComputerName";
            li.SubItems.Add(mParam.GetAuthorComputername());
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 2;
            li.Name = "Author user";
            li.Text = "UserName";
            li.SubItems.Add(mParam.GetAuthorUsername());
            listViewParam.Items.Add((ListViewItem)li.Clone());
        }

        private void paramShowList_CfgChecks()
        {
            String r;
            UInt32 i1 = 0;
            UInt32 i2 = 0;
            bool b;
            int task = 0;
            ListViewItem li = new ListViewItem();

            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();
            listViewParam.Columns.Add("Test", 200);
            listViewParam.Columns.Add("Ergebnis", 250);
            listViewParam.Columns.Add("", 50);


            li.SubItems.Clear();
            li.Tag = 0;
            li.Name = "Task Reihenfolge";
            li.Text = "Task Reihenfolge";
            b = mParam.CheckTaskSequence(out r);
            li.SubItems.Add(r);
            li.SubItems.Add(b ? "ok" : "fail");
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 1;
            li.Name = "Task Offset Prüfung";
            li.Text = "Task Offset Prüfung";
            b = mParam.CheckTaskOffset(out r, out i1);
            li.SubItems.Add(r);
            li.SubItems.Add(b ? "ok" : "fail");
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 2;
            li.Name = "Task Fehler Prüfung";
            li.Text = "Task Fehler Prüfung";
            r = mParam.CheckTaskErrors();
            if (String.IsNullOrEmpty(r))
            {
                li.SubItems.Add("Taskfehler plausibel");
                li.SubItems.Add("ok");
            }
            else
            {
                li.SubItems.Add(r);
                li.SubItems.Add("warn");

            }
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 3;
            li.Name = "Speicherauslastung Config";
            li.Text = "Speicherauslastung Config";
            i2 = mParam.ConfigDataSize();
            b = i2 > i1;
            r = String.Format("{0} / {1} = {2:F}%", i1, i2, 100 * ((float)i1 / (float)i2));
            li.SubItems.Add(r);
            li.SubItems.Add(b ? "ok" : "fail");
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 4;
            li.Name = "Task Checksummen Prüfung";
            li.Text = "Task Checksummen Prüfung";
            if (mParam.CheckTaskChecksums() == HJS.ECU.TaskIdentifier.taskInvalid)
            {
                li.SubItems.Add("Übereinstimmung");
                li.SubItems.Add("ok");
            }
            else
            {
                li.SubItems.Add("Erste falsche Task: " + mParam.CheckTaskChecksums().ToString());
                li.SubItems.Add("warn");
            }
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 5;
            li.Name = "Kennfeld Id CFGvsKF Prüfung";
            li.Text = "Kennfeld Id CFGvsKF Prüfung";
            if (mParam.CheckDatamapIDs(out r))
            {
                li.SubItems.Add(r);
                li.SubItems.Add("ok");
            }
            else
            {
                li.SubItems.Add(r);
                li.SubItems.Add("warn");
            }
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 6;
            li.Name = "CAN Baudraten Prüfung";
            li.Text = "CAN Baudraten Prüfung";
            if (mParam.CheckTaskBaudrates())
            {
                li.SubItems.Add("Cfg enthält eindeutige Baudrate");
                li.SubItems.Add("ok");
            }
            else
            {
                li.SubItems.Add("Cfg enthält unterschiedliche Baudraten");
                li.SubItems.Add("fail");
            }
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 7;
            li.Name = "Taskheader Plausibilitäts Prüfung";
            li.Text = "Taskheader Plausibilitäts Prüfung";
            if (mParam.CheckTaskHeader(out task))
            {
                li.SubItems.Add("Taskheader plausibel");
                li.SubItems.Add("ok");
            }
            else
            {
                li.SubItems.Add("Taskheader unplausibel: " + mParam.GetTaskIdentifier(task));
                li.SubItems.Add("fail");
            }
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 8;
            li.Name = "Plausibilität Taskitems";
            li.Text = "Plausibilität Taskitems";
            r = mParam.CheckTaskItems();
            if (String.IsNullOrEmpty(r))
            {
                li.SubItems.Add("Taskitems plausibel");
                li.SubItems.Add("ok");
            }
            else
            {
                li.SubItems.Add(r);
                li.SubItems.Add("warn");
            }
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 9;
            li.Name = "Prüfung Stacksumme";
            li.Text = "Prüfung Stacksumme";
            r = mParam.CheckTaskStackSizes();
            if (String.IsNullOrEmpty(r))
            {
                li.SubItems.Add("Stacksumme passend");
                li.SubItems.Add("ok");
            }
            else
            {
                li.SubItems.Add(r);
                li.SubItems.Add("Warn");
            }
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 10;
            li.Name = "Version Kennfelder";
            li.Text = "Version Kennfelder";
            if (mParam.CheckDatamapVersion(out r))
            {
                li.SubItems.Add(r);
                li.SubItems.Add("ok");
            }
            else
            {
                li.SubItems.Add(r);
                li.SubItems.Add("Warn");
            }
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 11;
            li.Name = "Speicherauslastung Kennfelder";
            li.Text = "Speicherauslastung Kennfelder";
            r = String.Format("{0}/4090", mParam.GetUsedSize(out i1, out i2));
            r += String.Format(" gap={0} ovr={1}", i1, i2);
            li.SubItems.Add(r);
            if ((i1 <= 4090) && (i2 != 0))
            {
                li.SubItems.Add("Warn");
            }
            else
            {
                li.SubItems.Add("ok");
            }
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 12;
            li.Name = "Flag Plausibilität Sprachen";
            li.Text = "Flag Plausibilität Sprachen";
            r = mParam.CheckLanguageSignedFlags();
            if (String.IsNullOrEmpty(r))
            {
                li.SubItems.Add("Übereinstimmung");
                li.SubItems.Add("ok");
            }
            else
            {
                li.SubItems.Add(r);
                li.SubItems.Add("warn");
            }
            listViewParam.Items.Add((ListViewItem)li.Clone());

            li.SubItems.Clear();
            li.Tag = 13;
            li.Name = "Flag Vergleich zwischen Sprachen";
            li.Text = "Flag Vergleich zwischen Sprachen";
            r = mParam.CheckLanguageFlags();
            if (String.IsNullOrEmpty(r))
            {
                li.SubItems.Add("Übereinstimmung");
                li.SubItems.Add("ok");
            }
            else
            {
                li.SubItems.Add(r);
                li.SubItems.Add("warn");
            }
            listViewParam.Items.Add((ListViewItem)li.Clone());

            for (int i = 0; i < mParam.GetUsedLanguages(); i++)
            {
                li.SubItems.Clear();
                li.Tag = 14 + i;
                li.Name = String.Format("Speicherauslastung Sprache {0}", i);
                li.Text = String.Format("Speicherauslastung {0}", mParam.GetLanguageId(i));
                i1 = mParam.GetUsedLanguagesBytes(i);
                i2 = 4090;
                li.SubItems.Add(String.Format("{0} / {1} = {2:F}%", i1, i2, 100 * ((float)i1 / (float)i2)));
                if (i1 <= i2)
                {
                    li.SubItems.Add("ok");
                }
                else
                {
                    li.SubItems.Add("fail");
                }
                li.SubItems.Add(r);
                listViewParam.Items.Add((ListViewItem)li.Clone());

                li.SubItems.Clear();
                li.Tag = 1;
                li.Name = String.Format("Anzahl Einzeltexte Sprache {0}", i);
                li.Text = String.Format("Anzahl Einzeltexte {0}", mParam.GetLanguageId(i));
                i1 = mParam.GetNumberOfUsedValues(i);
                HJS.ECU.Firmware fw = new HJS.ECU.Firmware(mParam.GetConfigCompatibility());
                i2 = fw.GetValueNumber();
                li.SubItems.Add(String.Format("{0} / {1}; {2} / {3}; {4} / {5}",
                    i1, i2, mParam.GetNumberOfUsedErrors(i), 64, mParam.GetNumberOfUsedBehaves(i), 16));
                if ((i1 == i2) &&
                    (mParam.GetNumberOfUsedErrors(i) == 64) &&
                    (mParam.GetNumberOfUsedBehaves(i) == 16))
                {
                    li.SubItems.Add("ok");
                }
                else
                {
                    li.SubItems.Add("fail");
                }
                listViewParam.Items.Add((ListViewItem)li.Clone());
            }
        }

        private void paramShowList_Languages()
        {
            ListViewItem li = new ListViewItem();

            listViewParam.Items.Clear();

            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();
            listViewParam.Columns.Add("Name", 100);
            listViewParam.Columns.Add("Size", 100);
            for (int i = 0; i < mParam.GetUsedLanguages(); i++)
            {
                li.SubItems.Clear();
                li.Tag = i;
                li.Name = mParam.GetLanguageId(i);
                li.Text = mParam.GetLanguageId(i);
                li.SubItems.Add(String.Format("{0} / {1}", mParam.GetUsedLanguagesBytes(i), 4090));
                listViewParam.Items.Add((ListViewItem)li.Clone());
            }
        }

        private void paramShowList_Language(int position)
        {
            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();
        }

        private void paramShowList_LngValues(int position)
        {
            ListViewItem li = new ListViewItem();
            HJS.ECU.Firmware fw = new HJS.ECU.Firmware(mParam.GetConfigCompatibility());
            String _text = "";

            listViewParam.Items.Clear();

            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();
            listViewParam.Columns.Add("intName", 200);
            listViewParam.Columns.Add("Name", 175);
            listViewParam.Columns.Add("lcd", 30);
            listViewParam.Columns.Add("hid", 30);
            listViewParam.Columns.Add("rw", 30);
            listViewParam.Columns.Add("vz", 30);
            listViewParam.Columns.Add("hex", 30);
            listViewParam.Columns.Add("dez", 30);
            listViewParam.Columns.Add("Einheit", 60);
            listViewParam.Columns.Add("A", 35);
            listViewParam.Columns.Add("B", 35);
            listViewParam.Columns.Add("C", 35);
            listViewParam.Columns.Add("Einh.Alt", 70);

            for (int i = 0; i < mParam.GetNumberOfUsedValues(position); i++)
            {
                li.SubItems.Clear();
                li.Tag = i;
                _text = fw.GetMessWertString(i);

                li.Name = _text;
                li.Text = _text;
                li.SubItems.Add(mParam.GetValueName(position, i));
                li.SubItems.Add(mParam.IsValueDisplayed(position, i) ? "1" : "0");
                li.SubItems.Add(mParam.IsValueHidden(position, i) ? "1" : "0");
                li.SubItems.Add(mParam.IsValueGroup(position, i) ? "1" : "0");
                li.SubItems.Add(mParam.IsValueSigned(position, i) ? "1" : "0");
                li.SubItems.Add(mParam.IsValueHexadecimal(position, i) ? "1" : "0");
                li.SubItems.Add(mParam.GetValueDecimals(position, i).ToString());
                li.SubItems.Add(mParam.GetValueUnit(position, i, false));
                if (mParam.GetValueFaktor(position, i) == 0)
                {
                    li.SubItems.Add("");
                }
                else
                {
                    li.SubItems.Add(mParam.GetValueFaktor(position, i).ToString());
                }
                if (mParam.GetValueDivisor(position, i) == 1)
                {
                    li.SubItems.Add("");
                }
                else
                {
                    li.SubItems.Add(mParam.GetValueDivisor(position, i).ToString());
                }
                li.SubItems.Add(mParam.GetValueOffset(position, i));
                li.SubItems.Add(mParam.GetValueUnit(position, i, true));
                listViewParam.Items.Add((ListViewItem)li.Clone());
            }
        }

        private void paramShowList_LngErrors(int position)
        {
            ListViewItem li = new ListViewItem();

            listViewParam.Items.Clear();

            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();
            listViewParam.Columns.Add("int.", 75);
            listViewParam.Columns.Add("Name", 200);
            listViewParam.Columns.Add("lcd", 30);
            listViewParam.Columns.Add("hid", 30);
            listViewParam.Columns.Add("ev", 30);
            listViewParam.Columns.Add("blu", 30);

            for (int i = 0; i < mParam.GetNumberOfUsedErrors(position); i++)
            {
                li.SubItems.Clear();
                li.Tag = i;
                li.Name = String.Format("event_{0}", i);
                li.Text = String.Format("Event {0}", i);
                li.SubItems.Add(mParam.GetErrorName(position, i));
                li.SubItems.Add(mParam.IsEventDisplayed(position, i) ? "1" : "0");
                li.SubItems.Add(mParam.IsEventHidden(position, i) ? "1" : "0");
                li.SubItems.Add(mParam.IsEventOrError(position, i) ? "1" : "0");
                li.SubItems.Add(mParam.IsEventBlueLed(position, i) ? "1" : "0");
                listViewParam.Items.Add((ListViewItem)li.Clone());
            }
        }

        private void paramShowList_LngBehave(int position)
        {
            ListViewItem li = new ListViewItem();

            listViewParam.Items.Clear();

            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();
            listViewParam.Columns.Add("int.", 75);
            listViewParam.Columns.Add("Name", 200);

            for (int i = 0; i < mParam.GetNumberOfUsedBehaves(position); i++)
            {
                li.SubItems.Clear();
                li.Tag = i;
                li.Name = String.Format("behave_{0}", i);
                li.Text = String.Format("Behave {0}", i);
                li.SubItems.Add(mParam.GetBehaveName(position, i));
                listViewParam.Items.Add((ListViewItem)li.Clone());
            }
        }

        private void paramShowList_Datamaps()
        {
            ListViewItem li = new ListViewItem();
            int i = 0;
            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();

            listViewParam.Columns.Add("Identifier", 75);
            listViewParam.Columns.Add("Type", 150);
            listViewParam.Columns.Add("Dimension", 75);
            listViewParam.Columns.Add("Addresse", 75);
            listViewParam.Columns.Add("Start", 75);
            listViewParam.Columns.Add("Stepsize", 75);
            listViewParam.Columns.Add("Steps", 75);
            for (i = 0; i < mParam.GetStoredMaps(); i++)
            {
                li.SubItems.Clear();
                li.Tag = i;
                li.Name = mParam.GetDatamapIdentifier(i).ToString();
                li.Text = mParam.GetDatamapIdentifier(i).ToString();
                li.SubItems.Add(((HJS.ECU.Firmware.KennfeldTyp)mParam.GetDatamapType(i)).ToString());
                li.SubItems.Add(mParam.GetDatamapDimension(i).ToString());
                li.SubItems.Add(mParam.GetDatamapOffset(i).ToString());
                li.SubItems.Add(mParam.GetDatamapStartString(i));
                li.SubItems.Add(mParam.GetDatamapStepsizeString(i));
                li.SubItems.Add(mParam.GetDatamapStepsString(i));
                listViewParam.Items.Add((ListViewItem)li.Clone());
            }
            if (mParam.GetStoredMaps() < 16)
            {
                i++;
                li.SubItems.Clear();
                li.Tag = i;
                li.Name = mParam.GetDatamapIdentifier(i).ToString();
                li.Text = "[+]";
                listViewParam.Items.Add((ListViewItem)li.Clone());
            }
        }

        private void paramShowList_Datamap(int position)
        {
            ListViewItem li = new ListViewItem();
            Int16[] start = new Int16[3];
            Int16[] stepsize = new Int16[3];
            UInt16[] steps = new UInt16[3];

            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();

            mParam.GetDatamapAxis(position, out start[0], out stepsize[0], out steps[0], out start[1],
                out stepsize[1], out steps[1], out start[2], out stepsize[2], out steps[2]);
            if (mParam.GetDatamapDimension(position) > 0)
            {
                //2D
                listViewParam.Columns.Add("", 45); // Spalte fuer Y-Achsen-Werte
                for (UInt16 x = 0; x < steps[0] + 1; x++)
                {
                    listViewParam.Columns.Add((start[0] + (x * stepsize[0])).ToString(), 45);
                }
                for (UInt16 y = 0; y < steps[1] + 1; y++)
                {
                    li.SubItems.Clear();
                    li.Tag = 0;
                    li.Name = (start[1] + (y * stepsize[1])).ToString();
                    li.Text = (start[1] + (y * stepsize[1])).ToString();
                    for (UInt16 x = 0; x < steps[0] + 1; x++)
                    {
                        li.SubItems.Add(mParam.GetDatamapValue(position, x, y).ToString());
                    }
                    listViewParam.Items.Add((ListViewItem)li.Clone());
                }
            }
            else
            {
                //1D
                for (UInt16 x = 0; x < steps[0] + 1; x++)
                {
                    listViewParam.Columns.Add((start[0] + (x * stepsize[0])).ToString(), 45);
                }
                li.SubItems.Clear();
                li.Tag = 0;
                li.Name = mParam.GetDatamapValue(position, 0, 0).ToString();
                li.Text = mParam.GetDatamapValue(position, 0, 0).ToString();
                for (UInt16 x = 1; x < steps[0] + 1; x++)
                {
                    li.SubItems.Add(mParam.GetDatamapValue(position, x, 0).ToString());
                }
                listViewParam.Items.Add((ListViewItem)li.Clone());
            }
        }

        /// <summary>Show report</summary>
        public void paramShowList_Report()
        {
            ListViewItem li = new ListViewItem();

            listViewParam.Items.Clear();
            listViewParam.Columns.Clear();

            listViewParam.Columns.Add("Name", 150);
            listViewParam.Columns.Add("Value", 100);
            for (int i = 0; i < mParam.GetReportItemNumber(); i++)
            {
                li.SubItems.Clear();
                li.Tag = i;
                li.Name = mParam.GetReportItemName(i);
                li.Text = mParam.GetReportItemName(i);
                li.SubItems.Add(mParam.GetReportItemValue(i));
                listViewParam.Items.Add((ListViewItem)li.Clone());
            }
        }
        #endregion

        #region List control
        private void listViewParam_DoubleClick(object sender, EventArgs e)
        {

            if (sender != null)
            {
                int _last_top = ((ListView)sender).TopItem.Index;
                int _ItemNo = (int)((ListView)sender).SelectedItems[0].Tag;
                string _ItemName = ((ListView)sender).SelectedItems[0].Text;
                if (treeViewParam.SelectedNode.Name == "init")
                {
                    paramEditListItem_Init(_ItemNo, _ItemName);
                }
                if (treeViewParam.SelectedNode.Name.StartsWith("task_"))
                {
                    paramEditListItem_Task(int.Parse(treeViewParam.SelectedNode.Name.Substring(5)), _ItemNo, _ItemName);
                }
                if (treeViewParam.SelectedNode.Name == "can2")
                {
                    paramEditListItem_Can2(_ItemNo, _ItemName);
                }
                if ((treeViewParam.SelectedNode.Name == "cfg")
                    && (_ItemNo == 0))
                {
                    EditVersionForm evf = new EditVersionForm(mParam.GetConfigVersion());
                    evf.Width = 370;
                    evf.Height = 180;
                    if (evf.ShowDialog() == DialogResult.OK)
                    {
                        if (!mParam.SetConfigVersion(evf.newVersion))
                        {
                            MessageBox.Show("Versionsnummer konnte nicht gesetzt werden.\r\nKompatibiliät kann nicht geändert werden!");
                        }
                        else
                        {
                            setFileModified();
                            isSaveDenied = true;
                        }
                        paramShowList_Config();
                    }
                }
                if ((treeViewParam.SelectedNode.Name == "cfg")
                    && (_ItemNo == 3))
                {
                    EditValueForm ef = new EditValueForm("Konfiguration", " Passwortlevel", mParam.GetConfigPasswordLevel(), null);
                    if (ef.ShowDialog() == DialogResult.OK)
                    {
                        if (!mParam.SetConfigPasswordLevel((byte)ef.NewValue))
                        {
                            MessageBox.Show("Passwortlevel konnte nicht gesetzt werden.\r\nPasswortlevel muss zwischen 0 und 3 liegen!");
                        }
                        else
                        {
                            setFileModified();
                        }
                        paramShowList_Config();
                    }
                }
                if (treeViewParam.SelectedNode.Name == "sequence")
                {
                    if (_ItemName.Equals("[+]"))
                    {
                        // New task
                        EditValueForm ef = new EditValueForm("Konfiguration", " Neuer Task", 0, mParam.GetTaskIdentifierArray());
                        if (ef.ShowDialog() == DialogResult.OK)
                        {
                            if (!mParam.AddTask((byte)ef.NewValue))
                            {
                                MessageBox.Show("Task konnte nicht zugefügt werden, da bereits enthalten!");
                            }
                            else
                            {
                                setFileModified();
                                treeViewParam_FillFromFile(selectParamNode.TaskSequence);
                                paramShowList_TaskSequence();
                            }
                        }
                    }
                    else
                    {
                        // edit task
                        paramEditListItem_TaskErrors(_ItemNo, _ItemName);
                    }
                }
                if (treeViewParam.SelectedNode.Name == "LngMrw")
                {
                    paramEditListItem_TextValue(_ItemNo, _ItemName);
                }
                if (treeViewParam.SelectedNode.Name == "LngBeh")
                {
                    paramEditListItem_TextBehave(_ItemNo, _ItemName);
                }
                if (treeViewParam.SelectedNode.Name == "LngErr")
                {
                    paramEditListItem_TextError(_ItemNo, _ItemName);
                }
                if (treeViewParam.SelectedNode.Name.StartsWith("datamap_"))
                {
                    paramEditListItem_DataMapContent(int.Parse(treeViewParam.SelectedNode.Name.Substring(8)), ((ListView)sender).SelectedItems[0].Index);
                }
                if (treeViewParam.SelectedNode.Name == "kf")
                {
                    if (_ItemName.Equals("[+]"))
                    {
                        // New Data map

                        mParam.AddDataMap();
                        setFileModified();
                        treeViewParam_FillFromFile(selectParamNode.DataMaps);
                        paramShowList_Datamaps();
                    }
                    else
                    {
                        // Edit data map
                        UInt32 id = mParam.GetDatamapIdentifier(_ItemNo);
                        HJS.ECU.Firmware.KennfeldTyp ty = (HJS.ECU.Firmware.KennfeldTyp)mParam.GetDatamapType(_ItemNo);
                        byte di = mParam.GetDatamapDimension(_ItemNo);
                        Int16 XStart, YStart, ZStart;
                        Int16 XSize, YSize, ZSize;
                        UInt16 XSteps, YSteps, ZSteps;
                        mParam.GetDatamapAxis(_ItemNo, out XStart, out XSize, out XSteps, out YStart, out YSize, out YSteps, out ZStart, out ZSize, out ZSteps);
                        EditDatamapForm ef = new EditDatamapForm(id, ty, di, XStart, XSize, XSteps, YStart, YSize, YSteps, ZStart, ZSize, ZSteps,
                            mParam.GetEnumerationArray(HJS.ECU.Parameter.TaskDataType.type_kf_type_8));
                        if (ef.ShowDialog() == DialogResult.OK)
                        {
                            mParam.SetDatamapId(_ItemNo, ef.Identifier, ef.KfType, ef.Dimension);
                            mParam.SetDatamapAxis(_ItemNo, ef.X_Start, ef.X_Size, ef.X_Steps, ef.Y_Start, ef.Y_Size, ef.Y_Steps, ef.Z_Start, ef.Z_Size, ef.Z_Steps);
                            setFileModified();
                            treeViewParam_FillFromFile(selectParamNode.DataMaps);
                            paramShowList_Datamaps();
                        }
                    }
                }
                ((ListView)sender).Items[_last_top].Selected = true;
                ((ListView)sender).TopItem = ((ListView)sender).SelectedItems[0];
                ((ListView)sender).Items[_last_top].Selected = false;
            }

            else
            {

                MessageBox.Show("Keine Auswahl");
            }

        }


        public void paramEditListItem_Init(int ItemNo, string ItemName)
        {
            HJS.ECU.Parameter.TaskDataType _ItemType = mParam.InitValueGetItemType(ItemNo);
            Array _ItemEnumValues = mParam.GetEnumerationArray(_ItemType);
            double _Value;
            if (_ItemEnumValues != null)
                _Value = mParam.GetEnumerationIndex(mParam.InitValueGetItemValue(ItemNo), _ItemType, _ItemEnumValues);
            else
                _Value = mParam.InitValueGetItemValue(ItemNo);

            using (EditValueForm ed = new EditValueForm("Initial Value", ItemName, _Value, _ItemEnumValues))
            {
                if (ed.ShowDialog() == DialogResult.OK)
                {
                    if (mParam.InitValueSetItemValue(ItemNo, ed.NewValue))
                    {
                        setFileModified();
                        mParam.GenerateChecksum();
                        //refresh
                        listViewParam.Items.Clear();
                        ListViewItem li = new ListViewItem();
                        for (int i = 0; i < mParam.InitValueGetNumber(); i++)
                        {
                            li.SubItems.Clear();
                            li.Tag = i;
                            li.Name = mParam.InitValueGetItemName(i);
                            li.Text = mParam.InitValueGetItemName(i);
                            li.SubItems.Add(mParam.InitValueGetItemValueString(i));
                            listViewParam.Items.Add((ListViewItem)li.Clone());
                        }
                    }
                    else MessageBox.Show("Wert konnte nicht gesetzt werden!");
                }
            }
        }


        private void paramEditListItem_Can2(int ItemNo, string ItemName)
        {
            HJS.ECU.Parameter.TaskDataType _ItemType = mParam.SecondCanGetItemType(ItemNo);
            Array _ItemEnumValues = mParam.GetEnumerationArray(_ItemType);
            double _Value;
            if (_ItemEnumValues != null)
                _Value = mParam.GetEnumerationIndex(mParam.SecondCanGetItemValue(ItemNo), _ItemType, _ItemEnumValues);
            else
                _Value = mParam.SecondCanGetItemValue(ItemNo);

            using (EditValueForm ed = new EditValueForm("Second CAN", ItemName, _Value, _ItemEnumValues))
            {
                if (ed.ShowDialog() == DialogResult.OK)
                {
                    if (mParam.SecondCanSetItemValue(ItemNo, ed.NewValue))
                    {
                        setFileModified();
                        mParam.GenerateChecksum();
                        //refresh
                        listViewParam.Items.Clear();
                        ListViewItem li = new ListViewItem();
                        for (int i = 0; i < mParam.SecondCanGetNumber(); i++)
                        {
                            li.SubItems.Clear();
                            li.Tag = i;
                            li.Name = mParam.SecondCanGetItemName(i);
                            li.Text = mParam.SecondCanGetItemName(i);
                            li.SubItems.Add(mParam.SecondCanGetItemValueString(i));
                            listViewParam.Items.Add((ListViewItem)li.Clone());
                        }
                    }
                    else MessageBox.Show("Wert konnte nicht gesetzt werden!");
                }
            }
        }

        public void paramEditListItem_Task(int TaskNo, int ItemNo, string ItemName)
        {
            HJS.ECU.Parameter.TaskDataType _ItemType = mParam.GetTaskItemType(TaskNo, ItemNo);
            Array _ItemEnumValues = mParam.GetEnumerationArray(_ItemType);
            double _Value;
            if (_ItemEnumValues != null)
                _Value = mParam.GetEnumerationIndex(mParam.GetTaskItemValue(TaskNo, ItemNo), _ItemType, _ItemEnumValues);
            else
                _Value = mParam.GetTaskItemValue(TaskNo, ItemNo);

            using (EditValueForm ed = new EditValueForm(mParam.GetTaskIdentifier(TaskNo), ItemName, _Value, _ItemEnumValues))
            {

                if (ed.ShowDialog() == DialogResult.OK)
                {
                    if (mParam.SetTaskItemValue(TaskNo, ItemNo, ed.NewValue))
                    {
                        setFileModified();
                        mParam.GenerateChecksum();

                        //refresh
                        listViewParam.Items.Clear();
                        ListViewItem li = new ListViewItem();
                        for (int i = 0; i < mParam.GetTaskItemNumber(TaskNo); i++)
                        {
                            li.SubItems.Clear();
                            li.Tag = i;
                            li.Name = mParam.GetTaskItemName(TaskNo, i);
                            li.Text = mParam.GetTaskItemName(TaskNo, i);
                            li.SubItems.Add(mParam.GetTaskItemValueString(TaskNo, i));
                            listViewParam.Items.Add((ListViewItem)li.Clone());
                        }
                    }
                    else MessageBox.Show("Wert konnte nicht gesetzt werden!");
                }

            }

        }


        static void Swap(IList<int> list, int indexA, int indexB)
        {
            int tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }


        private void paramEditListItem_TaskErrors(int ItemNo, string ItemName)
        {
            
            EditTaskErrorsForm etef = new EditTaskErrorsForm(ItemName);
            if (ItemName=="taskPostDiagnose")
            {
                etef.panel1.Visible = true;
                etef.Width = 530;
                etef.Height = 320;
            }
            etef.err0 = mParam.GetTaskErrorNumber(ItemNo, 0);
            etef.ring0 = mParam.GetTaskErrorRingFlag(ItemNo, 0);
            etef.reserve1 = mParam.GetTaskErrorResFlag(ItemNo, 0);
            etef.err1 = mParam.GetTaskErrorNumber(ItemNo, 1);
            etef.ring1 = mParam.GetTaskErrorRingFlag(ItemNo, 1);
            etef.reserve2 = mParam.GetTaskErrorResFlag(ItemNo, 1);
            etef.err2 = mParam.GetTaskErrorNumber(ItemNo, 2);
            etef.ring2 = mParam.GetTaskErrorRingFlag(ItemNo, 2);
            etef.reserve3 = mParam.GetTaskErrorResFlag(ItemNo, 2);
            etef.err3 = mParam.GetTaskErrorNumber(ItemNo, 3);
            etef.ring3 = mParam.GetTaskErrorRingFlag(ItemNo, 3);
            etef.reserve4 = mParam.GetTaskErrorResFlag(ItemNo, 3);
            etef.err4 = mParam.GetTaskErrorNumber(ItemNo, 4);
            etef.ring4 = mParam.GetTaskErrorRingFlag(ItemNo, 4);
            etef.reserve5 = mParam.GetTaskErrorResFlag(ItemNo, 4);
            etef.err5 = mParam.GetTaskErrorNumber(ItemNo, 5);
            etef.ring5 = mParam.GetTaskErrorRingFlag(ItemNo, 5);
            etef.reserve6 = mParam.GetTaskErrorResFlag(ItemNo, 5);
            etef.err6 = mParam.GetTaskErrorNumber(ItemNo, 6);
            etef.ring6 = mParam.GetTaskErrorRingFlag(ItemNo, 6);
            etef.reserve7 = mParam.GetTaskErrorResFlag(ItemNo, 6);
            etef.RefreshValues();
            
           

            if (etef.ShowDialog() == DialogResult.OK)
            {

                setFileModified();
                mParam.SetTaskErrorNumber(ItemNo, 0, (byte)etef.err0);
                mParam.SetTaskErrorRingFlag(ItemNo, 0, etef.ring0);
                mParam.SetTaskErrorResFlag(ItemNo, 0, etef.reserve1);
                mParam.SetTaskErrorNumber(ItemNo, 1, (byte)etef.err1);
                mParam.SetTaskErrorRingFlag(ItemNo, 1, etef.ring1);
                mParam.SetTaskErrorResFlag(ItemNo, 1, etef.reserve2);
                mParam.SetTaskErrorNumber(ItemNo, 2, (byte)etef.err2);
                mParam.SetTaskErrorRingFlag(ItemNo, 2, etef.ring2);
                mParam.SetTaskErrorResFlag(ItemNo, 2, etef.reserve3);
                mParam.SetTaskErrorNumber(ItemNo, 3, (byte)etef.err3);
                mParam.SetTaskErrorRingFlag(ItemNo, 3, etef.ring3);
                mParam.SetTaskErrorResFlag(ItemNo, 3, etef.reserve4);
                mParam.SetTaskErrorNumber(ItemNo, 4, (byte)etef.err4);
                mParam.SetTaskErrorRingFlag(ItemNo, 4, etef.ring4);
                mParam.SetTaskErrorResFlag(ItemNo, 4, etef.reserve5);
                mParam.SetTaskErrorNumber(ItemNo, 5, (byte)etef.err5);
                mParam.SetTaskErrorRingFlag(ItemNo, 5, etef.ring5);
                mParam.SetTaskErrorResFlag(ItemNo, 5, etef.reserve6);
                mParam.SetTaskErrorNumber(ItemNo, 6, (byte)etef.err6);
                mParam.SetTaskErrorRingFlag(ItemNo, 6, etef.ring6);
                mParam.SetTaskErrorResFlag(ItemNo, 6, etef.reserve7);
                paramShowList_TaskSequence();

            }
             if (ItemName=="taskPostDiagnose")
            {
                
              
              mParam.SetTaskItemValue(2, 306,Double.Parse(etef.behaveDisplayR.Text));

            }
            

        }

        private void paramEditListItem_TextValue(int ItemNo, string ItemName)
        {
            EditTextValueForm etvf = new EditTextValueForm(mParam, ItemNo);
            if (etvf.ShowDialog() == DialogResult.OK)
            {
                setFileModified();
                int i = int.Parse(treeViewParam.SelectedNode.Parent.Name.Replace("lng_", ""));
                paramShowList_LngValues(i);
            }
        }

        private void paramEditListItem_TextBehave(int ItemNo, string ItemName)
        {
            EditTextBehaveForm etbf = new EditTextBehaveForm(mParam, ItemNo);
            if (etbf.ShowDialog() == DialogResult.OK)
            {
                setFileModified();
                int i = int.Parse(treeViewParam.SelectedNode.Parent.Name.Replace("lng_", ""));
                paramShowList_LngBehave(i);
            }
        }

        private void paramEditListItem_TextError(int ItemNo, string ItemName)
        {
            EditTextErrorForm etef = new EditTextErrorForm(mParam, ItemNo);
            if (etef.ShowDialog() == DialogResult.OK)
            {
                setFileModified();
                int i = int.Parse(treeViewParam.SelectedNode.Parent.Name.Replace("lng_", ""));
                paramShowList_LngErrors(i);
            }
        }

        private void paramEditListItem_DataMapContent(int datamapPosition, int SelectedRow)
        {
            EditDatamapContentForm edcf = new EditDatamapContentForm(mParam, datamapPosition, SelectedRow);
            if (edcf.ShowDialog() == DialogResult.OK)
            {
                setFileModified();
                paramShowList_Datamap(datamapPosition);
            }
        }



        public void checkboxBehave()
        {
            ListViewItem li = new ListViewItem();            
            double _Value=3;
           // li.SubItems.Add(mParam.GetTaskItemValueString(2,306));
            li.SubItems.Add(mParam.SetTaskItemValue(2, 306, _Value).ToString());
            listViewParam.Items.Add(li).Clone();
            
            //if (groupName == "" && itemName == "bfMaskAQSave")
            //{
                

            //}

            

        }
        #endregion

        private void KonfigEditorMainForm_Load(object sender, EventArgs e)
        {
            

        }

        private void listViewParam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void Swapinlistbox(int indexA, int indexB)
        {
            ListViewItem item = listViewParam.Items[indexA];
            listViewParam.Items.Remove(item);
            listViewParam.Items.Insert(indexB, item);


        }

        private void button1_Click(object sender, EventArgs e)
        {


            MoveListViewItems(listViewParam, MoveDirection.Up);
        }

        public void Swap(int a, int b)
        {
            int tmp = a;
            a = b;
            b = tmp;

        }

        private enum MoveDirection { Up = -1, Down = 1 };

        private void MoveListViewItems(ListView listViewParam, MoveDirection direction)
        {
            int dir = (int)direction;

            foreach (ListViewItem lvi in listViewParam.SelectedItems)
            {
                int index = lvi.Index + dir;
                if (index >= listViewParam.Items.Count)
                    index = 0;
                else if (index < 0)
                    index = listViewParam.Items.Count + dir;

                listViewParam.Items.RemoveAt(lvi.Index);
                listViewParam.Items.Insert(index, lvi);
            }
        }


        private void listViewParam_Click(object sender, EventArgs e)
        {

        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void listViewParam_Move(object sender, EventArgs e)
        {

        }

        private void dnbutton_Click(object sender, EventArgs e)
        {

            MoveListViewItems(listViewParam, MoveDirection.Down);

        }



    }
}

