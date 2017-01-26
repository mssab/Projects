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
using System.Collections.Generic;

namespace KonfigEditor.Forms
{

    /// <summary>Form for editing single values</summary>
    public partial class EditValueForm : Form
    {
        /// <summary>New Value</summary>
        public double NewValue;
        private Array mEnm;
        System.Windows.Forms.Button button1;
        

        // private EditValueForm lvwColumnSorter;
        /// <summary>Constructor</summary>
        /// <param name="groupName">Group name (Task, init, ..)</param>
        /// <param name="itemName">Item name</param>
        /// <param name="itemValue">Value before editing</param>
        /// <param name="itemEnumValues">Array of selectable enum values</param>
        public EditValueForm(string groupName, string itemName, double itemValue, Array itemEnumValues)
        {
            TextBox tx = new TextBox();
            InitializeComponent();
            NewValue = itemValue;
            mEnm = itemEnumValues;
            Width = 370;
            Height = 180;
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

            if (groupName == "taskAcquisition" && itemName == "bfMaskAQSave")
            {
                panelcheckbox.Visible = true;
                tabControl1.Visible = true;
                Width = 367;
                Height = 537;
                tabControl1.TabPages.Remove(tabPage2);
                
            }



            if (groupName == "taskPostDiagnose" && itemName == "uiBehaveOnRing")
            {
                
                textBoxValue.Visible = true;
                textBoxValue.ReadOnly = false;
                tabControl1.Visible = true;
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Insert(0,tabPage2);
                
                Width = 360;
                Height = 400;
                

                //if (bh.checkboxV.Checked = true)
                //{
                //    //itemValue = 4;
                //    textBoxValue.Text = itemValue.ToString();
                //}

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
           

        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            labeldisplay.Text = textBoxValue.Text;
            behaveDisplay.Text = textBoxValue.Text;
        }

       
      

        private void checkBox0_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
          
            
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
          
           
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }
     

        public void checkboxtest()
        {
            double j = 0;
            double x = 0;
            CheckBox[] boxes = new CheckBox[30];
            boxes[0] = checkBox0;
            boxes[1] = checkBox1;
            boxes[2] = checkBox2;
            boxes[3] = checkBox3;
            boxes[4] = checkBox4;
            boxes[5] = checkBox5;
            boxes[6] = checkBox6;
            boxes[7] = checkBox7;
            boxes[8] = checkBox8;
            boxes[9] = checkBox9;
            boxes[10] = checkBox10;
            boxes[11] = checkBox11;
            boxes[12] = checkBox12;
            boxes[13] = checkBox13;
            boxes[14] = checkBox14;
            boxes[15] = checkBox15;
            boxes[16] = checkBox16;
            boxes[17] = checkBox17;
            boxes[18] = checkBox18;
            boxes[19] = checkBox19;
            boxes[20] = checkBox20;
            boxes[21] = checkBox21;
            boxes[22] = checkBox22;
            boxes[23] = checkBox23;
            boxes[24] = checkBox24;
            boxes[25] = checkBox25;
            boxes[26] = checkBox26;
            boxes[27] = checkBox27;
            boxes[28] = checkBox28;
            boxes[29] = checkBox29;


            for (int i = 0; i <30; i++)
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

            textBoxValue.Text = x.ToString();
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
           


            for (int i = 0; i <16; i++)
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

            textBoxValue.Text = x.ToString();
        
        }

        public void checkboxTest()
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


            UInt32 uMaske = 0;

            UInt32.TryParse(textBoxValue.Text, out uMaske);
            if ((uMaske > 0) && (uMaske <= 65535))
                for (int i = 0; i < 16; i++)
                {
                    //double result=double.Parse(textBoxValue.Text);
                    if (boxes[i].Enabled)
                    {
                        // if (((double)Math.Pow(2, i)) / 2 == result / 2)

                        boxes[i].Checked = false;
                    }

                    if ((uMaske & (1 << i)) != 0)
                    {
                        boxes[i].Checked = true;
                    }
                   

                }

            else
            {

                MessageBox.Show("Keine gültige Zahl!");
            }

        }

