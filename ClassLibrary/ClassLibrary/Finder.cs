/* Searches the database for entities based on their primary key and returns them
 * Author:  Jesus Barrera-Gilabert III
 * Date:    11/25/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.1
 */

using MySql.Data.MySqlClient;

namespace G81_Library
{
    // Search database for student/professor
    public class PersonFinder
    {
        // Constructor
        public PersonFinder(string conSql)
        {
            ConSql = conSql;
        }

        // Database address
        public string ConSql {  get; set; }

        // Searches for a Student with id
        // Returns the student if it exists, null if it doesn't
        public Student? GetStudent(int id)
        {
            // Create and use MySqlConnection to the database using the provided address 
            using (var conn = new MySqlConnection(ConSql))
            {
                conn.Open();

                // SQL SELECT query
                string commandText = "SELECT s.id, s.username, s.first_name, s.last_name, s.password, s.class_id, s.team_id" +
                    "FROM student as s" +
                    "WHERE s.id = @id";

                // Create and use MySqlCommand using con and the insert query
                using (var cmd = new MySqlCommand(commandText, conn))
                {
                    // Add id parameter to the query
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        // Create and use MySqlCommand using con and the insert query
                        using (var rd = cmd.ExecuteReader())
                        {
                            // Read resulting student info
                            try
                            {
                                if (rd.Read())
                                {
                                    return new Student(     // Resulting student
                                        rd.GetString(2),    // first_name
                                        rd.GetString(3),    // last_name
                                        rd.GetInt32(0),     // id
                                        rd.GetString(1),    // username
                                        rd.GetInt32(5),     // class_id
                                        rd.GetInt32(6),     // team_id
                                        rd.GetString(4));   // password
                                }
                            }
                            // Read failed
                            catch
                            {
                                return null;
                            }
                        }
                    }
                    // Add parameter failed
                    catch
                    {
                        return null;
                    }
                }
            }
            return null;    // Student not found
        }

        // Searches for every Student in the database
        // Returns a list of all Student ids, null if no students
        public List<int>? GetAllStudents()
        {
            return null;
        }

        // Search for a Professor with id
        // Returns the Professor if it exists, null if it doesn't
        public Professor? GetProfessor(int id)
        {
            return null;
        }

        // Search for every Professor in the database
        // Returns a list of all Professor ids, null if no professors
        public List<int>? GetAllProfessors()
        {
            return null;
        }
    }

    // Search database for class
    public class ClassFinder
    {
        public ClassFinder(string conSql)
        {
            ConSql = conSql;
        }

        public string ConSql { get; set; }
    }

    // Search database for Peer Review Entry
    public class PREFinder
    {
        public PREFinder(string conSql)
        {
            ConSql = conSql;
        }

        public string ConSql { get; set; }
    }

    // Search database for Review Period
    public class RPFinder
    {
        public RPFinder(string conSql)
        {
            ConSql = conSql;
        }

        public string ConSql { get; set; }
    }

    // Search database for Time Entry
    public class TEFinder
    {
        public TEFinder(string conSql)
        {
            ConSql = conSql;
        }

        public string ConSql { get; set; }
    }
}
