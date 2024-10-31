using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;//to read sql db


namespace Desktop_App_For_Professor
{
    internal class STUDENT
    {
        MY_DB db = new MY_DB();
        //gxk220025 create function to add a new student to database

        //current std info
        public static int Id { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string Email { get; set; }
        public static DateTime workDate { get; set; }
        public static Decimal workHour { get; set; }
        public static string workDescrip { get; set; }
        public static int workId { get; set; }


        // Optionally, you can add a method to clear the data if needed
        public static void ClearStudentInfo()
        {
            Id = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }

        //edit work hour
        public bool editWork() { return true; }

        //insert New Student info
        public bool insertStudent(String std_first, String std_last, String std_user, Int64 std_id, String std_email) // String std_tid //if use team_id
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO student(first_name, last_name, username, id, email, team_id) VALUES (@fn,@ln,@usr,@sid,@smail, @tid)", db.getConnection);

            //@fn,@ln,@usr,@sid,@smail
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = std_first;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = std_last;
            command.Parameters.Add("@usr", MySqlDbType.VarChar).Value = std_user;
            command.Parameters.Add("@sid", MySqlDbType.Int64).Value = std_id;
            command.Parameters.Add("@smail", MySqlDbType.VarChar).Value = std_email;
            command.Parameters.Add("@tid", MySqlDbType.VarChar).Value = null; //std_tid
            /* 
            //previous version without check duplicate id value

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }


            return true;
            */

            //gxk220025
            //check duplicate value for student id using try catch version
            try
            {
                db.openConnection();

                // Execute the command
                if (command.ExecuteNonQuery() == 1)
                {
                    db.closeConnection();
                    return true;
                }
                else
                {
                    db.closeConnection();
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                // Check for duplicate entry error code 1062
                if (ex.Number == 1062)
                {
                    // Notify the user about the duplicate ID
                    MessageBox.Show("Error: A student with this ID already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Handle other MySQL exceptions
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                db.closeConnection();
                return false;
            }
        }
    }
}
