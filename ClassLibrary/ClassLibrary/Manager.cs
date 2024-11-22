/* Manager implements functionallity from other classes to provide the simplified add and modify funcionts 
 * between predefined entities and a database accross multiple manager classes
 * Author:  Jesus Barrera-Gilabert III
 * Date:    11/21/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.1
 */

using MySql.Data.MySqlClient;

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

        // Adds a student entity to the database; returns success status
        public bool AddStudent(Student stu)
        {
            var fac = new PersonFactory(ConnStr);
            return fac.CreateStudent(stu);
        }

        // Creates and adds student entities to the database; returns indexes of student info that failed to get added
        public List<int> AddStudents(string filePath)
        {
            var factory = new PersonFactory(ConnStr);
            List<int> failed = new List<int>(); // List of indexes that failed to get added (empty if complete success)

            using (var rd = new StreamReader(filePath))
            {
                string line;
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

                    // Create student and verify that it was added successfully (failed if not)
                    if (factory.CreateStudent(Convert.ToInt32(l[3]), l[2], l[1], l[0]) == null)
                    {
                        failed.Add(index);
                    }
                    index++;    // Increment index
                }
            }

            return failed;
        }
    }
}
