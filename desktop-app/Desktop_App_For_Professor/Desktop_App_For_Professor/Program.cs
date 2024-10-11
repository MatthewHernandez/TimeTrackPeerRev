using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_App_For_Professor
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form_login());

            //start page is login form
            Form_login fLogin = new Form_login();

            //if login succesful, then move main form
            if(fLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Form_main());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
