/* Person.cs creates the IPerson interface and creates entity classes that implement it in order to represent the data of the student
 * and professors who will use the system. Both students and professors have name, net ID, UTD ID, and password variables. Students,
 * in addition to these, have variables for their class name and the group they're in. Professors have a list variable that contains 
 * the classes they are incharge of. The name, net ID, UTD ID, class names, and group numbers can be modified with regular get/set
 * methods. Passwords can be set but can't be retrieved; a password must be provided in order to check that it matches the Person's
 * password. Professor contains methods for adding class(es), removing class(es), retrieving classes, and checking if a certain
 * class is already in their class list.
 * Author:  Jesus Barrera-Gilabert III
 * Date:    10/29/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.4
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

        //Change password to pass
        void ChangePassword(string pass);
        //Check if pass matches Password; return true if it does
        bool CheckPassword(string pass);
    }

    //Represents the data of a student with operations for retrieving or editing it
    class Student : IPerson
    {
        private string _password;

        //Constructor
        public Student(string fname, string lname, int utdID, string netID, CSClass cName, int group)
        {
            FirstName = fname;
            LastName = lname;
            UtdID = utdID;
            NetID = netID;
            _password = Convert.ToString(utdID);
            Cname = cName;
            Group = group;
        }

        //Alt-Constructor
        public Student(string fname, string lname, int utdID, string netID, CSClass cName, int group, string password)
        {
            FirstName = fname;
            LastName = lname;
            UtdID = utdID;
            NetID = netID;
            _password = Convert.ToString(utdID);
            Cname = cName;
            Group = group;
            _password = password;
        }

        //Name property; readable and writeable
        public string FirstName { get; set; }

        //Last Name property; readable and writeable
        public string LastName { get; set; }

        //UTD ID property; readable and writeable 
        public int UtdID { get; set; }

        //Net ID property; readable and writeable
        public string NetID { get; set; }    

        //Class name property; readable and writeable
        public CSClass Cname { get; set; }

        //Group Number property; readable and writeable
        public int Group { get; set; }

        //Change _password to pass
        public void ChangePassword(string pass)
        {
            _password = pass;
        }

        //Check if pass matches Password; return true if it does
        public bool CheckPassword(string pass)
        {
            return pass.Equals(_password);
        }
    }

    //Represents the data of a professor with operations for retrieving, adding, and editing it
    class Professor : IPerson
    {
        private string _password;

        //Constructor
        public Professor(string fname, string lname, int utdID, string netID)
        {
            FirstName = fname;
            LastName = lname;
            UtdID = utdID;
            NetID = netID;
            _password = Convert.ToString(utdID);
            Projects = new List<CSProject>;
        }

        //Alt-Constructor
        public Professor(string fname, string lname, int utdID, string netID, string password)
        {
            FirstName = fname;
            LastName = lname;
            UtdID = utdID;
            NetID = netID;
            _password = password;
            Projects = new List<CSProject>;
        }

        //First Name property; readable and writeable
        public string FirstName { get; set; }

        //Last Name property; readable and writeable
        public string LastName { get; set; }

        //UTD ID property; readable and writeable
        public int UtdID { get; set; }

        //Net ID property; readable and writeable
        public string NetID { get; set; }

        public List<CSProject> Projects { get; set; }

        //Change _password to pass
        public void ChangePassword(string pass)
        {
            _password = pass;
        }
    
        //Check if pass matches Password; return true if it does
        public bool CheckPassword(string pass)
        {
            return pass.Equals(_password);
        }
    }

    //Represents the classes a Professor is in charge of and Students are a part of
    class CSClass
    {
        public Professor prof;
        public string name;
        public string semester;
        public int year;
        public CSClass(Professor prof, string name, string semester, int year)
        {
            this.prof = prof;
            this.name = name;
            this.semester = semester;
            this.year = year;
        }
    }

    class CSProject
    {
        public int projectNumber;   
        public string type;
        public int teamCount;
        public string company;
        public string title;

        public CSProject(int projectNumber, string type, string teamCount, string company, string title)
        {
            this.projectNumber = projectNumber;
            this.type = type;
            this.teamCount = teamCount;
            this.company = company;
            this.title = title;
        }
    }
}
