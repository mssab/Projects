namespace hjs_ecu_mini
{
    partial class FrameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrameForm));
            this.statusFrameStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarTotal = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelConnected = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelTimer = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametersatzAufspielenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.allesAuslesenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.akquisitionAuslesenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fehlerringAuslesenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.istwerteSpeichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fehlerstackSpeichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lernwerteSpeichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametersatzLesenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verbindungToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verbindungStartenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerStartenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lesenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageOverview = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSetAlarms = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxVorAlarmZeit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxVorAlarmDruck = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxHauptAlarmZeit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxHauptAlarmDruck = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonRebootReconfigReset = new System.Windows.Forms.Button();
            this.buttonRebootNotCfg = new System.Windows.Forms.Button();
            this.textBoxLastTimeStamp = new System.Windows.Forms.TextBox();
            this.buttonMasterReset = new System.Windows.Forms.Button();
            this.buttonReboot = new System.Windows.Forms.Button();
            this.dateTimePickerProgTime = new System.Windows.Forms.DateTimePicker();
            this.buttonSetProductionData = new System.Windows.Forms.Button();
            this.buttonSetTime = new System.Windows.Forms.Button();
            this.labelDegCelsius = new System.Windows.Forms.Label();
            this.textBoxSerialNumber = new System.Windows.Forms.TextBox();
            this.textBoxTempOffset = new System.Windows.Forms.TextBox();
            this.textOverviewInfotext = new System.Windows.Forms.TextBox();
            this.textOverviewVersions = new System.Windows.Forms.TextBox();
            this.tabPageValues = new System.Windows.Forms.TabPage();
            this.buttonDisplayed = new System.Windows.Forms.Button();
            this.buttonValuesSelectUnhidden = new System.Windows.Forms.Button();
            this.listViewValuesSelection = new System.Windows.Forms.ListView();
            this.buttonValuesSelectNone = new System.Windows.Forms.Button();
            this.buttonValueSelectAll = new System.Windows.Forms.Button();
            this.listViewValues = new System.Windows.Forms.ListView();
            this.contextMenuStripActualValues = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.istwerte32BitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.istwerteEinheitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageErrors = new System.Windows.Forms.TabPage();
            this.listViewErrors = new System.Windows.Forms.ListView();
            this.contextMenuStripError = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.alleZeigenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.fehlerLoeschenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageBehave = new System.Windows.Forms.TabPage();
            this.listViewBehaves = new System.Windows.Forms.ListView();
            this.tabPageRtc = new System.Windows.Forms.TabPage();
            this.listViewVolatiles = new System.Windows.Forms.ListView();
            this.tabPageEmpirical = new System.Windows.Forms.TabPage();
            this.listViewEmpiricalValues = new System.Windows.Forms.ListView();
            this.listViewEmpiricalGroups = new System.Windows.Forms.ListView();
            this.tabPageDtc = new System.Windows.Forms.TabPage();
            this.textBoxDtcDeratingAndFlags = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewDtcStack = new System.Windows.Forms.ListView();
            this.textBoxDtcFreezeFrame = new System.Windows.Forms.TextBox();
            this.textBoxDtcInfo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageMaintainance = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.buttonTankGainSet = new System.Windows.Forms.Button();
            this.comboBoxTankGain = new System.Windows.Forms.ComboBox();
            this.buttonTankSignalSet = new System.Windows.Forms.Button();
            this.comboBoxTankSignal = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxDrehzahlFaktor = new System.Windows.Forms.TextBox();
            this.buttonSetRpmFactor = new System.Windows.Forms.Button();
            this.buttonCalibrierDiff = new System.Windows.Forms.Button();
            this.buttonCalibrierRef = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonAgrUmschalten = new System.Windows.Forms.Button();
            this.textBoxDosierpulsAnzahl = new System.Windows.Forms.TextBox();
            this.buttonDosieren = new System.Windows.Forms.Button();
            this.buttonRegenerieren = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonAdditivReset = new System.Windows.Forms.Button();
            this.buttonFilterGereinigt = new System.Windows.Forms.Button();
            this.tabPageDirectOrder = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxKomSteuerByte = new System.Windows.Forms.TextBox();
            this.buttonKomSend = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButtonKomNoData = new System.Windows.Forms.RadioButton();
            this.textBoxKomParameter = new System.Windows.Forms.TextBox();
            this.buttonFindKomFile = new System.Windows.Forms.Button();
            this.radioButtonKomParamData = new System.Windows.Forms.RadioButton();
            this.textBoxKomFile = new System.Windows.Forms.TextBox();
            this.radioButtonKomDataFile = new System.Windows.Forms.RadioButton();
            this.timerLesen = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRead = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonTimer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonReset = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRebootNot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonReconfigReboot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMasterreset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonStartHeater = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOptions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.konfigurationAufspielenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kennfelderAufspielenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sprachenAufspielenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusFrameStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageOverview.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageValues.SuspendLayout();
            this.contextMenuStripActualValues.SuspendLayout();
            this.tabPageErrors.SuspendLayout();
            this.contextMenuStripError.SuspendLayout();
            this.tabPageBehave.SuspendLayout();
            this.tabPageRtc.SuspendLayout();
            this.tabPageEmpirical.SuspendLayout();
            this.tabPageDtc.SuspendLayout();
            this.tabPageMaintainance.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPageDirectOrder.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusFrameStrip
            // 
            this.statusFrameStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelVersion,
            this.toolStripProgressBarTotal,
            this.toolStripStatusLabelConnected,
            this.toolStripStatusLabelTimer});
            this.statusFrameStrip.Location = new System.Drawing.Point(0, 436);
            this.statusFrameStrip.Name = "statusFrameStrip";
            this.statusFrameStrip.Size = new System.Drawing.Size(698, 25);
            this.statusFrameStrip.TabIndex = 5;
            this.statusFrameStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(350, 20);
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelVersion
            // 
            this.toolStripStatusLabelVersion.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelVersion.Name = "toolStripStatusLabelVersion";
            this.toolStripStatusLabelVersion.Size = new System.Drawing.Size(50, 20);
            this.toolStripStatusLabelVersion.Text = "Version";
            this.toolStripStatusLabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBarTotal
            // 
            this.toolStripProgressBarTotal.Name = "toolStripProgressBarTotal";
            this.toolStripProgressBarTotal.Size = new System.Drawing.Size(100, 19);
            this.toolStripProgressBarTotal.Step = 1;
            this.toolStripProgressBarTotal.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripStatusLabelConnected
            // 
            this.toolStripStatusLabelConnected.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelConnected.Image = global::hjs_ecu_mini.Properties.Resources.empty;
            this.toolStripStatusLabelConnected.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.toolStripStatusLabelConnected.Name = "toolStripStatusLabelConnected";
            this.toolStripStatusLabelConnected.Size = new System.Drawing.Size(20, 20);
            // 
            // toolStripStatusLabelTimer
            // 
            this.toolStripStatusLabelTimer.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelTimer.Image = global::hjs_ecu_mini.Properties.Resources.empty;
            this.toolStripStatusLabelTimer.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.toolStripStatusLabelTimer.Name = "toolStripStatusLabelTimer";
            this.toolStripStatusLabelTimer.Size = new System.Drawing.Size(20, 20);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.verbindungToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(698, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parametersatzAufspielenToolStripMenuItem,
            this.parametersatzLesenToolStripMenuItem,
            this.toolStripSeparator2,
            this.konfigurationAufspielenToolStripMenuItem,
            this.kennfelderAufspielenToolStripMenuItem,
            this.sprachenAufspielenToolStripMenuItem,
            this.toolStripSeparator9,
            this.allesAuslesenToolStripMenuItem,
            this.toolStripSeparator4,
            this.akquisitionAuslesenToolStripMenuItem,
            this.fehlerringAuslesenToolStripMenuItem,
            this.istwerteSpeichernToolStripMenuItem,
            this.fehlerstackSpeichernToolStripMenuItem,
            this.lernwerteSpeichernToolStripMenuItem,
            this.toolStripSeparator1,
            this.beendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // parametersatzAufspielenToolStripMenuItem
            // 
            this.parametersatzAufspielenToolStripMenuItem.Name = "parametersatzAufspielenToolStripMenuItem";
            this.parametersatzAufspielenToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.parametersatzAufspielenToolStripMenuItem.Text = "Parametersatz aufspielen...";
            this.parametersatzAufspielenToolStripMenuItem.Click += new System.EventHandler(this.parametersatzAufspielenToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(212, 6);
            // 
            // allesAuslesenToolStripMenuItem
            // 
            this.allesAuslesenToolStripMenuItem.Name = "allesAuslesenToolStripMenuItem";
            this.allesAuslesenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.allesAuslesenToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.allesAuslesenToolStripMenuItem.Text = "Alles auslesen";
            this.allesAuslesenToolStripMenuItem.Click += new System.EventHandler(this.allesAuslesenToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(212, 6);
            // 
            // akquisitionAuslesenToolStripMenuItem
            // 
            this.akquisitionAuslesenToolStripMenuItem.Name = "akquisitionAuslesenToolStripMenuItem";
            this.akquisitionAuslesenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.akquisitionAuslesenToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.akquisitionAuslesenToolStripMenuItem.Text = "Akquisition auslesen...";
            this.akquisitionAuslesenToolStripMenuItem.Click += new System.EventHandler(this.akquisitionAuslesenToolStripMenuItem_Click);
            // 
            // fehlerringAuslesenToolStripMenuItem
            // 
            this.fehlerringAuslesenToolStripMenuItem.Name = "fehlerringAuslesenToolStripMenuItem";
            this.fehlerringAuslesenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.fehlerringAuslesenToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.fehlerringAuslesenToolStripMenuItem.Text = "Fehlerring auslesen...";
            this.fehlerringAuslesenToolStripMenuItem.Click += new System.EventHandler(this.fehlerringAuslesenToolStripMenuItem_Click);
            // 
            // istwerteSpeichernToolStripMenuItem
            // 
            this.istwerteSpeichernToolStripMenuItem.Name = "istwerteSpeichernToolStripMenuItem";
            this.istwerteSpeichernToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.istwerteSpeichernToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.istwerteSpeichernToolStripMenuItem.Text = "Istwerte speichern...";
            this.istwerteSpeichernToolStripMenuItem.Click += new System.EventHandler(this.istwerteSpeichernToolStripMenuItem_Click);
            // 
            // fehlerstackSpeichernToolStripMenuItem
            // 
            this.fehlerstackSpeichernToolStripMenuItem.Name = "fehlerstackSpeichernToolStripMenuItem";
            this.fehlerstackSpeichernToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.fehlerstackSpeichernToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.fehlerstackSpeichernToolStripMenuItem.Text = "Fehlerstack speichern...";
            this.fehlerstackSpeichernToolStripMenuItem.Click += new System.EventHandler(this.fehlerstackSpeichernToolStripMenuItem_Click);
            // 
            // lernwerteSpeichernToolStripMenuItem
            // 
            this.lernwerteSpeichernToolStripMenuItem.Name = "lernwerteSpeichernToolStripMenuItem";
            this.lernwerteSpeichernToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.lernwerteSpeichernToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.lernwerteSpeichernToolStripMenuItem.Text = "Lernwerte speichern...";
            this.lernwerteSpeichernToolStripMenuItem.Click += new System.EventHandler(this.lernwerteSpeichernToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.beendenToolStripMenuItem.Text = "Beenden";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // parametersatzLesenToolStripMenuItem
            // 
            this.parametersatzLesenToolStripMenuItem.Name = "parametersatzLesenToolStripMenuItem";
            this.parametersatzLesenToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.parametersatzLesenToolStripMenuItem.Text = "Parametersatz lesen...";
            this.parametersatzLesenToolStripMenuItem.Click += new System.EventHandler(this.parametersatzLesenToolStripMenuItem_Click);
            // 
            // verbindungToolStripMenuItem
            // 
            this.verbindungToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verbindungStartenToolStripMenuItem,
            this.timerStartenToolStripMenuItem,
            this.lesenToolStripMenuItem,
            this.toolStripSeparator5,
            this.einstellungenToolStripMenuItem});
            this.verbindungToolStripMenuItem.Name = "verbindungToolStripMenuItem";
            this.verbindungToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.verbindungToolStripMenuItem.Text = "Verbindung";
            // 
            // verbindungStartenToolStripMenuItem
            // 
            this.verbindungStartenToolStripMenuItem.Image = global::hjs_ecu_mini.Properties.Resources.Run;
            this.verbindungStartenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.verbindungStartenToolStripMenuItem.Name = "verbindungStartenToolStripMenuItem";
            this.verbindungStartenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.verbindungStartenToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.verbindungStartenToolStripMenuItem.Text = "Verbindung starten";
            this.verbindungStartenToolStripMenuItem.Click += new System.EventHandler(this.verbindungStartenToolStripMenuItem_Click);
            // 
            // timerStartenToolStripMenuItem
            // 
            this.timerStartenToolStripMenuItem.Image = global::hjs_ecu_mini.Properties.Resources.clock;
            this.timerStartenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.timerStartenToolStripMenuItem.Name = "timerStartenToolStripMenuItem";
            this.timerStartenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.timerStartenToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.timerStartenToolStripMenuItem.Text = "Dauerhaft lesen";
            this.timerStartenToolStripMenuItem.Click += new System.EventHandler(this.timerStartenToolStripMenuItem_Click);
            // 
            // lesenToolStripMenuItem
            // 
            this.lesenToolStripMenuItem.Image = global::hjs_ecu_mini.Properties.Resources.Refresh;
            this.lesenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lesenToolStripMenuItem.Name = "lesenToolStripMenuItem";
            this.lesenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.lesenToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.lesenToolStripMenuItem.Text = "Lesen (F5)";
            this.lesenToolStripMenuItem.Click += new System.EventHandler(this.lesenToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(191, 6);
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Image = global::hjs_ecu_mini.Properties.Resources.PropertiesHS;
            this.einstellungenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen...";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.einstellungenToolStripMenuItem_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageOverview);
            this.tabControlMain.Controls.Add(this.tabPageValues);
            this.tabControlMain.Controls.Add(this.tabPageErrors);
            this.tabControlMain.Controls.Add(this.tabPageBehave);
            this.tabControlMain.Controls.Add(this.tabPageRtc);
            this.tabControlMain.Controls.Add(this.tabPageEmpirical);
            this.tabControlMain.Controls.Add(this.tabPageDtc);
            this.tabControlMain.Controls.Add(this.tabPageMaintainance);
            this.tabControlMain.Controls.Add(this.tabPageDirectOrder);
            this.tabControlMain.Location = new System.Drawing.Point(0, 52);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(698, 384);
            this.tabControlMain.TabIndex = 7;
            // 
            // tabPageOverview
            // 
            this.tabPageOverview.Controls.Add(this.groupBox2);
            this.tabPageOverview.Controls.Add(this.groupBox1);
            this.tabPageOverview.Controls.Add(this.textOverviewInfotext);
            this.tabPageOverview.Controls.Add(this.textOverviewVersions);
            this.tabPageOverview.Location = new System.Drawing.Point(4, 22);
            this.tabPageOverview.Name = "tabPageOverview";
            this.tabPageOverview.Size = new System.Drawing.Size(690, 358);
            this.tabPageOverview.TabIndex = 4;
            this.tabPageOverview.Text = "Übersicht";
            this.tabPageOverview.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonSetAlarms);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxVorAlarmZeit);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxVorAlarmDruck);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxHauptAlarmZeit);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxHauptAlarmDruck);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(9, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(672, 68);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Alarm";
            // 
            // buttonSetAlarms
            // 
            this.buttonSetAlarms.Location = new System.Drawing.Point(325, 17);
            this.buttonSetAlarms.Name = "buttonSetAlarms";
            this.buttonSetAlarms.Size = new System.Drawing.Size(75, 23);
            this.buttonSetAlarms.TabIndex = 10;
            this.buttonSetAlarms.Text = "schreiben";
            this.buttonSetAlarms.UseVisualStyleBackColor = true;
            this.buttonSetAlarms.Click += new System.EventHandler(this.buttonSetAlarms_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(240, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "sec";
            // 
            // textBoxVorAlarmZeit
            // 
            this.textBoxVorAlarmZeit.Location = new System.Drawing.Point(173, 40);
            this.textBoxVorAlarmZeit.Name = "textBoxVorAlarmZeit";
            this.textBoxVorAlarmZeit.Size = new System.Drawing.Size(60, 20);
            this.textBoxVorAlarmZeit.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(136, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "mBar";
            // 
            // textBoxVorAlarmDruck
            // 
            this.textBoxVorAlarmDruck.Location = new System.Drawing.Point(69, 40);
            this.textBoxVorAlarmDruck.Name = "textBoxVorAlarmDruck";
            this.textBoxVorAlarmDruck.Size = new System.Drawing.Size(60, 20);
            this.textBoxVorAlarmDruck.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Voralarm";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(240, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "sec";
            // 
            // textBoxHauptAlarmZeit
            // 
            this.textBoxHauptAlarmZeit.Location = new System.Drawing.Point(173, 14);
            this.textBoxHauptAlarmZeit.Name = "textBoxHauptAlarmZeit";
            this.textBoxHauptAlarmZeit.Size = new System.Drawing.Size(60, 20);
            this.textBoxHauptAlarmZeit.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(136, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "mBar";
            // 
            // textBoxHauptAlarmDruck
            // 
            this.textBoxHauptAlarmDruck.Location = new System.Drawing.Point(69, 14);
            this.textBoxHauptAlarmDruck.Name = "textBoxHauptAlarmDruck";
            this.textBoxHauptAlarmDruck.Size = new System.Drawing.Size(60, 20);
            this.textBoxHauptAlarmDruck.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Hauptalarm";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonRebootReconfigReset);
            this.groupBox1.Controls.Add(this.buttonRebootNotCfg);
            this.groupBox1.Controls.Add(this.textBoxLastTimeStamp);
            this.groupBox1.Controls.Add(this.buttonMasterReset);
            this.groupBox1.Controls.Add(this.buttonReboot);
            this.groupBox1.Controls.Add(this.dateTimePickerProgTime);
            this.groupBox1.Controls.Add(this.buttonSetProductionData);
            this.groupBox1.Controls.Add(this.buttonSetTime);
            this.groupBox1.Controls.Add(this.labelDegCelsius);
            this.groupBox1.Controls.Add(this.textBoxSerialNumber);
            this.groupBox1.Controls.Add(this.textBoxTempOffset);
            this.groupBox1.Location = new System.Drawing.Point(9, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 77);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Produktion";
            // 
            // buttonRebootReconfigReset
            // 
            this.buttonRebootReconfigReset.Location = new System.Drawing.Point(512, 46);
            this.buttonRebootReconfigReset.Name = "buttonRebootReconfigReset";
            this.buttonRebootReconfigReset.Size = new System.Drawing.Size(91, 23);
            this.buttonRebootReconfigReset.TabIndex = 12;
            this.buttonRebootReconfigReset.Text = "Reconfig Reset";
            this.buttonRebootReconfigReset.UseVisualStyleBackColor = true;
            this.buttonRebootReconfigReset.Click += new System.EventHandler(this.buttonRebootReconfigReset_Click);
            // 
            // buttonRebootNotCfg
            // 
            this.buttonRebootNotCfg.Location = new System.Drawing.Point(419, 19);
            this.buttonRebootNotCfg.Name = "buttonRebootNotCfg";
            this.buttonRebootNotCfg.Size = new System.Drawing.Size(87, 20);
            this.buttonRebootNotCfg.TabIndex = 11;
            this.buttonRebootNotCfg.Text = "Reboot (Not)";
            this.buttonRebootNotCfg.UseVisualStyleBackColor = true;
            this.buttonRebootNotCfg.Click += new System.EventHandler(this.buttonRebootNotCfg_Click);
            // 
            // textBoxLastTimeStamp
            // 
            this.textBoxLastTimeStamp.Location = new System.Drawing.Point(6, 19);
            this.textBoxLastTimeStamp.Name = "textBoxLastTimeStamp";
            this.textBoxLastTimeStamp.Size = new System.Drawing.Size(172, 20);
            this.textBoxLastTimeStamp.TabIndex = 3;
            // 
            // buttonMasterReset
            // 
            this.buttonMasterReset.Location = new System.Drawing.Point(512, 18);
            this.buttonMasterReset.Name = "buttonMasterReset";
            this.buttonMasterReset.Size = new System.Drawing.Size(91, 20);
            this.buttonMasterReset.TabIndex = 9;
            this.buttonMasterReset.Text = "Masterreset";
            this.buttonMasterReset.UseVisualStyleBackColor = true;
            this.buttonMasterReset.Click += new System.EventHandler(this.buttonMasterReset_Click);
            // 
            // buttonReboot
            // 
            this.buttonReboot.Location = new System.Drawing.Point(419, 46);
            this.buttonReboot.Name = "buttonReboot";
            this.buttonReboot.Size = new System.Drawing.Size(87, 23);
            this.buttonReboot.TabIndex = 10;
            this.buttonReboot.Text = "Reboot (Cfg)";
            this.buttonReboot.UseVisualStyleBackColor = true;
            this.buttonReboot.Click += new System.EventHandler(this.buttonReboot_Click);
            // 
            // dateTimePickerProgTime
            // 
            this.dateTimePickerProgTime.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateTimePickerProgTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerProgTime.Location = new System.Drawing.Point(184, 19);
            this.dateTimePickerProgTime.Name = "dateTimePickerProgTime";
            this.dateTimePickerProgTime.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerProgTime.TabIndex = 2;
            // 
            // buttonSetProductionData
            // 
            this.buttonSetProductionData.Location = new System.Drawing.Point(325, 46);
            this.buttonSetProductionData.Name = "buttonSetProductionData";
            this.buttonSetProductionData.Size = new System.Drawing.Size(88, 23);
            this.buttonSetProductionData.TabIndex = 8;
            this.buttonSetProductionData.Text = "S/N schreiben";
            this.buttonSetProductionData.UseVisualStyleBackColor = true;
            this.buttonSetProductionData.Click += new System.EventHandler(this.buttonSetProductionData_Click);
            // 
            // buttonSetTime
            // 
            this.buttonSetTime.Location = new System.Drawing.Point(325, 19);
            this.buttonSetTime.Name = "buttonSetTime";
            this.buttonSetTime.Size = new System.Drawing.Size(88, 20);
            this.buttonSetTime.TabIndex = 4;
            this.buttonSetTime.Text = "Uhrzeit setzen";
            this.buttonSetTime.UseVisualStyleBackColor = true;
            this.buttonSetTime.Click += new System.EventHandler(this.buttonSetTime_Click);
            // 
            // labelDegCelsius
            // 
            this.labelDegCelsius.AutoSize = true;
            this.labelDegCelsius.Location = new System.Drawing.Point(301, 49);
            this.labelDegCelsius.Name = "labelDegCelsius";
            this.labelDegCelsius.Size = new System.Drawing.Size(18, 13);
            this.labelDegCelsius.TabIndex = 7;
            this.labelDegCelsius.Text = "°C";
            // 
            // textBoxSerialNumber
            // 
            this.textBoxSerialNumber.Location = new System.Drawing.Point(7, 46);
            this.textBoxSerialNumber.Name = "textBoxSerialNumber";
            this.textBoxSerialNumber.Size = new System.Drawing.Size(171, 20);
            this.textBoxSerialNumber.TabIndex = 5;
            this.textBoxSerialNumber.Text = "1000000";
            this.textBoxSerialNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxTempOffset
            // 
            this.textBoxTempOffset.Location = new System.Drawing.Point(184, 46);
            this.textBoxTempOffset.Name = "textBoxTempOffset";
            this.textBoxTempOffset.Size = new System.Drawing.Size(111, 20);
            this.textBoxTempOffset.TabIndex = 6;
            this.textBoxTempOffset.Text = "20";
            this.textBoxTempOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textOverviewInfotext
            // 
            this.textOverviewInfotext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textOverviewInfotext.Location = new System.Drawing.Point(8, 54);
            this.textOverviewInfotext.Multiline = true;
            this.textOverviewInfotext.Name = "textOverviewInfotext";
            this.textOverviewInfotext.Size = new System.Drawing.Size(673, 45);
            this.textOverviewInfotext.TabIndex = 1;
            // 
            // textOverviewVersions
            // 
            this.textOverviewVersions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textOverviewVersions.Location = new System.Drawing.Point(8, 3);
            this.textOverviewVersions.Multiline = true;
            this.textOverviewVersions.Name = "textOverviewVersions";
            this.textOverviewVersions.Size = new System.Drawing.Size(674, 45);
            this.textOverviewVersions.TabIndex = 0;
            // 
            // tabPageValues
            // 
            this.tabPageValues.Controls.Add(this.buttonDisplayed);
            this.tabPageValues.Controls.Add(this.buttonValuesSelectUnhidden);
            this.tabPageValues.Controls.Add(this.listViewValuesSelection);
            this.tabPageValues.Controls.Add(this.buttonValuesSelectNone);
            this.tabPageValues.Controls.Add(this.buttonValueSelectAll);
            this.tabPageValues.Controls.Add(this.listViewValues);
            this.tabPageValues.Location = new System.Drawing.Point(4, 22);
            this.tabPageValues.Name = "tabPageValues";
            this.tabPageValues.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageValues.Size = new System.Drawing.Size(690, 358);
            this.tabPageValues.TabIndex = 0;
            this.tabPageValues.Text = "Istwerte";
            this.tabPageValues.UseVisualStyleBackColor = true;
            // 
            // buttonDisplayed
            // 
            this.buttonDisplayed.Location = new System.Drawing.Point(153, 6);
            this.buttonDisplayed.Name = "buttonDisplayed";
            this.buttonDisplayed.Size = new System.Drawing.Size(31, 20);
            this.buttonDisplayed.TabIndex = 5;
            this.buttonDisplayed.Text = "lcd";
            this.buttonDisplayed.UseVisualStyleBackColor = true;
            this.buttonDisplayed.Click += new System.EventHandler(this.buttonDisplayed_Click);
            // 
            // buttonValuesSelectUnhidden
            // 
            this.buttonValuesSelectUnhidden.Location = new System.Drawing.Point(8, 6);
            this.buttonValuesSelectUnhidden.Name = "buttonValuesSelectUnhidden";
            this.buttonValuesSelectUnhidden.Size = new System.Drawing.Size(55, 20);
            this.buttonValuesSelectUnhidden.TabIndex = 4;
            this.buttonValuesSelectUnhidden.Text = "normal";
            this.buttonValuesSelectUnhidden.UseVisualStyleBackColor = true;
            this.buttonValuesSelectUnhidden.Click += new System.EventHandler(this.buttonValuesSelectUnhidden_Click);
            // 
            // listViewValuesSelection
            // 
            this.listViewValuesSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewValuesSelection.CheckBoxes = true;
            this.listViewValuesSelection.Location = new System.Drawing.Point(8, 32);
            this.listViewValuesSelection.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.listViewValuesSelection.Name = "listViewValuesSelection";
            this.listViewValuesSelection.Size = new System.Drawing.Size(176, 326);
            this.listViewValuesSelection.TabIndex = 3;
            this.listViewValuesSelection.UseCompatibleStateImageBehavior = false;
            this.listViewValuesSelection.View = System.Windows.Forms.View.Details;
            // 
            // buttonValuesSelectNone
            // 
            this.buttonValuesSelectNone.Location = new System.Drawing.Point(106, 6);
            this.buttonValuesSelectNone.Name = "buttonValuesSelectNone";
            this.buttonValuesSelectNone.Size = new System.Drawing.Size(41, 20);
            this.buttonValuesSelectNone.TabIndex = 2;
            this.buttonValuesSelectNone.Text = "keine";
            this.buttonValuesSelectNone.UseVisualStyleBackColor = true;
            this.buttonValuesSelectNone.Click += new System.EventHandler(this.buttonValuesSelectNone_Click);
            // 
            // buttonValueSelectAll
            // 
            this.buttonValueSelectAll.Location = new System.Drawing.Point(69, 6);
            this.buttonValueSelectAll.Name = "buttonValueSelectAll";
            this.buttonValueSelectAll.Size = new System.Drawing.Size(31, 20);
            this.buttonValueSelectAll.TabIndex = 1;
            this.buttonValueSelectAll.Text = "alle";
            this.buttonValueSelectAll.UseVisualStyleBackColor = true;
            this.buttonValueSelectAll.Click += new System.EventHandler(this.buttonValueSelectAll_Click);
            // 
            // listViewValues
            // 
            this.listViewValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewValues.ContextMenuStrip = this.contextMenuStripActualValues;
            this.listViewValues.Location = new System.Drawing.Point(190, 6);
            this.listViewValues.Name = "listViewValues";
            this.listViewValues.Size = new System.Drawing.Size(492, 353);
            this.listViewValues.TabIndex = 0;
            this.listViewValues.UseCompatibleStateImageBehavior = false;
            this.listViewValues.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuStripActualValues
            // 
            this.contextMenuStripActualValues.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.istwerte32BitToolStripMenuItem,
            this.istwerteEinheitToolStripMenuItem});
            this.contextMenuStripActualValues.Name = "contextMenuStripActualValues";
            this.contextMenuStripActualValues.Size = new System.Drawing.Size(156, 48);
            // 
            // istwerte32BitToolStripMenuItem
            // 
            this.istwerte32BitToolStripMenuItem.Checked = true;
            this.istwerte32BitToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.istwerte32BitToolStripMenuItem.Name = "istwerte32BitToolStripMenuItem";
            this.istwerte32BitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.istwerte32BitToolStripMenuItem.Text = "32-Bit-Wert";
            this.istwerte32BitToolStripMenuItem.Click += new System.EventHandler(this.istwerte32BitToolStripMenuItem_Click);
            // 
            // istwerteEinheitToolStripMenuItem
            // 
            this.istwerteEinheitToolStripMenuItem.Name = "istwerteEinheitToolStripMenuItem";
            this.istwerteEinheitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.istwerteEinheitToolStripMenuItem.Text = "Original Einheit";
            this.istwerteEinheitToolStripMenuItem.Click += new System.EventHandler(this.istwerteEinheitToolStripMenuItem_Click);
            // 
            // tabPageErrors
            // 
            this.tabPageErrors.Controls.Add(this.listViewErrors);
            this.tabPageErrors.Location = new System.Drawing.Point(4, 22);
            this.tabPageErrors.Name = "tabPageErrors";
            this.tabPageErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageErrors.Size = new System.Drawing.Size(690, 358);
            this.tabPageErrors.TabIndex = 1;
            this.tabPageErrors.Text = "Fehler/Events";
            this.tabPageErrors.UseVisualStyleBackColor = true;
            // 
            // listViewErrors
            // 
            this.listViewErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewErrors.ContextMenuStrip = this.contextMenuStripError;
            this.listViewErrors.Location = new System.Drawing.Point(8, 6);
            this.listViewErrors.Name = "listViewErrors";
            this.listViewErrors.Size = new System.Drawing.Size(674, 352);
            this.listViewErrors.TabIndex = 0;
            this.listViewErrors.UseCompatibleStateImageBehavior = false;
            this.listViewErrors.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuStripError
            // 
            this.contextMenuStripError.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alleZeigenToolStripMenuItem,
            this.toolStripSeparator3,
            this.fehlerLoeschenToolStripMenuItem});
            this.contextMenuStripError.Name = "contextMenuStripError";
            this.contextMenuStripError.Size = new System.Drawing.Size(134, 54);
            // 
            // alleZeigenToolStripMenuItem
            // 
            this.alleZeigenToolStripMenuItem.Name = "alleZeigenToolStripMenuItem";
            this.alleZeigenToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.alleZeigenToolStripMenuItem.Text = "Alle Zeigen";
            this.alleZeigenToolStripMenuItem.Click += new System.EventHandler(this.alleZeigenToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(130, 6);
            // 
            // fehlerLoeschenToolStripMenuItem
            // 
            this.fehlerLoeschenToolStripMenuItem.Name = "fehlerLoeschenToolStripMenuItem";
            this.fehlerLoeschenToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.fehlerLoeschenToolStripMenuItem.Text = "löschen";
            this.fehlerLoeschenToolStripMenuItem.Click += new System.EventHandler(this.fehlerLoeschenToolStripMenuItem_Click);
            // 
            // tabPageBehave
            // 
            this.tabPageBehave.Controls.Add(this.listViewBehaves);
            this.tabPageBehave.Location = new System.Drawing.Point(4, 22);
            this.tabPageBehave.Name = "tabPageBehave";
            this.tabPageBehave.Size = new System.Drawing.Size(690, 358);
            this.tabPageBehave.TabIndex = 2;
            this.tabPageBehave.Text = "Verhalten";
            this.tabPageBehave.UseVisualStyleBackColor = true;
            // 
            // listViewBehaves
            // 
            this.listViewBehaves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewBehaves.Location = new System.Drawing.Point(8, 3);
            this.listViewBehaves.Name = "listViewBehaves";
            this.listViewBehaves.Size = new System.Drawing.Size(674, 356);
            this.listViewBehaves.TabIndex = 0;
            this.listViewBehaves.UseCompatibleStateImageBehavior = false;
            this.listViewBehaves.View = System.Windows.Forms.View.Details;
            // 
            // tabPageRtc
            // 
            this.tabPageRtc.Controls.Add(this.listViewVolatiles);
            this.tabPageRtc.Location = new System.Drawing.Point(4, 22);
            this.tabPageRtc.Name = "tabPageRtc";
            this.tabPageRtc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRtc.Size = new System.Drawing.Size(690, 358);
            this.tabPageRtc.TabIndex = 5;
            this.tabPageRtc.Text = "Echtzeituhr";
            this.tabPageRtc.UseVisualStyleBackColor = true;
            // 
            // listViewVolatiles
            // 
            this.listViewVolatiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewVolatiles.Location = new System.Drawing.Point(8, 6);
            this.listViewVolatiles.Name = "listViewVolatiles";
            this.listViewVolatiles.Size = new System.Drawing.Size(674, 353);
            this.listViewVolatiles.TabIndex = 0;
            this.listViewVolatiles.UseCompatibleStateImageBehavior = false;
            this.listViewVolatiles.View = System.Windows.Forms.View.Details;
            // 
            // tabPageEmpirical
            // 
            this.tabPageEmpirical.Controls.Add(this.listViewEmpiricalValues);
            this.tabPageEmpirical.Controls.Add(this.listViewEmpiricalGroups);
            this.tabPageEmpirical.Location = new System.Drawing.Point(4, 22);
            this.tabPageEmpirical.Name = "tabPageEmpirical";
            this.tabPageEmpirical.Size = new System.Drawing.Size(690, 358);
            this.tabPageEmpirical.TabIndex = 6;
            this.tabPageEmpirical.Text = "Lernwerte";
            this.tabPageEmpirical.UseVisualStyleBackColor = true;
            // 
            // listViewEmpiricalValues
            // 
            this.listViewEmpiricalValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewEmpiricalValues.Location = new System.Drawing.Point(190, 3);
            this.listViewEmpiricalValues.Name = "listViewEmpiricalValues";
            this.listViewEmpiricalValues.Size = new System.Drawing.Size(492, 355);
            this.listViewEmpiricalValues.TabIndex = 1;
            this.listViewEmpiricalValues.UseCompatibleStateImageBehavior = false;
            this.listViewEmpiricalValues.View = System.Windows.Forms.View.Details;
            // 
            // listViewEmpiricalGroups
            // 
            this.listViewEmpiricalGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewEmpiricalGroups.CheckBoxes = true;
            this.listViewEmpiricalGroups.Location = new System.Drawing.Point(8, 3);
            this.listViewEmpiricalGroups.Name = "listViewEmpiricalGroups";
            this.listViewEmpiricalGroups.Size = new System.Drawing.Size(176, 355);
            this.listViewEmpiricalGroups.TabIndex = 0;
            this.listViewEmpiricalGroups.UseCompatibleStateImageBehavior = false;
            this.listViewEmpiricalGroups.View = System.Windows.Forms.View.Details;
            // 
            // tabPageDtc
            // 
            this.tabPageDtc.Controls.Add(this.textBoxDtcDeratingAndFlags);
            this.tabPageDtc.Controls.Add(this.label1);
            this.tabPageDtc.Controls.Add(this.listViewDtcStack);
            this.tabPageDtc.Controls.Add(this.textBoxDtcFreezeFrame);
            this.tabPageDtc.Controls.Add(this.textBoxDtcInfo);
            this.tabPageDtc.Controls.Add(this.label3);
            this.tabPageDtc.Controls.Add(this.label2);
            this.tabPageDtc.Location = new System.Drawing.Point(4, 22);
            this.tabPageDtc.Name = "tabPageDtc";
            this.tabPageDtc.Size = new System.Drawing.Size(690, 358);
            this.tabPageDtc.TabIndex = 7;
            this.tabPageDtc.Text = "Fehler Codes";
            this.tabPageDtc.UseVisualStyleBackColor = true;
            // 
            // textBoxDtcDeratingAndFlags
            // 
            this.textBoxDtcDeratingAndFlags.Location = new System.Drawing.Point(82, 57);
            this.textBoxDtcDeratingAndFlags.Name = "textBoxDtcDeratingAndFlags";
            this.textBoxDtcDeratingAndFlags.Size = new System.Drawing.Size(599, 20);
            this.textBoxDtcDeratingAndFlags.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Derate/Flags";
            // 
            // listViewDtcStack
            // 
            this.listViewDtcStack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDtcStack.Location = new System.Drawing.Point(3, 83);
            this.listViewDtcStack.Name = "listViewDtcStack";
            this.listViewDtcStack.Size = new System.Drawing.Size(679, 276);
            this.listViewDtcStack.TabIndex = 4;
            this.listViewDtcStack.UseCompatibleStateImageBehavior = false;
            this.listViewDtcStack.View = System.Windows.Forms.View.Details;
            // 
            // textBoxDtcFreezeFrame
            // 
            this.textBoxDtcFreezeFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDtcFreezeFrame.Location = new System.Drawing.Point(82, 31);
            this.textBoxDtcFreezeFrame.Name = "textBoxDtcFreezeFrame";
            this.textBoxDtcFreezeFrame.Size = new System.Drawing.Size(600, 20);
            this.textBoxDtcFreezeFrame.TabIndex = 3;
            // 
            // textBoxDtcInfo
            // 
            this.textBoxDtcInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDtcInfo.Location = new System.Drawing.Point(82, 4);
            this.textBoxDtcInfo.Name = "textBoxDtcInfo";
            this.textBoxDtcInfo.Size = new System.Drawing.Size(600, 20);
            this.textBoxDtcInfo.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Freeze Frame";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Info";
            // 
            // tabPageMaintainance
            // 
            this.tabPageMaintainance.Controls.Add(this.groupBox8);
            this.tabPageMaintainance.Controls.Add(this.groupBox5);
            this.tabPageMaintainance.Controls.Add(this.groupBox4);
            this.tabPageMaintainance.Controls.Add(this.groupBox3);
            this.tabPageMaintainance.Location = new System.Drawing.Point(4, 22);
            this.tabPageMaintainance.Name = "tabPageMaintainance";
            this.tabPageMaintainance.Size = new System.Drawing.Size(690, 358);
            this.tabPageMaintainance.TabIndex = 8;
            this.tabPageMaintainance.Text = "Wartung";
            this.tabPageMaintainance.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.buttonTankGainSet);
            this.groupBox8.Controls.Add(this.comboBoxTankGain);
            this.groupBox8.Controls.Add(this.buttonTankSignalSet);
            this.groupBox8.Controls.Add(this.comboBoxTankSignal);
            this.groupBox8.Location = new System.Drawing.Point(8, 183);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(674, 55);
            this.groupBox8.TabIndex = 12;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Tankgeber";
            // 
            // buttonTankGainSet
            // 
            this.buttonTankGainSet.Location = new System.Drawing.Point(384, 20);
            this.buttonTankGainSet.Name = "buttonTankGainSet";
            this.buttonTankGainSet.Size = new System.Drawing.Size(111, 23);
            this.buttonTankGainSet.TabIndex = 3;
            this.buttonTankGainSet.Text = "Verstärkung setzen";
            this.buttonTankGainSet.UseVisualStyleBackColor = true;
            this.buttonTankGainSet.Click += new System.EventHandler(this.buttonTankGainSet_Click);
            // 
            // comboBoxTankGain
            // 
            this.comboBoxTankGain.FormattingEnabled = true;
            this.comboBoxTankGain.Location = new System.Drawing.Point(256, 20);
            this.comboBoxTankGain.Name = "comboBoxTankGain";
            this.comboBoxTankGain.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTankGain.TabIndex = 2;
            // 
            // buttonTankSignalSet
            // 
            this.buttonTankSignalSet.Location = new System.Drawing.Point(135, 20);
            this.buttonTankSignalSet.Name = "buttonTankSignalSet";
            this.buttonTankSignalSet.Size = new System.Drawing.Size(100, 23);
            this.buttonTankSignalSet.TabIndex = 1;
            this.buttonTankSignalSet.Text = "Signal setzen";
            this.buttonTankSignalSet.UseVisualStyleBackColor = true;
            this.buttonTankSignalSet.Click += new System.EventHandler(this.buttonTankSignalSet_Click);
            // 
            // comboBoxTankSignal
            // 
            this.comboBoxTankSignal.FormattingEnabled = true;
            this.comboBoxTankSignal.Location = new System.Drawing.Point(7, 20);
            this.comboBoxTankSignal.Name = "comboBoxTankSignal";
            this.comboBoxTankSignal.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTankSignal.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.textBoxDrehzahlFaktor);
            this.groupBox5.Controls.Add(this.buttonSetRpmFactor);
            this.groupBox5.Controls.Add(this.buttonCalibrierDiff);
            this.groupBox5.Controls.Add(this.buttonCalibrierRef);
            this.groupBox5.Location = new System.Drawing.Point(8, 123);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(674, 55);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Sensoren";
            // 
            // textBoxDrehzahlFaktor
            // 
            this.textBoxDrehzahlFaktor.Location = new System.Drawing.Point(6, 19);
            this.textBoxDrehzahlFaktor.Name = "textBoxDrehzahlFaktor";
            this.textBoxDrehzahlFaktor.Size = new System.Drawing.Size(84, 20);
            this.textBoxDrehzahlFaktor.TabIndex = 7;
            this.textBoxDrehzahlFaktor.Text = "1,0";
            this.textBoxDrehzahlFaktor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonSetRpmFactor
            // 
            this.buttonSetRpmFactor.Location = new System.Drawing.Point(96, 19);
            this.buttonSetRpmFactor.Name = "buttonSetRpmFactor";
            this.buttonSetRpmFactor.Size = new System.Drawing.Size(92, 23);
            this.buttonSetRpmFactor.TabIndex = 8;
            this.buttonSetRpmFactor.Text = "Drehzahl Faktor";
            this.buttonSetRpmFactor.UseVisualStyleBackColor = true;
            this.buttonSetRpmFactor.Click += new System.EventHandler(this.buttonSetRpmFactor_Click);
            // 
            // buttonCalibrierDiff
            // 
            this.buttonCalibrierDiff.Location = new System.Drawing.Point(222, 19);
            this.buttonCalibrierDiff.Name = "buttonCalibrierDiff";
            this.buttonCalibrierDiff.Size = new System.Drawing.Size(182, 23);
            this.buttonCalibrierDiff.TabIndex = 5;
            this.buttonCalibrierDiff.Text = "Differenzdrucksensor Kalibrieren";
            this.buttonCalibrierDiff.UseVisualStyleBackColor = true;
            this.buttonCalibrierDiff.Click += new System.EventHandler(this.buttonCalibrierDiff_Click);
            // 
            // buttonCalibrierRef
            // 
            this.buttonCalibrierRef.Location = new System.Drawing.Point(436, 17);
            this.buttonCalibrierRef.Name = "buttonCalibrierRef";
            this.buttonCalibrierRef.Size = new System.Drawing.Size(182, 23);
            this.buttonCalibrierRef.TabIndex = 6;
            this.buttonCalibrierRef.Text = "Umgebungsdrucksensor Kalibrieren";
            this.buttonCalibrierRef.UseVisualStyleBackColor = true;
            this.buttonCalibrierRef.Click += new System.EventHandler(this.buttonCalibrierRef_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.buttonAgrUmschalten);
            this.groupBox4.Controls.Add(this.textBoxDosierpulsAnzahl);
            this.groupBox4.Controls.Add(this.buttonDosieren);
            this.groupBox4.Controls.Add(this.buttonRegenerieren);
            this.groupBox4.Location = new System.Drawing.Point(8, 62);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(674, 55);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Aktoren";
            // 
            // buttonAgrUmschalten
            // 
            this.buttonAgrUmschalten.Location = new System.Drawing.Point(436, 18);
            this.buttonAgrUmschalten.Name = "buttonAgrUmschalten";
            this.buttonAgrUmschalten.Size = new System.Drawing.Size(182, 23);
            this.buttonAgrUmschalten.TabIndex = 5;
            this.buttonAgrUmschalten.Text = "AGR Umschalten";
            this.buttonAgrUmschalten.UseVisualStyleBackColor = true;
            this.buttonAgrUmschalten.Click += new System.EventHandler(this.buttonAgrUmschalten_Click);
            // 
            // textBoxDosierpulsAnzahl
            // 
            this.textBoxDosierpulsAnzahl.Location = new System.Drawing.Point(6, 19);
            this.textBoxDosierpulsAnzahl.Name = "textBoxDosierpulsAnzahl";
            this.textBoxDosierpulsAnzahl.Size = new System.Drawing.Size(100, 20);
            this.textBoxDosierpulsAnzahl.TabIndex = 2;
            this.textBoxDosierpulsAnzahl.Text = "1";
            this.textBoxDosierpulsAnzahl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonDosieren
            // 
            this.buttonDosieren.Location = new System.Drawing.Point(113, 19);
            this.buttonDosieren.Name = "buttonDosieren";
            this.buttonDosieren.Size = new System.Drawing.Size(75, 23);
            this.buttonDosieren.TabIndex = 3;
            this.buttonDosieren.Text = "Dosieren";
            this.buttonDosieren.UseVisualStyleBackColor = true;
            this.buttonDosieren.Click += new System.EventHandler(this.buttonDosieren_Click);
            // 
            // buttonRegenerieren
            // 
            this.buttonRegenerieren.Location = new System.Drawing.Point(222, 19);
            this.buttonRegenerieren.Name = "buttonRegenerieren";
            this.buttonRegenerieren.Size = new System.Drawing.Size(182, 23);
            this.buttonRegenerieren.TabIndex = 4;
            this.buttonRegenerieren.Text = "Regenerations Start/Stop";
            this.buttonRegenerieren.UseVisualStyleBackColor = true;
            this.buttonRegenerieren.Click += new System.EventHandler(this.buttonRegenerieren_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.buttonAdditivReset);
            this.groupBox3.Controls.Add(this.buttonFilterGereinigt);
            this.groupBox3.Location = new System.Drawing.Point(8, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(674, 53);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Wartung";
            // 
            // buttonAdditivReset
            // 
            this.buttonAdditivReset.Location = new System.Drawing.Point(6, 19);
            this.buttonAdditivReset.Name = "buttonAdditivReset";
            this.buttonAdditivReset.Size = new System.Drawing.Size(182, 23);
            this.buttonAdditivReset.TabIndex = 0;
            this.buttonAdditivReset.Text = "Additiv getankt";
            this.buttonAdditivReset.UseVisualStyleBackColor = true;
            this.buttonAdditivReset.Click += new System.EventHandler(this.buttonAdditivReset_Click);
            // 
            // buttonFilterGereinigt
            // 
            this.buttonFilterGereinigt.Location = new System.Drawing.Point(222, 19);
            this.buttonFilterGereinigt.Name = "buttonFilterGereinigt";
            this.buttonFilterGereinigt.Size = new System.Drawing.Size(182, 23);
            this.buttonFilterGereinigt.TabIndex = 1;
            this.buttonFilterGereinigt.Text = "Filter gereinigt";
            this.buttonFilterGereinigt.UseVisualStyleBackColor = true;
            this.buttonFilterGereinigt.Click += new System.EventHandler(this.buttonFilterGereinigt_Click);
            // 
            // tabPageDirectOrder
            // 
            this.tabPageDirectOrder.Controls.Add(this.groupBox7);
            this.tabPageDirectOrder.Controls.Add(this.groupBox6);
            this.tabPageDirectOrder.Location = new System.Drawing.Point(4, 22);
            this.tabPageDirectOrder.Name = "tabPageDirectOrder";
            this.tabPageDirectOrder.Size = new System.Drawing.Size(690, 358);
            this.tabPageDirectOrder.TabIndex = 9;
            this.tabPageDirectOrder.Text = "Befehl";
            this.tabPageDirectOrder.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.textBoxKomSteuerByte);
            this.groupBox7.Controls.Add(this.buttonKomSend);
            this.groupBox7.Location = new System.Drawing.Point(8, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(674, 43);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Befehl";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Steuerbyte";
            // 
            // textBoxKomSteuerByte
            // 
            this.textBoxKomSteuerByte.Location = new System.Drawing.Point(105, 13);
            this.textBoxKomSteuerByte.Name = "textBoxKomSteuerByte";
            this.textBoxKomSteuerByte.Size = new System.Drawing.Size(67, 20);
            this.textBoxKomSteuerByte.TabIndex = 0;
            this.textBoxKomSteuerByte.Text = "0";
            this.textBoxKomSteuerByte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonKomSend
            // 
            this.buttonKomSend.Location = new System.Drawing.Point(265, 11);
            this.buttonKomSend.Name = "buttonKomSend";
            this.buttonKomSend.Size = new System.Drawing.Size(75, 23);
            this.buttonKomSend.TabIndex = 8;
            this.buttonKomSend.Text = "Senden";
            this.buttonKomSend.UseVisualStyleBackColor = true;
            this.buttonKomSend.Click += new System.EventHandler(this.buttonKomSend_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.radioButtonKomNoData);
            this.groupBox6.Controls.Add(this.textBoxKomParameter);
            this.groupBox6.Controls.Add(this.buttonFindKomFile);
            this.groupBox6.Controls.Add(this.radioButtonKomParamData);
            this.groupBox6.Controls.Add(this.textBoxKomFile);
            this.groupBox6.Controls.Add(this.radioButtonKomDataFile);
            this.groupBox6.Location = new System.Drawing.Point(8, 52);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(674, 105);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Daten";
            // 
            // radioButtonKomNoData
            // 
            this.radioButtonKomNoData.AutoSize = true;
            this.radioButtonKomNoData.Checked = true;
            this.radioButtonKomNoData.Location = new System.Drawing.Point(6, 19);
            this.radioButtonKomNoData.Name = "radioButtonKomNoData";
            this.radioButtonKomNoData.Size = new System.Drawing.Size(84, 17);
            this.radioButtonKomNoData.TabIndex = 2;
            this.radioButtonKomNoData.TabStop = true;
            this.radioButtonKomNoData.Text = "Keine Daten";
            this.radioButtonKomNoData.UseVisualStyleBackColor = true;
            // 
            // textBoxKomParameter
            // 
            this.textBoxKomParameter.Location = new System.Drawing.Point(105, 44);
            this.textBoxKomParameter.Name = "textBoxKomParameter";
            this.textBoxKomParameter.Size = new System.Drawing.Size(100, 20);
            this.textBoxKomParameter.TabIndex = 1;
            this.textBoxKomParameter.Text = "0";
            this.textBoxKomParameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonFindKomFile
            // 
            this.buttonFindKomFile.Location = new System.Drawing.Point(312, 70);
            this.buttonFindKomFile.Name = "buttonFindKomFile";
            this.buttonFindKomFile.Size = new System.Drawing.Size(28, 23);
            this.buttonFindKomFile.TabIndex = 7;
            this.buttonFindKomFile.Text = "...";
            this.buttonFindKomFile.UseVisualStyleBackColor = true;
            // 
            // radioButtonKomParamData
            // 
            this.radioButtonKomParamData.AutoSize = true;
            this.radioButtonKomParamData.Location = new System.Drawing.Point(6, 45);
            this.radioButtonKomParamData.Name = "radioButtonKomParamData";
            this.radioButtonKomParamData.Size = new System.Drawing.Size(93, 17);
            this.radioButtonKomParamData.TabIndex = 3;
            this.radioButtonKomParamData.Text = "Nur Parameter";
            this.radioButtonKomParamData.UseVisualStyleBackColor = true;
            // 
            // textBoxKomFile
            // 
            this.textBoxKomFile.Location = new System.Drawing.Point(105, 70);
            this.textBoxKomFile.Name = "textBoxKomFile";
            this.textBoxKomFile.Size = new System.Drawing.Size(201, 20);
            this.textBoxKomFile.TabIndex = 6;
            // 
            // radioButtonKomDataFile
            // 
            this.radioButtonKomDataFile.AutoSize = true;
            this.radioButtonKomDataFile.Location = new System.Drawing.Point(6, 71);
            this.radioButtonKomDataFile.Name = "radioButtonKomDataFile";
            this.radioButtonKomDataFile.Size = new System.Drawing.Size(82, 17);
            this.radioButtonKomDataFile.TabIndex = 4;
            this.radioButtonKomDataFile.Text = "Daten Datei";
            this.radioButtonKomDataFile.UseVisualStyleBackColor = true;
            // 
            // timerLesen
            // 
            this.timerLesen.Tick += new System.EventHandler(this.timerLesen_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonStart,
            this.toolStripButtonStop,
            this.toolStripButtonRead,
            this.toolStripButtonTimer,
            this.toolStripSeparator6,
            this.toolStripButtonReset,
            this.toolStripButtonRebootNot,
            this.toolStripButtonReconfigReboot,
            this.toolStripButtonMasterreset,
            this.toolStripSeparator8,
            this.toolStripButtonStartHeater,
            this.toolStripSeparator7,
            this.toolStripButtonOptions});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(698, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonStart
            // 
            this.toolStripButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStart.Image = global::hjs_ecu_mini.Properties.Resources.Run;
            this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStart.Name = "toolStripButtonStart";
            this.toolStripButtonStart.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonStart.Text = "Verbindung starten";
            this.toolStripButtonStart.Click += new System.EventHandler(this.toolStripButtonStart_Click);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Enabled = false;
            this.toolStripButtonStop.Image = global::hjs_ecu_mini.Properties.Resources.Stop;
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonStop.Text = "Verbindung anhalten";
            this.toolStripButtonStop.Click += new System.EventHandler(this.toolStripButtonStop_Click);
            // 
            // toolStripButtonRead
            // 
            this.toolStripButtonRead.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRead.Image = global::hjs_ecu_mini.Properties.Resources.Refresh;
            this.toolStripButtonRead.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRead.Name = "toolStripButtonRead";
            this.toolStripButtonRead.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRead.Text = "Lesen";
            this.toolStripButtonRead.Click += new System.EventHandler(this.toolStripButtonRead_Click);
            // 
            // toolStripButtonTimer
            // 
            this.toolStripButtonTimer.CheckOnClick = true;
            this.toolStripButtonTimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTimer.Image = global::hjs_ecu_mini.Properties.Resources.clock;
            this.toolStripButtonTimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTimer.Name = "toolStripButtonTimer";
            this.toolStripButtonTimer.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonTimer.Text = "Dauerhaft lesen";
            this.toolStripButtonTimer.Click += new System.EventHandler(this.toolStripButtonTimer_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonReset
            // 
            this.toolStripButtonReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReset.Image = global::hjs_ecu_mini.Properties.Resources.Repeat;
            this.toolStripButtonReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReset.Name = "toolStripButtonReset";
            this.toolStripButtonReset.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonReset.Text = "Reboot (Cfg)";
            this.toolStripButtonReset.Click += new System.EventHandler(this.toolStripButtonReset_Click);
            // 
            // toolStripButtonRebootNot
            // 
            this.toolStripButtonRebootNot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRebootNot.Image = global::hjs_ecu_mini.Properties.Resources.Repeat_stop;
            this.toolStripButtonRebootNot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRebootNot.Name = "toolStripButtonRebootNot";
            this.toolStripButtonRebootNot.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRebootNot.Text = "Reboot (NotCfg)";
            this.toolStripButtonRebootNot.Click += new System.EventHandler(this.toolStripButtonRebootNot_Click);
            // 
            // toolStripButtonReconfigReboot
            // 
            this.toolStripButtonReconfigReboot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReconfigReboot.Image = global::hjs_ecu_mini.Properties.Resources.Repeat_plus;
            this.toolStripButtonReconfigReboot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReconfigReboot.Name = "toolStripButtonReconfigReboot";
            this.toolStripButtonReconfigReboot.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonReconfigReboot.Text = "Reconfig Reboot";
            this.toolStripButtonReconfigReboot.Click += new System.EventHandler(this.toolStripButtonReconfigReboot_Click);
            // 
            // toolStripButtonMasterreset
            // 
            this.toolStripButtonMasterreset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMasterreset.Image = global::hjs_ecu_mini.Properties.Resources.edit_bomb;
            this.toolStripButtonMasterreset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMasterreset.Name = "toolStripButtonMasterreset";
            this.toolStripButtonMasterreset.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonMasterreset.Text = "Masterreset";
            this.toolStripButtonMasterreset.Click += new System.EventHandler(this.toolStripButtonMasterreset_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonStartHeater
            // 
            this.toolStripButtonStartHeater.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStartHeater.Image = global::hjs_ecu_mini.Properties.Resources.Heater;
            this.toolStripButtonStartHeater.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStartHeater.Name = "toolStripButtonStartHeater";
            this.toolStripButtonStartHeater.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonStartHeater.Text = "Regenerationsstart";
            this.toolStripButtonStartHeater.Click += new System.EventHandler(this.toolStripButtonStartHeater_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonOptions
            // 
            this.toolStripButtonOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOptions.Image = global::hjs_ecu_mini.Properties.Resources.PropertiesHS;
            this.toolStripButtonOptions.ImageTransparentColor = System.Drawing.Color.Black;
            this.toolStripButtonOptions.Name = "toolStripButtonOptions";
            this.toolStripButtonOptions.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOptions.Text = "Einstellungen";
            this.toolStripButtonOptions.Click += new System.EventHandler(this.toolStripButtonOptions_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(212, 6);
            // 
            // konfigurationAufspielenToolStripMenuItem
            // 
            this.konfigurationAufspielenToolStripMenuItem.Name = "konfigurationAufspielenToolStripMenuItem";
            this.konfigurationAufspielenToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.konfigurationAufspielenToolStripMenuItem.Text = "Konfiguration aufspielen...";
            this.konfigurationAufspielenToolStripMenuItem.Click += new System.EventHandler(this.konfigurationAufspielenToolStripMenuItem_Click);
            // 
            // kennfelderAufspielenToolStripMenuItem
            // 
            this.kennfelderAufspielenToolStripMenuItem.Name = "kennfelderAufspielenToolStripMenuItem";
            this.kennfelderAufspielenToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.kennfelderAufspielenToolStripMenuItem.Text = "Kennfelder aufspielen...";
            this.kennfelderAufspielenToolStripMenuItem.Click += new System.EventHandler(this.kennfelderAufspielenToolStripMenuItem_Click);
            // 
            // sprachenAufspielenToolStripMenuItem
            // 
            this.sprachenAufspielenToolStripMenuItem.Name = "sprachenAufspielenToolStripMenuItem";
            this.sprachenAufspielenToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.sprachenAufspielenToolStripMenuItem.Text = "Sprachen aufspielen...";
            this.sprachenAufspielenToolStripMenuItem.Click += new System.EventHandler(this.sprachenAufspielenToolStripMenuItem_Click);
            // 
            // FrameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(698, 461);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.statusFrameStrip);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(500, 250);
            this.Name = "FrameForm";
            this.Text = "ECU-Diagnose";
            this.statusFrameStrip.ResumeLayout(false);
            this.statusFrameStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageOverview.ResumeLayout(false);
            this.tabPageOverview.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageValues.ResumeLayout(false);
            this.contextMenuStripActualValues.ResumeLayout(false);
            this.tabPageErrors.ResumeLayout(false);
            this.contextMenuStripError.ResumeLayout(false);
            this.tabPageBehave.ResumeLayout(false);
            this.tabPageRtc.ResumeLayout(false);
            this.tabPageEmpirical.ResumeLayout(false);
            this.tabPageDtc.ResumeLayout(false);
            this.tabPageDtc.PerformLayout();
            this.tabPageMaintainance.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabPageDirectOrder.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusFrameStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelVersion;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verbindungToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verbindungStartenToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageValues;
        private System.Windows.Forms.TabPage tabPageErrors;
        private System.Windows.Forms.TabPage tabPageBehave;
        private System.Windows.Forms.ListView listViewValues;
        private System.Windows.Forms.Button buttonValuesSelectNone;
        private System.Windows.Forms.Button buttonValueSelectAll;
        private System.Windows.Forms.ListView listViewValuesSelection;
        private System.Windows.Forms.Button buttonValuesSelectUnhidden;
        private System.Windows.Forms.ListView listViewErrors;
        private System.Windows.Forms.TabPage tabPageOverview;
        private System.Windows.Forms.TextBox textOverviewVersions;
        private System.Windows.Forms.TextBox textOverviewInfotext;
        private System.Windows.Forms.ToolStripMenuItem akquisitionAuslesenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarTotal;
        private System.Windows.Forms.ToolStripMenuItem parametersatzAufspielenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TextBox textBoxLastTimeStamp;
        private System.Windows.Forms.DateTimePicker dateTimePickerProgTime;
        private System.Windows.Forms.Button buttonSetTime;
        private System.Windows.Forms.TextBox textBoxSerialNumber;
        private System.Windows.Forms.Button buttonSetProductionData;
        private System.Windows.Forms.Label labelDegCelsius;
        private System.Windows.Forms.TextBox textBoxTempOffset;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripError;
        private System.Windows.Forms.ToolStripMenuItem fehlerLoeschenToolStripMenuItem;
        private System.Windows.Forms.Timer timerLesen;
        private System.Windows.Forms.ToolStripMenuItem timerStartenToolStripMenuItem;
        private System.Windows.Forms.Button buttonMasterReset;
        private System.Windows.Forms.ToolStripMenuItem alleZeigenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem fehlerringAuslesenToolStripMenuItem;
        private System.Windows.Forms.Button buttonReboot;
        private System.Windows.Forms.ListView listViewBehaves;
        private System.Windows.Forms.TabPage tabPageRtc;
        private System.Windows.Forms.ListView listViewVolatiles;
        private System.Windows.Forms.TabPage tabPageEmpirical;
        private System.Windows.Forms.ListView listViewEmpiricalValues;
        private System.Windows.Forms.ListView listViewEmpiricalGroups;
        private System.Windows.Forms.TabPage tabPageDtc;
        private System.Windows.Forms.TextBox textBoxDtcInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDtcFreezeFrame;
        private System.Windows.Forms.ListView listViewDtcStack;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxVorAlarmZeit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxVorAlarmDruck;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxHauptAlarmZeit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxHauptAlarmDruck;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSetAlarms;
        private System.Windows.Forms.TabPage tabPageMaintainance;
        private System.Windows.Forms.Button buttonDosieren;
        private System.Windows.Forms.TextBox textBoxDosierpulsAnzahl;
        private System.Windows.Forms.Button buttonFilterGereinigt;
        private System.Windows.Forms.Button buttonAdditivReset;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonSetRpmFactor;
        private System.Windows.Forms.TextBox textBoxDrehzahlFaktor;
        private System.Windows.Forms.Button buttonCalibrierRef;
        private System.Windows.Forms.Button buttonCalibrierDiff;
        private System.Windows.Forms.Button buttonRegenerieren;
        private System.Windows.Forms.Button buttonDisplayed;
        private System.Windows.Forms.ToolStripMenuItem lesenToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageDirectOrder;
        private System.Windows.Forms.RadioButton radioButtonKomDataFile;
        private System.Windows.Forms.RadioButton radioButtonKomParamData;
        private System.Windows.Forms.RadioButton radioButtonKomNoData;
        private System.Windows.Forms.TextBox textBoxKomParameter;
        private System.Windows.Forms.TextBox textBoxKomSteuerByte;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button buttonFindKomFile;
        private System.Windows.Forms.TextBox textBoxKomFile;
        private System.Windows.Forms.Button buttonKomSend;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelConnected;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTimer;
        private System.Windows.Forms.Button buttonRebootNotCfg;
        private System.Windows.Forms.ToolStripMenuItem istwerteSpeichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fehlerstackSpeichernToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button buttonTankGainSet;
        private System.Windows.Forms.ComboBox comboBoxTankGain;
        private System.Windows.Forms.Button buttonTankSignalSet;
        private System.Windows.Forms.ComboBox comboBoxTankSignal;
        private System.Windows.Forms.ToolStripMenuItem lernwerteSpeichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allesAuslesenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Button buttonRebootReconfigReset;
        private System.Windows.Forms.Button buttonAgrUmschalten;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripActualValues;
        private System.Windows.Forms.ToolStripMenuItem istwerte32BitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem istwerteEinheitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonReset;
        private System.Windows.Forms.ToolStripButton toolStripButtonStartHeater;
        private System.Windows.Forms.ToolStripButton toolStripButtonStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripButton toolStripButtonRead;
        private System.Windows.Forms.ToolStripButton toolStripButtonTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton toolStripButtonOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolStripButtonRebootNot;
        private System.Windows.Forms.ToolStripButton toolStripButtonReconfigReboot;
        private System.Windows.Forms.ToolStripButton toolStripButtonMasterreset;
        private System.Windows.Forms.TextBox textBoxDtcDeratingAndFlags;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem parametersatzLesenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konfigurationAufspielenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kennfelderAufspielenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sprachenAufspielenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    }
}

