/* Manager contains functions to CRUB operations of Persons, including Professor and Students.
 * Author:  Matthew Louis Hernandez
 * Date:    10/31/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  mlh190006
 * Version: 0.1
 * Description: version provides template/psuedo code for factory and other class interactions
 */

namespace G81_Library
{
    public class PersonManager 
    {
        private List<Professor> _professors;
        private List<Student> _students;
        public PersonManager()
        {
            _professors = new List<Professor>();
            _students = new List<Student>();
        }

        public bool AddProfessor(Professor prof)
        {
            // Professor professor = PersonFactory.CreateProfessor(prof.fname, prof.lname, prof.netID, prof.utdID)
            // if (professor == null) return false;
            // _professors.Add(professor);
            // return true;
        }

        public bool AddStudent(Student stu)
        {
            // Student student = PersonFactory.CreateStudent(stu.fname, stu.lname, stu.netID, stu.utdID, stu.cName, stu.group)
            // if (student == null) return false;
            // _students.Add(student);
            // return true;
        }

        public Professor GetProfessor(int netID)
        {
            // Professor professor = PersonSearch.RetrieveProfessor(netID);
            // if (professor == null) return null;
            // return professor;
        }

        public Student GetStudent(int netID)
        {
            // Student student = PersonSearch.RetrieveStudent(netID);
            // if (student == null) return null;
            // return student;
        }
    }
}