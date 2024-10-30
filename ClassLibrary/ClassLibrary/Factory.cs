/* Factory contains functions for creating and destroying Student and Professor entities and propagating
 * those functions onto the database.
 * Author:  Jesus Barrera-Gilabert III
 * Date:    10/29/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.2
 */

using Microsoft.Data.SqlClient;

namespace G81_Library
{
    //Creates and stores instances of Students and Professors in the database
    class PersonFactory
    {
        private string _SqlCon;
        private string _StudentDB;
        private string _ProfessorDB;

        //Constructor
        public PersonFactory(string sqlCon, string studentDB, string professorDB)
        {
            _SqlCon = sqlCon;
            _StudentDB = studentDB;
            _ProfessorDB = professorDB;
        }

        //Creates a Student entity and stores it in the database
        Student? CreateStudent(string fname, string lname, string netID, int utdID, CSClass cName, int group)
        {
            string sCom = "INSERT INTO" + _StudentDB + "([FirstName], [LastName], [NetID], [UTDID], [Password], [CName], [Group]) VALUES(@first, @last, @net, @utd, @pass, @cn, @group)";

            using (SqlConnection cnn = new SqlConnection(_SqlCon))
            {
                try
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(sCom))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@first", fname);
                            cmd.Parameters.AddWithValue("@last", lname);
                            cmd.Parameters.AddWithValue("@net", netID);
                            cmd.Parameters.AddWithValue("@utd", utdID);
                            cmd.Parameters.AddWithValue("@pass", Convert.ToString(utdID));
                            cmd.Parameters.AddWithValue("@cn", cName.name);
                            cmd.Parameters.AddWithValue("@group", group);

                            if(cmd.ExecuteNonQuery() <= 0)
                            {
                                return null;
                            }
                        }
                        catch
                        {
                            throw new Exception("Failed to Create SqlCommand Object");
                        }
                    }
                }
                catch
                {
                    throw new Exception("CreateStudent: Failed to Create SqlConnection Object");
                }
            }

                return new Student(fname, lname, utdID, netID, cName, group);
        }

        //Creates a Professor entity and stores it in the database
        Professor? CreateProfessor(string fname, string lname, string netID, int utdID)
        {
            string pCom = "INSERT INTO" + _ProfessorDB + "([FirstName], [LastName], [NetID], [UTDID], [Password]) VALUES(@first, @last, @net, @utd, @pass)";

            using (SqlConnection cnn = new SqlConnection(_SqlCon))
            {
                try
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(pCom))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@first", fname);
                            cmd.Parameters.AddWithValue("@last", lname);
                            cmd.Parameters.AddWithValue("@net", netID);
                            cmd.Parameters.AddWithValue("@utd", utdID);
                            cmd.Parameters.AddWithValue("@pass", Convert.ToString(utdID));
                            
                            if(cmd.ExecuteNonQuery() <= 0)
                            {
                                return null;
                            }
                        }
                        catch
                        {
                            throw new Exception("CreateProfessor: Failed to Create SqlCommand Object");
                        }
                        
                    }
                }
                catch
                {
                    throw new Exception("CreateProfessor: Failed to Create SqlConnection Object");
                }
            }

            return new Professor(fname, lname, utdID, netID);
        }
    }
}
