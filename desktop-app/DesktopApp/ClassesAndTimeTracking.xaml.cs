using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Maui.Controls;
using System.IO;
using System.Linq;

using MySql.Data.MySqlClient;
using System.Data;//Mysql

namespace DesktopApp
{
    public partial class ClassesAndTimeTracking : ContentPage
    {
        //written by William LaFoy for CS4485, Project, 10/9/2024-10/25/2024. NetID wel190000
        /*
         Base framework for listing out classes, listing out associated students and cummulative hours
         and specific dates with those hours.

        10/18 now allows you to select cells with ease and modify the date hour etc.

        10/25 now displays peer reviews, button to make a new peer review exists, but functionality will depend on sql calls.
         
         10/30 features backend connectivity with mysql, used gwangmo's code and info to jumpstart this.
         */

        


        private int professorId; // Store the professor ID
        

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
                                    ClassId = reader.GetInt32(0), // Assuming id is the first column
                                    ClassName = reader.GetString(1) // Assuming class_name is the second column
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
                LoadStudentHours(selectedClass.ClassId);
            }
        }






        // Sample data structure for student hours
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
        WHERE sh.class_id = @classId
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
                                    TeamNumber = reader.GetInt32(3), // Assuming team_id is the fourth column
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





        // Event handler for importing students (not implemented)
        private async void ImportStudents_Click(object sender, EventArgs e)
        {
            // Prompt for the CSV file path
            //string filePath = "C:/Users/YourUsername/Documents/file.csv";
            //string filePath = "C:\\Users\\YourUsername\\Documents\\file.csv";
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

                    // Read each line in the CSV
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Split the line by commas into an array of strings (columns)
                        var columns = line.Split(',');

                        // Add the row (array of columns) to the studentsData list
                        studentsData.Add(columns);


                        
                    }
                }

                // Display success message (optional)
                await DisplayAlert("Success", $"{studentsData.Count} rows imported successfully!", "OK");

                //process studentsData further
                foreach (var row in studentsData)
                {
                    //Will need to read class teamnumber student name date hour date hour ...etc
                    Console.WriteLine($"First column: {row[0]}, Second column: {row[1]}, Third column: {row[2]}");
                }
            }
            catch (Exception ex)
            {
                // Handle errors file format issues, read errors
                await DisplayAlert("Error", $"An error occurred while importing students: {ex.Message}", "OK");
            }
        }












        /// 
        /// student details section
        /// 


        // Event handler for selecting a student to view detailed hours
        private void StudentHoursCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0 && e.CurrentSelection[0] is StudentHour selectedHour)
            {
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

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        // Method to load details for the selected student
        private void LoadStudentDetails(int studentId, int classId)
        {
            var hours = new List<StudentHourDetail>();
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
            WHERE sh.student_id = @studentId AND sh.class_id = @classId
            ORDER BY sh.date DESC"; // Order by date for latest first

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@studentId", studentId);
                        cmd.Parameters.AddWithValue("@classId", classId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                hours.Add(new StudentHourDetail
                                {
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
            HoursCollectionView.ItemsSource = hours; // Bind the list of hours to the CollectionView
            StudentDetailStack.IsVisible = true; // Show the details stack
            PeerReviewStack.IsVisible = true; // Show the peer review stack
        }





        ///
        ///Peer reviews section
        ///
        public class PeerReview
        {
            public DateTime Timestamp { get; set; } // The deadline or timestamp of the review

           
            public List<TeamMemberComment> TeamMemberComments { get; set; } // Comments for each team member
        }

        public class TeamMemberComment
        {
            public string TeammateName { get; set; }
            public string Comment { get; set; }

            public int QualityOfWork { get; set; }
            public int Timeliness { get; set; }
            public int Communication { get; set; }
            public int Teamwork { get; set; }
            public int EffortParticipation { get; set; }

            public int TeammateId { get; set; }
        }



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
            pr.review_date AS Timestamp,
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
            pr.reviewee_id = @studentId 
            AND pr.class_id = @classId
        ORDER BY 
            pr.review_date DESC;";  // Order by review date

                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@studentId", studentId);
                        cmd.Parameters.AddWithValue("@classId", classId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Get the review timestamp
                                var reviewTimestamp = reader.GetDateTime("Timestamp");

                                // Check if the peer review already exists
                                var existingPeerReview = peerReviews.FirstOrDefault(pr => pr.Timestamp == reviewTimestamp);

                                if (existingPeerReview == null)
                                {
                                    // Create a new peer review if it does not exist
                                    existingPeerReview = new PeerReview
                                    {
                                        Timestamp = reviewTimestamp,
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
                                    EffortParticipation = reader.GetInt32("EffortParticipation")
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
                PeerReviewDetailsStack.IsVisible = true;

                // Bind comments to CommentsCollectionView for the selected teammate
                CommentsCollectionView.ItemsSource = (PeerReviewDetailsStack.BindingContext as PeerReview)?.TeamMemberComments;
            
                // Set the binding context to the selected teammate
                PeerReviewDetailsStack.BindingContext = selectedTeammate;

                }
        }




        private void AddNewPeerReviewButton_Clicked(object sender, EventArgs e)
        {
            // Logic to add a new peer review
            
           
        }

    }
}
