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
        //written by William LaFoy for CS4485, Project, 10/9/2024-10/18/2024. NetID wel190000
        /*
         Base framework for listing out classes, listing out associated students and cummulative hours
         and specific dates with those hours.

        10/18 now allows you to select cells with ease and modify the date hour etc.
         */

        // Sample data structure for student hours
        public class StudentHour
        {
            public string StudentName { get; set; }


            public int TeamNumber { get; set; }
            public decimal Hours { get; set; }
        }

        // Updated StudentHourDetail with INotifyPropertyChanged
        public class StudentHourDetail : INotifyPropertyChanged
        {
            private DateTime _date;
            private decimal _hours;

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

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
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
            }
        }

        // Method to load details for the selected student
        private void LoadStudentDetails(string studentName)
        {
            // This should be replaced with a database call to get specific dates and hours for the selected student
            var hours = new List<StudentHourDetail>
            {
                new StudentHourDetail { Date = DateTime.Today.AddDays(-1), Hours = 3 },
                new StudentHourDetail { Date = DateTime.Today.AddDays(-2), Hours = 2 },
                new StudentHourDetail { Date = DateTime.Today.AddDays(-3), Hours = 4 }
            };

            HoursCollectionView.ItemsSource = hours; // Bind to CollectionView
            StudentDetailStack.IsVisible = true; // Show the details stack
        }
    }
}
