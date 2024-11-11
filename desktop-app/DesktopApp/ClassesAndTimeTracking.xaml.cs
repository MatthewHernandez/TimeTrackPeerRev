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
        //written by William LaFoy for CS4485, Project, 10/9/2024-11/10/2024. NetID wel190000
        /*
         Base framework for listing out classes, listing out associated students and cummulative hours
         and specific dates with those hours.

        10/18 now allows you to select cells with ease and modify the date hour etc.

        10/25 now displays peer reviews, button to make a new peer review exists, but functionality will depend on sql calls.
         
         10/30 features backend connectivity with mysql, used gwangmo's code and critical info to do this. displays db info

        11/8-10 features add/modify/deletion and reflection to the connected db
         */

        


        private int professorId; // Store the professor ID
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

        // Declare a private field to hold student hour details
        private List<StudentHourDetail> _studentHourDetails = new List<StudentHourDetail>();

        // Constructor that takes professorId as a parameter
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

        // Get classes from the database
        private List<ClassInfo> GetClassesForProfessor(int professorId)
        {
            var classes = new List<ClassInfo>();
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema";

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
                    // Handle exceptions (e.g., log them)
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return classes; // Return the list of class info
        }


        // Class to hold class information
        public class ClassInfo
        {
            public int ClassId { get; set; }
            public string ClassName { get; set; }

            public override string ToString() => ClassName; // This makes the ClassPicker display class names
        }


        // Event handler when the class is selected
        private void ClassPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClassPicker.SelectedItem is ClassInfo selectedClass)
            {
                this.classId = selectedClass.ClassId;
                LoadStudentHours(selectedClass.ClassId);
            }
        }






        //data structure for student hours
        public class StudentHour
        {
            public string StudentName { get; set; }


            public int TeamNumber { get; set; }
            public decimal Hours { get; set; }

            public int StudentId { get; set; }
            public int ClassId { get; set; }
            // public string studentinfo { get; set; }
        }


        // Method to load student hours based on selected class
        private void LoadStudentHours(int classId)
        {
            var studentHours = new List<StudentHour>();
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema";

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
                FROM student_hours sh
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
                    // Handle exceptions (e.g., log them)
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            // Bind to CollectionView
            StudentHoursCollectionView.ItemsSource = studentHours.OrderBy(sh => sh.TeamNumber).ThenBy(sh => sh.StudentName).ToList(); // Sorting by team number and name
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

                        // CSV file has columns that match id, team_id, first_name, last_name, email, class_id
                        if (columns.Length >= 6)
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
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    foreach (var student in studentsData)
                    {
                        // Insert student
                        var commandText = "INSERT INTO students (id, team_id, first_name, last_name, email, class_id) VALUES (@id, @team_id, @first_name, @last_name, @email, @class_id)";
                        using (var command = new MySqlCommand(commandText, connection))
                        {
                            command.Parameters.AddWithValue("@id", student[0]);
                            command.Parameters.AddWithValue("@team_id", student[1]);
                            command.Parameters.AddWithValue("@first_name", student[2]);
                            command.Parameters.AddWithValue("@last_name", student[3]);
                            command.Parameters.AddWithValue("@email", student[4]);
                            command.Parameters.AddWithValue("@class_id", student[5]);

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
        /// student details section, hours date what was done
        /// 


        // Event handler for selecting a student to view detailed hours
        private void StudentHoursCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0 && e.CurrentSelection[0] is StudentHour selectedHour)
            {
                this.currentStudentId = selectedHour.StudentId;
                LoadStudentDetails(selectedHour.StudentId, selectedHour.ClassId);
                LoadPeerReviews(selectedHour.StudentId, selectedHour.ClassId); // Load peer reviews for the selected student
            }
        }



        // Updated StudentHourDetail with INotifyPropertyChanged
        public class StudentHourDetail : INotifyPropertyChanged
        {
            private DateTime _date;
            
            private decimal _hours;
            private string _studentinfo;


            private DateTime current_date;

            private decimal current_hours;
            private string current_studentinfo;

            public DateTime Date
            {
                get => _date;
                set
                {
                    if (_date != value)
                    {
                        _date = value;
                        OnPropertyChanged(nameof(Date));
                    }
                }
            }


            public decimal Hours
            {
                get => _hours;
                set
                {
                    if (_hours != value)
                    {
                        _hours = value;
                        OnPropertyChanged(nameof(Hours));
                    }
                }
            }

            public string studentinfo
            {
                get => _studentinfo;
                set
                {
                    if (_studentinfo != value)
                    {
                        _studentinfo = value;
                        OnPropertyChanged(nameof(studentinfo));
                    }
                }
            }

            public DateTime CurrentDate
            {
                get => current_date;
                set
                {
                    if (current_date != value)
                    {
                        current_date = value;
                        OnPropertyChanged(nameof(current_date));
                    }
                }
            }


            public decimal CurrentHours
            {
                get => current_hours;
                set
                {
                    if (current_hours != value)
                    {
                        current_hours = value;
                        OnPropertyChanged(nameof(current_hours));
                    }
                }
            }

            public string Currentstudentinfo
            {
                get => current_studentinfo;
                set
                {
                    if (current_studentinfo != value)
                    {
                        current_studentinfo = value;
                        OnPropertyChanged(nameof(current_studentinfo));
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        // Method to load details for the selected student
        private void LoadStudentDetails(int studentId, int classId)
        {
            _studentHourDetails.Clear(); // Clear any existing data before loading new details
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema";

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
            FROM student_hours sh
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

        private async void StudentDetailsOnUpdateInformationClicked(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    foreach (var detail in _studentHourDetails)
                    {
                        
                        // Check if Hours has changed
                        if (detail.CurrentHours != detail.Hours)
                        {
                            Console.WriteLine("Hours has changed");

                            // Update hours in database
                            string updateHoursQuery = @"
                        UPDATE student_hours
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





                        /*
                         // Check if Date has changed
                        if (detail.CurrentDate != detail.Date)
                        {
                            Console.WriteLine("Date has changed");

                            // Update date in database
                            string updateDateQuery = @"
                        UPDATE student_hours
                        SET date = @newDate
                        WHERE student_id = @studentId AND date = @currentDate";

                            using (var cmd = new MySqlCommand(updateDateQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@newDate", detail.Date);
                                cmd.Parameters.AddWithValue("@studentId", this.currentStudentId);
                                cmd.Parameters.AddWithValue("@currentDate", detail.CurrentDate);
                                await cmd.ExecuteNonQueryAsync();
                            }

                            detail.CurrentDate = detail.Date; // Update the local current value
                        }



                        // Check if Comments (studentinfo) has changed
                        if (detail.Currentstudentinfo != detail.studentinfo)
                        {
                            Console.WriteLine("Comments have changed");

                            // Update comments in database
                            string updateCommentsQuery = @"
                        UPDATE student_hours
                        SET comments = @newComments
                        WHERE student_id = @studentId AND date = @currentDate";

                            using (var cmd = new MySqlCommand(updateCommentsQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@newComments", detail.studentinfo);
                                cmd.Parameters.AddWithValue("@studentId", this.currentStudentId);
                                cmd.Parameters.AddWithValue("@currentDate", detail.CurrentDate);
                                await cmd.ExecuteNonQueryAsync();
                            }

                            detail.Currentstudentinfo = detail.studentinfo; // Update the local current value
                        }

                         */

                    }
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




        public class TeamMemberComment : INotifyPropertyChanged
        {
            private int _qualityOfWork;
            private int _timeliness;
            private int _communication;
            private int _teamwork;
            private int _effortParticipation;
            private DateTime _startDate;
            private DateTime _endDate;
            private string _comment;

            // Fields to store initial values
            private DateTime _initialStartDate;
            private DateTime _initialEndDate;

            public string Comment
            {
                get => _comment;
                set
                {
                    if (_comment != value)
                    {
                        _comment = value;
                        OnPropertyChanged(nameof(Comment));
                    }
                }
            }

            public int TeammateId { get; set; }
            public string TeammateName { get; set; }
            public int ReviewId { get; set; }
            public int ClassId { get; set; }

            public int QualityOfWork
            {
                get => _qualityOfWork;
                set
                {
                    if (_qualityOfWork != value)
                    {
                        _qualityOfWork = value;
                        OnPropertyChanged(nameof(QualityOfWork));
                    }
                }
            }

            public int Timeliness
            {
                get => _timeliness;
                set
                {
                    if (_timeliness != value)
                    {
                        _timeliness = value;
                        OnPropertyChanged(nameof(Timeliness));
                    }
                }
            }

            public int Communication
            {
                get => _communication;
                set
                {
                    if (_communication != value)
                    {
                        _communication = value;
                        OnPropertyChanged(nameof(Communication));
                    }
                }
            }

            public int Teamwork
            {
                get => _teamwork;
                set
                {
                    if (_teamwork != value)
                    {
                        _teamwork = value;
                        OnPropertyChanged(nameof(Teamwork));
                    }
                }
            }

            public int EffortParticipation
            {
                get => _effortParticipation;
                set
                {
                    if (_effortParticipation != value)
                    {
                        _effortParticipation = value;
                        OnPropertyChanged(nameof(EffortParticipation));
                    }
                }
            }

            public DateTime StartDate
            {
                get => _startDate;
                set
                {
                    if (_startDate != value)
                    {
                        // Set initial date if this is the first change
                        if (_initialStartDate == DateTime.MinValue) _initialStartDate = _startDate;
                        _startDate = value;
                        OnPropertyChanged(nameof(StartDate));
                    }
                }
            }

            public DateTime EndDate
            {
                get => _endDate;
                set
                {
                    if (_endDate != value)
                    {
                        // Set initial date if this is the first change
                        if (_initialEndDate == DateTime.MinValue) _initialEndDate = _endDate;
                        _endDate = value;
                        OnPropertyChanged(nameof(EndDate));
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {

                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
               // UpdateDatabaseOnPropertyChanged(propertyName);
            }

            



        }




        //need to edit class_id should not exist in peer_review directly.

        // Loading peer reviews for a student
        private void LoadPeerReviews(int studentId, int classId)
        {
            var peerReviews = new List<PeerReview>();
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
 SELECT 
    pr.id AS ReviewId,
    pr.start_date AS StartDate,
    pr.end_date AS EndDate,
    pr.qual_of_work_rating AS QualityOfWork,
    pr.timeliness_rating AS Timeliness,
    pr.communication_rating AS Communication,
    pr.teamwork_rating AS Teamwork,
    pr.eff_and_part_rating AS EffortParticipation,
    pr.comments AS ReviewComments,
    r.first_name AS ReviewerFirstName,
    r.last_name AS ReviewerLastName,
    e.first_name AS RevieweeFirstName,
    e.last_name AS RevieweeLastName
FROM 
    test_schema.peer_review pr
JOIN 
    test_schema.student r ON pr.reviewer_id = r.id  -- Reviewer details
JOIN 
    test_schema.student e ON pr.reviewee_id = e.id  -- Reviewee details
WHERE 
    pr.reviewer_id = @studentId   -- Get all peer reviews for this reviewer ID
ORDER BY 
    pr.start_date DESC;";

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
                                    // Use the reviewer's name as the TeammateName
                                    TeammateName = $"{reader.GetString("ReviewerFirstName")} {reader.GetString("ReviewerLastName")}",
                                    Comment = reader.GetString("ReviewComments"),
                                    QualityOfWork = reader.GetInt32("QualityOfWork"),
                                    Timeliness = reader.GetInt32("Timeliness"),
                                    Communication = reader.GetInt32("Communication"),
                                    Teamwork = reader.GetInt32("Teamwork"),
                                    EffortParticipation = reader.GetInt32("EffortParticipation"),
                                    StartDate = startDate,
                                    EndDate = endDate,
                                    ReviewId = reader.GetInt32("ReviewId"),
                                    ClassId = classId
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
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema";

            // Initialize the connection object
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Open the MySQL connection
                    await connection.OpenAsync();

                    // Call the method to generate peer reviews for the specified class
                    await GeneratePeerReviewsForClass(connection, this.classId);

                    await DisplayAlert("Success", "Peer reviews generated successfully for the selected class.", "OK");
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during connection or peer review generation
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
        }

       
        // Method to generate peer reviews for all students in a selected class by team
        private async Task GeneratePeerReviewsForClass(MySqlConnection connection, int classId)
        {

            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today;

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
                        await CreatePeerReview(connection, reviewerId, revieweeId, startDate, endDate); // Create review
                    }
                }
            }

            await DisplayAlert("Success", "Peer reviews (including self-reviews) generated successfully for the selected class.", "OK");
        }

        // Method to create a peer review using existing start and end dates and with self-review capability
        private async Task CreatePeerReview(MySqlConnection connection, int reviewerId, int revieweeId, DateTime? optionalStartDate = null, DateTime? optionalEndDate = null)
        {
            // Use optional dates if provided, otherwise default to today's date
            DateTime startDate = optionalStartDate ?? DateTime.Today;
            DateTime endDate = optionalEndDate ?? DateTime.Today;

            // Insert the new peer review with the specified or defaulted start and end dates
            var insertPeerReviewCommandText = @"
INSERT INTO peer_review (reviewer_id, reviewee_id, start_date, end_date, 
                         qual_of_work_rating, timeliness_rating, teamwork_rating, 
                         eff_and_part_rating, communication_rating, comments)
VALUES (@reviewer_id, @reviewee_id, @start_date, @end_date, 
        0, 0, 0, 0, 0, '')";

            using (var insertCommand = new MySqlCommand(insertPeerReviewCommandText, connection))
            {
                insertCommand.Parameters.AddWithValue("@reviewer_id", reviewerId);
                insertCommand.Parameters.AddWithValue("@reviewee_id", revieweeId);
                insertCommand.Parameters.AddWithValue("@start_date", startDate);
                insertCommand.Parameters.AddWithValue("@end_date", endDate);

                await insertCommand.ExecuteNonQueryAsync();
            }
        }











        //deletion of peer reviews
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
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // DELETE query to remove peer reviews that match the start date, end date, and class_id
                    string deleteQuery = @"
            DELETE pr
            FROM peer_review pr
            INNER JOIN student s ON pr.reviewer_id = s.id
            WHERE s.class_id = @classId 
            AND pr.start_date = @startDate
            AND pr.end_date = @endDate";

                    using (var cmd = new MySqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@classId", classId);
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        // Notify how many records were deleted
                        await DisplayAlert("Success", $"{rowsAffected} peer review record(s) deleted.", "OK");
                    }
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

            }
            else
            {
                Console.WriteLine("BindingContext is null");
            }
        }

        private void UpdateDatabaseOnPropertyChangedDate(DateTime newStartDate, DateTime newEndDate)
        {
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;

                    // Update both start_date and end_date based on class_id
                    cmd.CommandText = @"
                UPDATE peer_review
                SET start_date = @newStartDate, end_date = @newEndDate
                WHERE class_id = @classId";

                    cmd.Parameters.AddWithValue("@newStartDate", newStartDate);
                    cmd.Parameters.AddWithValue("@newEndDate", newEndDate);
                    cmd.Parameters.AddWithValue("@classId", this.classId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateDatabaseOnPropertyChanged(string propertyName, object propertyvalue)
        {
            string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;

                    // Prepare the SQL query based on the property name
                    cmd.CommandText = $"UPDATE peer_review SET {propertyName.ToLower()} = @newValue WHERE id = @reviewId";

                    // Ensure the correct type is handled based on the property value
                    cmd.Parameters.AddWithValue("@newValue", propertyvalue);
                    cmd.Parameters.AddWithValue("@reviewId", this.ReviewId);

                    cmd.ExecuteNonQuery();
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
