using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_App_For_Professor
{
    //gxk220025
    internal static class Program
    {
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            

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
