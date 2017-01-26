/*
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
using System.Collections.Generic;
using HJS.ECU;
using HJS.ECU.Parameter;

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
        //EditValueForm fillbehave = new EditValueForm();
        private HJS.ECU.Parameter.ParameterSet mParam;
        //private HJS.ECU.Parameter.ParameterSet treeViewParam;
        /// <summary>Constructor</summary>
        /// 
        /// <param name="taskName">Name of task</param>
        public EditTaskErrorsForm(string taskName)
        {
            InitializeComponent();
            labeltaskName.Text = taskName;
            //
       
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

        public void buttonOK_Click(object sender, EventArgs e)
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
            
           //EditValueForm 
            }
         
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBoxRing1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRes7_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void EditTaskErrorsForm_Load(object sender, EventArgs e)
        {
           
           
           
        }

        private void checkBoxRes1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BehavRing_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

      

        private void behave1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            evf= new EditValueForm("taskPostDiagnose", "uiBehaveOnRing", 0, null);
            evf.ShowDialog();
            
        }

        private void checkboxV_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Behave1_CheckedChanged_1(object sender, EventArgs e)
        {
            BehaveAdd();
        }
        public void BehaveAdd()
        {

            double j = 0;
            double x = 0;
            CheckBox[] boxes = new CheckBox[16];
            boxes[0] = Behave1;
            boxes[1] = Behave2;
            boxes[2] = Behave3;
            boxes[3] = Behave4;
            boxes[4] = Behave5;
            boxes[5] = Behave6;
            boxes[6] = Behave7;
            boxes[7] = Behave8;
            boxes[8] = Behave9;
            boxes[9] = Behave10;
            boxes[10] = Behave11;
            boxes[11] = Behave12;
            boxes[12] = Behave13;
            boxes[13] = Behave14;
            boxes[14] = Behave15;
            boxes[15] = Behave16;



            for (int i = 0; i < 16; i++)
            {
                if (boxes[i].Checked == true && boxes[i].Enabled)
                {
                    x = (double)Math.Pow(2, i);

                    x = x + j;
                    j = x;
                }
                else
                {

                    j = 0;

                }

            }

            behaveDisplayR.Text = x.ToString();

        }

        private void SelAll_Click(object sender, EventArgs e)
        {
            Behave1.Checked = true; Behave9.Checked = true;
            Behave2.Checked = true; Behave10.Checked = true;
            Behave3.Checked = true; Behave11.Checked = true;
            Behave4.Checked = true; Behave12.Checked = true;
            Behave5.Checked = true; Behave13.Checked = true;
            Behave6.Checked = true; Behave14.Checked = true;
            Behave7.Checked = true; Behave15.Checked = true;
            Behave8.Checked = true; Behave16.Checked = true;  
          
        }

        private void restAll_Click(object sender, EventArgs e)
        {
            Behave1.Checked = false; Behave9.Checked = false;
            Behave2.Checked = false; Behave10.Checked = false;
            Behave3.Checked = false; Behave11.Checked = false;
            Behave4.Checked = false; Behave12.Checked = false;
            Behave5.Checked = false; Behave13.Checked = false;
            Behave6.Checked = false; Behave14.Checked = false;
            Behave7.Checked = false; Behave15.Checked = false;
            Behave8.Checked = false; Behave16.Checked = false;  
        }

        
        private void Behave2_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave3_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave4_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave5_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave6_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave7_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave8_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave9_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave10_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave11_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave12_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave13_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave14_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave15_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave16_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }
    }

}
