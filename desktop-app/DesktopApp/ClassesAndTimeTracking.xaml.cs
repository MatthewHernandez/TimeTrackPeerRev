/*

Professors Classes, Timetracking and Peer Review program

This program relays relevant data based on Professor who logged in and what he clicks;

*Assume Professor logs in

*Professor may click from a list of classes where he is designated as a Professor in DB

*This click will retrieve a list of students relevant to said class

*Clicking a student will retrieve certain data;
*List of Time Entries relevant to student
*Peer reviews relevant to student

*Professor has the option to modify contents of student's time entries then clicking update info button

*Professor has the option to click a possible peer review where;
*A list of relevant peer review "targets" are shown and clickable

*Clicking a "target" will bring up a peer review entry relevant to peer reviewer and the reviewee (target)
*Professor has ability to modify the review itself or even the entirety of the peer review period's date

*Assume Professor modifies the dates, we will scan for the relevant peer review period to change the associated date, which all relevant peer reviews are using
*Assume Professor modifies comments or rating, this will change only the relevant review.

*Also the Professor has the ability to Import students with .CSV file or Instantiate/Delete a peer review period (and all relevant reviews therein) within their respective sequences.

*Ensure you follow the DB Schema closely to ensure functionality--this includes mimicing DB table for students when importing a .CSV

program written by William LaFoy for CS4485 Project, UT Dallas, 10/9/2024-11/29/2024. NetID wel190000

        Base framework for listing out classes, listing out associated students and cummulative hours
         and specific dates with those hours along with peer reviews and their associated details.

        10/18 now allows you to select cells with ease and modify the date hour etc.

        10/25 now displays peer reviews, button to make a new peer review exists, but functionality will depend on sql calls.
         
         10/30 features backend connectivity with mysql, with gxk220025 Gwangmo Kim's help and sample code to upstart this. now displays db info

        11/8-10 features add/modify/deletion and reflection to the connected db

        11/29 polished and tweaked. UI is fluid and reactive based on how and what you click, also other QOL things, will be looking to merge DBs with web app side (different structures and variable names are the issue)

        12/1 Spent time debugging & merging into the comprehensive database that both web and desktop apps should be utilizing.
        
 */



using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Maui.Controls;
using System.IO;
using System.Linq;

using MySql.Data.MySqlClient;
using System.Data;


namespace DesktopApp
{
    public partial class ClassesAndTimeTracking : ContentPage
    {
        //Program written by William LaFoy for CS4485, Project, 10/9/2024-11/29/2024. NetID wel190000
        /*
         Base framework for listing out classes, listing out associated students and cummulative hours
         and specific dates with those hours along with peer reviews and their associated details.

        10/18 now allows you to select cells with ease and modify the date hour etc.

        10/25 now displays peer reviews, button to make a new peer review exists, but functionality will depend on sql calls.
         
         10/30 features backend connectivity with mysql, used Gwangmo Kim's code and helpful critical info to upstart this. now displays db info

        11/8-10 features add/modify/deletion and reflection to the connected db

        11/29 polished and tweaked. UI is fluid and reactive based on how and what you click, also other QOL things, will be looking to merge DBs with web app side (different structures and variable names are the issue)
         */

        

        //Variables to hold critical and relevant data.
        private int professorId; 
        private int classId;
        private int currentStudentId;
        private int currentRevieweeStudentId;

        private string TeammateName;
        private string Comment;
        private int QualityOfWork;
        private int Timeliness;
        private int Communication;
        private int Teamwork;
        private int EffortParticipation;
        private DateTime StartDate;
        private DateTime EndDate;
        private int ReviewId;

        // Private field to hold all student hour details for current student id
        private List<StudentHourDetail> _studentHourDetails = new List<StudentHourDetail>();

        // Constructor that takes professorId as a parameter, given from login page
        public ClassesAndTimeTracking(int professorId)
        {
            InitializeComponent();
            this.professorId = professorId; // Store the professor ID
            LoadClasses(); // Load classes on initialization
        }

        // Method to load classes into the Picker
        private void LoadClasses()
        {
            // Load classes associated with the professor
            var classes = GetClassesForProfessor(professorId);
            ClassPicker.ItemsSource = classes;
        }

        

