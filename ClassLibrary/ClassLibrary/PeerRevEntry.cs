/* PeerRevEntry contains and allows for the viewing and editing of Peer Review entry data.
 * Each entry is created by a student who will input also provide all relevant
 * into. This includes the ...
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
        // Rank of attribute (0 .. 5)
        public int Rank { get; set; }
        // Comment of review
        public string Comment { get; set; }
        // Date of review entry
        public DateOnly Date { get; set; }  // TODO: check if needed


        public PeerRevEntry(Student reviewer, Student reviewee, int rank, string comment, DateOnly date)
        {
            Reviewer = reviewer;
            Reviewee = reviewee;
            Rank = rank;
            Comment = comment;
            Date = date;
        }

        // edit rank and validates newRank value
        public void EditRank(int newRank) {
            if (newRank == Rank) return;
            if (newRank <= 0 || newRank >= 5) {
                Rank = newRank;
                return;
            } else throw new ArgumentOutOfRangeException("newRank must be <= 0 or >= 5.");
        }

        // edit comment and validate newComment
        public void EditComment(string newComment) {

            if (newComment.Length >= 0 && newComment.Length <= 500){
                Comment = newComment;
                return;
            } else throw new ArgumentOutOfRangeException("newRank must be <= 0 or >= 5.");
            
        }

        // edit Reviewee and validate newReviewee
        public void EditReviewee(Student newReviewee) {
            if (newReviewee == null) throw new ArgumentNullException("null newReviewee");
            if (newReviewee == Reviewee) return;
            else {
                Reviewee = newReviewee;
            }
        }
    }
}