using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //to connect sql db

namespace Desktop_App_For_Professor
{
    //gxk220025
    public partial class Form_login : Form
    {
        public Form_login()
        {
            InitializeComponent();
            //for press enter key
            this.KeyPreview = true;  // Ensure form intercepts key presses
            this.KeyDown += new KeyEventHandler(Form_login_KeyDown);  // Attach KeyDown event handler
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label_account_id_Click(object sender, EventArgs e)
        {

        }

        private void Form_login_Load(object sender, EventArgs e)
        {

        }

        private void button_login_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        //gxk220025
        //login with press enter
        private void Form_login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // Check if Enter key is pressed
            {
                button_login_Click(sender, e);  // Trigger login
                e.Handled = true;  // Prevent default handling of the key
            }
        }
        //gxk220025
        private void button_login_Click(object sender, EventArgs e)
        {
            // Call MY_DB.cs
            MY_DB db = new MY_DB();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table_adm = new DataTable();  // Table to read admin info

            MySqlCommand command = new MySqlCommand(@"
            SELECT * 
            FROM professor 
            WHERE username = @in_un AND password = @in_pw", db.getConnection);

            // Take text boxes' input information as login access data
            command.Parameters.Add("@in_un", MySqlDbType.VarChar).Value = textBox_account.Text;
            command.Parameters.Add("@in_pw", MySqlDbType.VarChar).Value = textBox_password.Text;

            adapter.SelectCommand = command;

            adapter.Fill(table_adm);

            // Check read successful
            if (table_adm.Rows.Count > 0)  // Reading db exists
            {
                // Store the logged-in professor's data in the session class
                DataRow row = table_adm.Rows[0];  // Get the first (and only) row

                ProfessorSession.Username = row["username"].ToString();  // Store the username
                ProfessorSession.ProfessorId = Convert.ToInt32(row["id"]);  // Store the professor's ID
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Invalid Account or Password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox_account_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
