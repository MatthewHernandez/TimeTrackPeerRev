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
        //gxk220025
        // Delegate for passing data back to Form_spsh
        public delegate void AddStudentHandler(string firstName, string lastName, int id, string userName);
        public event AddStudentHandler StudentAdded;

        public Form_spsh_add()
        {
            InitializeComponent();
        }

        private void Form_spsh_add_Load(object sender, EventArgs e)
        {

        }


        //gxk220025
        bool verif()
        {
            if (int.TryParse(textBox_id.Text, out int studentId) &&
            !string.IsNullOrWhiteSpace(textBox_first.Text) &&
            !string.IsNullOrWhiteSpace(textBox_last.Text) &&
            !string.IsNullOrWhiteSpace(textBox_user.Text))
            //(textBox_email.Text.Trim() == ""))

            {
                return true;
            }
            return false;
        }

        //gxk220025
        private void button_add_Click(object sender, EventArgs e)
        {
            if (verif())
            {
                // Pass data back to Form_spsh
                StudentAdded?.Invoke(textBox_first.Text, textBox_last.Text, Convert.ToInt32(textBox_id.Text), textBox_user.Text);
                // Close this form after adding
                this.Close();
            }
            else
            {
                
                    MessageBox.Show("Please enter valid data in all fields.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
        }

        //gxk220025
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
