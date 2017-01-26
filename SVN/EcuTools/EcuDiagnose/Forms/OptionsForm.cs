/*
 * Object: EcuDiagnose.Forms.OptionsForm
 * Description: Options dialog form
 * 
 * $LastChangedDate: 2013-12-02 10:31:16 +0100 (Mo, 02 Dez 2013) $
 * $LastChangedRevision: 26 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/EcuDiagnose/Forms/OptionsForm.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Windows.Forms;

namespace EcuDiagnose.Forms
{
    /// <summary>Options dialog form</summary>
    public partial class OptionsForm : Form
    {
        /// <summary>Default constructor</summary>
        public OptionsForm()
        {
            InitializeComponent();

            textBoxComPort.Text = Properties.Settings.Default.Port.ToString();
            textBoxTimerInterval.Text = Properties.Settings.Default.Timer.ToString();
            textBoxPassword.Text = Properties.Settings.Default.Key.ToString("X");
            foreach (byte s in Enum.GetValues(typeof(HJS.ECU.Port.Comm.ServerByte)))
            {
                comboBoxServerId.Items.Add((char)s);
            }
            comboBoxOptionLanguage.SelectedIndex = Properties.Settings.Default.Language;
            comboBoxServerId.Text = ((char)Properties.Settings.Default.ServerId).ToString();
            checkBoxLocalTime.Checked = Properties.Settings.Default.LocalTime;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            //save
            Properties.Settings.Default.Port = Convert.ToByte(textBoxComPort.Text);
            Properties.Settings.Default.Timer = Convert.ToUInt16(textBoxTimerInterval.Text);
            Properties.Settings.Default.Language = (byte)comboBoxOptionLanguage.SelectedIndex;
            Properties.Settings.Default.Key = Convert.ToUInt64(textBoxPassword.Text, 16);
            char[] _server = comboBoxServerId.Text.ToCharArray();
            Properties.Settings.Default.ServerId = (byte)_server[0];
            Properties.Settings.Default.LocalTime = checkBoxLocalTime.Checked;
            Properties.Settings.Default.Save();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}