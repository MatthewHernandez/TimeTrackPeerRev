/* PeerRevEntry contains and allows for the viewing and editing of Peer Review entry data.
 * Each entry is created by a student who will input also provide all relevant
 * into. This includes the Ranking, commenting, date and reviewer/reveiwee information.
 * Author:  Matthew Louis Hernandez
 * Editor:  Jesus Barrera-Gilabert III
 * Date:    11/21/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  mlh190006
 * Version: 0.3
 */

using System;
using System.Runtime.InteropServices;

namespace G81_Library
{
    // Represents data for Peer Review Entries
    public class PeerRevEntry
    {
        // Constructor
        public PeerRevEntry(int reviewer, int reviewee, string comment, int pNum, int[] ranks)
        {
            Reviewer = reviewer;
            Reviewee = reviewee;
            Comment = comment;
            PeriodID = pNum;
            QualRating = ranks[0];
            TimelinessRating = ranks[1];
            TeamworkRating = ranks[2];
            EffRating = ranks[3];
            CommRating = ranks[4];
        }

        // Student who createing review
        public int Reviewer { get; set; }

        // Student to be reviewed
        public int Reviewee { get; set; }

        // Comment of review
        public string Comment { get; set; }

        // Start of review period
        public int PeriodID { get; set; }

        // quality_of_work_rating
        public int QualRating {  get; set; }

        // timeliness_rating
        public int TimelinessRating { get; set; }

        // teamwork_rating
        public int TeamworkRating { get; set; }

        // eff_and_part_rating 
        public int EffRating { get; set; }

        // communication_rating
        public int CommRating { get; set; }
    }

    // PeerReviewEntity Period entity class that holds info on the period start and end dates
    public class PREPeriod
    {
        // Constructor
        public PREPeriod(int num, int cID, DateTime start, DateTime end)
        {
            PeriodNum = num;
            CID = cID;
            Start = start;
            End = end;
        }

        // Period number
        public int PeriodNum { get; set; }

        // Class ID the Period number corresponds to
        public int CID { get; set; }

        // Start of the review period
        public DateTime Start { get; set; }

        // End of the review period
        public DateTime End { get; set; }
    }
}