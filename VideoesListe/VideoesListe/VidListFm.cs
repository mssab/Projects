using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoesListe
{
    public partial class VidListFm : Form
    {
        public VidListFm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            
        }

        private void VidListFm_Load(object sender, EventArgs e)
        {

        }

        private void Okbt_Click(object sender, EventArgs e)
        {

            if ((TextboxTitle.Text == "" ) || (TextboxZeit.Text==""))
            {
                MessageBox.Show("Bitte Geben Sie die  Namen oder  Zeit ein !","Fehler");
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

            }

            
        }

        private void TextboxTitle_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void Okbt_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void TextboxZeit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        
        }
    }
}
