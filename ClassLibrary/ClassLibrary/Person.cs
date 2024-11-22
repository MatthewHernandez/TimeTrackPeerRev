/* Person.cs creates the IPerson interface and creates entity classes that implement it in order to represent the data of the student
 * and professors who will use the system. Both students and professors have name, net ID, UTD ID, and password variables. Students,
 * in addition to these, have variables for their class name and the group they're in. Professors have a list variable that contains 
 * the classes they are incharge of. The name, net ID, UTD ID, class names, and group numbers can be modified with regular get/set
 * methods. Passwords can be set but can't be retrieved; a password must be provided in order to check that it matches the Person's
 * password. Professor contains methods for adding class(es), removing class(es), retrieving classes, and checking if a certain
 * class is already in their class list.
 * Authors:  Jesus Barrera-Gilabert III, Matthew Hernandez
 * Date:    11/18/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.6
 */

namespace G81_Library
{
    //Interface used by Student and Professor
    interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int UtdID { get; set; }
        string NetID { get; set; }
        string Password { get; set; }
    }

    //Represents the data of a student with operations for retrieving or editing it
    public class Student : IPerson
    {
        //Constructor (new student)
        public Student(string fname, string lname, int utdID, string netID)
        {
            FirstName = fname;
            LastName = lname;
            UtdID = utdID;
            NetID = netID;
            CID = 0;
            Group = 0;
            Password = Convert.ToString(utdID);
        }

        //Alt-Constructor (existing student)
        public Student(string fname, string lname, int utdID, string netID, int cID, int group, string password)
        {
            FirstName = fname;
            LastName = lname;
            UtdID = utdID;
            NetID = netID;
            CID = cID;
            Group = group;
            Password = password;
        }

        //Name property; read only
        public string FirstName { get; set; }

        //Last Name property; read only
        public string LastName { get; set; }

        //UTD ID property; read only 
        public int UtdID { get; set; }

        //Net ID property; read only
        public string NetID { get; set; }    

        //Class name property; read only
        public int CID { get; set; }

        //Group Number property; read only
        public int Group { get; set; }

        //Password property; read only
        public string Password { get; set; }
    }

    //Represents the data of a professor with operations for retrieving, adding, and editing it
    public class Professor : IPerson
    {
        //Constructor (new professor)
        public Professor(string fname, string lname, int utdID, string netID)
        {
            FirstName = fname;
            LastName = lname;
            UtdID = utdID;
            NetID = netID;
            Password = Convert.ToString(utdID);
        }

        //Alt-Constructor (existing professor)
        public Professor(string fname, string lname, int utdID, string netID, string password)
        {
            FirstName = fname;
            LastName = lname;
            UtdID = utdID;
            NetID = netID;
            Password = password;
        }

        //First Name property; read only
        public string FirstName { get; set; }

        //Last Name property; read only
        public string LastName { get; set; }

        //UTD ID property; read only
        public int UtdID { get; set; }

        //Net ID property; read only
        public string NetID { get; set; }

        //Password Property; read only
        public string Password { get; set; }
    }

    
    //Represents the classes a Professor is in charge of and Students are a part of
    public class PClass
    {
        //Constructor
        public PClass(int prof, int id, string name, string semester, int year)
        {
            Professor = prof;
            ID = id;
            Name = name;
            Semester = semester;
            Year = year;
        }

        public int Professor { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Semester { get; set; }

        public int Year { get; set; }
    }
}
