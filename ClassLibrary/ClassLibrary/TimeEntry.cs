/* TimeEntry contains and allows for the viewing and editing of time entry data.
 * Each time entry is created by a student who will input also provide all relevant
 * into. This includes the time they spent on the project and the date they spent it
 * on, as well as their accomplishments, problems they faced, and their plans moving
 * forward.
 * Author:  Jesus Barrera-Gilabert III
 * Date:    11/18/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.3
 */

namespace G81_Library
{
    public class TimeEntry
    {
        //Constructor
        public TimeEntry (int student, decimal time, DateTime date, string comments)
        {
            Stu = student;
            Time = time;
            Date = date;
            Comments = comments;          
        }

        //Student who created the Time Entry
        public int Stu { get; set; }

        //Amount of time project on the given Date
        public decimal Time { get; set; }

        //Date of the Time Entry
        public DateTime Date { get; set; }

        //Comments on time spent
        public string Comments { get; set; }
    }
}
