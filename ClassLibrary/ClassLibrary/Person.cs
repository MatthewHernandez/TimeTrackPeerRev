/* Person.cs creates the IPerson interface and creates entity classes that implement it in order to represent the data of the student
 * and professors who will use the system. Both students and professors have name, net ID, UTD ID, and password variables. Students,
 * in addition to these, have variables for their class name and the group they're in. Professors have a list variable that contains 
 * the classes they are incharge of. The name, net ID, UTD ID, class names, and group numbers can be modified with regular get/set
 * methods. Passwords can be set but can't be retrieved; a password must be provided in order to check that it matches the Person's
 * password. Professor contains methods for adding class(es), removing class(es), retrieving classes, and checking if a certain
 * class is already in their class list.
 * Author:  Jesus Barrera-Gilabert III
 * Date:    10/11/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.2
 */
using System;
using System.Collections.Generic;

namespace G81_Library
{
    //Interface used by Student and Professor
    interface IPerson
    {
        string Name { get; set; }
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
        public Student(string name, int utdID, string netID, string cName, int group)
        {
            Name = name;
            UtdID = utdID;
            NetID = netID;
            _password = utdID;
            Cname = cName;
            Group = group;
        }

        //Name property; readable and writeable
        public string Name { get; set; }

        //UTD ID property; readable and writeable 
        public int UtdID { get; set; }

        //Net ID property; readable and writeable
        public string NetID { get; set; }    

        //Class name property; readable and writeable
        public string Cname { get; set; }

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
        //The list of classes the professor is incharge of
        private List<string> _classes;

        //Constructor
        public Professor(string name, int utdID, string netID)
        {
            Name = name;
            UtdID = utdID;
            NetID = netID;
            _password = utdID;
            _classes = new List<string>();
        }

        //Name property; readable and writeable
        public string Name { get; set; }

        //UTD ID property; readable and writeable
        public int UtdID { get; set; }

        //Net ID property; readable and writeable
        public string NetID { get; set; }

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

        //Check if class c is already the class list and add it if it isn't; return true if added
        public bool AddClass(string c)
        {
            if (_classes.Contains(c))
            {
                return false;
            }
            _classes.Add(c);
            return true;
        }

        //Adds classes from newClasses to the class list using AddClass(); return true if at least 1 was added
        public bool AddClasses(List<string> newClasses)
        {
            bool added = false;
            foreach(string i in newClasses)
            {
                added = (added | AddClass(i));
            }
            return added;
        }

        //Delete the remClass if it's in the class list; return true if successfully deleted
        public bool RemoveClass(string remClass)
        {
            return _classes.Remove(remClass);
        }

        //Delete all classes from the class list
        public void RemoveAllClasses()
        {
            _classes.Clear();
        }

        //Return the class list
        public List<string> GetClasses()
        {
            return _classes;
        }

        //Check if hClass is in the class list; return true if it's present
        public bool HasClass(string hClass)
        {
            return _classes.Contains(hClass);
        }
    }

}
