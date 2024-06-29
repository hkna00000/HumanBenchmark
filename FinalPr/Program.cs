using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalPr
{
    internal static class Program
    {
        static LoginInfo li = new LoginInfo();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (li.CurrentUser == null)
            {
                Application.Run(new Login());
            }
            else
            {
                Application.Run(new Form1());
            }
            
        }
    }
}
