using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_App_For_Professor
{
    public partial class Form_main : Form
    {
        public Form_main()
        {
            InitializeComponent();
            // Attach the double-click event handler
            dataGridViewStudents.CellDoubleClick += dataGridViewStudents_CellDoubleClick;
        }

        private void Form_main_Load(object sender, EventArgs e)
        {
            //gxk220025 display logged in user
            // Check if the user is logged in and display the username
            if (!string.IsNullOrEmpty(ProfessorSession.Username))
            {
                // Set the label to display the current professor's username
                labelUsername.Text = "Welcome, " + ProfessorSession.Username;
                LoadProfessorClasses();
            }
            else
            {
                // If no user is logged in, show a default message
                labelUsername.Text = "No user logged in.";
            }
        }

        private void LoadProfessorClasses()
        {
            // Call MY_DB to connect to the database
            MY_DB db = new MY_DB();
            /*
            try
            {
                
                db.openConnection();

                // Query to get the logged-in professor's classes
                string query = "SELECT class_name FROM class WHERE professor_id = @prof_id";

                // Prepare the MySqlCommand with the query
                MySqlCommand command = new MySqlCommand(query, db.getConnection);

                // Use the professor's ID from the session
                command.Parameters.AddWithValue("@prof_id", ProfessorSession.ProfessorId);

                // Execute the query and read the results
                MySqlDataReader reader = command.ExecuteReader();

                // Clear existing items
                comboBoxClasses.Items.Clear();

                // Loop through the result and add class names to the ComboBox
                while (reader.Read())
                {
                    comboBoxClasses.Items.Add(reader["class_name"].ToString());
                }

                // Close the reader
                reader.Close();

                db.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading classes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
               */
            try
            {
                db.openConnection();
                string query = "SELECT class_name FROM class WHERE professor_id = @prof_id";
                MySqlCommand command = new MySqlCommand(query, db.getConnection);
                command.Parameters.AddWithValue("@prof_id", ProfessorSession.ProfessorId);

                MySqlDataReader reader = command.ExecuteReader();
                comboBoxClasses.Items.Clear();
                while (reader.Read())
                {
                    comboBoxClasses.Items.Add(reader["class_name"].ToString());
                }
                reader.Close();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading classes: " + ex.Message);
            }

        }

        // Event handler for ComboBox selection change
        private void comboBoxClasses_SelectedIndexChanged(object sender,    EventArgs e)
        {
            // When a class is selected, load the enrolled students
            //LoadEnrolledStudents();

            
        }
        // Load the students enrolled in the selected class
        //10/22 not user now
        private void LoadEnrolledStudents()
        {
            MY_DB db = new MY_DB();
            try
            {
                db.openConnection();

                // Get the selected class name from ComboBox
                string selectedClass = comboBoxClasses.SelectedItem.ToString();

                //check test, erase later
                MessageBox.Show("Invalid Account or Password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error); MessageBox.Show("Select: " + selectedClass);

                // Query to get the students enrolled in the selected class
                string query = null;
                query = @"
                    SELECT *
                    FROM class sc
                    WHERE class_name = @class_name";

                MySqlCommand cmd = new MySqlCommand(query, db.getConnection);
                cmd.Parameters.AddWithValue("@class_name", selectedClass);

                MySqlDataAdapter adpt = new MySqlDataAdapter(cmd);
                DataTable classInfo = new DataTable();
                adpt.Fill(classInfo);
                DataRow crow = classInfo.Rows[0];
                int cid = Convert.ToInt32(crow["id"]);   
                //cid = class id of selected class




                /* prvious code, check it and modify
                query = @"
                    SELECT s.id, s.first_name, s.last_name, s.email
                    FROM student_class_enrolled sc
                    JOIN student s ON sc.student_id = s.id
                    JOIN class c ON sc.class_id = c.id
                    WHERE c.class_name = @class_name AND c.professor_id = @prof_id";

                MySqlCommand command = new MySqlCommand(query, db.getConnection);
                command.Parameters.AddWithValue("@class_name", selectedClass);
                command.Parameters.AddWithValue("@prof_id", ProfessorSession.ProfessorId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable studentTable = new DataTable();

                // Fill the DataTable with the query result
                adapter.Fill(studentTable);

                // Bind the DataTable to the DataGridView
                dataGridViewStudents.DataSource = studentTable;
                */

                db.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void studentInfomationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void edittempToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gxk220025
            //when click edit student infomation button, then open edit form 
            Form_std_edit editStdInfo = new Form_std_edit();
            editStdInfo.Show(this);
        }

        private void mailTempToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gxk220025
            //when click add student infomation button, then open add form 
            Form_std_add addStdInfo = new Form_std_add();
            addStdInfo.Show(this);

        }

        private void button_read_Click(object sender, EventArgs e)
        {
            // Get the selected class name from ComboBox
            string selectedClass = comboBoxClasses.SelectedItem.ToString();

            //check test, erase later
            MessageBox.Show("Class: " + selectedClass, "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information); //MessageBox.Show("Select: " + selectedClass);

        }

        private void LoadProfessorClasses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       
        //Cell Double Click Event
        private void dataGridViewStudents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            // Ensure the row is valid (not a header or an empty row)
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridViewStudents.Rows[e.RowIndex];

                // Retrieve the student data from the selected row
                int studentId = Convert.ToInt32(selectedRow.Cells["id"].Value);         // Assuming "id" is the name of the column
                string firstName = selectedRow.Cells["first_name"].Value.ToString();   // Assuming "first_name" is the column name
                string lastName = selectedRow.Cells["last_name"].Value.ToString();     // Assuming "last_name" is the column name
                string email = selectedRow.Cells["email"].Value.ToString();            // Assuming "email" is the column name

                // Store the retrieved data in the STUDENT class
                STUDENT.Id = studentId;
                STUDENT.FirstName = firstName;
                STUDENT.LastName = lastName;
                STUDENT.Email = email;

                // Optionally, display a message or take another action
                MessageBox.Show($"Selected Student:\nID: {studentId}\nName: {firstName} {lastName}\nEmail: {email}");

                // Open Work Hours Form (Students)
                Form_std_work workStdInfo = new Form_std_work();
                workStdInfo.Show(this);
            }
        }
        
        private void button_stdlist_Click(object sender, EventArgs e)
        {
            MY_DB db = new MY_DB();

            /*
            //pre version
            try {
                db.openConnection();

                // Get the selected class name from ComboBox
                string selectedClass = comboBoxClasses.SelectedItem.ToString();

                //check test, erase later
               // MessageBox.Show("Invalid Account or Password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error); MessageBox.Show("Select: " + selectedClass);

                // Query to get the students enrolled in the selected class
                string query = null;
                query = @"
                    SELECT s.id, s.first_name, s.last_name, s.email
                    FROM student_class_enrolled sc
                    JOIN student s ON sc.student_id = s.id
                    JOIN class c ON sc.class_id = c.id
                    WHERE c.class_name = @class_name AND c.professor_id = @prof_id";

                MySqlCommand command = new MySqlCommand(query, db.getConnection);
                command.Parameters.AddWithValue("@class_name", selectedClass);
                command.Parameters.AddWithValue("@prof_id", ProfessorSession.ProfessorId);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable studentTable = new DataTable();

                // Fill the DataTable with the query result
                adapter.Fill(studentTable);

                // Bind the DataTable to the DataGridView
                dataGridViewStudents.DataSource = studentTable;

            }
            catch(Exception ex) {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
            */

            //10/22 newversion.
            //save selected class id into professor's session.
            if (comboBoxClasses.Text != "") //none data
            {
                try
                {
                    db.openConnection();

                    // Get the selected class name from ComboBox
                    string selectedClass = comboBoxClasses.SelectedItem.ToString();

                    // Query to get the class ID for the selected class
                    string classIdQuery = "SELECT id FROM class WHERE class_name = @class_name AND professor_id = @prof_id";
                    MySqlCommand classIdCommand = new MySqlCommand(classIdQuery, db.getConnection);
                    classIdCommand.Parameters.AddWithValue("@class_name", selectedClass);
                    classIdCommand.Parameters.AddWithValue("@prof_id", ProfessorSession.ProfessorId);

                    // Execute the query to get the class ID
                    object result = classIdCommand.ExecuteScalar();

                    if (result != null)
                    {
                        // Save the class ID in the session
                        ProfessorSession.curClass = Convert.ToInt32(result);

                        // Query to get the students enrolled in the selected class
                        string query = @"
                SELECT s.id, s.first_name, s.last_name, s.email
                FROM student_class_enrolled sc
                JOIN student s ON sc.student_id = s.id
                JOIN class c ON sc.class_id = c.id
                WHERE c.class_name = @class_name AND c.professor_id = @prof_id";

                        MySqlCommand command = new MySqlCommand(query, db.getConnection);
                        command.Parameters.AddWithValue("@class_name", selectedClass);
                        command.Parameters.AddWithValue("@prof_id", ProfessorSession.ProfessorId);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        DataTable studentTable = new DataTable();

                        // Fill the DataTable with the query result
                        adapter.Fill(studentTable);

                        // Bind the DataTable to the DataGridView
                        dataGridViewStudents.DataSource = studentTable;
                    }
                    else
                    {
                        MessageBox.Show("Selected class ID not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading students: " + ex.Message);
                }
                finally
                {
                    db.closeConnection();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void labelUsername_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
