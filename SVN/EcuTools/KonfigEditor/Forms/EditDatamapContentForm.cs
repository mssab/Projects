/*
 * Object: KonfigEditor.Forms.EditDatamapContentForm
 * Description: From for editing data map content
 * 
 * $LastChangedDate: 2015-02-17 12:11:31 +0100 (Di, 17 Feb 2015) $
 * $LastChangedRevision: 88 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/KonfigEditor/Forms/EditDatamapContentForm.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Windows.Forms;

namespace KonfigEditor.Forms
{
    /// <summary>From for editing data map content</summary>
    public partial class EditDatamapContentForm : Form
    {
        private HJS.ECU.Parameter.ParameterSet mParam;

        private Int16 XStart, XSize, YStart, YSize, ZStart,  ZSize;
        private UInt16 XSteps, YSteps, ZSteps;
        private int mDatamapPosition;

        /// <summary>Consrturctor</summary>
        /// <param name="param">Parameter set</param>
        /// <param name="datamapPosition">Position of datamap</param>
        /// <param name="SelectedRow">Selected row number</param>
        public EditDatamapContentForm(HJS.ECU.Parameter.ParameterSet param, int datamapPosition, int SelectedRow)
        {
            InitializeComponent();

            mParam = param;
            mDatamapPosition = datamapPosition;

            labelDatamapName.Text = String.Format("Kf ID={0} ({1})",
                mParam.GetDatamapIdentifier(datamapPosition),
                mParam.GetDatamapType(datamapPosition));

            mParam.GetDatamapAxis(datamapPosition,
                out XStart, out XSize, out XSteps,
                out YStart, out YSize, out YSteps,
                out ZStart, out ZSize, out ZSteps);
            numericUpDownX.Minimum = XStart;
            numericUpDownX.Increment = XSize;
            numericUpDownX.Maximum = XStart + XSteps * XSize;
            if (mParam.GetDatamapDimension(datamapPosition) > 0)
            {
                numericUpDownY.Minimum = YStart;
                numericUpDownY.Increment = YSize;
                numericUpDownY.Maximum = YStart + YSteps * YSize;
            }
            else
            {
                numericUpDownY.Enabled = false;
            }

            if (SelectedRow > 0)
            {
                numericUpDownY.Value = YStart + SelectedRow * YSize;
            }
            else
            {
                displayValue(0, 0);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonApply_Click(sender, e);
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            int x = (int)(numericUpDownX.Value - XStart);
            int y;
            if (mParam.GetDatamapDimension(mDatamapPosition) > 0)
            {
                y = (int)(numericUpDownY.Value - YStart);
            }
            else
            {
                y = 0;
            }
            mParam.SetDatamapValue(mDatamapPosition, (UInt16)(x / XSize), (UInt16)(y / YSize), UInt16.Parse(textBoxContent.Text));
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            int x = (int)(numericUpDownX.Value - XStart);
            int y;
            if (mParam.GetDatamapDimension(mDatamapPosition) > 0)
            {
                y = (int)(numericUpDownY.Value - YStart);
                if (numericUpDownY.Value < YStart) return;
            }else{
                y = 0;
            }
            displayValue((UInt16)(x / XSize), (UInt16)(y / YSize));
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            int x = (int)(numericUpDownX.Value - XStart);
            int y;
            if (mParam.GetDatamapDimension(mDatamapPosition) > 0)
            {
                y = (int)(numericUpDownY.Value - YStart);
            }else{
                y = 0;
            }
            displayValue((UInt16)(x / XSize), (UInt16)(y / YSize));
        }

        private void displayValue(UInt16 x, UInt16 y)
        {
            textBoxContent.Text = mParam.GetDatamapValue(mDatamapPosition, x, y).ToString();
        }
    }
}
