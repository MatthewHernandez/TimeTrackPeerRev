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
    public partial class Form_login : Form
    {
        public Form_login()
        {
            InitializeComponent();
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

        
        private void button_login_Click(object sender, EventArgs e)
        {
            //call MY_DB.cs
            MY_DB db = new MY_DB();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table_adm = new DataTable(); //table to read admin info


            //MySql db read
            /*
            //previous version. check damin info then read student info. don't need to read std info on login page.
            MySqlCommand command = new MySqlCommand(@"
            SELECT s.*
            FROM student_info s 
            JOIN admin_info a
            ON a.adm_username = @in_un AND a.adm_password = @in_pw", db.getConnection);//("select * from student_info where admin_info.adm_username = @in_un and admin_info.adm_id = @in_id", db.getConnection);//("SELECT * FROM mydb.user_info where 'User_Name' = @usn AND 'Student_id' = @pass", db.getConnection);
            */

            //gxk220025 call admin info on sql then check login info exist.
            MySqlCommand command = new MySqlCommand(@"
            SELECT * 
            FROM professor 
            WHERE username = @in_un AND password = @in_pw", db.getConnection);


            //under line take text boxes' input information as login access data.
            command.Parameters.Add("@in_un", MySqlDbType.VarChar).Value = textBox_account.Text; //account id is username, temporary
            command.Parameters.Add("@in_pw", MySqlDbType.VarChar).Value = textBox_password.Text; //password temporary user ID. change it as DB column type

            adapter.SelectCommand = command;

            adapter.Fill(table_adm);

            //check read succesful.
            if (table_adm.Rows.Count > 0) //reading db exist
            {
                //gxk220025 
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
