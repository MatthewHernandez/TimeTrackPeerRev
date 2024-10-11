using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Desktop_App_For_Professor
{
    public partial class Form_std_add : Form
    {
        public Form_std_add()
        {
            InitializeComponent();
        }

        private void stdadd_button_cancle_Click(object sender, EventArgs e)
        {
            Close();
        }

       

        //function to verify data   
        bool verif()
        {
            if ((textBox_username.Text.Trim() == "") ||
                (textBox_id.Text.Trim() == "") ||
                (textBox_first.Text.Trim() == "") ||
                (textBox_last.Text.Trim() == "") ||
                (textBox_email.Text.Trim() == ""))
            {
                return false;
            }
            return true;
        }
        private void stdadd_button_confirm_Click(object sender, EventArgs e)
        {
            //add new student
            STUDENT student = new STUDENT();
            String stdusr = textBox_username.Text;
            Int64 stdid = Convert.ToInt64(textBox_id.Text);
            String stdf = textBox_first.Text;
            String stdl = textBox_last.Text;
            String stdm = textBox_email.Text;

            if (verif())
            {
                if (student.insertStudent(stdf, stdl, stdusr, stdid, stdm))
                {
                    MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //blank text box for next input
                    textBox_first.Text = null;
                    textBox_last.Text = null;
                    textBox_username.Text = null;
                    textBox_id.Text = null;
                    textBox_email.Text = null;
                }
                else
                {
                    MessageBox.Show("Fail to add student", "Add Student Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Field Empty", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void addlast_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form_std_add_Load(object sender, EventArgs e)
        {

        }
    }
}
