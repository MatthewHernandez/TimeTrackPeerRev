using Microsoft.Maui.Controls;

namespace DesktopApp;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
    }

    //written by William LaFoy for CS4485, Project, started 9/25/2024. NetID wel190000
    /*
         Base framework for logging in
    */
    // Event handler for login button click
    private void ClickedLogin(object sender, EventArgs e)
    {
        //Upon clicking login attempt, fetch the entry .Text, we will then check if valid credentials.
        string Username = UsernameEntry.Text;
        string Password = PasswordEntry.Text;

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
    }
}