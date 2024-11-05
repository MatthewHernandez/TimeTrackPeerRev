using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_App_For_Professor
{
    public partial class Form_spsh_add : Form
    {

        // Delegate for passing data back to Form_spsh
        public delegate void AddStudentHandler(string firstName, string lastName, long id, string userName);
        public event AddStudentHandler StudentAdded;

        public Form_spsh_add()
        {
            InitializeComponent();
        }

        private void Form_spsh_add_Load(object sender, EventArgs e)
        {

        }



        bool verif()
        {
            if (long.TryParse(textBox_id.Text, out long studentId) &&
            !string.IsNullOrWhiteSpace(textBox_first.Text) &&
            !string.IsNullOrWhiteSpace(textBox_last.Text) &&
            !string.IsNullOrWhiteSpace(textBox_user.Text))
            //(textBox_email.Text.Trim() == ""))

            {
                return true;
            }
            return false;
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (verif())
            {
                // Pass data back to Form_spsh
                StudentAdded?.Invoke(textBox_first.Text, textBox_last.Text, Convert.ToInt64(textBox_id.Text), textBox_user.Text);
                // Close this form after adding
                this.Close();
            }
            else
            {
                
                    MessageBox.Show("Please enter valid data in all fields.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
