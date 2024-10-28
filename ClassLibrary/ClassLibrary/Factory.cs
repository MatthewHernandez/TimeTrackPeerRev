/* Factory contains functions for creating and destroying Student and Professor entities and propagating
 * those functions onto the database.
 * Author:  Jesus Barrera-Gilabert III
 * Date:    10/27/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.1
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace G81_Library
{
    class PersonFactory
    {
        public PersonFactory()
        {

        }
        //Creates a Student entity and stores it in the database
        Student CreateStudent(string name, string netID, int utdID, string cName, int group)
        {
            return new Student(name, utdID, netID, cName, group);
        }

        //Creates a Professor entity and stores it in the database
        Professor CreateProfessor(string name, string netID, int utdID)
        {
            return new Professor(name, utdID, netID);
        }

        //Takes a netID and checks that it's a valid Student netID; try to delete the Student and return result
        public bool DeleteStudent(string netID)
        {
            return false;
        }

        //Takes a netID and checks that it's a valid Professor netID; try to delete the Professor and return result
        public bool DeleteProfessor(string netID)
        {
            return false;
        }
    }
}
