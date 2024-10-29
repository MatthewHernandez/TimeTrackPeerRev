using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_App_For_Professor
{
    // Static class to store the current professor's session data
    public static class ProfessorSession
    {
        // Property to store the professor's username
        public static string Username { get; set; }

        // Property to store the professor's ID
        public static Int32 ProfessorId { get; set; }

        // Property to store the current selected class for work
        public static Int32 curClass { get; set; }

        // Property to store the professor's class id

        // Method to clear the session when the professor logs out or the session ends
        public static void ClearSession()
        {
            Username = null;
            ProfessorId = 0;
        }
    }
}