        // Get classes from the database relevant to ProfessorId
        private List<ClassInfo> GetClassesForProfessor(int professorId)
        {
            var classes = new List<ClassInfo>();
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema3";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT id, class_name FROM class WHERE professor_id = @professorId";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@professorId", professorId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                classes.Add(new ClassInfo
                                {
                                    ClassId = reader.GetInt32(0), //id is the first column
                                    ClassName = reader.GetString(1) //class_name is the second column
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return classes; // Return list of class info
        }


        // Class to hold class information
        public class ClassInfo
        {
            public int ClassId { get; set; }
            public string ClassName { get; set; }

            public override string ToString() => ClassName; // Allows ClassPicker display class names
        }


        // Event handler when a class is selected
        private void ClassPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClassPicker.SelectedItem is ClassInfo selectedClass)
            {
                this.classId = selectedClass.ClassId;
                LoadStudentHours(selectedClass.ClassId);
                StudentHoursCollectionView.IsVisible = true;
                StudentHoursHeader.IsVisible = true;
                ImportStudentsButton.IsVisible = true;

                StudentDetailStack.IsVisible = false;
                PeerReviewStack.IsVisible = false;
                PeerReviewDetailsStack.IsVisible = false;

            }
        }






        //Data structure for student hours
        public class StudentHour
        {
            public string StudentName { get; set; }


            public int TeamNumber { get; set; }
            public decimal Hours { get; set; }

