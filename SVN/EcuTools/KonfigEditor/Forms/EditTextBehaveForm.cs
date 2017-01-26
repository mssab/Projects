/*
 * Object: KonfigEditor.Forms.EditTextBehaveForm
 * Description: Form for editing single language strings
 * 
 * $LastChangedDate: 2015-02-17 12:11:31 +0100 (Di, 17 Feb 2015) $
 * $LastChangedRevision: 88 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/KonfigEditor/Forms/EditTextBehaveForm.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Windows.Forms;

namespace KonfigEditor.Forms
{
    /// <summary>Form for editing single language strings</summary>
    public partial class EditTextBehaveForm : Form
    {
        private HJS.ECU.Parameter.ParameterSet mParam;
        private Controls.LanguageStringUserControl[] lsuc;

        /// <summary>Constructor</summary>
        /// <param name="param">Parameter set</param>
        /// <param name="BehavePosition">Position of behave</param>
        public EditTextBehaveForm(HJS.ECU.Parameter.ParameterSet param, int BehavePosition)
        {
            InitializeComponent();

            mParam = param;
            lsuc = new Controls.LanguageStringUserControl[param.GetUsedLanguages()];
            for (int i = 0; i < param.GetUsedLanguages(); i++)
            {
                lsuc[i] = new Controls.LanguageStringUserControl(param.GetLanguageId(i));
                lsuc[i].Dock = DockStyle.Fill;
                flowLayoutPanelStrings.Controls.Add(lsuc[i]);
            }
            numericUpDownPosition.Value = BehavePosition;
            numericUpDownPosition_ValueChanged(null, EventArgs.Empty);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonApply_Click(sender, e);
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < mParam.GetUsedLanguages(); i++)
            {
                lsuc[i].UpdataData(true);
                mParam.SetBehaveName(i, (int)numericUpDownPosition.Value, lsuc[i].LanguageString);
            }
        }

        private void numericUpDownPosition_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < mParam.GetUsedLanguages(); i++)
            {
                lsuc[i].LanguageString = mParam.GetBehaveName(i, (int)numericUpDownPosition.Value);
                lsuc[i].UpdataData(false);
            }
        }
    }
}
