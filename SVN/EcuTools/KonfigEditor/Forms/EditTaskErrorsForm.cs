﻿/*
 * Object: KonfigEditor.Forms.EditTaskErrorsForm
 * Description: Form for editing task errors
 * 
 * $LastChangedDate: 2015-02-17 12:11:31 +0100 (Di, 17 Feb 2015) $
 * $LastChangedRevision: 88 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/KonfigEditor/Forms/EditTaskErrorsForm.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Windows.Forms;

namespace KonfigEditor.Forms
{
    /// <summary>Form for editing task errors</summary>
    public partial class EditTaskErrorsForm : Form
    {
        /// <summary>Value for task error 0</summary>
        public decimal err0;
        /// <summary>Value for task error 1</summary>
        public decimal err1;
        /// <summary>Value for task error 2</summary>
        public decimal err2;
        /// <summary>Value for task error 3</summary>
        public decimal err3;
        /// <summary>Value for task error 4</summary>
        public decimal err4;
        /// <summary>Value for task error 5</summary>
        public decimal err5;
        /// <summary>Value for task error 6</summary>
        public decimal err6;
        /// <summary>Flag if task error 0 is stored in ring memory</summary>
        public bool ring0;
        /// <summary>Flag if task error 1 is stored in ring memory</summary>
        public bool ring1;
        /// <summary>Flag if task error 2 is stored in ring memory</summary>
        public bool ring2;
        /// <summary>Flag if task error 3 is stored in ring memory</summary>
        public bool ring3;
        /// <summary>Flag if task error 4 is stored in ring memory</summary>
        public bool ring4;
        /// <summary>Flag if task error 5 is stored in ring memory</summary>
        public bool ring5;
        /// <summary>Flag if task error 6 is stored in ring memory</summary>
        public bool ring6;
        /// <summary>Reserved flag for task error 0</summary>
        public bool reserve1;
        /// <summary>Reserved flag for task error 1</summary>
        public bool reserve2;
        /// <summary>Reserved flag for task error 2</summary>
        public bool reserve3;
        /// <summary>Reserved flag for task error 3</summary>
        public bool reserve4;
        /// <summary>Reserved flag for task error 4</summary>
        public bool reserve5;
        /// <summary>Reserved flag for task error 5</summary>
        public bool reserve6;
        /// <summary>Reserved flag for task error 0</summary>
        public bool reserve7;

        /// <summary>Constructor</summary>
        /// <param name="taskName">Name of task</param>
        public EditTaskErrorsForm(string taskName)
        {
            InitializeComponent();
            labeltaskName.Text = taskName;
        }

        /// <summary>Refresh values from public variables</summary>
        public void RefreshValues()
        {
            numericUpDownError1.Value = err0;
            numericUpDownError2.Value = err1;
            numericUpDownError3.Value = err2;
            numericUpDownError4.Value = err3;
            numericUpDownError5.Value = err4;
            numericUpDownError6.Value = err5;
            numericUpDownError7.Value = err6;
            checkBoxRing1.Checked = ring0;
            checkBoxRing2.Checked = ring1;
            checkBoxRing3.Checked = ring2;
            checkBoxRing4.Checked = ring3;
            checkBoxRing5.Checked = ring4;
            checkBoxRing6.Checked = ring5;
            checkBoxRing7.Checked = ring6;
            checkBoxRes1.Checked = reserve1;
            checkBoxRes2.Checked = reserve2;
            checkBoxRes3.Checked = reserve3;
            checkBoxRes4.Checked = reserve4;
            checkBoxRes5.Checked = reserve5;
            checkBoxRes6.Checked = reserve6;
            checkBoxRes7.Checked = reserve7;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            err0 = numericUpDownError1.Value;
            err1 = numericUpDownError2.Value;
            err2 = numericUpDownError3.Value;
            err3 = numericUpDownError4.Value;
            err4 = numericUpDownError5.Value;
            err5 = numericUpDownError6.Value;
            err6 = numericUpDownError7.Value;
            ring0 = checkBoxRing1.Checked;
            ring1 = checkBoxRing2.Checked;
            ring2 = checkBoxRing3.Checked;
            ring3 = checkBoxRing4.Checked;
            ring4 = checkBoxRing5.Checked;
            ring5 = checkBoxRing6.Checked;
            ring6 = checkBoxRing7.Checked;
            reserve1 = checkBoxRes1.Checked;
            reserve2 = checkBoxRes2.Checked;
            reserve3 = checkBoxRes3.Checked;
            reserve4 = checkBoxRes4.Checked;
            reserve5 = checkBoxRes5.Checked;
            reserve6 = checkBoxRes6.Checked;
            reserve7 = checkBoxRes7.Checked;
        }
    }
}
