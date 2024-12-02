using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Desktop_App_For_Professor
{
    public partial class Form_spsh_ch : Form
    {
        //gxk220025
        // Delegate for confirming changes and passing new data back
        public delegate void ChangeConfirmedHandler(string firstName, string lastName, int id, string userName);
        public event ChangeConfirmedHandler ChangeConfirmed;

        //gxk220025
        public Form_spsh_ch(int studentId, string firstName, string lastName, string userName)
        {
            InitializeComponent();
            // Set current values in text boxes to display
            textBoxID.Text = studentId.ToString();
            textBoxFirstName.Text = firstName;
            textBoxLastName.Text = lastName;
            textBoxUsername.Text = userName;
        }

        private void Form_spsh_ch_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        //gxk220025
        private void button_confirm_Click(object sender, EventArgs e)
        {
            // Validate and collect updated data
            if (int.TryParse(textBoxIDIn.Text, out int newId) &&
                !string.IsNullOrWhiteSpace(textBoxFirstNameIn.Text) &&
                !string.IsNullOrWhiteSpace(textBoxLastNameIn.Text) &&
                !string.IsNullOrWhiteSpace(textBoxUsernameIn.Text))
            {
                // Trigger the ChangeConfirmed event to pass new values back to Form_spsh
                ChangeConfirmed?.Invoke(textBoxFirstNameIn.Text, textBoxLastNameIn.Text, newId, textBoxUsernameIn.Text);

                // Close the form after confirming the change
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

        //gxk220025
        private void button_copy_Click(object sender, EventArgs e)
        {
            textBoxIDIn.Text = textBoxID.Text;
            textBoxFirstNameIn.Text = textBoxFirstName.Text;
            textBoxLastNameIn.Text = textBoxLastName.Text;
            textBoxUsernameIn.Text = textBoxUsername.Text;
        }

        //gxk220025
        private void button_clean_Click(object sender, EventArgs e)
        {
            textBoxIDIn.Text = null;
            textBoxFirstNameIn.Text = null;
            textBoxLastNameIn.Text = null;
            textBoxUsernameIn.Text = null;
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
    

