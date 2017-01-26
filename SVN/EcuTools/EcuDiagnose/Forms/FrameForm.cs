/*
 * Object: EcuDiagnose.Forms.FrameForm
 * Description: ECU diagnostics form
 * 
 * $LastChangedDate: 2016-06-07 09:55:50 +0200 (Di, 07 Jun 2016) $
 * $LastChangedRevision: 102 $
 * $LastChangedBy: jdr $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/EcuDiagnose/Forms/FrameForm.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Windows.Forms;

namespace EcuDiagnose.Forms
{
    /// <summary>Main form object</summary>
    public partial class FrameForm : Form
    {
        private Controller mController;

        private bool mConnecteded;

        private bool mShowOccuredErrorsOnly;

        private bool mShow32BitValues;

        /// <summary>Accessors to controller object</summary>
        public Controller TheController
        {
            set { mController = value; }
        }

        /// <summary>Constructor</summary>
        public FrameForm()
        {
            InitializeComponent();
            ReportStatus("Bereit");
            toolStripStatusLabelVersion.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //
            listViewValuesSelection.Columns.Add("Variable", 160);
            listViewValues.Columns.Add("Variable", 150);
            listViewValues.Columns.Add("Größe");
            listViewValues.Columns.Add("Einheit");
            //
            listViewErrors.Columns.Add("ID", 25);
            listViewErrors.Columns.Add("Fehler/Event", 40);
            listViewErrors.Columns.Add("Zustand", 60);
            listViewErrors.Columns.Add("Fehlername", 150);
            listViewErrors.Columns.Add("erstes Auftr.", 110);
            listViewErrors.Columns.Add("letztes Auftr.", 110);
            listViewErrors.Columns.Add("Auftr. bis", 110);
            listViewErrors.Columns.Add("Anzahl", 55);
            //
            listViewBehaves.Columns.Add("ID", 25);
            listViewBehaves.Columns.Add("Verhalten", 150);
            listViewBehaves.Columns.Add("Zustand", 60);
            //
            listViewVolatiles.Columns.Add("", 250);
            listViewEmpiricalGroups.Columns.Add("Task", 160);
            listViewEmpiricalValues.Columns.Add("Lernwert", 250);
            //
            listViewDtcStack.Columns.Add("DTC Nummer", 80);
            listViewDtcStack.Columns.Add("SPN", 80);
            listViewDtcStack.Columns.Add("FMI", 80);
            listViewDtcStack.Columns.Add("Warmups", 80);
            listViewDtcStack.Columns.Add("Anzahl", 80);
            listViewDtcStack.Columns.Add("Pending", 80);
            listViewDtcStack.Columns.Add("Active", 80);
            listViewDtcStack.Columns.Add("PrevActive", 80);
            //
            comboBoxTankSignal.DataSource = Enum.GetValues(typeof(HJS.ECU.Firmware.TankSignal));
            comboBoxTankGain.DataSource = Enum.GetValues(typeof(HJS.ECU.Firmware.PsocGain));
            //
            mShowOccuredErrorsOnly = true;
            mShow32BitValues = true;
            mConnecteded = false;
            EnableConnectionRequiredControls();
            //
            timerLesen.Interval = Properties.Settings.Default.Timer;
            timerLesen.Stop();
            timerLesen.Enabled = false;
            //-------------------------------------------------------------------------------------------
            ToolTip myToolTip = new ToolTip();
            myToolTip.AutoPopDelay = 5000;
            myToolTip.InitialDelay = 1000;
            myToolTip.ReshowDelay = 500;
            myToolTip.ShowAlways = true;
            myToolTip.IsBalloon = true;
            //myToolTip.ToolTipTitle = "Hinweis!";
            myToolTip.BackColor = System.Drawing.Color.Red;   //Funktioniert nicht
            myToolTip.ForeColor = System.Drawing.Color.Green; //Funktioniert nicht
            //
            myToolTip.SetToolTip(this.buttonSetTime, "Das Ändern der aktuellen Uhrzeit im Steuergerät.");


            //-------------------------------------------------------------------------------------------




            // uncomment following line to hide all controls
            // that are not required for low level access
            // HideControlsForLowAccess();

        }

        private void EnableConnectionRequiredControls()
        {
            // ...ToolStripMenuItem
            lesenToolStripMenuItem.Enabled = mConnecteded;
            akquisitionAuslesenToolStripMenuItem.Enabled = mConnecteded;
            fehlerLoeschenToolStripMenuItem.Enabled = mConnecteded;
            timerStartenToolStripMenuItem.Enabled = mConnecteded;
            fehlerringAuslesenToolStripMenuItem.Enabled = mConnecteded;
            istwerteSpeichernToolStripMenuItem.Enabled = mConnecteded;
            fehlerstackSpeichernToolStripMenuItem.Enabled = mConnecteded;
            lernwerteSpeichernToolStripMenuItem.Enabled = mConnecteded;
            allesAuslesenToolStripMenuItem.Enabled = mConnecteded;
            // button...
            buttonSetTime.Enabled = mConnecteded;
            buttonSetProductionData.Enabled = mConnecteded;
            buttonMasterReset.Enabled = mConnecteded;
            buttonSetAlarms.Enabled = mConnecteded;
            buttonAdditivReset.Enabled = mConnecteded;
            buttonFilterGereinigt.Enabled = mConnecteded;
            buttonDosieren.Enabled = mConnecteded;
            buttonRegenerieren.Enabled = mConnecteded;
            buttonSetRpmFactor.Enabled = mConnecteded;
            buttonKomSend.Enabled = mConnecteded;
            buttonTankSignalSet.Enabled = mConnecteded;
            buttonTankGainSet.Enabled = mConnecteded;
            buttonAgrUmschalten.Enabled = mConnecteded;
            buttonReadSbc.Enabled = mConnecteded;
            // Toolbar
            toolStripButtonStartHeater.Enabled = mConnecteded;
            toolStripButtonRead.Enabled = mConnecteded;
            toolStripButtonTimer.Enabled = mConnecteded;
            toolStripButtonMasterreset.Enabled = mConnecteded;

            if (mController != null)
            {
                // version depending
                if (mController.ProtocolVersion > 14)
                {
                    parametersatzAufspielenToolStripMenuItem.Enabled = mConnecteded;
                    if (mController.CompatibilityNumber > 9)
                    {
                        parametersatzLesenToolStripMenuItem.Enabled = mConnecteded;
                    }
                    else
                    {
                        parametersatzLesenToolStripMenuItem.Enabled = false;
                    }
                    konfigurationAufspielenToolStripMenuItem.Enabled = false;
                    kennfelderAufspielenToolStripMenuItem.Enabled = false;
                    sprachenAufspielenToolStripMenuItem.Enabled = false;
                    lernwerteSpeichernToolStripMenuItem.Enabled = mConnecteded;
                    buttonRebootReconfigReset.Enabled = mConnecteded;
                    buttonCalibrierDiff.Enabled = mConnecteded;
                    buttonCalibrierRef.Enabled = mConnecteded;
                    buttonReboot.Enabled = mConnecteded;
                    buttonRebootNotCfg.Enabled = mConnecteded;
                    toolStripButtonReset.Enabled = mConnecteded;
                    toolStripButtonRebootNot.Enabled = mConnecteded;
                    toolStripButtonReconfigReboot.Enabled = mConnecteded;
                }
                else
                {
                    parametersatzAufspielenToolStripMenuItem.Enabled = false;
                    parametersatzLesenToolStripMenuItem.Enabled = false;
                    konfigurationAufspielenToolStripMenuItem.Enabled = mConnecteded;
                    kennfelderAufspielenToolStripMenuItem.Enabled = mConnecteded;
                    sprachenAufspielenToolStripMenuItem.Enabled = mConnecteded;
                    lernwerteSpeichernToolStripMenuItem.Enabled = false;
                    buttonRebootReconfigReset.Enabled = false;
                    buttonCalibrierDiff.Enabled = false;
                    buttonCalibrierRef.Enabled = false;
                    buttonReboot.Enabled = false;
                    buttonRebootNotCfg.Enabled = false;
                    toolStripButtonReset.Enabled = false;
                    toolStripButtonRebootNot.Enabled = false;
                    toolStripButtonReconfigReboot.Enabled = false;
                }
            }
            else
            {
                parametersatzAufspielenToolStripMenuItem.Enabled = false;
                parametersatzLesenToolStripMenuItem.Enabled = false;
                konfigurationAufspielenToolStripMenuItem.Enabled = false;
                kennfelderAufspielenToolStripMenuItem.Enabled = false;
                sprachenAufspielenToolStripMenuItem.Enabled = false;
                lernwerteSpeichernToolStripMenuItem.Enabled = false;
                buttonRebootReconfigReset.Enabled = false;
                buttonCalibrierDiff.Enabled = false;
                buttonCalibrierRef.Enabled = false;
                buttonReboot.Enabled = false;
                buttonRebootNotCfg.Enabled = false;
                toolStripButtonReset.Enabled = false;
                toolStripButtonRebootNot.Enabled = false;
                toolStripButtonReconfigReboot.Enabled = false;
            }
        }

        private void HideControlsForLowAccess()
        {
            // toolbar
            toolStripButtonStartHeater.Visible = false;
            toolStripButtonOptions.Visible = false;
            toolStripSeparator7.Visible = false;
            toolStripButtonStartHeater.Visible = false;
            toolStripSeparator8.Visible = false;
            toolStripButtonMasterreset.Visible = false;
            toolStripButtonReconfigReboot.Visible = false;
            toolStripButtonRebootNot.Visible = false;
            toolStripButtonReset.Visible = false;
            toolStripSeparator6.Visible = false;
            toolStripButtonTimer.Visible = false;
            // menu
            dateiToolStripMenuItem.Visible = false;
            timerStartenToolStripMenuItem.Visible = false;
            toolStripSeparator5.Visible = false;
            // tabctrl
            tabControlMain.TabPages.Remove(tabPageDirectOrder);
            tabControlMain.TabPages.Remove(tabPageMaintainance);
            tabControlMain.TabPages.Remove(tabPageDtc);
            tabControlMain.TabPages.Remove(tabPageEmpirical);
            tabControlMain.TabPages.Remove(tabPageRtc);
            tabControlMain.TabPages.Remove(tabPageBehave);
            tabControlMain.TabPages.Remove(tabPageErrors);
            tabControlMain.TabPages.Remove(tabPageValues);
            tabControlMain.TabPages.Remove(tabPageUpdatable);
            // page overview controls
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            // form size
            //this.Size = this.MinimumSize;
        }

        /// <summary>Delegated function to report progress to GUI</summary>
        /// <param name="percentage">Progress</param>
        public void ReportProgress(int percentage)
        {
            toolStripProgressBarTotal.Value = percentage;
        }

        /// <summary>Delegated function to report status to GUI</summary>
        /// <param name="message">Status message</param>
        public void ReportStatus(string message)
        {
            toolStripStatusLabel1.Text = message;
            Refresh();
        }

        private void timerLesen_Tick(object sender, EventArgs e)
        {
            lesenToolStripMenuItem_Click(sender, e);
        }

        #region Menu Events
        private void verbindungStartenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string _port = String.Format(@"COM{0}", Properties.Settings.Default.Port);
            Cursor = Cursors.WaitCursor;

            if (mConnecteded)
            {
                //disc
                if (mController.Disconnect())
                {
                    // stop timer
                    if (timerStartenToolStripMenuItem.Checked)
                    {
                        timerStartenToolStripMenuItem_Click(null, null);
                    }
                    // stop connection
                    mConnecteded = false;
                    EnableConnectionRequiredControls();
                    verbindungStartenToolStripMenuItem.Text = "Verbindung starten";
                    ReportStatus("Verbindung beendet.");
                    // clear controls
                    textOverviewVersions.Text = "";
                    textOverviewInfotext.Text = "";
                    textBoxDtcInfo.Text = "";
                    textBoxDtcFreezeFrame.Text = "";
                    listViewValuesSelection.Items.Clear();
                    listViewValues.Items.Clear();
                    listViewErrors.Items.Clear();
                    listViewBehaves.Items.Clear();
                    listViewVolatiles.Items.Clear();
                    listViewEmpiricalGroups.Items.Clear();
                    listViewEmpiricalValues.Items.Clear();
                    listViewDtcStack.Items.Clear();
                    toolStripStatusLabelConnected.Image = Properties.Resources.empty;
                    toolStripButtonStart.Enabled = true;
                    toolStripButtonStop.Enabled = false;
                }
            }
            else
            {
                if (mController.Connect(_port,
                    (byte)Properties.Settings.Default.Language,
                    Properties.Settings.Default.LocalTime))
                {
                    textOverviewVersions.Text = mController.GetVersionsString();
                    mConnecteded = true;
                    EnableConnectionRequiredControls();
                    verbindungStartenToolStripMenuItem.Text = "Verbindung beenden";
                    refreshValueSelection();
                    buttonValuesSelectUnhidden_Click(null, null);
                    refreshEmpiricalSelection();
                    toolStripStatusLabelConnected.Image = Properties.Resources.connected;
                    toolStripButtonStart.Enabled = false;
                    toolStripButtonStop.Enabled = true;
                }
            }
            Cursor = Cursors.Default;
        }

        private void lesenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool StopTimer = true;
            if (tabControlMain.SelectedIndex == 0)
            {
                // Overview
                textOverviewInfotext.Text = mController.GetInfoText();
                textBoxLastTimeStamp.Text = mController.GetLastTimeStamp();
                textBoxSerialNumber.Text = mController.GetSerialNumber();
                textBoxTempOffset.Text = mController.GetEcuTemperatureOffset().ToString();
                listViewLanguageAvailable.Items.Clear();
                System.Collections.Generic.List<string> lngs = mController.ReadAvailableLanguages();
                if (lngs != null)
                {
                    foreach (string s in lngs)
                    {
                        listViewLanguageAvailable.Items.Add(s);
                    }
                }
                listViewRunningTasks.Items.Clear();
                System.Collections.Generic.List<string> tsks = mController.ReadActiveTaskList();
                if (tsks != null)
                {
                    foreach (string s in tsks)
                    {
                        listViewRunningTasks.Items.Add(s);
                    }
                }

                ReportStatus(String.Format("Letzte lesen um {0}", mController.GetLastTimeStamp()));
            }
            if (tabControlMain.SelectedIndex == 1)
            {
                // Istwerte lesen
                if (refreshValues())
                {
                    StopTimer = false;
                }
            }
            if ((tabControlMain.SelectedIndex == 2) || (tabControlMain.SelectedIndex == 3))
            {
                // fehler events und verhalten lesen
                if (refreshErrors())
                {
                    StopTimer = false;
                }
            }
            if (tabControlMain.SelectedIndex == 4)
            {
                // RTC lesen
                if (refreshRtc())
                {
                    StopTimer = false;
                }
            }
            if (tabControlMain.SelectedIndex == 5)
            {
                // Lernwerte lesen
                if (refreshEpiricals())
                {
                    StopTimer = false;
                }
            }
            if (tabControlMain.SelectedIndex == 6)
            {
                // Updatable Lernwerte lesen
                if (refreshUpdatables())
                {
                    StopTimer = false;
                }
            }
            if (tabControlMain.SelectedIndex == 7)
            {
                // DTC lesen
                if (refreshDtcStack())
                {
                    StopTimer = false;
                }
            }
            if (timerStartenToolStripMenuItem.Checked && StopTimer)
            {
                timerStartenToolStripMenuItem.Checked = false;
                timerLesen.Stop();
                timerLesen.Enabled = false;
                toolStripStatusLabelTimer.Image = Properties.Resources.empty;
                toolStripButtonTimer.Checked = false;
            }
        }

        private void akquisitionAuslesenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool TimerRunning = timerStartenToolStripMenuItem.Checked;
            if (TimerRunning)
            {
                // Stop timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
            using (SaveFileDialog fd = new SaveFileDialog())
            {
                fd.FileName = String.Format("{0}_acqi", mController.GetRawFileName());
                fd.DefaultExt = "xml";
                fd.Filter = "extended markup language file (*.xml)|*.xml|comma seperated value file (*.csv)|*.csv";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    // progress bar
                    mController.ReadAcquisition(fd.FileName, fd.FilterIndex);
                    Cursor = Cursors.Default;
                }
            }
            if (TimerRunning)
            {
                // Restart timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
        }

        private void parametersatzAufspielenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool TimerRunning = timerStartenToolStripMenuItem.Checked;
            if (TimerRunning)
            {
                // Stop timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.DefaultExt = "kbf";
                fd.Filter = "Konfigurations block file (*.kbf)|*.kbf";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ReportStatus(String.Format("Uploading file {0}", fd.FileName));
                    Cursor = Cursors.WaitCursor;
                    // progress bar
                    if (mController.UploadConfig(fd.FileName))
                    {
                        // Disconnect
                        verbindungStartenToolStripMenuItem_Click(this, EventArgs.Empty);
                        // Connect
                        verbindungStartenToolStripMenuItem_Click(this, EventArgs.Empty);
                    }
                    Cursor = Cursors.Default;
                }
            }
            if (TimerRunning)
            {
                // Restart timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timerStartenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timerStartenToolStripMenuItem.Checked)
            {
                timerStartenToolStripMenuItem.Checked = false;
                timerLesen.Stop();
                timerLesen.Enabled = false;
                toolStripStatusLabelTimer.Image = Properties.Resources.empty;
                toolStripButtonTimer.Checked = false;
            }
            else
            {
                timerStartenToolStripMenuItem.Checked = true;
                timerLesen.Enabled = false;
                timerLesen.Start();
                toolStripStatusLabelTimer.Image = Properties.Resources.clock;
            }
        }

        private void fehlerringAuslesenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool TimerRunning = timerStartenToolStripMenuItem.Checked;
            if (TimerRunning)
            {
                // Stop timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
            using (SaveFileDialog fd = new SaveFileDialog())
            {
                fd.FileName = String.Format("{0}_err_ring", mController.GetRawFileName());
                fd.DefaultExt = "xml";
                fd.Filter = "extended markup language file (*.xml)|*.xml|comma seperated value file (*.csv)|*.csv";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ReportStatus(String.Format("Start reading file {0}", fd.FileName));
                    Cursor = Cursors.WaitCursor;
                    // progress bar
                    mController.ReadErrorRing(fd.FileName, fd.FilterIndex);
                    Cursor = Cursors.Default;
                }
            }
            if (TimerRunning)
            {
                // Restart timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
        }


        private void alleZeigenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mShowOccuredErrorsOnly)
            {
                mShowOccuredErrorsOnly = false;
                alleZeigenToolStripMenuItem.Checked = true;
            }
            else
            {
                mShowOccuredErrorsOnly = true;
                alleZeigenToolStripMenuItem.Checked = false;
            }
        }

        private void istwerteSpeichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool TimerRunning = timerStartenToolStripMenuItem.Checked;
            if (TimerRunning)
            {
                // Stop timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
            using (SaveFileDialog fd = new SaveFileDialog())
            {
                fd.FileName = String.Format("{0}_actual", mController.GetRawFileName());
                fd.DefaultExt = "xml";
                fd.Filter = "extended markup language file (*.xml)|*.xml|comma seperated value file (*.csv)|*.csv";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    // progress bar
                    mController.ActualValuesSaveFile(fd.FileName, fd.FilterIndex);
                    Cursor = Cursors.Default;
                }
            }
            if (TimerRunning)
            {
                // Restart timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
        }

        private void fehlerstackSpeichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool TimerRunning = timerStartenToolStripMenuItem.Checked;
            if (TimerRunning)
            {
                // Stop timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
            using (SaveFileDialog fd = new SaveFileDialog())
            {
                fd.FileName = String.Format("{0}_err_stack", mController.GetRawFileName());
                fd.DefaultExt = "xml";
                fd.Filter = "extended markup language file (*.xml)|*.xml|comma seperated value file (*.csv)|*.csv";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    // progress bar
                    mController.ErrorBehaveSaveFile(fd.FileName, fd.FilterIndex);
                    Cursor = Cursors.Default;
                }
            }
            if (TimerRunning)
            {
                // Restart timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
        }

        private void lernwerteSpeichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool TimerRunning = timerStartenToolStripMenuItem.Checked;
            if (TimerRunning)
            {
                // Stop timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
            using (SaveFileDialog fd = new SaveFileDialog())
            {
                fd.FileName = String.Format("{0}_empirical", mController.GetRawFileName());
                fd.DefaultExt = "xml";
                fd.Filter = "extended markup language file (*.xml)|*.xml|comma seperated value file (*.csv)|*.csv";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    // progress bar
                    mController.EmpiricalSaveFile(fd.FileName, fd.FilterIndex);
                    Cursor = Cursors.Default;
                }
            }
            if (TimerRunning)
            {
                // Restart timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
        }

        private void allesAuslesenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool TimerRunning = timerStartenToolStripMenuItem.Checked;
            if (TimerRunning)
            {
                // Stop timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
            string _filename = mController.GetRawFileName();
            using (FolderBrowserDialog fd = new FolderBrowserDialog())
            {
                fd.Description = String.Format("Ordner zum Speichern der Dateien {0}_*.xml", mController.GetRawFileName());
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    // progress bar
                    _filename = String.Format("{0}\\{1}.dbf", fd.SelectedPath, mController.GetRawFileName());
                    mController.SaveReadDataFile(_filename);

                    mController.SaveAllFiles(fd.SelectedPath);
                    // while ecu busy
                    Cursor = Cursors.Default;
                }
            }
            if (TimerRunning)
            {
                // Restart timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm of = new OptionsForm();
            DialogResult res = of.ShowDialog();
            if (res == DialogResult.OK)
            {
                mController.ApplySettings();
                // apply timer
                timerLesen.Stop();
                timerLesen.Enabled = false;
                timerLesen.Interval = Properties.Settings.Default.Timer;
                if (timerStartenToolStripMenuItem.Checked)
                {
                    timerLesen.Enabled = false;
                    timerLesen.Start();
                }
            }
        }

        private void istwerte32BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mShow32BitValues)
            {
                mShow32BitValues = false;
                istwerte32BitToolStripMenuItem.Checked = false;
            }
            else
            {
                mShow32BitValues = true;
                istwerte32BitToolStripMenuItem.Checked = true;
            }
        }

        private void istwerteEinheitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (istwerteEinheitToolStripMenuItem.Checked != false)
            {
                istwerteEinheitToolStripMenuItem.Checked = false;
                mController.ActualValuesToggleUnit(false);
            }
            else
            {
                istwerteEinheitToolStripMenuItem.Checked = true;
                mController.ActualValuesToggleUnit(true);
            }
        }

        private void parametersatzLesenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool TimerRunning = timerStartenToolStripMenuItem.Checked;
            if (TimerRunning)
            {
                // Stop timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
            using (SaveFileDialog fd = new SaveFileDialog())
            {
                fd.FileName = mController.GetRawFileName();
                fd.DefaultExt = "kbf";
                fd.Filter = "Konfigurations block file (*.kbf)|*.kbf";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    // progress bar
                    mController.ReadConfig(fd.FileName);
                    Cursor = Cursors.Default;
                }
            }
            if (TimerRunning)
            {
                // Restart timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
        }

        private void konfigurationAufspielenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool TimerRunning = timerStartenToolStripMenuItem.Checked;
            if (TimerRunning)
            {
                // Stop timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.DefaultExt = "cfg";
                fd.Filter = "Konfigurations Datei (*.cfg)|*.cfg";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ReportStatus(String.Format("Uploading file {0}", fd.FileName));
                    Cursor = Cursors.WaitCursor;
                    // progress bar
                    mController.UploadCfgFile(fd.FileName);
                    Cursor = Cursors.Default;
                }
            }
            if (TimerRunning)
            {
                // Restart timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
        }

        private void kennfelderAufspielenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool TimerRunning = timerStartenToolStripMenuItem.Checked;
            if (TimerRunning)
            {
                // Stop timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.DefaultExt = "dat";
                fd.Filter = "Kennfelder Datei (*.dat)|*.dat";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ReportStatus(String.Format("Uploading file {0}", fd.FileName));
                    Cursor = Cursors.WaitCursor;
                    // progress bar
                    mController.UploadDatFile(fd.FileName);
                    Cursor = Cursors.Default;
                }
            }
            if (TimerRunning)
            {
                // Restart timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
        }

        private void sprachenAufspielenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool TimerRunning = timerStartenToolStripMenuItem.Checked;
            if (TimerRunning)
            {
                // Stop timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.DefaultExt = "lng";
                fd.Filter = "Sprachen Datei (*.lng)|*.lng";
                DialogResult result = fd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ReportStatus(String.Format("Uploading file {0}", fd.FileName));
                    Cursor = Cursors.WaitCursor;
                    // progress bar
                    mController.UploadLngFile(fd.FileName);
                    Cursor = Cursors.Default;
                }
            }
            if (TimerRunning)
            {
                // Restart timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
        }
        #endregion

        #region Buttons
        private void buttonSetTime_Click(object sender, EventArgs e)
        {
            mController.SetTime(dateTimePickerProgTime.Value);
        }

        private void buttonSetProductionData_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ServerId == (byte)HJS.ECU.Port.Comm.ServerByte.Production)
            {
                UInt32 sn = Convert.ToUInt32(textBoxSerialNumber.Text);
                Int16 t = Convert.ToInt16(textBoxTempOffset.Text);
                Cursor = Cursors.WaitCursor;
                if (mController.SetProductionData(sn, t))
                {
                    //reconnect
                    mController.Disconnect();
                    mConnecteded = false;
                    toolStripStatusLabelConnected.Image = Properties.Resources.empty;
                    verbindungStartenToolStripMenuItem_Click(null, null);
                }
                Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Befehl nicht Ausführbar! Dies ist ein Produktionsbefehl!");
            }
        }

        private void buttonMasterReset_Click(object sender, EventArgs e)
        {
            ReportStatus("Masterreset started..");
            Cursor = Cursors.WaitCursor;
            mController.MasterReset();
            Cursor = Cursors.Default;
        }

        private void buttonReboot_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mController.Reboot(HJS.ECU.Protocol.ProtocolBase.RebootMode.RebootNormal);
            Cursor = Cursors.Default;
        }

        private void buttonRebootNotCfg_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mController.Reboot(HJS.ECU.Protocol.ProtocolBase.RebootMode.RebootNotConfig);
            Cursor = Cursors.Default;
        }

        private void buttonRebootReconfigReset_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mController.Reboot(HJS.ECU.Protocol.ProtocolBase.RebootMode.ReconfigReset);
            Cursor = Cursors.Default;
        }

        private void buttonSetAlarms_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.mainAlarmPressure, Convert.ToUInt16(textBoxHauptAlarmDruck.Text));
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.mainAlarmTime, Convert.ToUInt16(textBoxHauptAlarmZeit.Text));
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.preAlarmPressure, Convert.ToUInt16(textBoxVorAlarmDruck.Text));
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.preAlarmTime, Convert.ToUInt16(textBoxVorAlarmZeit.Text));
            Cursor = Cursors.Default;
        }

        private void buttonAdditivReset_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.additiveReset, 0);
            Cursor = Cursors.Default;
        }

        private void buttonFilterGereinigt_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.filterChanged, 0);
            Cursor = Cursors.Default;
        }

        private void buttonDosieren_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.dosingPulses, Convert.ToUInt16(textBoxDosierpulsAnzahl.Text));
            Cursor = Cursors.Default;
        }

        private void buttonRegenerieren_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.regenerationStart, 0);
            Cursor = Cursors.Default;
        }

        private void buttonSetRpmFactor_Click(object sender, EventArgs e)
        {
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.turnsFactor, (UInt16)(10 * Convert.ToSingle(textBoxDrehzahlFaktor.Text)));
        }

        private void buttonCalibrierDiff_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.calibrateDiffPSensor, 0);
            Cursor = Cursors.Default;
        }

        private void buttonCalibrierRef_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.calibrateAmbPSensor, 0);
            Cursor = Cursors.Default;
        }

        private void buttonKomSend_Click(object sender, EventArgs e)
        {
            if (radioButtonKomDataFile.Checked)
            {
                MessageBox.Show("Dateien können noch nicht versendet werden!");
            }
            else
            {
                if (radioButtonKomParamData.Checked)
                {
                    Cursor = Cursors.WaitCursor;
                    mController.DirectOrder(Convert.ToByte(textBoxKomSteuerByte.Text),
                        Convert.ToUInt16(textBoxKomParameter.Text));
                    Cursor = Cursors.Default;
                }
                else
                {
                    Cursor = Cursors.WaitCursor;
                    mController.DirectOrder(Convert.ToByte(textBoxKomSteuerByte.Text), 0);
                    Cursor = Cursors.Default;
                }
            }
        }

        private void buttonTankSignalSet_Click(object sender, EventArgs e)
        {
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.fuelSensorSignal,
                (UInt16)(HJS.ECU.Firmware.TankSignal)comboBoxTankSignal.SelectedItem);
        }

        private void buttonTankGainSet_Click(object sender, EventArgs e)
        {
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.fuelSensorGain,
                (UInt16)(HJS.ECU.Firmware.TankSignal)comboBoxTankGain.SelectedItem);
        }

        private void buttonAgrUmschalten_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.toggleEgr, 0);
            Cursor = Cursors.Default;
        }

        private void buttonUpdatableX_Click(object sender, EventArgs e)
        {
            byte[] _data;
            UInt16 _ui;
            Single _f;
            UInt32 _ul;
            switch (((Button)sender).Name)
            {
                case "buttonUpdatable1":
                    _ui = UInt16.Parse(textBoxUpdatable1.Text);
                    _data = BitConverter.GetBytes(_ui);
                    // Array muss 4 bytes beinhalten
                    Array.Resize(ref _data, 4);
                    _data[2] = 0;
                    _data[3] = 0;
                    mController.SetUpdatable(1, _data);
                    break;
                case "buttonUpdatable2":
                    _ui = UInt16.Parse(textBoxUpdatable2.Text);
                    _data = BitConverter.GetBytes(_ui);
                    Array.Resize(ref _data, 4);
                    _data[2] = 0;
                    _data[3] = 0;
                    mController.SetUpdatable(2, _data);
                    break;
                case "buttonUpdatable3":
                    _ui = UInt16.Parse(textBoxUpdatable3.Text);
                    _data = BitConverter.GetBytes(_ui);
                    Array.Resize(ref _data, 4);
                    _data[2] = 0;
                    _data[3] = 0;
                    mController.SetUpdatable(3, _data);
                    break;
                case "buttonUpdatable4":
                    _ui = UInt16.Parse(textBoxUpdatable4.Text);
                    _data = BitConverter.GetBytes(_ui);
                    Array.Resize(ref _data, 4);
                    _data[2] = 0;
                    _data[3] = 0;
                    mController.SetUpdatable(4, _data);
                    break;
                case "buttonUpdatable5":
                    _ui = UInt16.Parse(textBoxUpdatable5.Text);
                    _data = BitConverter.GetBytes(_ui);
                    Array.Resize(ref _data, 4);
                    _data[2] = 0;
                    _data[3] = 0;
                    mController.SetUpdatable(5, _data);
                    break;
                case "buttonUpdatable6":
                    _f = float.Parse(textBoxUpdatable6.Text);
                    _data = BitConverter.GetBytes(_f);
                    mController.SetUpdatable(6, _data);
                    break;
                case "buttonUpdatable7":
                    _f = float.Parse(textBoxUpdatable7.Text);
                    _data = BitConverter.GetBytes(_f);
                    mController.SetUpdatable(7, _data);
                    break;
                case "buttonUpdatable8":
                    _ul = UInt32.Parse(textBoxUpdatable8.Text);
                    _data = BitConverter.GetBytes(_ul);
                    mController.SetUpdatable(8, _data);
                    break;
                case "buttonUpdatable9":
                    _ul = UInt32.Parse(textBoxUpdatable9.Text);
                    _data = BitConverter.GetBytes(_ul);
                    mController.SetUpdatable(9, _data);
                    break;
                case "buttonUpdatable10":
                    _f = float.Parse(textBoxUpdatable10.Text);
                    _data = BitConverter.GetBytes(_f);
                    mController.SetUpdatable(10, _data);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Toolstrip
        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            verbindungStartenToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            verbindungStartenToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButtonRead_Click(object sender, EventArgs e)
        {
            lesenToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButtonTimer_Click(object sender, EventArgs e)
        {
            // ???
            timerStartenToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButtonReset_Click(object sender, EventArgs e)
        {
            bool TimerRunning = timerStartenToolStripMenuItem.Checked;
            if (TimerRunning)
            {
                // Stop timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
            Cursor = Cursors.WaitCursor;
            mController.Reboot(HJS.ECU.Protocol.ProtocolBase.RebootMode.RebootNormal);
            Cursor = Cursors.Default;
            if (TimerRunning)
            {
                // Restart timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
        }

        private void toolStripButtonStartHeater_Click(object sender, EventArgs e)
        {
            bool TimerRunning = timerStartenToolStripMenuItem.Checked;
            if (TimerRunning)
            {
                // Stop timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
            Cursor = Cursors.WaitCursor;
            mController.SetOrder(HJS.ECU.Diagnostics.Diag.Orders.regenerationStart, 0);
            Cursor = Cursors.Default;
            if (TimerRunning)
            {
                // Restart timer
                timerStartenToolStripMenuItem_Click(null, null);
            }
        }

        private void toolStripButtonOptions_Click(object sender, EventArgs e)
        {
            einstellungenToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButtonRebootNot_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mController.Reboot(HJS.ECU.Protocol.ProtocolBase.RebootMode.RebootNotConfig);
            Cursor = Cursors.Default;
        }

        private void toolStripButtonReconfigReboot_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            mController.Reboot(HJS.ECU.Protocol.ProtocolBase.RebootMode.ReconfigReset);
            Cursor = Cursors.Default;
        }

        private void toolStripButtonMasterreset_Click(object sender, EventArgs e)
        {
            ReportStatus("Masterreset started..");
            Cursor = Cursors.WaitCursor;
            mController.MasterReset();
            Cursor = Cursors.Default;
        }
        #endregion

        #region Tab Istwerte
        private void refreshValueSelection()
        {
            string ValueName, FormattedValue, ValueUnit;
            byte numOfRows = mController.GetNumberOfValues();

            listViewValuesSelection.BeginUpdate();
            listViewValuesSelection.Items.Clear();
            ListViewItem li = new ListViewItem();

            for (byte i = 0; i < numOfRows; i++)
            {
                mController.GetValueRow(i, out ValueName, out FormattedValue, out ValueUnit);
                li.SubItems.Clear();
                li.Name = ValueName;
                li.Text = ValueName;
                li.Checked = true;
                listViewValuesSelection.Items.Add((ListViewItem)li.Clone());
            }
            listViewValuesSelection.EndUpdate();
        }

        private bool refreshValues()
        {
            string ValueName, FormattedValue, ValueUnit;
            string ValueName2, FormattedValue2, ValueUnit2;
            double ValueLow = 0;
            double ValueHigh = 0;
            listViewValues.BeginUpdate();
            listViewValues.Items.Clear();
            Cursor = Cursors.WaitCursor;

            // aktuelle werte lesen
            if (mController.GetActualValues())
            {
                byte numOfRows = mController.GetNumberOfValues();
                ListViewItem li = new ListViewItem();

                for (byte i = 0; i < numOfRows; i++)
                {
                    // nur angehakte darstellen
                    if (listViewValuesSelection.Items[i].Checked)
                    {
                        mController.GetValueRow(i, out ValueName, out FormattedValue, out ValueUnit);
                        if (mShow32BitValues)
                        {
                            // bei bedarf 32-bit-werte
                            if ((i + 1) < numOfRows)
                            {
                                mController.GetValueRow((byte)(i + 1), out ValueName2, out FormattedValue2, out ValueUnit2);
                                if (ValueUnit2 == "x65536")
                                {
                                    // 32-bit-wert erkannt
                                    if (Double.TryParse(FormattedValue, out ValueLow))
                                    {
                                        if (Double.TryParse(FormattedValue2, out ValueHigh))
                                        {
                                            // Ueberschreiben des alten low wertes
                                            FormattedValue = ((ValueHigh * 65536) + ValueLow).ToString();
                                        }
                                    }
                                }
                            }
                            if (ValueUnit != "x65536")
                            {
                                // ausblenden der high werte
                                li.SubItems.Clear();
                                li.Name = ValueName;
                                li.Text = ValueName;
                                li.SubItems.Add(FormattedValue);
                                li.SubItems.Add(ValueUnit);
                                listViewValues.Items.Add((ListViewItem)li.Clone());
                            }
                        }
                        else
                        {
                            // 16-Bit Werte
                            li.SubItems.Clear();
                            li.Name = ValueName;
                            li.Text = ValueName;
                            li.SubItems.Add(FormattedValue);
                            li.SubItems.Add(ValueUnit);
                            listViewValues.Items.Add((ListViewItem)li.Clone());
                        }
                    }
                }
                ReportStatus(String.Format("Letzte lesen um {0}", mController.GetLastTimeStamp()));
                listViewValues.EndUpdate();
                Cursor = Cursors.Default;
                return true;
            }
            else
            {
                listViewValues.EndUpdate();
                Cursor = Cursors.Default;
                return false;
            }
        }

        private void buttonValueSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in listViewValuesSelection.Items)
            {
                li.Checked = true;
            }
        }

        private void buttonValuesSelectNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in listViewValuesSelection.Items)
            {
                li.Checked = false;
            }
        }

        private void buttonValuesSelectUnhidden_Click(object sender, EventArgs e)
        {
            for (byte i = 0; i < mController.GetNumberOfValues(); i++)
            {
                listViewValuesSelection.Items[i].Checked = !mController.GetActualValueHiddenFlag(i);
            }
        }
        
        private void buttonDisplayed_Click(object sender, EventArgs e)
        {
            for (byte i = 0; i < mController.GetNumberOfValues(); i++)
            {
                listViewValuesSelection.Items[i].Checked = !mController.GetActualValueDisplayFlag(i);
            }
        }
        #endregion

        #region Tab errors / events
        private bool refreshErrors()
        {
            string ErrorName, Event, ErrorState, FirstA, LastA, UntilA, Anzahl;
            byte i = 0;

            Cursor = Cursors.WaitCursor;

            // aktuelle werte lesen
            if (mController.GetErrorStack())
            {
                listViewErrors.BeginUpdate();
                listViewErrors.Items.Clear();
                listViewBehaves.BeginUpdate();
                listViewBehaves.Items.Clear();
                ListViewItem li = new ListViewItem();

                for (i = 0; i < 64; i++)
                {
                    if (mShowOccuredErrorsOnly)
                    {
                        if (mController.IsErrorOccured(i))
                        {
                            mController.GetErrorRow(i, out ErrorName, out Event, out ErrorState,
                                out FirstA, out LastA, out UntilA, out Anzahl);
                            li.SubItems.Clear();
                            li.Name = i.ToString();
                            li.Text = li.Name;
                            li.SubItems.Add(Event);
                            li.SubItems.Add(ErrorState);
                            li.SubItems.Add(ErrorName);
                            li.SubItems.Add(FirstA);
                            li.SubItems.Add(LastA);
                            li.SubItems.Add(UntilA);
                            li.SubItems.Add(Anzahl);
                            listViewErrors.Items.Add((ListViewItem)li.Clone());
                        }
                        // else: ignore
                    }
                    else
                    {
                        mController.GetErrorRow(i, out ErrorName, out Event, out ErrorState,
                            out FirstA, out LastA, out UntilA, out Anzahl);
                        li.SubItems.Clear();
                        li.Name = i.ToString();
                        li.Text = li.Name;
                        li.SubItems.Add(Event);
                        li.SubItems.Add(ErrorState);
                        li.SubItems.Add(ErrorName);
                        li.SubItems.Add(FirstA);
                        li.SubItems.Add(LastA);
                        li.SubItems.Add(UntilA);
                        li.SubItems.Add(Anzahl);
                        listViewErrors.Items.Add((ListViewItem)li.Clone());
                    }
                }
                for (i = 1; i < 16; i++)
                {
                    mController.GetBehaveRow(i, out ErrorName, out ErrorState);
                    li.SubItems.Clear();
                    li.Name = i.ToString();
                    li.Text = li.Name;
                    li.SubItems.Add(ErrorName);
                    li.SubItems.Add(ErrorState);
                    listViewBehaves.Items.Add((ListViewItem)li.Clone());
                }
                ReportStatus(String.Format("Letzte lesen um {0}", mController.GetLastTimeStamp()));

                listViewErrors.EndUpdate();
                listViewBehaves.EndUpdate();
                Cursor = Cursors.Default;
                return true;
            }
            else
            {
                listViewErrors.Items.Clear();
                listViewBehaves.Items.Clear();
                Cursor = Cursors.Default;
                return false;
            }
        }

        private void fehlerLoeschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mController.ClearErrorStack();
        }

        #endregion

        private bool refreshRtc()
        {
            string text;
            if (mController.ReadRtc())
            {
                listViewVolatiles.BeginUpdate();
                listViewVolatiles.Items.Clear();
                ListViewItem li = new ListViewItem();
                for (UInt16 i = 0; i < mController.GetNumberOfVolatiles(); i++)
                {
                    text = mController.GetVolatileValue(i);
                    li.Name = text;
                    li.Text = text;
                    li.Checked = true;
                    listViewVolatiles.Items.Add((ListViewItem)li.Clone());
                }
                listViewVolatiles.EndUpdate();
                ReportStatus(String.Format("Letzte lesen um {0}", mController.GetLastTimeStamp()));
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool refreshEpiricals()
        {
            string text;
            if (mController.ReadEmpiricals())
            {
                listViewEmpiricalValues.BeginUpdate();
                listViewEmpiricalValues.Items.Clear();
                ListViewItem li = new ListViewItem();
                for (UInt16 g = 0; g < listViewEmpiricalGroups.Items.Count; g++)
                {
                    if (listViewEmpiricalGroups.Items[g].Checked)
                    {
                        for (UInt16 i = 0; i < mController.GetNumberOfEmpiricalValues(g); i++)
                        {
                            text = mController.GetEmpiricalValue(g, i);
                            li.Name = text;
                            li.Text = text;
                            li.Checked = true;
                            listViewEmpiricalValues.Items.Add((ListViewItem)li.Clone());
                        }
                    }
                }
                listViewEmpiricalValues.EndUpdate();
                ReportStatus(String.Format("Letzte lesen um {0}", mController.GetLastTimeStamp()));
                return true;
            }
            else
            {
                return false;
            }
        }

        private void refreshEmpiricalSelection()
        {
            string[] EmpGroups;
            if (mController.GetEmpiricalGroupNames(out EmpGroups))
            {
                listViewEmpiricalGroups.BeginUpdate();
                listViewEmpiricalGroups.Items.Clear();
                ListViewItem li = new ListViewItem();

                for (byte i = 0; i < EmpGroups.Length; i++)
                {
                    li.SubItems.Clear();
                    li.Name = EmpGroups[i];
                    li.Text = EmpGroups[i];
                    li.Checked = true;
                    listViewEmpiricalGroups.Items.Add((ListViewItem)li.Clone());
                }
                listViewEmpiricalGroups.EndUpdate();
            }
        }

        private bool refreshDtcStack()
        {
            if (mController.ReadDtc())
            {
                string strWarmUps = "";
                string strErrNo = "";
                string strOc = "";
                string strPending = "";
                string strActive = "";
                string strPrevActive = "";
                string strSPN = "";
                string strFMI = "";
                textBoxDtcInfo.Text = mController.GetDtcInfo();
                textBoxDtcFreezeFrame.Text = mController.GetDtcFF();
                textBoxDtcDeratingAndFlags.Text = mController.GetDtcDerateFlags();
                //
                listViewDtcStack.BeginUpdate();
                listViewDtcStack.Items.Clear();
                ListViewItem li = new ListViewItem();
                for (byte i = 0; i < mController.GetDtcItemCount(); i++)
                {
                    mController.GetDtcItem(i, out strWarmUps, out strErrNo, out strOc,
                        out strPending, out strActive, out strPrevActive, out strSPN, out strFMI);
                    if (strErrNo != "0")
                    {
                        li.SubItems.Clear();
                        li.Name = strErrNo;
                        li.Text = strErrNo;
                        li.SubItems.Add(strSPN);
                        li.SubItems.Add(strFMI);
                        li.SubItems.Add(strWarmUps);
                        li.SubItems.Add(strOc);
                        li.SubItems.Add(strPending);
                        li.SubItems.Add(strActive);
                        li.SubItems.Add(strPrevActive);
                        listViewDtcStack.Items.Add((ListViewItem)li.Clone());
                    }
                }
                listViewDtcStack.EndUpdate();
                ReportStatus(String.Format("Letzte lesen um {0}", mController.GetLastTimeStamp()));
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool refreshUpdatables()
        {
            mController.ReadUpdatables();
            textBoxUpdatable1.Text = mController.GetUpdatable(0);
            textBoxUpdatable2.Text = mController.GetUpdatable(1);
            textBoxUpdatable3.Text = mController.GetUpdatable(2);
            textBoxUpdatable4.Text = mController.GetUpdatable(3);
            textBoxUpdatable5.Text = mController.GetUpdatable(4);
            textBoxUpdatable6.Text = mController.GetUpdatable(5);
            textBoxUpdatable7.Text = mController.GetUpdatable(6);
            textBoxUpdatable8.Text = mController.GetUpdatable(7);
            textBoxUpdatable9.Text = mController.GetUpdatable(8);
            textBoxUpdatable10.Text = mController.GetUpdatable(9);
            //todo
            return false;
        }

        private void buttonReadSbc_Click(object sender, EventArgs e)
        {
            UInt16 sbc = mController.ReadSbcRegister();
            textBoxSBC.Text = sbc.ToString();
            checkedListBoxSbcWu.SetItemChecked(0, (sbc & 0x0080) == 0x0080);
            checkedListBoxSbcWu.SetItemChecked(1, (sbc & 0x0040) == 0x0040);
            checkedListBoxSbcWu.SetItemChecked(2, (sbc & 0x0020) == 0x0020);
            checkedListBoxSbcWu.SetItemChecked(3, (sbc & 0x0010) == 0x0010);
            checkedListBoxSbcErr.SetItemChecked(0, (sbc & 0x0008) == 0x0008);
            checkedListBoxSbcErr.SetItemChecked(1, (sbc & 0x0004) == 0x0004);
            checkedListBoxSbcErr.SetItemChecked(2, (sbc & 0x0002) == 0x0002);
            checkedListBoxSbcErr.SetItemChecked(3, (sbc & 0x0001) == 0x0001);
        }
    }
}