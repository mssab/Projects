using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;

namespace KonfigEditor.Controls
{
    /// <summary>User control for editable value texts</summary>
    public partial class LanguageValueUserControl : UserControl
    {
        /// <summary>Name of value</summary>
        public string ValueName;
        /// <summary>Unit of value</summary>
        public string Unit;
        /// <summary>Factor of value</summary>
        public byte Factor;
        /// <summary>Divisor of value</summary>
        public byte Divisor;
        /// <summary>Offset of value</summary>
        public string Offset;
        /// <summary>Alternative unit of value</summary>
        public string AltUnit;

        /// <summary>Constructor</summary>
        public LanguageValueUserControl()
        {
            InitializeComponent();
            ValueName = "N/A";
            Unit = "N/A";
            Factor = 0;
            Divisor = 0;
            Offset = "N/A";
            AltUnit = "N/A";
        }

        /// <summary>Refresh contents of controls with content of public variables</summary>
        public void UpdataData(bool SaveAndValidate)
        {
            if (SaveAndValidate)
            {
                ValueName = textBoxName.Text;
                Unit = textBoxUnit.Text;
                Factor = (byte)numericUpDownFactor.Value;
                Divisor = (byte)numericUpDownDivisor.Value;
                Offset = textBoxOffset.Text;
                AltUnit = textBoxAltUnit.Text;
            }
            else
            {
                textBoxName.Text = ValueName;
                textBoxUnit.Text = Unit;
                numericUpDownFactor.Value = Factor;
                numericUpDownDivisor.Value = Divisor;
                textBoxOffset.Text = Offset;
                textBoxAltUnit.Text = AltUnit;
            }
        }
    }
}
