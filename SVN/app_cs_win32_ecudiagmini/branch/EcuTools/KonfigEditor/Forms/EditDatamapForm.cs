/*
 * Object: KonfigEditor.Forms.EditDatamapForm
 * Description: ECU diagnostics form
 * 
 * $LastChangedDate: 2015-02-17 12:11:31 +0100 (Di, 17 Feb 2015) $
 * $LastChangedRevision: 88 $
 * $LastChangedBy: ksi $
 * $HeadURL: http://menden22/svn/devel/electronic/app_cs_win32_ecudiagmini/branch/EcuTools/KonfigEditor/Forms/EditDatamapForm.cs $
 * 
 * LastReviewDate: 
 * LastReviewRevision: 
 * LastReviewBy: 
 */
using System;
using System.Windows.Forms;

namespace KonfigEditor.Forms
{
    /// <summary>Edit datamap structure form</summary>
    public partial class EditDatamapForm : Form
    {
        /// <summary>Identifier</summary>
        public UInt32 Identifier;
        /// <summary>Start of X Axis</summary>
        public Int16 X_Start;
        /// <summary>Start of Y Axis</summary>
        public Int16 Y_Start;
        /// <summary>Start of Z Axis</summary>
        public Int16 Z_Start;
        /// <summary>Step size of X Axis</summary>
        public Int16 X_Size;
        /// <summary>Step size of Y Axis</summary>
        public Int16 Y_Size;
        /// <summary>Step size of Z Axis</summary>
        public Int16 Z_Size;
        /// <summary>Steps of X Axis</summary>
        public UInt16 X_Steps;
        /// <summary>Steps of Y Axis</summary>
        public UInt16 Y_Steps;
        /// <summary>Steps of Z Axis</summary>
        public UInt16 Z_Steps;
        /// <summary>Dimension of data map</summary>
        public byte Dimension;
        /// <summary>Type of data map</summary>
        public byte KfType;

        private Array mEnm;

        /// <summary>Default constructor</summary>
        public EditDatamapForm(UInt32 Id, HJS.ECU.Firmware.KennfeldTyp Type, byte Dim,
            Int16 XStart, Int16 XSize, UInt16 XSteps, Int16 YStart, Int16 YSize, UInt16 YSteps,
            Int16 ZStart, Int16 ZSize, UInt16 ZSteps, Array DatamapTypes)
        {
            InitializeComponent();

            Identifier = Id;
            Dimension = Dim;
            X_Start = XStart;
            X_Size = XSize;
            Y_Start = YStart;
            Y_Size = YSize;
            Z_Start = ZStart;
            Z_Size = ZSize;
            X_Steps = XSteps;
            Y_Steps = YSteps;
            Z_Steps = ZSteps;
            KfType = (byte)Type;
            mEnm = DatamapTypes;

            int sel = 0; int i = 0;
            foreach (object obj in DatamapTypes)
            {
                comboBoxType.Items.Add(obj.ToString());
                if ((HJS.ECU.Firmware.KennfeldTyp)obj == Type) sel = i;
                i++;
            }
            comboBoxType.SelectedIndex = sel;
            textBoxIdentifier.Text = Identifier.ToString();
            numericUpDownDimension.Value = Dimension;
            if (Dimension == 0 ) groupBox_X.Enabled = true; groupBox_Z.Enabled = false; groupBox1y.Enabled = false;
            if (Dimension == 1 ) groupBox_X.Enabled = true; groupBox1y.Enabled = true; groupBox_Z.Enabled = false;
            if (Dimension == 2) groupBox_X.Enabled = true; groupBox1y.Enabled = true; groupBox_Z.Enabled = true;
            
    
            numericUpDown_X_Start.Value = (UInt16)X_Start;
            numericUpDown_X_Stepsize.Value = (UInt16)X_Size;
            numericUpDown_X_Steps.Value = X_Steps;
            numericUpDown_Y_Start.Value = (UInt16)Y_Start;
            numericUpDown_Y_Stepsize.Value = (UInt16)Y_Size;
            numericUpDown_Y_Steps.Value = Y_Steps;
            numericUpDown_Z_Start.Value = (UInt16)Z_Start;
            numericUpDown_Z_Stepsize.Value = (UInt16)Z_Size;
            numericUpDown_Z1_Stepsize.Value = Z_Steps;
        }

