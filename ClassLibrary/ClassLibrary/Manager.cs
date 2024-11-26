/* Manager implements functionallity from other classes to provide the simplified add and modify funcionts 
 * between predefined entities and a database accross multiple manager classes
 * Author:  Jesus Barrera-Gilabert III
 * Date:    11/25/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.2
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
            var fac = new PersonFactory(ConnStr);

            if(person.GetType() == typeof(Student))
            {
                try
                {
                    Student stu = (Student)person;
                    return fac.CreateStudent(stu);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    Professor prof = (Professor)person;
                    return fac.CreateProfessor(prof);
                }
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
    }
}
