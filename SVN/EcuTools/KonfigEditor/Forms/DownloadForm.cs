/*
 * Object: KonfigEditor.Forms.DownloadForm
 * Description: Download form for SVN files
 * 
 * $LastChangedDate: 2015-02-17 12:11:31 +0100 (Di, 17 Feb 2015) $
 * $LastChangedRevision: 88 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/KonfigEditor/Forms/DownloadForm.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Net;
using System.Windows.Forms;

namespace KonfigEditor.Forms
{
    /// <summary>Form for selecting file to download</summary>
    public partial class DownloadForm : Form
    {
        /// <summary>Selected file name</summary>
        public string Filename;
        /// <summary>User name</summary>
        public string User;
        /// <summary>Password</summary>
        public string Password;

        /// <summary>Default constructor</summary>
        public DownloadForm()
        {
            InitializeComponent();
            labelMessage.Text = "";
            textBoxUser.Select();
            this.Text = "Download von menden22";
        }

        private void buttonDownloadList_Click(object sender, EventArgs e)
        {
            fillComboBox();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Filename = comboBoxFilename.SelectedItem.ToString();
            User = textBoxUser.Text;
            Password = textBoxPassword.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void fillComboBox()
        {
            WebClient wc = new WebClient();
            try
            {
                //wc.Proxy = null;
                wc.Credentials = new NetworkCredential(textBoxUser.Text, textBoxPassword.Text);
                Cursor = Cursors.WaitCursor;
                string all = wc.DownloadString("http://menden22/production/list-ecu-kbf-files.php");
                comboBoxFilename.Items.AddRange(all.Split('\n'));
                comboBoxFilename.SelectedIndex = 0;
                Cursor = Cursors.Default;
            }
            catch (Exception Ex)
            {
                labelMessage.Text = string.Format("Error downloading file list:\r\n{0}", Ex.Message);
            }
        }

        private void DownloadForm_Load(object sender, EventArgs e)
        {

        }
    }
}
