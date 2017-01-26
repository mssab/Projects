using System;
using System.Windows.Forms;

namespace KonfigEditor.Controls
{
    /// <summary>User control for one editable string</summary>
    public partial class LanguageStringUserControl : UserControl
    {
        /// <summary>Content of form</summary>
        public string LanguageString;

        /// <summary>Constructor</summary>
        public LanguageStringUserControl(string LanguageId)
        {
            InitializeComponent();
            labelLanguageID.Text = LanguageId;
        }

        /// <summary>Refresh contents of controls with content of public variables</summary>
        public void UpdataData(bool SaveAndValidate)
        {
            if (SaveAndValidate)
            {
                LanguageString = textBoxLanguageString.Text;
            }
            else
            {
                textBoxLanguageString.Text = LanguageString;
            }
        }
    }
}
