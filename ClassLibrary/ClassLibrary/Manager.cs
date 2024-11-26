/* Manager implements functionallity from other classes to provide the simplified add and modify funcionts 
 * between predefined entities and a database accross multiple manager classes
 * Author:  Jesus Barrera-Gilabert III
 * Date:    11/26/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.3
 */

using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace G81_Library
{
    // Provide add and modify functions for Student and Professor entities
    public class PersonManager
    {
        // Constructor
        public PersonManager(string conStr)
        {
            ConnStr = conStr;
        }

        // Database address
        public string ConnStr { get; set; }

        // Add/Update a student/professor into the database
        // Returns success status
        public bool AddUpdate(IPerson person)
        {

            //Check if person is null
            if (person == null)
            {
                return false;   // Failed
            }

            var fac = new PersonFactory(ConnStr);

            // Check if person is a Student
            if(person.GetType() == typeof(Student))
            {
                // Convert to Student and pass to CreateStudent
                try
                {
                    Student stu = (Student)person;
                    return fac.CreateStudent(stu);  // Add to database and return result
                }
                // Convert or pass failed
                catch
                {
                    return false;
                }
            }
            else    // person is a Professor
            {
                // Convert to Professor and pass to CreateProfessor
                try
                {
                    Professor prof = (Professor)person;
                    return fac.CreateProfessor(prof);   // Add to database and return result
                }
                // Convert or pass failed
                catch
                {
                    return false;
                }
            }
        }

        // Creates new Student/Professor entities from a csv file
        // person = Student ('s')/Professor ('p'); Default value: person = Student
        // Returns the list of lines that failed to be added
        public List<int>? AddFromFile(string filePath, char person = 's')
        {
            // Check that filePath is not empty
            if(filePath.Length < 1)
            {
                return null;    //Invalid filePath
            }

            var factory = new PersonFactory(ConnStr);
            var failed = new List<int>(); // List of indexes that failed to get added (empty if total success)

            using (var rd = new StreamReader(filePath))
            {
                var line = "";
                int index = 0;  // Index on the file

                // Reads the file line-by-line 
                while ((line = rd.ReadLine()) != null)
                {
                    var l = line.Split(',');    // Separate variables and store in array of strings

                    // Check if the expected number of strings are present (failed if not)
                    if (l.Length <= 0 || l.Length > 4)
                    {
                        failed.Add(index);
                        index++;    // Increment index
                        continue;
                    }

                    switch (person)
                    {
                        case 's':
                            // Create student and verify that it was added successfully (failed if not)
                            if (factory.CreateStudent(Convert.ToInt32(l[3]), l[2], l[1], l[0]) == null)
                            {
                                failed.Add(index);
                            }
                            break;

                        case 'p':
                            // Create student and verify that it was added successfully (failed if not)
                            if (factory.CreateProfessor(Convert.ToInt32(l[3]), l[2], l[1], l[0]) == null)
                            {
                                failed.Add(index);
                            }
                            break;
                        default:
                            return null;    // Return null if invalid Person type
                    }
                    index++;    // Increment index
                }
            }

            return failed;
        }

        // Finds and returns Student/Professor based on id
        // Returns Student/Professor entity
        public IPerson? GetPerson(int id)
        {
            // Check that id is valid
            if (id < 0)
            {
                return null;    // Invalid id
            }

            var finder = new PersonFinder(ConnStr); // Person with matching id

            // Check if id points to student
            var stu = finder.GetStudent(id);
            if (stu == null)
            {
                // Check if id points to professor
                var prof = finder.GetProfessor(id);
                if (prof == null)
                {
                    return null;    // No person with id
                }
                else
                {
                    return prof;    // Return professor with id
                }
            }
            else
            {
                return stu; // Return student with id
            }
        }

        // Finds and deletes Student/Professor based on id
        // Returns deleted Student/Professor
        public IPerson? DeletePerson(int id)
        {
            // Check that id is valid
            if(id < 0)
            {
                return null;    // Invalid id
            }

            var person = GetPerson(id); // Person with matching id

            // Check if person was found 
            if (person == null)
            {
                return null;    // No person with id
            }
            else
            {
                // Create and use a MySqlConnection to the database using the provided adress
                using (var conn = new MySqlConnection(ConnStr))
                {
                    conn.Open();

                    // Check if the person is a student or professor
                    string commandText;
                    if (person.GetType() == typeof(Student))    // Student SQL delete query
                    {
                        commandText = "DELETE FROM student as s " +
                            "WHERE s.id = @id;";
                    }
                    else    // Professor SQL delete query
                    {
                        commandText = "DELETE FROM professor as p " +
                            "WHERE p.id = @id;";
                    }

                    // Create and use MySqlCommand using conn and the chosen delete query
                    using (var cmd = new MySqlCommand(commandText, conn))
                    {
                        // Add id parameter to query
                        try
                        {
                            cmd.Parameters.AddWithValue("@id", id);

                            // Execute query
                            if(0 >= cmd.ExecuteNonQuery())
                            {
                                person = null;  // Query failed
                            }
                        }
                        // Add failed
                        catch
                        {
                            person = null;
                        }
                    }
                }
            }
            return person;  // Return deleted person (null if failed)
        }
    }
}
