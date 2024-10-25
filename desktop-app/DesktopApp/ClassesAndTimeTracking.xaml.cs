using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Maui.Controls;
using System.IO;
using System.Linq;

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
         */

        // Sample data structure for student hours
        public class StudentHour
        {
            public string StudentName { get; set; }


            public int TeamNumber { get; set; }
            public decimal Hours { get; set; }

           // public string studentinfo { get; set; }
        }

       

        // Constructor
        public ClassesAndTimeTracking()
        {
            InitializeComponent();
            LoadClasses(); // Load classes on initialization
        }

        // Method to load classes into the Picker
        private void LoadClasses()
        {
            // This should be replaced with a database call to get classes taught by the professor
            var classes = new List<string> { "Class 1", "Class 2", "Class 3" };
            ClassPicker.ItemsSource = classes;
        }

        // Event handler when the class is selected
        private void ClassPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClassPicker.SelectedItem != null)
            {
                LoadStudentHours(ClassPicker.SelectedItem.ToString());
            }
        }

        // Method to load student hours based on selected class
        private void LoadStudentHours(string className)
        {
            // This should be replaced with a database call to get students & hours based on className
            var studentHours = new List<StudentHour>
            {
                new StudentHour { StudentName = "William LaFoy", TeamNumber = 1, Hours = 9 },
                new StudentHour { StudentName = "abc xyz", TeamNumber = 1, Hours = 9 }
            };

            // Sort and group each team with their associated students
            var sortedStudentHours = studentHours.OrderBy(sh => sh.TeamNumber).ToList();

            // Bind to CollectionView
            StudentHoursCollectionView.ItemsSource = sortedStudentHours;

           
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



        // Event handler for selecting a student to view detailed hours
        private void StudentHoursCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0 && e.CurrentSelection[0] is StudentHour selectedHour)
            {
                LoadStudentDetails(selectedHour.StudentName);
                LoadPeerReviews(selectedHour.StudentName); // Load peer reviews for the selected student
            }
        }








        /// 
        /// student details section
        /// 






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
        private void LoadStudentDetails(string studentName)
        {
            // This should be replaced with a database call to get specific dates and hours for the selected student
            var hours = new List<StudentHourDetail>
            {
                new StudentHourDetail { Date = DateTime.Today.AddDays(-1), Hours = 3, studentinfo = "Did x, y, z" },
                new StudentHourDetail { Date = DateTime.Today.AddDays(-2), Hours = 2, studentinfo = "Did x, y, z" },
                new StudentHourDetail { Date = DateTime.Today.AddDays(-3), Hours = 4, studentinfo = "Did x, y, z" }
            };

            HoursCollectionView.ItemsSource = hours; // Bind to CollectionView
            StudentDetailStack.IsVisible = true; // Show the details stack
            PeerReviewStack.IsVisible = true; // Show the peer review stack
        }


        
        ///
        ///Peer reviews section
        ///
        public class PeerReview
        {
            public DateTime Timestamp { get; set; } // The deadline or timestamp of the review
            public int QualityOfWork { get; set; }
            public int Timeliness { get; set; }
            public int Communication { get; set; }
            public int Teamwork { get; set; }
            public int EffortParticipation { get; set; }

            public List<TeamMemberComment> TeamMemberComments { get; set; } // Comments for each team member
        }

        public class TeamMemberComment
        {
            public string TeammateName { get; set; }
            public string Comment { get; set; }
        }



        // Loading peer reviews for a student
        private void LoadPeerReviews(string studentName)
        {
            // Sample peer reviews for demonstration
            var peerReviews = new List<PeerReview>
    {
        new PeerReview
        {
            Timestamp = DateTime.Today.AddDays(-10),
            QualityOfWork = 5,
            Timeliness = 5,
            Communication = 5,
            Teamwork = 5,
            EffortParticipation = 5,
            TeamMemberComments = new List<TeamMemberComment>
            {
                new TeamMemberComment { TeammateName = "William LaFoy", Comment = "Excellent work!" },
                new TeamMemberComment { TeammateName = "abc xyz", Comment = "Needs improvement." }
            }
        },
        new PeerReview
        {
            Timestamp = DateTime.Today.AddDays(-20),
            QualityOfWork = 4,
            Timeliness = 3,
            Communication = 4,
            Teamwork = 4,
            EffortParticipation = 4,
            TeamMemberComments = new List<TeamMemberComment>
            {
                new TeamMemberComment { TeammateName = "William LaFoy", Comment = "Solid teamwork." },
                new TeamMemberComment { TeammateName = "abc xyz", Comment = "Great communication." }
            }
        }
    };

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
            }
        }



        private void AddNewPeerReviewButton_Clicked(object sender, EventArgs e)
        {
            // Logic to add a new peer review
            
           
        }

    }
}