        private void numericUpDownDimension_ValueChanged(object sender, EventArgs e)
        {





            
            if (numericUpDownDimension.Value == 2)
            {
                if (numericUpDownDimension.Value == 1)
                {
                    groupBox1y.Enabled = true; groupBox_Z.Enabled = false; 
                }
                else
                {
                    groupBox1y.Enabled = true; groupBox_Z.Enabled = true;
                }
                
            }
            else
            {
                groupBox1y.Enabled =true; groupBox_Z.Enabled = false;
            }
            if (numericUpDownDimension.Value == 0) { groupBox1y.Enabled = false; groupBox_Z.Enabled = false; }

           
        
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {


            //{


                Identifier = UInt32.Parse(textBoxIdentifier.Text);
                KfType = (byte)(int)mEnm.GetValue(comboBoxType.SelectedIndex);
                Dimension = (byte)numericUpDownDimension.Value;
                X_Start = (Int16)(UInt16)numericUpDown_X_Start.Value;
                X_Size = (Int16)(UInt16)numericUpDown_X_Stepsize.Value;
                X_Steps = (UInt16)numericUpDown_X_Steps.Value;
                Y_Start = (Int16)(UInt16)numericUpDown_Y_Start.Value;
                Y_Size = (Int16)(UInt16)numericUpDown_Y_Stepsize.Value;
                Y_Steps = (UInt16)numericUpDown_Y_Steps.Value;
                Z_Start = (Int16)(UInt16)numericUpDown_Z_Start.Value;
                Z_Size = (Int16)(UInt16)numericUpDown_Z_Stepsize.Value;
                Z_Steps = (UInt16)numericUpDown_Z1_Stepsize.Value;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            
            
            
            
            
            
            //}
            //catch
            //{
            //    MessageBox.Show("Enter Material Name Please.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}


           
        }

        private void numericUpDown_Y_Start_ValueChanged(object sender, EventArgs e)
        {

           
        }

        private void numericUpDown_X_Start_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1y_Enter(object sender, EventArgs e)
        {
            if (numericUpDownDimension.Value <1) groupBox1y.Enabled = false;
        }

        private void groupBox_Z_Enter(object sender, EventArgs e)
        {
            if (numericUpDownDimension.Value <= 1)  groupBox_Z.Enabled = false;
        }

        //private void numericUpDown_X_Start_ValueChanged_1(object sender, EventArgs e)
        //{


        //}

        private void numericUpDown_Y_Stepsize_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown_Y_Steps_ValueChanged(object sender, EventArgs e)
        {
            

        }

        private void numericUpDown_Z_Steps_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown_X_Steps_ValueChanged(object sender, EventArgs e)
        {


        }

        private void numericUpDown_Z_Stepsize_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void numericUpDown_Z_Start_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void EditDatamapForm_Load(object sender, EventArgs e)
        {

            numericUpDown_Y_Start.Value = 0;
            numericUpDown_Y_Steps.Value = 1;
            numericUpDown_Y_Stepsize.Value =0;
            numericUpDown_Z_Start.Value = 0;
            numericUpDown_Z1_Stepsize.Value = 1;
            numericUpDown_Z_Stepsize.Value = 0;


            // about groupbox
            if (numericUpDownDimension.Value == 2)
            {
                if (numericUpDownDimension.Value == 1)
                {
                    groupBox1y.Enabled = true; groupBox_Z.Enabled = false;
                }
                else
                {
                    groupBox1y.Enabled = true; groupBox_Z.Enabled = true;
                }

            }
            else
            {
                groupBox1y.Enabled = true; groupBox_Z.Enabled = false;
            }
            if (numericUpDownDimension.Value == 0) { groupBox1y.Enabled = false; groupBox_Z.Enabled = false; }

          
            return;
        }

        private void numericUpDown_X_Stepsize_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBoxIdentifier_TextChanged(object sender, EventArgs e)
        {

            

        }

        private void textBoxIdentifier_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {

            }
            else
            {
                e.Handled = e.KeyChar != (char)Keys.Back;
                MessageBox.Show("Nur Nummer !"," Warning !",MessageBoxButtons.OK);
                textBoxIdentifier.Clear();
            }
        }

        //private void numericUpDown_Y_Stepsize_ValueChanged_1(object sender, EventArgs e)
        //{
           
        //}

        //private void numericUpDown_Z_Stepsize_ValueChanged_1(object sender, EventArgs e)
        //{

        //}
    } 
}
