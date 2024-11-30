/* Person.cs creates the IPerson interface and creates entity classes that implement it in order to represent the data of the student
 * and professors who will use the system. Both students and professors have name, net ID, UTD ID, and password variables. Students,
 * in addition to these, have variables for their class name and the group they're in. Professors have a list variable that contains 
 * the classes they are incharge of. The name, net ID, UTD ID, class names, and group numbers can be modified with regular get/set
 * methods. Passwords can be set but can't be retrieved; a password must be provided in order to check that it matches the Person's
 * password. Professor contains methods for adding class(es), removing class(es), retrieving classes, and checking if a certain
 * class is already in their class list.
 * Author:  Jesus Barrera-Gilabert III
 * Editor:  Matthew Hernandez
 * Date:    11/25/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.7
 */

namespace G81_Library
{
    //Interface used by Student and Professor
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int ID { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }

    //Represents the data of a student
    public class Student : IPerson
    {
        //Constructor (new student)
        public Student(string fname, string lname, int utdID, string netID)
        {
            FirstName = fname;
            LastName = lname;
            ID = utdID;
            Username = netID;
            CID = 0;
            TeamID = 0;
            Password = Convert.ToString(utdID);
        }

        //Alt-Constructor (existing student)
        public Student(string fname, string lname, int utdID, string netID, int cID, int group, string password)
        {
            FirstName = fname;
            LastName = lname;
            ID = utdID;
            Username = netID;
            CID = cID;
            TeamID = group;
            Password = password;
        }

        //Name property; read only
        public string FirstName { get; set; }

        //Last Name property; read only
        public string LastName { get; set; }

        //UTD ID property; read only 
        public int ID { get; set; }

        //Net ID property; read only
        public string Username { get; set; }    

        //Class name property; read only
        public int CID { get; set; }

        //Group Number property; read only
        public int TeamID { get; set; }

        //Password property; read only
        public string Password { get; set; }
    }

    //Represents the data of a professor
    public class Professor : IPerson
    {
        //Constructor (new professor)
        public Professor(string fname, string lname, int utdID, string netID)
        {
            FirstName = fname;
            LastName = lname;
            ID = utdID;
            Username = netID;
            Password = Convert.ToString(utdID);
        }

        //Alt-Constructor (existing professor)
        public Professor(string fname, string lname, int utdID, string netID, string password)
        {
            FirstName = fname;
            LastName = lname;
            ID = utdID;
            Username = netID;
            Password = password;
        }

        //First Name property; read only
        public string FirstName { get; set; }

        //Last Name property; read only
        public string LastName { get; set; }

        //UTD ID property; read only
        public int ID { get; set; }

        //Net ID property; read only
        public string Username { get; set; }

        //Password Property; read only
        public string Password { get; set; }
    }

    
    //Represents the classes a Professor is in charge of and Students are a part of
    public class PClass
    {
        //Constructor
        public PClass(int prof, int id, string name, string semester, int year)
        {
            ProfessorID = prof;
            ID = id;
            Name = name;
            Semester = semester;
            Year = year;
        }

        // Class's Professor
        public int ProfessorID { get; set; }

        // Class ID number
        public int ID { get; set; }

        // Class name
        public string Name { get; set; }

        // Class semester
        public string Semester { get; set; }

        // Class year
        public int Year { get; set; }
    }
}
