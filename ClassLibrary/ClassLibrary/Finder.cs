/* Searches the database for entities based on their primary key and returns them
 * Author:  Jesus Barrera-Gilabert III
 * Date:    11/26/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.2
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
            // Check that id is valid
            if (id < 0)
            {
                return null;    // Invalid id
            }

            // Create and use MySqlConnection to the database using the provided address 
            using (var conn = new MySqlConnection(ConSql))
            {
                conn.Open();

                // SQL SELECT query
                string commandText = "SELECT s.id, s.username, s.first_name, s.last_name, s.password, s.class_id, s.team_id " +
                    "FROM student as s " +
                    "WHERE s.id = @id;";

                // Create and use MySqlCommand using con and the insert query
                using (var cmd = new MySqlCommand(commandText, conn))
                {
                    // Add id parameter to the query
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        // Create and use a Reader from cmd to read results
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
        // Returns a list of all Student ids
        public List<int>? GetAllStudents()
        {
            var ids = new List<int>();  // List of student ids

            // Create and use MySqlConnection to the database using the provided address
            using (var conn = new MySqlConnection(ConSql))
            {
                conn.Open();

                // SQL SELECT query
                string commandText = "SELECT s.id " +
                    "FROM student as s " +
                    "ORDER BY s.last_name, s.first_name;";

                // Create and use MySqlCommand using conn and the select query
                using (var cmd = new MySqlCommand(commandText, conn))
                {
                    // Create and use a Reader from cmd to read results
                    using (var rd = cmd.ExecuteReader())
                    {
                        // Read resulting student ids
                        try
                        {
                            while (rd.Read())
                            {
                                ids.Add(rd.GetInt32(0));
                            }
                        }
                        // Read failed
                        catch
                        {
                            return null;
                        }
                    }
                }
            }

            // Checks if the list is empty
            if ( ids.Count > 0)
            {
                return ids; // Success (student ids)
            }
            else
            {
                return null;    // Failed (no student ids)
            }
        }

        // Search for a Professor with id
        // Returns the Professor if it exists, null if it doesn't
        public Professor? GetProfessor(int id)
        {
            // Check that id is valid
            if (id < 0)
            {
                return null;    // Invalid id
            }

            // Create and use MySqlConnection to the database using the provided address 
            using (var conn = new MySqlConnection(ConSql))
            {
                conn.Open();

                // SQL SELECT query
                string commandText = "SELECT p.id, p.username, p.first_name, p.last_name, p.password " +
                    "FROM professor as p " +
                    "WHERE p.id = @id;";

                // Create and use MySqlCommand using con and the insert query
                using (var cmd = new MySqlCommand(commandText, conn))
                {
                    // Add id parameter to the query
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        // Create and use a Reader from cmd to read results
                        using (var rd = cmd.ExecuteReader())
                        {
                            // Read resulting professor info
                            try
                            {
                                if (rd.Read())
                                {
                                    return new Professor(   // Resulting professor
                                        rd.GetString(2),    // first_name
                                        rd.GetString(3),    // last_name
                                        rd.GetInt32(0),     // id
                                        rd.GetString(1),    // username
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
            return null;    // Professor not found
        }

        // Search for every Professor in the database
        // Returns a list of all Professor ids, null if no professors
        public List<int>? GetAllProfessors()
        {
            var ids = new List<int>();  // List of professor ids

            // Create and use MySqlConnection to the database using the provided address
            using (var conn = new MySqlConnection(ConSql))
            {
                conn.Open();

                // SQL SELECT query
                string commandText = "SELECT p.id " +
                    "FROM professor as p " +
                    "ORDER BY p.last_name, p.first_name;";

                // Create and use MySqlCommand using conn and the select query
                using (var cmd = new MySqlCommand(commandText, conn))
                {
                    // Create and use a Reader from cmd to read results
                    using (var rd = cmd.ExecuteReader())
                    {
                        // Read resulting professor ids
                        try
                        {
                            while (rd.Read())
                            {
                                ids.Add(rd.GetInt32(0));
                            }
                        }
                        // Read failed
                        catch
                        {
                            return null;
                        }
                    }
                }
            }

            // Checks if the list is empty
            if (ids.Count > 0)
            {
                return ids; // Success (professor ids)
            }
            else
            {
                return null;    // Failed (no professor ids)
            }
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
