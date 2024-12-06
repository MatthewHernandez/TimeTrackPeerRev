/* Factory contains classes and functions for creating/updating Student, Professor, PeerReviewEntry, and TimeEntry
 * entities and adding them to the database given to them 
 * Author:  Jesus Barrera-Gilabert III
 * Editor:  Matthew Hernandez
 * Date:    11/26/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.8
 */

using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Xml.Linq;
using static Mysqlx.Expect.Open.Types.Condition.Types;

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

        // Creates a Student entity and stores it in the database
        // Return the created instance (new Student)
        public Student? CreateStudent(int id, string username, string fname, string lname)
        {
            // Create a student with given parameters
            try
            {
                var student = new Student(fname, lname, id, username);

                // Create and use MySqlConnection to the database using the provided address
                using (var con = new MySqlConnection(SqlCon))
                {
                    con.Open();

                    // SQL insert query with duplicate handeling (don't update)
                    string commandText = "INSERT IGNORE INTO students (id, username, first_name, last_name, password, class_id, team_id) VALUES (@id, @username, @first_name, @last_name, @password, @class_id, @team_id) ";

                    // Create and use MySqlCommand using con and the insert query 
                    using (var cmd = new MySqlCommand(commandText, con))
                    {
                        // Add available parameters to the query
                        try
                        {
                            cmd.Parameters.AddWithValue("@id", student.ID);
                            cmd.Parameters.AddWithValue("@username", student.Username);
                            cmd.Parameters.AddWithValue("@password", student.Password);
                            cmd.Parameters.AddWithValue("@first_name", student.FirstName);
                            cmd.Parameters.AddWithValue("@last_name", student.LastName);

                            // Check if any rows were affected; change student to null if <= 0
                            if (0 >= cmd.ExecuteNonQuery())
                            {
                                student = null;
                            }
                        }

                        // Adding/Executing Query failed, so set student to null
                        catch
                        {
                            student = null;
                        }
                    }
                }

                return student; //returns null if task failed
            }

            // Create failed
            catch
            {
                return null;
            }
        }

        // Stores an existing Student in the database
        // Return the success status
        public bool CreateStudent(Student stu)
        {
            // Check if student is null
            if (stu == null)
            {
                return false;    // Invalid student
            }

            // Create and use MySqlConnection to the database using the provided address
            using (var con = new MySqlConnection(SqlCon))
            {
                con.Open();

                // SQL insert query with duplicate handeling (update)
                string commandText = "INSERT INTO students (id, username, first_name, last_name, password, class_id, team_id) VALUES (@id, @username, @first_name, @last_name, @password, @class_id, @team_id) " +
                    "ON DUPLICATE KEY UPDATE " +
                    "username = VALUES (@username), " +
                    "first_name = VALUES (@first_name), " +
                    "last_name = VAlUES (@last_name), " +
                    "password = VALUES (@password), " +
                    "class_id = VALUES (@class_id), " +
                    "team_id = VALUES (@team_id);";

                // Create and use MySqlCommand using con and the insert query
                using (var cmd = new MySqlCommand(commandText, con))
                {
                    // Add available parameters to the query
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", stu.ID);
                        cmd.Parameters.AddWithValue("@username", stu.Username);
                        cmd.Parameters.AddWithValue("@password", stu.Password);
                        cmd.Parameters.AddWithValue("@team_id", stu.TeamID);
                        cmd.Parameters.AddWithValue("@first_name", stu.FirstName);
                        cmd.Parameters.AddWithValue("@last_name", stu.LastName);
                        cmd.Parameters.AddWithValue("@class_id", stu.CID);

                        // Check if any rows were affected; return false if <= 0
                        if (0 >= cmd.ExecuteNonQuery())
                        {
                            return false;
                        }
                    }

                    // Adding/Executing Query failed, so return false
                    catch
                    {
                        return false;
                    }
                }
            }

            return true; 
        }

        // Creates a Professor entity and stores it in the database
        // Return the created instance (no password)
        public Professor? CreateProfessor(int id, string username, string fname, string lname)
        {
            // Create a professor with given parameters
            try
            {
                var prof = new Professor(fname, lname, id, username);

                // Create and use MySqlConnection to the database using the provided address
                using (var con = new MySqlConnection(SqlCon))
                {
                    con.Open();

                    // SQL insert query with duplicate handeling (don't update)
                    string commandText = "INSERT IGNORE INTO professors (id, username, first_name, last_name, password) VALUES (@id, @username, @first_name, @last_name, @password)";

                    // Create and use MySqlCommand using con and the insert query
                    using (var cmd = new MySqlCommand(commandText, con))
                    {
                        // Add available parameters to the query
                        try
                        {
                            cmd.Parameters.AddWithValue("@id", prof.ID);
                            cmd.Parameters.AddWithValue("@username", prof.Username);
                            cmd.Parameters.AddWithValue("@password", prof.Password);
                            cmd.Parameters.AddWithValue("@first_name", prof.FirstName);
                            cmd.Parameters.AddWithValue("@last_name", prof.LastName);

                            // Check if any rows were affected; set prof to null if <= 0
                            if (0 >= cmd.ExecuteNonQuery())
                            {
                                prof = null;
                            }
                        }

                        // Adding/Executing Query failed, so set prof to null
                        catch
                        {
                            prof = null;
                        }
                    }
                }

                return prof; //returns null if task failed
            }

            // Create failed
            catch
            {
                return null;
            }
        }

        // Stores an existing Professor in the database
        // Return the success status
        public bool CreateProfessor(Professor prof)
        {
            // Check if professor is null
            if (prof == null)
            {
                return false;    // Invalid professor
            }
            // Create and use MySqlConnection to the database using the provided address
            using (var con =new MySqlConnection(SqlCon))
            {
                con.Open();

                // SQL insert query with duplicate handeling (update)
                string commandText = "INSERT INTO professors (id, username, first_name, last_name, password) VALUES (@id, @username, @first_name, @last_name, @password)" +
                    "ON DUPLICATE KEY UPDATE " +
                    "username = VALUES (@username), " +
                    "first_name = VALUES (@first_name), " +
                    "last_name = VALUES (@last_name), " +
                    "password = VALUES (@password);";

                // Create and use MySqlCommand using con and the insert query
                using (var cmd = new MySqlCommand(commandText, con))
                {
                    // Add available parameters to the query
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", prof.ID);
                        cmd.Parameters.AddWithValue("@username", prof.Username);
                        cmd.Parameters.AddWithValue("@password", prof.Password);
                        cmd.Parameters.AddWithValue("@first_name", prof.FirstName);
                        cmd.Parameters.AddWithValue("@last_name", prof.LastName);

                        // Check if any rows were affected; return false if <= 0
                        if (0 >= cmd.ExecuteNonQuery())
                        {
                            return false;
                        }
                    }

                    // Adding/Executing Query failed, so return false
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
                    cmd.Parameters.AddWithValue("@p_num", review.PeriodID);
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
                    cmd.Parameters.AddWithValue("@prof", pClass.ProfessorID);
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
