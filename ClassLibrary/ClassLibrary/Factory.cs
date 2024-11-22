/* Factory contains classes and functions for creating/updating Student, Professor, PeerReviewEntry, and TimeEntry
 * entities and adding them to the database given to them 
 * Author:  Jesus Barrera-Gilabert III
 * Date:    11/21/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.5
 */

using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Xml.Linq;

namespace G81_Library
{
    // Creates and stores instances of Students and Professors in the database
    class PersonFactory
    {
        // Constructor
        public PersonFactory(string con)
        {
            SqlCon = con;
        }

        // Database address
        string SqlCon { get; set; }

        // Creates a Student entity and stores it in the database; return the created instance (new Student)
        public Student? CreateStudent(int utdID, string netID, string fname, string lname)
        {
            var student = new Student(fname, lname, utdID, netID);

            using (var con = new MySqlConnection(SqlCon))
            {
                con.Open();

                string commandText = "INSERT INTO students (id, username, password, team_id, first_name, last_name, class_id) VALUES (@id, @username, @password, 0, @first_name, @last_name, 0)";

                using (var cmd = new MySqlCommand(commandText, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", student.UtdID);
                        cmd.Parameters.AddWithValue("@username", student.NetID);
                        cmd.Parameters.AddWithValue("@password", student.Password);
                        cmd.Parameters.AddWithValue("@first_name", student.FirstName);
                        cmd.Parameters.AddWithValue("@last_name", student.LastName);
                    }
                    catch
                    {
                        student = null;
                    }
                }
            }

            return student;
        }

        // Creates a Student entity and stores it in the database; return the created instance (existing Student)
        public Student? CreateStudent(int utdID, string netID, string password, int group, string fname, string lname, int cID)
        {
            var student = new Student(fname, lname, utdID, netID, cID, group, password);

            using (var con = new MySqlConnection(SqlCon))
            {
                con.Open();

                string commandText = "INSERT INTO students (id, username, password, team_id, first_name, last_name, class_id) VALUES (@id, @username, @password, @team_id, @first_name, @last_name, @class_id)";

                using (var cmd = new MySqlCommand(commandText, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", student.UtdID);
                        cmd.Parameters.AddWithValue("@username", student.NetID);
                        cmd.Parameters.AddWithValue("@password", student.Password);
                        cmd.Parameters.AddWithValue("@team_id", student.Group);
                        cmd.Parameters.AddWithValue("@first_name", student.FirstName);
                        cmd.Parameters.AddWithValue("@last_name", student.LastName);
                        cmd.Parameters.AddWithValue("@class_id", student.CID);
                    }
                    catch
                    {
                        student = null;
                    }
                }
            }

            return student;
        }

