/* TimeEntry contains and allows for the viewing and editing of time entry data.
 * Each time entry is created by a student who will input also provide all relevant
 * into. This includes the time they spent on the project and the date they spent it
 * on, as well as their accomplishments, problems they faced, and their plans moving
 * forward.
 * Author:  Jesus Barrera-Gilabert III
 * Date:    11/12/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.2
 */

namespace G81_Library
{
    class TimeEntry
    {
        //Constructor
        public TimeEntry (Student student, TimeSpan time, DateOnly date, string comments)
        {
            Stu = student;
            Time = time;
            Date = date;
            Comm = comments;
            /*Accomplish = accomplish;
            Plan = plan;
            Problems = problems;*/            
        }

        //Student who created the Time Entry
        public Student Stu { get; }

        //Amount of time project on the given Date
        public TimeSpan Time { get; }

        //Date of the Time Entry
        public DateOnly Date { get;  }

        //Comments on time spent
        public string Comm { get; }

        /*
        //Comments on what was accomplished
        public string Accomplish {  get; }

        //Comments on problems faced 
        public string Problems {  get; }

        //Comments on plans for next Time Entry/Week
        public string Plan {  get; }*/
    }
}
