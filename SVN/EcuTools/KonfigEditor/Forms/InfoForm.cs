/*
 * Object: KonfigEditor.Forms.InfoForm
 * Description: Information form
 * 
 * $LastChangedDate: 2015-02-17 12:11:31 +0100 (Di, 17 Feb 2015) $
 * $LastChangedRevision: 88 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/KonfigEditor/Forms/InfoForm.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Reflection;
using System.Windows.Forms;

namespace KonfigEditor.Forms
{
    /// <summary>Information form</summary>
    public partial class InfoForm : Form
    {
        /// <summary>Constructor</summary>
        public InfoForm()
        {
            InitializeComponent();
            string[] ver = Application.ProductVersion.Split('.');
            labelVersion.Text = String.Format("Version {0}.{1}.{2} (r{3})"
                , ver[0], ver[1], ver[2], ver[3]);
            labelCompany.Text = Application.CompanyName;
            labelCopyright.Text = "Alle Rechte vorbehalten.";
        }
    }
}
