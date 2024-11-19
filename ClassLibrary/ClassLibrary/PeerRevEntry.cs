/* PeerRevEntry contains and allows for the viewing and editing of Peer Review entry data.
 * Each entry is created by a student who will input also provide all relevant
 * into. This includes the Ranking, commenting, date and reviewer/reveiwee information.
 * Author:  Matthew Louis Hernandez
 * Date:    11/18/2024
 * Class:   Computer Science Project CS 4485.0W1
 * Net ID:  mlh190006
 * Version: 0.2
 */

using System;
using System.Runtime.InteropServices;

namespace G81_Library
{
    class PeerRevEntry
    {
        //Constructor
        public PeerRevEntry(int cID, int reviewer, int reviewee, string comment, DateTime periodStart, DateTime periodEnd, int[] ranks)
        {
            CID = cID;
            Reviewer = reviewer;
            Reviewee = reviewee;
            Comment = comment;
            PeriodStart = periodStart;
            PeriodEnd = periodEnd;
            QualRating = ranks[0];
            TimelinessRating = ranks[1];
            TeamworkRating = ranks[2];
            EffRating = ranks[3];
            CommRating = ranks[4];
        }

        //Class ID
        public int CID { get; set; }

        // Student who createing review
        public int Reviewer { get; set; }

        // Student to be reviewed
        public int Reviewee { get; set; }

        // Comment of review
        public string Comment { get; set; }

        // Start of review period
        public DateTime PeriodStart { get; set; }

        // End of review period
        public DateTime PeriodEnd { get; set; }

        //quality_of_work_rating
        public int QualRating {  get; set; }

        //timeliness_rating
        public int TimelinessRating { get; set; }

        //teamwork_rating
        public int TeamworkRating { get; set; }

        //eff_and_part_rating 
        public int EffRating { get; set; }

        //communication_rating
        public int CommRating { get; set; }
    }
}