        // Stores an existing Student in the database; return the success status
        public bool CreateStudent(Student student)
        {
            using (var con = new MySqlConnection(SqlCon))
            {
                con.Open();

                string commandText = "INSERT INTO students (id, username, password, team_id, first_name, last_name, class_id) VALUES (@id, @username, @password, @team_id, @first_name, @last_name, @class_id)";
                using (var cmd = new MySqlCommand(commandText, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", student.UtdID);
                        cmd.Parameters.AddWithValue("@username", student.NetID);
                        cmd.Parameters.AddWithValue("@password", student.Password);
                        cmd.Parameters.AddWithValue("@team_id", student.Group);
                        cmd.Parameters.AddWithValue("@first_name", student.FirstName);
                        cmd.Parameters.AddWithValue("@last_name", student.LastName);
                        cmd.Parameters.AddWithValue("@class_id", student.CID);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // Creates a Professor entity and stores it in the database; return the created instance (no password)
        public Professor? CreateProfessor(int utdID, string netID, string fname, string lname)
        {
            var prof = new Professor(fname, lname, utdID, netID);

            using (var con = new MySqlConnection(SqlCon))
            {
                con.Open();
                
                string commandText = "INSERT INTO professors (id, username, password, first_name, last_name) VALUES (@id, @username, @password, @first_name, @last_name)";

                using (var cmd = new MySqlCommand(commandText, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", prof.UtdID);
                        cmd.Parameters.AddWithValue("@username", prof.NetID);
                        cmd.Parameters.AddWithValue("@password", prof.Password);
                        cmd.Parameters.AddWithValue("@first_name", prof.FirstName);
                        cmd.Parameters.AddWithValue("@last_name", prof.LastName);
                    }
                    catch
                    {
                        prof = null;
                    }
                }
            }

            return prof;
        }

        // Creates a Professor entity and stores it in the database; return the created instance (password)
        public Professor? CreateProfessor(int utdID, string netID, string password, string fname, string lname)
        {
            var prof = new Professor(fname, lname, utdID, netID, password);
            using (var con = new MySqlConnection(SqlCon))
            {
                con.Open();
                
                string commandText = "INSERT INTO professors (id, username, password, first_name, last_name) VALUES (@id, @username, @password, @first_name, @last_name)";

                using (var cmd = new MySqlCommand(commandText, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", prof.UtdID);
                        cmd.Parameters.AddWithValue("@username", prof.NetID);
                        cmd.Parameters.AddWithValue("@password", prof.Password);
                        cmd.Parameters.AddWithValue("@first_name", prof.FirstName);
                        cmd.Parameters.AddWithValue("@last_name", prof.LastName);
                    }
                    catch
                    {
                        prof = null;
                    }
                }
            }

            return prof;
        }

        // Stores an existing Professor in the database; return the success status
        public bool CreateProfessor(Professor prof)
        {
            using (var con =new MySqlConnection(SqlCon))
            {
                con.Open();

                string commandText = "INSERT INTO professors (id, username, password, first_name, last_name) VALUES (@id, @username, @password, @first_name, @last_name)";

                using (var cmd = new MySqlCommand(commandText, con))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", prof.UtdID);
                        cmd.Parameters.AddWithValue("@username", prof.NetID);
                        cmd.Parameters.AddWithValue("@password", prof.Password);
                        cmd.Parameters.AddWithValue("@first_name", prof.FirstName);
                        cmd.Parameters.AddWithValue("@last_name", prof.LastName);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

    // Create and store instances of PeerRevEntry in the database
    class PeerFactory
    {
        // Constructor
        public PeerFactory(MySqlConnection con)
        {
            SqlCon = con;
        }

        // Database address
        MySqlConnection SqlCon { get; set; }

        // Create a PeerRevEntry intance and store in the database; return the created instance (empty ranks; no comments)
        public PeerRevEntry? CreateReview(int reviewer, int reviewee, int pNum)
        {
            string commandText = "INSERT INTO peer_review(period_number, reviewer_id, reviewee_id, qual_of_work_rating, timeliness_rating, teamwork_rating, eff_and_part_rating, communication_rating, comments) " +
                "VALUES(@p_num, @reviewer_id, @reviewee_id, 0, 0, 0, 0, 0, '')";

            using (var cmd = new MySqlCommand(commandText, SqlCon))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@p_num", pNum);
                    cmd.Parameters.AddWithValue("@reviewer_id", reviewer);
                    cmd.Parameters.AddWithValue("@reviewee_id", reviewee);
                }
                catch
                {
                    return null;
                }
            }
            return new PeerRevEntry(reviewer, reviewee, "", pNum, [0, 0, 0, 0, 0]);
        }

        // Create a PeerRevEntry intance and store in the database; return the created instance (ranks; comments)
        public PeerRevEntry? CreateReview(int reviewer, int reviewee, int pNum, int[] ranks, string comments)
        {
            string commandText = "INSERT INTO peer_review(period_number, reviewer_id, reviewee_id, qual_of_work_rating, timeliness_rating, teamwork_rating, eff_and_part_rating, communication_rating, comments) " +
                "VALUES(@p_num, @reviewer_id, @reviewee_id, @qual, @timeliness, @teamwork, @eff, @communication, @comments)";

            using (var cmd = new MySqlCommand(commandText, SqlCon))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@p_num", pNum);
                    cmd.Parameters.AddWithValue("@reviewer_id", reviewer);
                    cmd.Parameters.AddWithValue("@reviewee_id", reviewee);
                    cmd.Parameters.AddWithValue("@qual", ranks[0]);
                    cmd.Parameters.AddWithValue("@timeliness", ranks[1]);
                    cmd.Parameters.AddWithValue("@teamwork", ranks[2]);
                    cmd.Parameters.AddWithValue("@eff", ranks[3]);
                    cmd.Parameters.AddWithValue("@communication", ranks[4]);
                    cmd.Parameters.AddWithValue("@comments", comments);
                }
                catch
                {
                    return null;
                }
            }
            return new PeerRevEntry(reviewer, reviewee, comments, pNum, ranks);
        }

        // Store an existing PeerReviewEntry in the database; return the success state
        public bool CreateReview(PeerRevEntry review)
        {
            string commandText = "INSERT INTO peer_review(period_number, reviewer_id, reviewee_id, qual_of_work_rating, timeliness_rating, teamwork_rating, eff_and_part_rating, communication_rating, comments) " +
                "VALUES(@p_num, @reviewer_id, @reviewee_id, @qual, @timeliness, @teamwork, @eff, @communication, @comments)";

            using (var cmd = new MySqlCommand(commandText, SqlCon))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@p_num", review.PeriodNumber);
                    cmd.Parameters.AddWithValue("@reviewer_id", review.Reviewer);
                    cmd.Parameters.AddWithValue("@reviewee_id", review.Reviewee);
                    cmd.Parameters.AddWithValue("@qual", review.QualRating);
                    cmd.Parameters.AddWithValue("@timeliness", review.TimelinessRating);
                    cmd.Parameters.AddWithValue("@teamwork", review.TeamworkRating);
                    cmd.Parameters.AddWithValue("@eff", review.EffRating);
                    cmd.Parameters.AddWithValue("@communication", review.CommRating);
                    cmd.Parameters.AddWithValue("@comments", review.Comment);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }

    // Create and store instances of TimeEntry in the database
    class TimeFactory
    {
        // Constructor 
        public TimeFactory(MySqlConnection con)
        {
            SqlCon = con;
        }

        // Database address
        MySqlConnection SqlCon { get; set; }

        // Create TimeEntry instance and store in the database; return the created instance
        public TimeEntry? CreateTime(int stu, decimal time, DateTime date, string comm)
        {
            string commandText = "INSERT INTO student_hours(student_id, date, hours, comments) VALUES(@id, @date, @hours, @comments)";

            using (var cmd = new MySqlCommand(commandText, SqlCon))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@id", stu);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@hours", time);
                    cmd.Parameters.AddWithValue("@comments", comm);
                }
                catch
                {
                    return null;
                }
            }
            return new TimeEntry(stu, time, date, comm);
        }

        // Store an existing TimeEntry in the database; return the success state
        public bool CreateTime(TimeEntry entry)
        {
            string commandText = "INSERT INTO student_hours(student_id, date, hours, comments) VALUES(@id, @date, @hours, @comments)";

            using (var cmd = new MySqlCommand(commandText, SqlCon))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@id", entry.Stu);
                    cmd.Parameters.AddWithValue("@date", entry.Date);
                    cmd.Parameters.AddWithValue("@hours", entry.Time);
                    cmd.Parameters.AddWithValue("@comments", entry.Comments);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
     
    // Create instances of PClass
    class ClassFactory
    {
        // Constructor
        public ClassFactory(MySqlConnection con)
        {
            SqlCon = con;
        }

        //Database address
        MySqlConnection SqlCon { get; set; }

        // Creates PClass instances and stores them in the database; return the PClass instance
        public PClass? CreatePClass(int cID, int prof, string name, string semester, int year)
        {
            string commandText = "INSERT INTO class(id, professor_id, class_name, semester, year) VALUES(@id, @prof, @name, @semester, @year)";

            using (var cmd = new MySqlCommand(commandText, SqlCon))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@id", cID);
                    cmd.Parameters.AddWithValue("@prof", prof);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@semester", semester);
                    cmd.Parameters.AddWithValue("@year", year);
                }
                catch
                {
                    return null;
                }
            }
            return new PClass(prof, cID, name, semester, year);
        }

        // Stores them in the database; returns the success status
        public bool CreatePClass(PClass pClass)
        {
            string commandText = "INSERT INTO class(id, professor_id, class_name, semester, year) VALUES(@id, @prof, @name, @semester, @year)";

            using (var cmd = new MySqlCommand(commandText, SqlCon))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@id", pClass.ID);
                    cmd.Parameters.AddWithValue("@prof", pClass.Professor);
                    cmd.Parameters.AddWithValue("@name", pClass.Name);
                    cmd.Parameters.AddWithValue("@semester", pClass.Semester);
                    cmd.Parameters.AddWithValue("@year", pClass.Year);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
}
