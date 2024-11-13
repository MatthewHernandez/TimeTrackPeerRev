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
using System.Runtime.InteropServices;

namespace G81_Library
{
    class PeerRevEntry
    {
        //Constructor
        public PeerRevEntry(PClass cID, Student reviewer, Student reviewee, string comment, DateOnly periodStart, DateOnly periodEnd, int[] ranks)
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
        public PClass CID { get; private set; }

        // Student who createing review
        public Student Reviewer { get; private set; }

        // Student to be reviewed
        public Student Reviewee { get; private set; }

        // Comment of review
        public string Comment { get; private set; }

        // Start of review period
        public DateOnly PeriodStart { get; private set; }

        // End of review period
        public DateOnly PeriodEnd { get; private set; }

        //quality_of_work_rating
        public int QualRating {  get; private set; }

        //timeliness_rating
        public int TimelinessRating { get; private set; }

        //teamwork_rating
        public int TeamworkRating { get; private set; }

        //eff_and_part_rating 
        public int EffRating { get; private set; }

        //communication_rating
        public int CommRating { get; private set; }
    }
}