        public void checkboxTest1()
        {
            CheckBox[] boxes = new CheckBox[30];
            boxes[0] = checkBox0;
            boxes[1] = checkBox1;
            boxes[2] = checkBox2;
            boxes[3] = checkBox3;
            boxes[4] = checkBox4;
            boxes[5] = checkBox5;
            boxes[6] = checkBox6;
            boxes[7] = checkBox7;
            boxes[8] = checkBox8;
            boxes[9] = checkBox9;
            boxes[10] = checkBox10;
            boxes[11] = checkBox11;
            boxes[12] = checkBox12;
            boxes[13] = checkBox13;
            boxes[14] = checkBox14;
            boxes[15] = checkBox15;
            boxes[16] = checkBox16;
            boxes[17] = checkBox17;
            boxes[18] = checkBox18;
            boxes[19] = checkBox19;
            boxes[20] = checkBox20;
            boxes[21] = checkBox21;
            boxes[22] = checkBox22;
            boxes[23] = checkBox23;
            boxes[24] = checkBox24;
            boxes[25] = checkBox25;
            boxes[26] = checkBox26;
            boxes[27] = checkBox27;
            boxes[28] = checkBox28;
            boxes[29] = checkBox29;

            UInt32 bit = 0;
            
            UInt32.TryParse(textBoxValue.Text, out bit);
            if ((bit > 0) && (bit <= 1073741823))
                for (int i = 0; i < 30; i++)
                {
                    if (boxes[i].Enabled)
                    {
                        boxes[i].Checked = false;
                    }

                    if ((bit & (1 << i)) != 0)
                    {
                        boxes[i].Checked = true;
                    }
                    

                }

            else
            {

                MessageBox.Show("Keine gültige Zahl!");
            }

        }
        
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void checkBox29_CheckedChanged(object sender, EventArgs e)
        {
            checkboxtest();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labeldisplay_Click(object sender, EventArgs e)
        {
            labeldisplay.Text = textBoxValue.Text;
        }

        private void buttoncheckboxrest_Click(object sender, EventArgs e)
        {
            checkBox0.Checked = false; checkBox10.Checked = false; checkBox20.Checked = false;
            checkBox1.Checked = false; checkBox11.Checked = false; checkBox21.Checked = false;
            checkBox2.Checked = false; checkBox12.Checked = false; checkBox22.Checked = false;
            checkBox3.Checked = false; checkBox13.Checked = false; checkBox23.Checked = false;
            checkBox4.Checked = false; checkBox14.Checked = false; checkBox24.Checked = false;
            checkBox5.Checked = false; checkBox15.Checked = false; checkBox25.Checked = false;
            checkBox6.Checked = false; checkBox16.Checked = false; checkBox26.Checked = false;
            checkBox7.Checked = false; checkBox17.Checked = false; checkBox27.Checked = false;
            checkBox8.Checked = false; checkBox18.Checked = false; checkBox28.Checked = false;
            checkBox9.Checked = false; checkBox19.Checked = false; checkBox29.Checked = false;
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            checkBox0.Checked = true; checkBox10.Checked = true; checkBox20.Checked = true;
            checkBox1.Checked = true; checkBox11.Checked = true; checkBox21.Checked = true;
            checkBox2.Checked = true; checkBox12.Checked = true; checkBox22.Checked = true;
            checkBox3.Checked = true; checkBox13.Checked = true; checkBox23.Checked = true;
            checkBox4.Checked = true; checkBox14.Checked = true; checkBox24.Checked = true;
            checkBox5.Checked = true; checkBox15.Checked = true; checkBox25.Checked = true;
            checkBox6.Checked = true; checkBox16.Checked = true; checkBox26.Checked = true;
            checkBox7.Checked = true; checkBox17.Checked = true; checkBox27.Checked = true;
            checkBox8.Checked = true; checkBox18.Checked = true; checkBox28.Checked = true;
            checkBox9.Checked = true; checkBox19.Checked = true; checkBox29.Checked = true;
        }

        private void label11_Click(object sender, EventArgs e)
        {

            
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox38_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave1_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave2_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave3_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void behaveDisplay_Click(object sender, EventArgs e)
        {
            
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox41_CheckedChanged(object sender, EventArgs e)
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

        private void Behave15_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
        }

        private void Behave16_CheckedChanged(object sender, EventArgs e)
        {
            BehaveAdd();
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

        private void btnTest_Click(object sender, EventArgs e)
        {
            checkboxTest();
        }

        private void btntest1_Click(object sender, EventArgs e)
        {
            checkboxTest1();
        }

        
        
    }
}
