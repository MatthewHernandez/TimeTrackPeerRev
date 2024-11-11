/* TimeEntry contains and allows for the viewing and editing of time entry data.
 * Each time entry is created by a student who will input also provide all relevant
 * into. This includes the time they spent on the project and the date they spent it
 * on, as well as their accomplishments, problems they faced, and their plans moving
 * forward.
 * Author:  Jesus Barrera-Gilabert III
 * Date:    10/22/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  jxb171030
 * UTD ID:  2021348532
 * Version: 0.1
 */

namespace G81_Library
{
    class TimeEntry
    {
        //Student who created the Time Entry
        public Student Stu { get; set; }

        //Amount of time project on the given Date
        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                //Can't add negative time
                if(!TimeSpan.Equals(value, value.Duration()))
                {
                    throw new ArgumentException("Invalid Time");
                }
                _time = value;
            }
        }

        //Date of the Time Entry
        public DateOnly Date { get; set; }

        //Comments on what was accomplished
        public string Accomplish {  get; set; }

        //Comments on problems faced 
        public string Problems {  get; set; }

        //Comments on plans for next Time Entry/Week
        public string Plan {  get; set; }

        public TimeEntry (Student student, TimeSpan time, DateOnly date, string problems, string plan, string accomplish)
        {
            Stu = student;
            Time = time;
            Date = date;
            Accomplish = accomplish;
            Plan = plan;
            Problems = problems;
        }

        //Add some amount of time t to Time
        public void AddTime(TimeSpan t) => Time += t;

        //Subtract some amount of time t from Time
        public void SubTime(TimeSpan t) => Time -= t;
    }
}
