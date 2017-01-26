using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KonfigEditor
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
           

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new KonfigEditor.Forms.KonfigEditorMainForm(args));
            


            }

            }
    }

