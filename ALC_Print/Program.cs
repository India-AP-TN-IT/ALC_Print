using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ALC_Print
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //<<중복처리 
            bool isNew = true;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out isNew);
            if (isNew == false)
            {    // 중복실행시 처리
                MessageBox.Show("Duplicate Excution", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //>>

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmPrint());
        }
    }
}
