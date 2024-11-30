/*
 
Basic Professor Login program

This program allows or prevents entry into the ClassesAndTimeTracking portion of the Desktop App for professors

Simply input Username and Password, and we then authenticate entry request.

Entirety of program written by William LaFoy for CS4485, Project, started 9/25/2024. NetID wel190000
 */

using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;//Mysql

namespace DesktopApp;



public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
    }

    //Program written by William LaFoy for CS4485, Project, started 9/25/2024. NetID wel190000
    /*
         Base framework for logging in
    */
    // Event handler for login button click
    private async void ClickedLogin(object sender, EventArgs e)
    {
        //Upon clicking login attempt, fetch the entry .Text, we will then check if valid credentials.
        string Username = UsernameEntry.Text;
        string Password = PasswordEntry.Text;


        string connectionString = "server=localhost;uid=root;pwd=kotori1430;database=test_schema";
        using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                
                string query = "SELECT * FROM professor WHERE username=@username AND password=@password";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", Username);
                    cmd.Parameters.AddWithValue("@password", Password);
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        // Ready the professor ID to pass into ClassesAndTimeTracking
                        int professorId = reader.GetInt32(0);

                        LoginStatusLabel.TextColor = Colors.Green;
                        LoginStatusLabel.Text = "Login Successful!";
                        // Wait for 2 seconds
                        await Task.Delay(2000);

                        // Navigate to ClassesAndTimeTracking and pass the professorId
                        await Navigation.PushAsync(new ClassesAndTimeTracking(professorId)); // Pass professorId here

                    }
                    else
                    {
                        LoginStatusLabel.TextColor = Colors.Red;
                        LoginStatusLabel.Text = "Invalid username or password.";
                    }
                }
            }
            catch (Exception ex)
            {
                LoginStatusLabel.TextColor = Colors.Red;
                LoginStatusLabel.Text = $"Error: {ex.Message}";
            }
        }

        /*
         // Simple login logic
        if (Username == "admin" && Password == "password")
        {
            LoginStatusLabel.TextColor = Colors.Green;
            LoginStatusLabel.Text = "Login Successful!";
        }
        else
        {
            LoginStatusLabel.TextColor = Colors.Red;
            LoginStatusLabel.Text = "Invalid username or password.";
        }
        */
    }
}