/* PeerRevEntry contains and allows for the viewing and editing of Peer Review entry data.
 * Each entry is created by a student who will input also provide all relevant
 * into. This includes the Ranking, commenting, date and reviewer/reveiwee information.
 * Author:  Matthew Louis Hernandez
 * Date:    10/22/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  mlh190006
 * Version: 0.1
 */

using System;

namespace G81_Library
{
    class PeerRevEntry
    {
        // Student who createing review
        public Student Reviewer { get; set; }
        // Student to be reviewed
        public Student Reviewee { get; set; }
        // Comment of review
        public string Comment { get; set; }
        // Date of review entry
        public DateOnly Date { get; set; } 
        // Start of review period
        public DateOnly PeriodStart { get; set; }
        // End of review period
        public DateOnly PeriodEnd { get; set; }
        // Rank of attribute (0 .. 5)
        public int Rank
        {
            get { return Rank; }
            set 
            { 
                if (value == Rank) return;
                if (value <= 0 || value >= 5) {
                    Rank = value;
                    return;
                } else throw new ArgumentOutOfRangeException("newRank must be <= 0 or >= 5.");
            }
        }


        public PeerRevEntry(Student reviewer, Student reviewee, int rank, string comment, DateOnly date, DateOnly periodStart, DateOnly periodEnd)
        {
            Reviewer = reviewer;
            Reviewee = reviewee;
            Rank = rank;
            Comment = comment;
            Date = date;
            PeriodStart = periodStart;
            PeriodEnd = periodEnd;
        }
    }
}