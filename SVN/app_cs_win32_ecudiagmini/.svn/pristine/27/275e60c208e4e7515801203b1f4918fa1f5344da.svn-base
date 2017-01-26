using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EcuDiagnose
{
    static class Program
    {
        private static Forms.FrameForm mMainForm;
        private static Controller mController;
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mMainForm = new Forms.FrameForm();
            mController = new Controller(mMainForm);
            Application.Run(mMainForm);
        }
    }
}