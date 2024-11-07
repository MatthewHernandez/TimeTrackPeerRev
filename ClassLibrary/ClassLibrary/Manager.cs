/* Manager contains functions to CRUB operations of Persons, including Professor and Students.
 * Author:  Matthew Louis Hernandez
 * Date:    11/07/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  mlh190006
 * Version: 0.2
 * Description: version provides template/psuedo code for factory and other class interactions
 */

namespace G81_Library
{
    public class PersonManager 
    {
        private string _sqlCon;
        private string _studentDB;
        private string _professorDB;
        private PersonFactory _factory;
        private IPerson _user;
        public PersonManager(string sqlCon, string studentDB, string professorDB, IPerson user)
        {
            _sqlCon = sqlCon;
            _user = user;
            _studentDB = studentDB;
            _professorDB = professorDB;
            _factory = new PersonFactory(_sqlCon, _studentDB, _professorDB);
        }

        bool AddProfessor(string fname, string lname, int utdID, string netID)
        {
            // Professor professor = PersonFactory.CreateProfessor(prof.fname, prof.lname, prof.netID, prof.utdID)
            // if (professor == null) return false;
            // _professors.Add(professor);
            // return true;
            return false;
        }

        bool AddStudent(string fname, string lname, int utdID, string netID, float classID, int group)
        {
            // Student student = PersonFactory.CreateStudent(stu.fname, stu.lname, stu.netID, stu.utdID, stu.cName, stu.group)
            // if (student == null) return false;
            // _students.Add(student);
            // return true;
            return false;
        }

        Professor? GetProfessor(int utdID)
        {
            // Professor professor = PersonSearch.RetrieveProfessor(netID);
            // if (professor == null) return null;
            // return professor;
            return null;
        }

        Student? GetStudent(int utdID)
        {
            // Student student = PersonSearch.RetrieveStudent(netID);
            // if (student == null) return null;
            // return student;
            return null;
        }

        bool DeleteProfessor(int utdID)
        {
            return false;
        }

        bool DeleteStudent(int utdID)
        {
            return false;
        }
    }
}