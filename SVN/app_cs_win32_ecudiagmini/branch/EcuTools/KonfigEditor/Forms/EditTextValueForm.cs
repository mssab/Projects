/*
 * Object: KonfigEditor.Forms.EditTextValueForm
 * Description: Form for editing value texts
 * 
 * $LastChangedDate: 2015-02-17 12:11:31 +0100 (Di, 17 Feb 2015) $
 * $LastChangedRevision: 88 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/KonfigEditor/Forms/EditTextValueForm.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Windows.Forms;

namespace KonfigEditor.Forms
{
    /// <summary>Form for editing value texts</summary>
    public partial class EditTextValueForm : Form
    {
        private HJS.ECU.Firmware mFirmware;
        private HJS.ECU.Parameter.ParameterSet mParam;
        private Controls.LanguageValueUserControl[] lvuc;
        private TabPage[] tp;

        /// <summary>Constructor</summary>
        /// <param name="param">Parameter set</param>
        /// <param name="ValuePosition">Position of value</param>
        public EditTextValueForm(HJS.ECU.Parameter.ParameterSet param, int ValuePosition)
        {
            InitializeComponent();

            mParam = param;
            mFirmware = new HJS.ECU.Firmware(param.GetConfigCompatibility());
            numericUpDownPosition.Maximum = mFirmware.GetValueNumber();

            lvuc = new Controls.LanguageValueUserControl[param.GetUsedLanguages()];
            tp = new TabPage[param.GetUsedLanguages()];
            for (int i = 0; i < param.GetUsedLanguages(); i++)
            {
                lvuc[i] = new Controls.LanguageValueUserControl();
                lvuc[i].Dock = DockStyle.Fill;
                //lvuc[i]. inhalt
                tp[i] = new TabPage(param.GetLanguageId(i));
                tp[i].Controls.Add(lvuc[i]);
                tabControlText.TabPages.Add(tp[i]);
            }
            tabControlText.SelectedTab = tp[0];

            numericUpDownPosition.Value = ValuePosition;
            numericUpDownPosition_ValueChanged(null, EventArgs.Empty);
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < mParam.GetUsedLanguages(); i++)
            {
                lvuc[i].UpdataData(true);
                // @todo: returnwerte!

                mParam.SetValueName(i, (int)numericUpDownPosition.Value, lvuc[i].ValueName);
                mParam.SetValueUnit(i, (int)numericUpDownPosition.Value, lvuc[i].Unit);
                mParam.SetValueAltUnit(i, (int)numericUpDownPosition.Value, lvuc[i].AltUnit);
                mParam.SetValueFactor(i, (int)numericUpDownPosition.Value, lvuc[i].Factor);
                mParam.SetValueDivisor(i, (int)numericUpDownPosition.Value, lvuc[i].Divisor);
                mParam.SetValueOffset(i, (int)numericUpDownPosition.Value, lvuc[i].Offset);
                mParam.SetValueDisplayed(i, (int)numericUpDownPosition.Value, checkBoxDisplay.Checked);
                mParam.SetValueHidden(i, (int)numericUpDownPosition.Value, checkBoxHidden.Checked);
                mParam.SetValueGroup(i, (int)numericUpDownPosition.Value, checkBoxCalculated.Checked);
                mParam.SetValueSigned(i, (int)numericUpDownPosition.Value, checkBoxSigned.Checked);
                mParam.SetValueHex(i, (int)numericUpDownPosition.Value, checkBoxHex.Checked);
                mParam.SetValueDecimals(i, (int)numericUpDownPosition.Value, (int)numericUpDownDezStellen.Value);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonApply_Click(sender, e);
        }

        private void numericUpDownPosition_ValueChanged(object sender, EventArgs e)
        {
            labelMrwId.Text = mFirmware.GetMessWertString((int)numericUpDownPosition.Value);

            // flags
            checkBoxDisplay.Checked = mParam.IsValueDisplayed(0, (int)numericUpDownPosition.Value);
            checkBoxHidden.Checked = mParam.IsValueHidden(0, (int)numericUpDownPosition.Value);
            checkBoxCalculated.Checked = mParam.IsValueGroup(0, (int)numericUpDownPosition.Value);
            checkBoxSigned.Checked = mParam.IsValueSigned(0, (int)numericUpDownPosition.Value);
            checkBoxHex.Checked = mParam.IsValueHexadecimal(0, (int)numericUpDownPosition.Value);
            numericUpDownDezStellen.Value = mParam.GetValueDecimals(0, (int)numericUpDownPosition.Value);

            // tabpages
            for (int i = 0; i < mParam.GetUsedLanguages(); i++)
            {
                lvuc[i].ValueName = mParam.GetValueName(i, (int)numericUpDownPosition.Value);
                lvuc[i].Unit = mParam.GetValueUnit(i, (int)numericUpDownPosition.Value, false);
                lvuc[i].Factor = mParam.GetValueFaktor(i, (int)numericUpDownPosition.Value);
                lvuc[i].Divisor = mParam.GetValueDivisor(i, (int)numericUpDownPosition.Value);
                lvuc[i].Offset = mParam.GetValueOffset(i, (int)numericUpDownPosition.Value);
                lvuc[i].AltUnit = mParam.GetValueUnit(i, (int)numericUpDownPosition.Value, true);
                lvuc[i].UpdataData(false);
            }
        }

        private void numericUpDownDezStellen_ValueChanged(object sender, EventArgs e)
        {
            if(numericUpDownDezStellen.Value > 6)
            {
                checkBoxHex.Checked = true;
                numericUpDownDezStellen.Enabled = false;
            }
            else
            {
                numericUpDownDezStellen.Enabled = true;
            }
        }

        private void checkBoxHex_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHex.Checked)
            {
                numericUpDownDezStellen.Value = 0;
                numericUpDownDezStellen.Enabled = false;
            }
            else
            {
                numericUpDownDezStellen.Enabled = true;
            }
        }
    }
}
