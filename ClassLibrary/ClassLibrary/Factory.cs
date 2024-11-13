/* Factory contains functions for creating and destroying Student and Professor entities and propagating
 * those functions onto the database.
 * Author:  Jesus Barrera-Gilabert III
 * Date:    11/07/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.3
 */

using Microsoft.Data.SqlClient;

namespace G81_Library
{
    // Creates and stores instances of Students and Professors in the database
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

        // Creates a Student entity and stores it in the database; return the created instance
        public Student? CreateStudent(string fname, string lname, string netID, int utdID, PClass cID, int group)
        {
            string sCom = "INSERT INTO" + _StudentDB + "([FirstName], [LastName], [NetID], [UTDID], [Password], [CID], [Group]) VALUES(@first, @last, @net, @utd, @pass, @c, @group)";

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
                            cmd.Parameters.AddWithValue("@c", cID.ID);
                            cmd.Parameters.AddWithValue("@group", group);

                            if(cmd.ExecuteNonQuery() <= 0)
                            {
                                return null;
                            }
                        }
                        catch
                        {
                            throw new Exception("CreateStudent: Failed to Create SqlCommand Object");
                        }
                    }
                }
                catch
                {
                    throw new Exception("CreateStudent: Failed to Create SqlConnection Object");
                }
            }

                return new Student(fname, lname, utdID, netID, cID, group);
        }

        // Creates a Professor entity and stores it in the database; return the created instance
        public Professor? CreateProfessor(string fname, string lname, string netID, int utdID)
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

    // Create and store instances of PeerRevEntry in the database
    class PeerFactory
    {
        private string _sqlCon;
        private string _peerDB;

        // Constructor
        public PeerFactory(string sqlCon, string peerDB)
        {
            _sqlCon = sqlCon;
            _peerDB = peerDB;
        }

        // Create a PeerRevEntry intance and store in the database; return the created instance
        public PeerRevEntry? CreateReview(PClass pClass, Student reviewer, Student reviewee, string comment, DateOnly start, DateOnly end, int[] ranks)
        {
            string pCom = "INSERT INTO" + _peerDB + "([ReviewerID], [RevieweeID], [Comment], [Start], [End], [Rank]) VALUE(@rr, @re, @com, @s, @e, @rank)";

            using (SqlConnection cnn = new SqlConnection(_sqlCon))
            {
                try
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(pCom))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@rr", reviewer.UtdID);
                            cmd.Parameters.AddWithValue("@re", reviewee.UtdID);
                            cmd.Parameters.AddWithValue("@com", comment);
                            cmd.Parameters.AddWithValue("@s", start);
                            cmd.Parameters.AddWithValue("@e", end);
                            cmd.Parameters.AddWithValue("@rank", ranks[0]);

                            if (cmd.ExecuteNonQuery() <= 0)
                            {
                                return null;
                            }
                        }
                        catch
                        {
                            throw new Exception("CreateReview: Failed to Create SqlCommand Object");
                        }
                    }
                }
                catch
                {
                    throw new Exception("CreateReview: Failed to Create SqlConnection Object");
                }
            }

            return new PeerRevEntry(pClass, reviewer, reviewee, comment, start, end, ranks);
        }
    }

    // Create and store instances of TimeEntry in the database
    class TimeFactory
    {
        private string _sqlCon;
        private string _timeDB;

        // Constructor 
        public TimeFactory(string sqlCon, string timeDB)
        {
            _sqlCon = sqlCon;
            _timeDB = timeDB;
        }

        // Create TimeEntry instance and store in the database; return the created instance
        public TimeEntry? CreateTime(Student stu, TimeSpan time, DateOnly date, string comm)
        {
            string tCom = "INSERT INTO" + _timeDB + "([StudentID], [Time], [Date], [Comments]) VALUES(@stu, @time, @date, @comm)";

            using (SqlConnection cnn = new SqlConnection(_sqlCon))
            {
                try
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(tCom))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@stu", stu.UtdID);
                            cmd.Parameters.AddWithValue("@time", time);
                            cmd.Parameters.AddWithValue("@date", date);
                            cmd.Parameters.AddWithValue("@comm", comm);

                            if (cmd.Parameters.Count <= 0)
                            {
                                return null;
                            }
                        }
                        catch
                        {
                            throw new Exception("CreateTime: Failed to Create SqlCommand Object");
                        }
                    }
                }
                catch
                {
                    throw new Exception("CreateTime: Failed to Create SqlConnection Object");
                }
            }

            return new TimeEntry(stu, time, date, comm);
        }
    }
     
    //Create instances of PClass
    class ClassFactory
    {
        //Creates and returns PClass instances
        public PClass? CreatePClass(Professor prof, int id, string name, string semester, int year)
        {
            return new PClass(prof, id, name, semester, year);
        }
    }
}
