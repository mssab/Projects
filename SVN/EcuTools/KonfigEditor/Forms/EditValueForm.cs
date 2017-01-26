/*
 * Object: KonfigEditor.Forms.EditValueForm
 * Description: ECU diagnostics form
 * 
 * $LastChangedDate: 2015-02-17 12:11:31 +0100 (Di, 17 Feb 2015) $
 * $LastChangedRevision: 88 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/KonfigEditor/Forms/EditValueForm.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Windows.Forms;

namespace KonfigEditor.Forms
{
    /// <summary>Form for editing single values</summary>
    public partial class EditValueForm : Form
    {
        /// <summary>New Value</summary>
        public double NewValue;

        private Array mEnm;

        /// <summary>Constructor</summary>
        /// <param name="groupName">Group name (Task, init, ..)</param>
        /// <param name="itemName">Item name</param>
        /// <param name="itemValue">Value before editing</param>
        /// <param name="itemEnumValues">Array of selectable enum values</param>
        public EditValueForm(string groupName, string itemName, double itemValue, Array itemEnumValues)
        {
            InitializeComponent();

            NewValue = itemValue;
            mEnm = itemEnumValues;

            labelName.Text = groupName + "." + itemName;
            if (itemEnumValues != null)
            {
                foreach (object obj in itemEnumValues)
                {
                    comboBoxValue.Items.Add(obj.ToString());
                }
                comboBoxValue.SelectedIndex = (int)itemValue;
                if (comboBoxValue.SelectedIndex > 0)
                    labelPreviousValue.Text = itemEnumValues.GetValue((int)itemValue).ToString();
                textBoxValue.Visible = false;
            }
            else
            {
                labelPreviousValue.Text = itemValue.ToString();
                textBoxValue.Text = itemValue.ToString();
                comboBoxValue.Visible = false;
                textBoxValue.Select();
            }
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            if (comboBoxValue.Visible)
            {
                NewValue = (int)mEnm.GetValue(comboBoxValue.SelectedIndex);
            }
            else
            {
                NewValue = Double.Parse(textBoxValue.Text);
            }
        }

        private void textBoxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOkay_Click(this, new EventArgs());
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void EditValueForm_Load(object sender, EventArgs e)
        {

        }
    }
}
