using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hjs_ecu_mini.Forms
{
    /// <summary>
    /// Options dialog form
    /// </summary>
    public partial class OptionsForm : Form
    {
        /// <summary>
        /// Default constructor
        /// </summary>
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