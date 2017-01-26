using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace hjs_ecu_mini
{
    static class Program
    {
        private static FrameForm mMainForm;
        private static Controller mController;
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mMainForm = new FrameForm();
            mController = new Controller(mMainForm);
            Application.Run(mMainForm);
        }
    }
}