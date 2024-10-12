using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace DesktopApp
{



    public partial class ClassesAndTimeTracking : ContentPage
    {

        //written by William LaFoy for CS4485, Project, 10/9/2024-10/11/2024. NetID wel190000
        /*
         Base framework for listing out classes, listing out associated students and cummulative hours
        and specific dates with those those hours.
         */

        // Sample data structure for student hours
        public class StudentHour
        {
            public string StudentName { get; set; }
           
            public decimal Hours { get; set; }
        }

        public class StudentHourDetail
        {
            public DateTime Date { get; set; }
            public decimal Hours { get; set; }
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
                new StudentHour { StudentName = "William LaFoy", Hours = 9 },
                new StudentHour { StudentName = "abc xyz", Hours = 9 }
            };

            StudentHoursCollectionView.ItemsSource = studentHours; // Bind to CollectionView
        }

        // Event handler for editing hours
        private async void EditHours_Click(object sender, EventArgs e)
        {
            if (StudentHoursCollectionView.SelectedItem is StudentHour selectedHour)
            {
                // Prompt for date
                string dateResponse = await DisplayPromptAsync("Edit Hours", $"Please enter a date (M/D/Y) for {selectedHour.StudentName}:", "OK", "Cancel");
                if (string.IsNullOrEmpty(dateResponse)) return; // User canceled

                // Prompt for hours
                string hoursResponse = await DisplayPromptAsync("Edit Hours", $"Please enter the number of hours for {selectedHour.StudentName}:", "OK", "Cancel");
                if (string.IsNullOrEmpty(hoursResponse)) return; // User canceled

                // Validate inputs
                if (DateTime.TryParse(dateResponse, out DateTime date) && decimal.TryParse(hoursResponse, out decimal hours))
                {
                    // Here you would implement the logic to update the hours in your data source.
                    // For example, you could replace the hours for the selected student on the specified date.
                    await DisplayAlert("Success", $"Updated {selectedHour.StudentName}'s hours to {hours} on {date:MM/dd/yyyy}.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Invalid date or hours input. Please try again.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please select a student hour to edit.", "OK");
            }
        }

        // Event handler for importing students (not implemented)
        private async void ImportStudents_Click(object sender, EventArgs e)
        {
            // Implement functionality to import student data from a spreadsheet or file
            await DisplayAlert("Import Students", "Import Students functionality to be implemented.", "OK");
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
