/*
 * Object: EcuDiagnose.Forms.FrameForm
 * Description: Edit version number form
 * 
 * $LastChangedDate: 2015-02-17 12:11:31 +0100 (Di, 17 Feb 2015) $
 * $LastChangedRevision: 88 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/KonfigEditor/Forms/EditVersionForm.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Windows.Forms;

namespace KonfigEditor.Forms
{
    /// <summary>Edit version number form</summary>
    public partial class EditVersionForm : Form
    {
        /// <summary>New version</summary>
        public HJS.Block.VersionT newVersion;

        /// <summary>Constructor</summary>
        /// <param name="currentVersion">Current version</param>
        public EditVersionForm(string currentVersion)
        {
            InitializeComponent();
            string[] cv = currentVersion.Split('.');
            newVersion = new HJS.Block.VersionT();
            newVersion.Hauptversion = UInt16.Parse(cv[0]);
            newVersion.Nebenversion = byte.Parse(cv[1]);
            newVersion.Revision = byte.Parse(cv[2]);
            numericUpDownMain.Value = newVersion.Hauptversion;
            numericUpDownNeben.Value = newVersion.Nebenversion;
            numericUpDownRevision.Value = newVersion.Revision;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            newVersion.Hauptversion = (UInt16)numericUpDownMain.Value;
            newVersion.Nebenversion = (byte)numericUpDownNeben.Value;
            newVersion.Revision = (byte)numericUpDownRevision.Value;
        }
    }
}