            public int StudentId { get; set; }
            public int ClassId { get; set; }
            // public string studentinfo { get; set; }
        }


        
        // Method to load student hours based on selected class, this function retrieves all students relevant to classId
        private void LoadStudentHours(int classId)
        {
            var studentHours = new List<StudentHour>();
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema3";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    s.id,            
                    s.first_name, 
                    s.last_name, 
                    s.team_id, 
                    SUM(sh.hours) AS TotalHours
                FROM time_entry sh
                JOIN student s ON sh.student_id = s.id
                WHERE s.class_id = @classId
                GROUP BY s.id, s.team_id
                ORDER BY s.last_name, s.first_name"; // Ordering by last and first name

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@classId", classId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                studentHours.Add(new StudentHour
                                {
                                    StudentName = $"{reader.GetString(1)} {reader.GetString(2)}", // Combining first and last name
                                    TeamNumber = reader.GetInt32(3), // team_id is the fourth column
                                    Hours = reader.GetDecimal(4), // Total hours worked in the selected class
                                    StudentId = reader.GetInt32(0), // Student ID is the first column
                                    ClassId = classId // Class ID is passed in as a method parameter
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            // Bind to CollectionView
            StudentHoursCollectionView.ItemsSource = studentHours.OrderBy(sh => sh.TeamNumber).ThenBy(sh => sh.StudentName).ToList(); // Sorting by team number and name of student
        }





        // Event handler for importing students (with database insertion)
        private async void ImportStudents_Click(object sender, EventArgs e)
        {
            // Prompt for the CSV file path
            string filePath = await DisplayPromptAsync("File Path", "Please enter a filepath to import CSV data:", "OK", "Cancel");
            if (string.IsNullOrEmpty(filePath)) return; // User canceled or didn't enter anything

            // Check if file exists
            if (!File.Exists(filePath))
            {
                await DisplayAlert("Error", "File not found. Please make sure the path is correct.", "OK");
                return;
            }

            try
            {
                var studentsData = new List<string[]>();

                // Read the CSV file line by line
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var columns = line.Split(',');

                        // CSV file has columns that match the DB Schema id, username, first_name, last_name, password, class_id, team_id
                        if (columns.Length >= 7)
                        {
                            studentsData.Add(columns);
                        }
                    }
                }

                // Insert each student record into MySQL database
                await InsertStudentsIntoDatabase(studentsData);

                // Display success message
                await DisplayAlert("Success", $"{studentsData.Count} rows imported and inserted successfully!", "OK");
            }
            catch (Exception ex)
            {
                // Handle file format issues or read errors
                await DisplayAlert("Error", $"An error occurred while importing students: {ex.Message}", "OK");
            }
        }

        // Function to insert student records into MySQL database
        private async Task InsertStudentsIntoDatabase(List<string[]> studentsData)
        {
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema3";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    foreach (var student in studentsData)
                    {
                        // Insert student with given non null data.
                        var commandText = "INSERT INTO students (id, username, first_name, last_name, password, class_id, team_id) VALUES (@id, @username, @first_name, @last_name, @password, @class_id, @team_id)";
                        using (var command = new MySqlCommand(commandText, connection))
                        {
                            command.Parameters.AddWithValue("@id", student[0]);
                            command.Parameters.AddWithValue("@username", student[1]);
                            command.Parameters.AddWithValue("@first_name", student[2]);
                            command.Parameters.AddWithValue("@last_name", student[3]);
                            command.Parameters.AddWithValue("@password", student[4]);
                            command.Parameters.AddWithValue("@class_id", student[5]);
                            command.Parameters.AddWithValue("@team_id", student[6]);

                            await command.ExecuteNonQueryAsync();
                        }

                        
                    }
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Database Error", $"An error occurred while inserting students into the database: {ex.Message}", "OK");
            }
        }



       




        /// 
        /// Student details section, hours date what was done
        /// 


        // Event handler for selecting a student to view detailed hours
        private void StudentHoursCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0 && e.CurrentSelection[0] is StudentHour selectedHour)
            {
                this.currentStudentId = selectedHour.StudentId;
                LoadStudentDetails(selectedHour.StudentId, selectedHour.ClassId);
                StudentDetailStack.IsVisible = true;
               
                LoadPeerReviews(selectedHour.StudentId, selectedHour.ClassId); // Load peer reviews for the selected student
                PeerReviewStack.IsVisible = true;
                TeammatesCollectionView.IsVisible = false;
                PeerReviewDetailsStack.IsVisible = false;
            }
        }



        // StudentHourDetail, date, hours, Comments along with initial vars for those entries. Ended up not using propertychanged due to bugs
        public class StudentHourDetail
        {
            public DateTime Date { get; set; }
            public decimal Hours { get; set; }
            public string studentinfo { get; set; }
            public DateTime CurrentDate { get; set; }
            public decimal CurrentHours { get; set; }
            public string Currentstudentinfo { get; set; }
        }

        // Method to load student details for the selected student, time entries.
        private void LoadStudentDetails(int studentId, int classId)
        {
            // Clear the student hour details list
            _studentHourDetails.Clear();

            // Refresh the CollectionView's binding
            HoursCollectionView.ItemsSource = null; // Unbind first to ensure proper refresh
            HoursCollectionView.ItemsSource = _studentHourDetails; // Rebind to the cleared list

            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema3";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
            SELECT 
                sh.date, 
                sh.hours, 
                sh.comments 
            FROM time_entry sh
            JOIN student s ON sh.student_id = s.id
            WHERE s.id = @studentId AND s.class_id = @classId
            ORDER BY sh.date DESC"; // Order by date for latest first

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@studentId", studentId);
                        cmd.Parameters.AddWithValue("@classId", classId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _studentHourDetails.Add(new StudentHourDetail
                                {
                                    //Purpose for double reader is one is the initial and one can be changed. this is important later for modifications
                                    CurrentDate = reader.GetDateTime(0),
                                    CurrentHours = reader.GetDecimal(1), // Hours is the second column
                                    Currentstudentinfo = reader.IsDBNull(2) ? string.Empty : reader.GetString(2), // Comments is the third column
                                    Date = reader.GetDateTime(0), // Date is the first column
                                    Hours = reader.GetDecimal(1), // Hours is the second column
                                    studentinfo = reader.IsDBNull(2) ? string.Empty : reader.GetString(2) // Comments is the third column
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log them)
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            // Bind to CollectionView
            HoursCollectionView.ItemsSource = _studentHourDetails; // Bind the list to the CollectionView
            StudentDetailStack.IsVisible = true; // Show the details stack
            PeerReviewStack.IsVisible = true; // Show the peer review stack
        }

        // Update Student hours entries to DB, scan through all entries.
        private async void StudentDetailsOnUpdateInformationClicked(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema3";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    int Rows = 0;

                    foreach (var detail in _studentHourDetails)
                    {
                        
                        // Check if Hours has changed
                        if (detail.CurrentHours != detail.Hours)
                        {
                            Rows += 1;
                            Console.WriteLine("Hours has changed");

                            // Update hours in database
                            string updateHoursQuery = @"
                        UPDATE time_entry
                        SET hours = @newHours
                        WHERE student_id = @studentId AND date = @currentDate";

                            using (var cmd = new MySqlCommand(updateHoursQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@newHours", detail.Hours);
                                cmd.Parameters.AddWithValue("@studentId", this.currentStudentId);
                                cmd.Parameters.AddWithValue("@currentDate", detail.CurrentDate);
                                await cmd.ExecuteNonQueryAsync();
                            }

                            detail.CurrentHours = detail.Hours; // Update the local current value
                        }





                       
                    }

                    await DisplayAlert("Success", $"{Rows} rows updated successfully!", "OK");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }







        ///
        ///Peer reviews section
        ///

        public class PeerReview
        {
            public int Id { get; set; }
            public DateTime StartDate { get; set; } // Renamed from Timestamp to StartDate
            public DateTime EndDate { get; set; } // Ensure end date is also added if not already
            public List<TeamMemberComment> TeamMemberComments { get; set; } // Comments for each team member
        }



        //Peer review details, ended up not using propertychanged due to bugs
        public class TeamMemberComment
        {
            public string Comment { get; set; }
            public int TeammateId { get; set; }
            public string TeammateName { get; set; }
            public int ReviewId { get; set; }
            public int ClassId { get; set; }
            public int QualityOfWork { get; set; }
            public int Timeliness { get; set; }
            public int Communication { get; set; }
            public int Teamwork { get; set; }
            public int EffortParticipation { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }




        // Loading peer reviews for a student
        private void LoadPeerReviews(int studentId, int classId)
        {
            var peerReviews = new List<PeerReview>();
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema3";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
 SELECT 
    pr.review_period_id AS ReviewId,
    rp.start_date AS StartDate,
    rp.end_date AS EndDate,
    pr.qual_of_work_rating AS QualityOfWork,
    pr.timeliness_rating AS Timeliness,
    pr.communication_rating AS Communication,
    pr.teamwork_rating AS Teamwork,
    pr.eff_and_part_rating AS EffortParticipation,
    pr.comments AS ReviewComments,
    r.first_name AS ReviewerFirstName,
    r.last_name AS ReviewerLastName,
    e.first_name AS RevieweeFirstName,
    e.last_name AS RevieweeLastName,
    pr.reviewee_id AS RevieweeId -- Extract the reviewee_id
    
FROM 
    test_schema3.peer_review pr
JOIN 
    test_schema3.review_period rp ON pr.review_period_id = rp.review_id
JOIN 
    test_schema3.student r ON pr.reviewer_id = r.id
JOIN 
    test_schema3.student e ON pr.reviewee_id = e.id
WHERE 
    pr.reviewer_id = @studentId
ORDER BY 
    rp.start_date DESC;";

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@studentId", studentId);
                        cmd.Parameters.AddWithValue("@classId", classId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Get StartDate and EndDate from the database
                                var startDate = reader.GetDateTime("StartDate");
                                var endDate = reader.GetDateTime("EndDate");

                                // Check if a peer review with the same StartDate and EndDate already exists
                                var existingPeerReview = peerReviews.FirstOrDefault(pr => pr.StartDate == startDate && pr.EndDate == endDate);

                                if (existingPeerReview == null)
                                {
                                    // Create a new peer review if it does not exist
                                    existingPeerReview = new PeerReview
                                    {
                                        StartDate = startDate,
                                        EndDate = endDate,
                                        TeamMemberComments = new List<TeamMemberComment>() // Initialize the comments list
                                    };

                                    // Add the new peer review to the list
                                    peerReviews.Add(existingPeerReview);
                                }

                                // Now add the comment to the existing or newly created peer review
                                existingPeerReview.TeamMemberComments.Add(new TeamMemberComment
                                {
                                    // Use the reviewee's name as the TeammateName
                                    TeammateName = $"{reader.GetString("RevieweeFirstName")} {reader.GetString("RevieweeLastName")}",
                                    Comment = reader.GetString("ReviewComments"),
                                    QualityOfWork = reader.GetInt32("QualityOfWork"),
                                    Timeliness = reader.GetInt32("Timeliness"),
                                    Communication = reader.GetInt32("Communication"),
                                    Teamwork = reader.GetInt32("Teamwork"),
                                    EffortParticipation = reader.GetInt32("EffortParticipation"),
                                    StartDate = startDate,
                                    EndDate = endDate,
                                    ReviewId = reader.GetInt32("ReviewId"),
                                    ClassId = classId,
                                    TeammateId = reader.GetInt32("RevieweeId") // Assign the reviewee_id here
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log them)
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            // Bind peer reviews to PeerReviewsCollectionView
            PeerReviewsCollectionView.ItemsSource = peerReviews;
        }


        // Event handler for selecting a peer review
        private void PeerReviewsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0 && e.CurrentSelection[0] is PeerReview selectedReview)
            {
                
                // Bind teammates in selected review to TeammatesCollectionView
                TeammatesCollectionView.ItemsSource = selectedReview.TeamMemberComments;
                PeerReviewDetailsStack.BindingContext = selectedReview;
                PeerReviewDetailsStack.IsVisible = false; // Hide details until teammate is selected
                TeammatesCollectionView.IsVisible = true;
            }
        }

        // Event handler for selecting a teammate
        
        private void TeammatesCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0 && e.CurrentSelection[0] is TeamMemberComment selectedTeammate)
            {
                this.currentRevieweeStudentId = selectedTeammate.TeammateId;
                PeerReviewDetailsStack.IsVisible = true;

                // Bind comments to CommentsCollectionView for the selected teammate
                //CommentsCollectionView.ItemsSource = (PeerReviewDetailsStack.BindingContext as PeerReview)?.TeamMemberComments;

                // Use the reviewer's name as the TeammateName
                this.TeammateName = selectedTeammate.TeammateName;
                this.Comment = selectedTeammate.Comment;
                this.QualityOfWork = selectedTeammate.QualityOfWork;
                this.Timeliness = selectedTeammate.Timeliness;
                this.Communication = selectedTeammate.Communication;
                this.Teamwork = selectedTeammate.Teamwork;
                this.EffortParticipation = selectedTeammate.EffortParticipation;
                this.StartDate = selectedTeammate.StartDate;
                this.EndDate = selectedTeammate.EndDate;
                this.ReviewId = selectedTeammate.ReviewId;
                //this.ClassId = selectedTeammate.ClassId; this is bugging, something about string error

                // Set the binding context to the selected teammate
                PeerReviewDetailsStack.BindingContext = selectedTeammate;
                
            }
        }









        /// 
        /// 
        /// add and removal of peer reviews
        /// 
        /// 

        private async void AddNewPeerReviewButton_Clicked(object sender, EventArgs e)
        {
            // Define the MySQL connection string
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema3";

            // Initialize the connection object
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the MySQL connection
                    await connection.OpenAsync();

                    // Call the method to generate peer reviews for the specified class
                    await GeneratePeerReviewsForClass(connection, this.classId);

                    LoadPeerReviews(this.currentStudentId, this.classId); // Load peer reviews for the selected student
                    PeerReviewDetailsStack.IsVisible = false;

                    //await DisplayAlert("Success", "Peer reviews generated successfully for the selected class.", "OK");

                    
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during connection or peer review generation
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
        }


        // Method to generate peer reviews for all students in a selected class by team, and instill a new peer review period
        private async Task GeneratePeerReviewsForClass(MySqlConnection connection, int classId)
        {
            
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today;

           
            int reviewPeriodId = await CreateNewReviewPeriod(connection, startDate, endDate);

            // Retrieve all students in the specified class, grouped by team_id
            var selectStudentsCommandText = @"
        SELECT id, team_id 
        FROM student 
        WHERE class_id = @class_id 
        ORDER BY team_id";

            var studentsByTeam = new Dictionary<int, List<int>>(); // team_id -> list of student IDs

            using (var selectStudentsCommand = new MySqlCommand(selectStudentsCommandText, connection))
            {
                selectStudentsCommand.Parameters.AddWithValue("@class_id", classId);

                using (var reader = await selectStudentsCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int studentId = reader.GetInt32(0);
                        int teamId = reader.GetInt32(1);

                        if (!studentsByTeam.ContainsKey(teamId))
                        {
                            studentsByTeam[teamId] = new List<int>();
                        }

                        studentsByTeam[teamId].Add(studentId);
                    }
                }
            }

            // Generate peer reviews for each team
            foreach (var team in studentsByTeam.Values)
            {
                // Every student reviews every other student in the same team
                foreach (var reviewerId in team)
                {
                    foreach (var revieweeId in team)
                    {
                        await CreatePeerReview(connection, reviewerId, revieweeId, reviewPeriodId); // Create review
                    }
                }
            }

            await DisplayAlert("Success", "Peer reviews (including self-reviews) generated successfully for the selected class.", "OK");
        }

        // Method to create a new review period with specified start and end dates, linked to a specific class
        private async Task<int> CreateNewReviewPeriod(MySqlConnection connection, DateTime startDate, DateTime endDate)
        {
            // Inserting a new review period with provided dates and class_id
            string insertReviewPeriodCommandText = @"
INSERT INTO review_period (class_id, start_date, end_date) 
VALUES (@class_id, @start_date, @end_date);
SELECT LAST_INSERT_ID();";

            using (var insertCommand = new MySqlCommand(insertReviewPeriodCommandText, connection))
            {
                insertCommand.Parameters.AddWithValue("@class_id", this.classId);

                insertCommand.Parameters.AddWithValue("@start_date", startDate);
                insertCommand.Parameters.AddWithValue("@end_date", endDate);
                
                object result = await insertCommand.ExecuteScalarAsync();
                return Convert.ToInt32(result); // Return the generated review_period_id
            }
        }

        // Method to create a peer review, associating it with the given review period ID
        private async Task CreatePeerReview(MySqlConnection connection, int reviewerId, int revieweeId, int reviewPeriodId)
        {
            // Initialize rating variables
            int qualOfWorkRating = 0;
            int timelinessRating = 0;
            int teamworkRating = 0;
            int effAndPartRating = 0;
            int communicationRating = 0;
            string comments = "";

            // Insert the new peer review associated with the review period
            var insertPeerReviewCommandText = @"
        INSERT INTO peer_review (review_period_id, reviewer_id, reviewee_id, 
                                 qual_of_work_rating, timeliness_rating, teamwork_rating, 
                                 eff_and_part_rating, communication_rating, comments)
        VALUES (@review_period_id, @reviewer_id, @reviewee_id, 
                @qual_of_work_rating, @timeliness_rating, @teamwork_rating, 
                @eff_and_part_rating, @communication_rating, @comments)";

            using (var insertCommand = new MySqlCommand(insertPeerReviewCommandText, connection))
            {
                insertCommand.Parameters.AddWithValue("@review_period_id", reviewPeriodId);
                insertCommand.Parameters.AddWithValue("@reviewer_id", reviewerId);
                insertCommand.Parameters.AddWithValue("@reviewee_id", revieweeId);
                insertCommand.Parameters.AddWithValue("@qual_of_work_rating", qualOfWorkRating);
                insertCommand.Parameters.AddWithValue("@timeliness_rating", timelinessRating);
                insertCommand.Parameters.AddWithValue("@teamwork_rating", teamworkRating);
                insertCommand.Parameters.AddWithValue("@eff_and_part_rating", effAndPartRating);
                insertCommand.Parameters.AddWithValue("@communication_rating", communicationRating);
                insertCommand.Parameters.AddWithValue("@comments", comments);

                await insertCommand.ExecuteNonQueryAsync();
            }
        }









        
        // Deletion of peer reviews and their corresponding review period
        private async void RemovePeerReviewButton_Clicked(object sender, EventArgs e)
        {
            // Get classId and prompt for dates
            int classId = this.classId;
            string startDateInput = await DisplayPromptAsync("Start Date", "Enter the start date (yyyy-mm-dd):");
            string endDateInput = await DisplayPromptAsync("End Date", "Enter the end date (yyyy-mm-dd):");

            // Parse dates
            if (!DateTime.TryParseExact(startDateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime startDate) ||
                !DateTime.TryParseExact(endDateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime endDate))
            {
                await DisplayAlert("Error", "Invalid date format. Please enter dates in yyyy-mm-dd format.", "OK");
                return;
            }

            // Database connection
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema3";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Finding the review_period_id with matching class_id, start_date, and end_date
                    string selectReviewPeriodQuery = @"
SELECT review_id 
FROM review_period 
WHERE class_id = @classId 
AND start_date = @startDate 
AND end_date = @endDate";

                    int reviewPeriodId = 0;

                    using (var selectCmd = new MySqlCommand(selectReviewPeriodQuery, connection))
                    {
                        selectCmd.Parameters.AddWithValue("@classId", classId);
                        selectCmd.Parameters.AddWithValue("@startDate", startDate);
                        selectCmd.Parameters.AddWithValue("@endDate", endDate);

                        object result = await selectCmd.ExecuteScalarAsync();
                        if (result != null)
                        {
                            reviewPeriodId = Convert.ToInt32(result);
                        }
                    }

                    // If no matching review_period is found, notify and exit
                    if (reviewPeriodId == 0)
                    {
                        await DisplayAlert("Error", "No matching review period found for the specified dates and class.", "OK");
                        return;
                    }

                    // Deleting peer reviews associated with the found review_period_id
                    string deletePeerReviewsQuery = @"
DELETE FROM peer_review 
WHERE review_period_id = @reviewPeriodId";

                    int peerReviewsDeleted = 0;

                    using (var deleteCmd = new MySqlCommand(deletePeerReviewsQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("@reviewPeriodId", reviewPeriodId);
                        peerReviewsDeleted = await deleteCmd.ExecuteNonQueryAsync();
                    }

                    // Deleting the review period itself
                    string deleteReviewPeriodQuery = @"
DELETE FROM review_period 
WHERE review_id = @reviewPeriodId";

                    int reviewPeriodDeleted = 0;

                    using (var deleteCmd = new MySqlCommand(deleteReviewPeriodQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("@reviewPeriodId", reviewPeriodId);
                        reviewPeriodDeleted = await deleteCmd.ExecuteNonQueryAsync();
                    }

                    // Refresh the peer reviews and notify the user
                    LoadPeerReviews(this.currentStudentId, this.classId); // Reload peer reviews for the selected student
                    PeerReviewDetailsStack.IsVisible = false;

                    // Notify the number of records deleted
                    await DisplayAlert("Success", $"{peerReviewsDeleted} peer review record(s) and {reviewPeriodDeleted} review period(s) deleted.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }




        /// 
        /// 
        /// update button for peer review view
        /// 
        /// 

        private async void PeerReviewOnUpdateInformationClicked(object sender, EventArgs e)
        {
            // Access the current BindingContext
            var currentContext = PeerReviewDetailsStack.BindingContext as TeamMemberComment; 

            if (currentContext != null)
            {
                // Retrieve updated values from the BindingContext
                var updatedComment = currentContext.Comment;
                var updatedTimeliness = currentContext.Timeliness;
                var updatedStartDate = currentContext.StartDate;
                var updatedEndDate = currentContext.EndDate;
                var updatedQualityOfWork = currentContext.QualityOfWork;
                var updatedTeamwork = currentContext.Teamwork;
                var updatedEffortParticipation = currentContext.EffortParticipation;
                var updatedCommunication = currentContext.Communication;


                /*
        
        private string Comment;
        private int QualityOfWork;
        private int Timeliness;
        private int Communication;
        private int Teamwork;
        private int EffortParticipation;
        private DateTime StartDate;
        private DateTime EndDate;
        private int ReviewId;
                private int classId;
                */


                //check each updated var, starting with startdate and enddate as those two are the worst to query

                /*
                 basically check if updatedvar != this.var, if it does not equal then we need to update query
                now for dates, we can hasten query by simulatenously dealing with both updated start and end dates

                scan all peer_review, crossreference peer_review's reviewer_id (fkey to student id) by going into student table ensuring the student is within the same this.classId
                if class_id in student record == this.classId then modify the both start_date and end_date respectively



                for the rest, simply check if the updatedvar != this.var, if it does not equal then we need to plan to change it
                so instead of querying one at a time we could prepare a list or table of vars to modify or something so we scan once and apply changes in a burst
                 */
                if (updatedComment != this.Comment)
                {
                    UpdateDatabaseOnPropertyChanged("comments", updatedComment);
                    this.Communication = updatedCommunication;
                }
                
                if (updatedTimeliness != this.Timeliness)
                {
                    UpdateDatabaseOnPropertyChanged("timeliness_rating", updatedTimeliness);
                    this.Communication = updatedCommunication;
                }
                if (updatedQualityOfWork != this.QualityOfWork)
                {
                    UpdateDatabaseOnPropertyChanged("qual_of_work_rating", updatedQualityOfWork);
                    this.Communication = updatedCommunication;
                }
                if (updatedTeamwork != this.Teamwork)
                {
                    UpdateDatabaseOnPropertyChanged("teamwork_rating", updatedTeamwork);
                    this.Communication = updatedCommunication;
                }
                if (updatedEffortParticipation != this.EffortParticipation)
                {
                    UpdateDatabaseOnPropertyChanged("eff_and_part_rating", updatedEffortParticipation);
                    this.Communication = updatedCommunication;
                }
                if (updatedCommunication != this.Communication)
                {
                    UpdateDatabaseOnPropertyChanged("communication_rating", updatedCommunication);
                    this.Communication = updatedCommunication;  

                }

                if (updatedStartDate != this.StartDate || updatedEndDate != this.EndDate)
                {
                    UpdateDatabaseOnPropertyChangedDate(updatedStartDate, updatedEndDate);
                    this.StartDate = updatedStartDate;
                    this.EndDate = updatedEndDate;  
                }
                await DisplayAlert("Success", "Peer Review(s) updated!", "OK");
            }
            else
            {
                Console.WriteLine("BindingContext is null");
            }
        }


        // Updates the review period and all associated peer reviews within the same class
        private void UpdateDatabaseOnPropertyChangedDate(DateTime newStartDate, DateTime newEndDate)
        {
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema3";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Update the review_period table with the new dates
                    using (var updateReviewPeriodCmd = new MySqlCommand())
                    {
                        updateReviewPeriodCmd.Connection = connection;

                        updateReviewPeriodCmd.CommandText = @"
UPDATE review_period
SET start_date = @newStartDate, end_date = @newEndDate
WHERE review_id = @reviewId";

                        // Use Date property to ensure only the date part is passed
                        updateReviewPeriodCmd.Parameters.AddWithValue("@newStartDate", newStartDate.Date);
                        updateReviewPeriodCmd.Parameters.AddWithValue("@newEndDate", newEndDate.Date);
                        updateReviewPeriodCmd.Parameters.AddWithValue("@reviewId", this.ReviewId);

                        int rowsAffected = updateReviewPeriodCmd.ExecuteNonQuery();

                        // Log the result
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Review period updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No rows updated. Ensure the ReviewId is correct.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle and log exceptions
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }



        private void UpdateDatabaseOnPropertyChanged(string propertyName, object propertyValue)
        {
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema3";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = connection;

                        // Prepare the SQL query
                        cmd.CommandText = $@"
UPDATE peer_review
SET {propertyName.ToLower()} = @newValue
WHERE review_period_id = @reviewPeriodId
  AND reviewer_id = @currentStudentId
  AND reviewee_id = @currentRevieweeStudentId";

                        // Add parameters
                        cmd.Parameters.AddWithValue("@newValue", propertyValue);
                        cmd.Parameters.AddWithValue("@reviewPeriodId", this.ReviewId);
                        cmd.Parameters.AddWithValue("@currentStudentId", this.currentStudentId);
                        cmd.Parameters.AddWithValue("@currentRevieweeStudentId", this.currentRevieweeStudentId);

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        //Log the outcome
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"{rowsAffected} row(s) updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No rows updated. Ensure the review period and student IDs are correct.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        /// 
        /// 
        /// update button for peer review view
        /// 
        /// 


    }
}
