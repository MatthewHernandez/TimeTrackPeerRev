using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_App_For_Professor
{
    public partial class Form_main : Form
    {
        private string csvFilePath; // Track the CSV file path for saving
        public Form_main()
        {
            InitializeComponent();
            // Attach the double-click event handler
            dataGridViewStudents.CellDoubleClick += dataGridViewStudents_CellDoubleClick;
        }

        private void Form_main_Load(object sender, EventArgs e)
        {

            //gxk220025 display logged in user
            // Display logged-in user
            if (!string.IsNullOrEmpty(ProfessorSession.Username))
            {
                // Set the label to display the current professor's username
                labelUsername.Text = "Welcome, " + ProfessorSession.Username;

                // Load professor's classes
                LoadProfessorClasses();

                // Subscribe to the SelectedIndexChanged event of comboBoxClasses
                comboBoxClasses.SelectedIndexChanged += comboBoxClasses_SelectedIndexChanged;
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

            try
            {
                db.openConnection();

                // Query to load classes for the professor
                string query = "SELECT class_name FROM class WHERE professor_id = @prof_id";
                MySqlCommand command = new MySqlCommand(query, db.getConnection);
                command.Parameters.AddWithValue("@prof_id", ProfessorSession.ProfessorId);

                MySqlDataReader reader = command.ExecuteReader();

                // Clear the ComboBox and add "All Students" option
                comboBoxClasses.Items.Clear();
                comboBoxClasses.Items.Add("All Students"); // Add special option at the top

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
            
        }
        private void comboBoxClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ensure a valid selection
            if (comboBoxClasses.SelectedItem == null)
            {
                MessageBox.Show("Please select a class.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MY_DB db = new MY_DB();

            try
            {
                db.openConnection();

                // Get the selected class name from ComboBox
                string selectedClass = comboBoxClasses.SelectedItem.ToString();

                if (selectedClass == "All Students")
                {
                    // Load all students without filtering by class
                    string allStudentsQuery = @"
                SELECT last_name AS 'Last Name', first_name AS 'First Name', username AS 'Username', id AS 'Student ID'
                FROM student";

                    MySqlCommand allStudentsCommand = new MySqlCommand(allStudentsQuery, db.getConnection);
                    MySqlDataAdapter allStudentsAdapter = new MySqlDataAdapter(allStudentsCommand);
                    DataTable allStudentsTable = new DataTable();

                    // Fill the DataTable with all students
                    allStudentsAdapter.Fill(allStudentsTable);

                    // Bind the DataTable to the DataGridView
                    dataGridViewStudents.DataSource = allStudentsTable;

                    label_class.Text = "All Students";
                }
                else
                {
                    // Load students enrolled in the selected class
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
                    SELECT s.last_name AS 'Last Name', s.first_name AS 'First Name', s.username AS 'Username', s.id AS 'Student ID'
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

                        label_class.Text = selectedClass;
                    }
                    else
                    {
                        MessageBox.Show("Selected class ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }
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
                int studentId = Convert.ToInt32(selectedRow.Cells["Student ID"].Value);         // Assuming "id" is the name of the column
                string firstName = selectedRow.Cells["First Name"].Value.ToString();   // Assuming "first_name" is the column name
                string lastName = selectedRow.Cells["Last Name"].Value.ToString();     // Assuming "last_name" is the column name
                string username = selectedRow.Cells["Username"].Value.ToString();            // Assuming "username" is the column name
                
                // Store the retrieved data in the STUDENT class
                STUDENT.Id = studentId;
                STUDENT.FirstName = firstName;
                STUDENT.LastName = lastName;
                STUDENT.User = username;

                // Optionally, display a message or take another action
                MessageBox.Show($"Selected Student:\nID: {studentId}\nName: {firstName} {lastName}\nUsername: {username}");

                // Open Work Hours Form (Students)
                Form_std_work workStdInfo = new Form_std_work();
                workStdInfo.Show(this);
            }
        }
        
        private void button_stdlist_Click(object sender, EventArgs e)
        {
            MY_DB db = new MY_DB();

            if (comboBoxClasses.Text != "") // Ensure a class is selected
            {
                try
                {
                    db.openConnection();

                    // Get the selected class name from ComboBox
                    string selectedClass = comboBoxClasses.SelectedItem.ToString();

                    if (selectedClass == "All Students")
                    {
                        // Load all students without filtering by class
                        string allStudentsQuery = @"
                    SELECT last_name AS 'Last Name', first_name AS 'First Name', username AS 'Username', id AS 'Student ID'
                    FROM student";

                        MySqlCommand allStudentsCommand = new MySqlCommand(allStudentsQuery, db.getConnection);
                        MySqlDataAdapter allStudentsAdapter = new MySqlDataAdapter(allStudentsCommand);
                        DataTable allStudentsTable = new DataTable();

                        // Fill the DataTable with all students
                        allStudentsAdapter.Fill(allStudentsTable);

                        // Bind the DataTable to the DataGridView
                        dataGridViewStudents.DataSource = allStudentsTable;

                        label_class.Text = "All Students";
                    }
                    else
                    {
                        // Load students enrolled in the selected class
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
                        SELECT s.last_name AS 'Last Name', s.first_name AS 'First Name', s.username AS 'Username', s.id AS 'Student ID'
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

                            label_class.Text = selectedClass;
                        }
                        else
                        {
                            MessageBox.Show("Selected class ID not found.");
                        }
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
            else
            {
                MessageBox.Show("Please select a class or 'All Students' option.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void button_spread_Click(object sender, EventArgs e)
        {
            // Open Spreadsheet
            Form_spsh spsheet = new Form_spsh();
            spsheet.Show(this);
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            // Use SaveFileDialog to specify where to save the CSV file
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV Files|*.csv";
                saveFileDialog.Title = "Save as CSV File";
                saveFileDialog.FileName = ProfessorSession.ProfessorId + "_" + label_class.Text + "_Students.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path chosen by the user
                    string filePath = saveFileDialog.FileName;

                    // Save DataGridView data to the specified CSV file
                    SaveDataGridViewToCsv(filePath);
                    MessageBox.Show("Data successfully saved to CSV file.", "Save Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        // Method to save the DataGridView data to a CSV file
        private void SaveDataGridViewToCsv(string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    // Write custom headers
                    var customHeaders = new[] { "First Name", "Last Name", "Username", "Student ID"};
                    sw.WriteLine(string.Join(",", customHeaders));

                    // Write rows
                    foreach (DataGridViewRow row in dataGridViewStudents.Rows)
                    {
                        if (!row.IsNewRow) // Ignore the placeholder row
                        {
                            var fields = row.Cells.Cast<DataGridViewCell>()
                                         .Select(cell => cell.Value?.ToString() ?? ""); // Handle null values
                            sw.WriteLine(string.Join(",", fields));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving to CSV file: " + ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label_class_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
        private void button_import_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV Files|*.csv";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    csvFilePath = openFileDialog.FileName; // Save the file path for later use
                    ImportFromCsv(csvFilePath);
                }
            }
        }
        // Method to import data from an Excel file
        private void ImportFromCsv(string filePath)
        {
            try
            {
                DataTable dataTable = new DataTable();

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    bool isFirstLine = true;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');

                        // Create columns on the first line (header row)
                        if (isFirstLine)
                        {
                            foreach (var column in values)
                            {
                                dataTable.Columns.Add(column.Trim());
                            }
                            isFirstLine = false;
                        }
                        else
                        {
                            // Add rows for each subsequent line
                            dataTable.Rows.Add(values);
                        }
                    }
                }

                // Bind the DataTable to the DataGridView
                dataGridViewStudents.DataSource = dataTable;

                // Save data to the server
                SaveToServer(dataGridViewStudents);

                // Show a success message
                MessageBox.Show("CSV imported and data successfully saved to MySQL.", "Import Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing data: " + ex.Message, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveToServer(DataGridView dataGridView)
        {
            // Initialize MY_DB to handle the database connection
            MY_DB db = new MY_DB();

            try
            {
                db.openConnection(); // Open the database connection

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (!row.IsNewRow) // Skip the placeholder row
                    {
                        // Extract data from DataGridView cells
                        string lastName = row.Cells["Last Name"].Value?.ToString() ?? "";
                        string firstName = row.Cells["First Name"].Value?.ToString() ?? "";
                        string userName = row.Cells["Username"].Value?.ToString() ?? "";
                        string email = $"{userName}@utdallas.edu";
                        long studentId = Convert.ToInt64(row.Cells["Student ID"].Value);

                        // Check if the student ID already exists in the database
                        string checkQuery = "SELECT COUNT(*) FROM student WHERE id = @id";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, db.getConnection))
                        {
                            checkCmd.Parameters.AddWithValue("@id", studentId);
                            int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                            if (count > 0)
                            {
                                // ID already exists, skip this row
                                continue;
                            }
                        }

                        // Define the INSERT query with parameters
                        string insertQuery = @"
                    INSERT INTO student (last_name, first_name, username, id, email)
                    VALUES (@lastName, @firstName, @userName, @id, @Email)";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, db.getConnection))
                        {
                            // Add parameters to the command
                            insertCmd.Parameters.AddWithValue("@lastName", lastName);
                            insertCmd.Parameters.AddWithValue("@firstName", firstName);
                            insertCmd.Parameters.AddWithValue("@userName", userName);
                            insertCmd.Parameters.AddWithValue("@id", studentId);
                            insertCmd.Parameters.AddWithValue("@Email", email);

                            // Execute the insertion
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Data successfully inserted into the MySQL server.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data into MySQL: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection(); // Ensure the connection is closed
            }
        }

        private bool UpdateStudentInMySQL(long id, string firstName, string lastName, string userName)
        {
            MY_DB db = new MY_DB();
            bool isUpdated = false;

            try
            {
                db.openConnection();

                // Define the UPDATE query with parameters
                string updateQuery = @"
            UPDATE student 
            SET last_name = @lastName, first_name = @firstName, username = @userName, email = @Email
            WHERE id = @id";

                using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, db.getConnection))
                {
                    // Add parameters to the command
                    updateCmd.Parameters.AddWithValue("@lastName", lastName);
                    updateCmd.Parameters.AddWithValue("@firstName", firstName);
                    updateCmd.Parameters.AddWithValue("@userName", userName);
                    updateCmd.Parameters.AddWithValue("@Email", $"{userName}@utdallas.edu");
                    updateCmd.Parameters.AddWithValue("@id", id);

                    // Execute the update
                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    isUpdated = rowsAffected > 0; // Set to true if at least one row was updated
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating student in MySQL: " + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }

            return isUpdated;
        }
        //method to added information into MySql
        private bool AddStudentToMySQL(long id, string firstName, string lastName, string userName)
        {
            MY_DB db = new MY_DB();
            bool isAdded = false;

            try
            {
                db.openConnection();

                // Check if the student ID already exists in the database
                string checkQuery = "SELECT COUNT(*) FROM student WHERE id = @id";
                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, db.getConnection))
                {
                    checkCmd.Parameters.AddWithValue("@id", id);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // ID already exists, skip insertion and return false
                        MessageBox.Show("Student ID already exists in the database. Please use a unique ID.",
                                        "Duplicate ID Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                // Define the INSERT query with parameters
                string insertQuery = @"
            INSERT INTO student (last_name, first_name, username, id, email)
            VALUES (@lastName, @firstName, @userName, @id, @Email)";

                using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, db.getConnection))
                {
                    // Add parameters to the command
                    insertCmd.Parameters.AddWithValue("@lastName", lastName);
                    insertCmd.Parameters.AddWithValue("@firstName", firstName);
                    insertCmd.Parameters.AddWithValue("@userName", userName);
                    insertCmd.Parameters.AddWithValue("@id", id);
                    insertCmd.Parameters.AddWithValue("@Email", $"{userName}@utdallas.edu");

                    // Execute the insertion
                    insertCmd.ExecuteNonQuery();
                    isAdded = true; // Set isAdded to true if insertion succeeds
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding student to MySQL: " + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }

            return isAdded;
        }
        private bool EnrollStudentInClass(long studentId, int classId)
        {
            MY_DB db = new MY_DB();
            bool isEnrolled = false;

            try
            {
                db.openConnection();

                // Check if the student is already enrolled
                string checkQuery = "SELECT COUNT(*) FROM student_class_enrolled WHERE student_id = @studentId AND class_id = @classId";
                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, db.getConnection))
                {
                    checkCmd.Parameters.AddWithValue("@studentId", studentId);
                    checkCmd.Parameters.AddWithValue("@classId", classId);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        return false; // Already enrolled
                    }
                }

                // Enroll the student in the class
                string enrollQuery = "INSERT INTO student_class_enrolled (student_id, class_id) VALUES (@studentId, @classId)";
                using (MySqlCommand enrollCmd = new MySqlCommand(enrollQuery, db.getConnection))
                {
                    enrollCmd.Parameters.AddWithValue("@studentId", studentId);
                    enrollCmd.Parameters.AddWithValue("@classId", classId);
                    int rowsAffected = enrollCmd.ExecuteNonQuery();
                    isEnrolled = rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error enrolling student in class: " + ex.Message, "Enrollment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }

            return isEnrolled;
        }
        private void button_add_Click(object sender, EventArgs e)
        {
            // Create a new instance of Form_spsh_add
            Form_spsh_add addForm = new Form_spsh_add();

            // Subscribe to the StudentAdded event to handle new student data
            addForm.StudentAdded += (firstName, lastName, id, userName) =>
            {
                // Attempt to add to MySQL first
                bool isAddedToMySQL = AddStudentToMySQL(id, firstName, lastName, userName);

                // Only update DataGridView if added to MySQL successfully
                if (isAddedToMySQL)
                {
                    // Ensure DataGridView is bound to a DataTable
                    DataTable dataTable;

                    if (dataGridViewStudents.DataSource == null)
                    {
                        // Initialize a new DataTable with required columns
                        dataTable = new DataTable();
                        dataTable.Columns.Add("Student ID", typeof(long));
                        dataTable.Columns.Add("First Name", typeof(string));
                        dataTable.Columns.Add("Last Name", typeof(string));
                        dataTable.Columns.Add("Username", typeof(string));

                        // Bind the new DataTable to the DataGridView
                        dataGridViewStudents.DataSource = dataTable;
                    }
                    else
                    {
                        // Use the existing DataTable bound to the DataGridView
                        dataTable = (DataTable)dataGridViewStudents.DataSource;
                    }

                    // Create a new row with the added student data
                    DataRow newRow = dataTable.NewRow();
                    newRow["Student ID"] = id;
                    newRow["First Name"] = firstName;
                    newRow["Last Name"] = lastName;
                    newRow["Username"] = userName;

                    // Add the new row to the DataTable
                    dataTable.Rows.Add(newRow);

                    // Refresh the DataGridView to display the new data
                    dataGridViewStudents.DataSource = null;
                    dataGridViewStudents.DataSource = dataTable;

                    // Show success message
                    MessageBox.Show("Student data successfully added to MySQL and updated in the application.",
                                    "Add Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            // Show the Form_spsh_add form as a modal dialog
            addForm.ShowDialog();
        }
        private bool StudentExistsInDB(long studentId)
        {
            MY_DB db = new MY_DB();
            bool exists = false;

            try
            {
                db.openConnection();

                string query = "SELECT COUNT(*) FROM student WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, db.getConnection))
                {
                    cmd.Parameters.AddWithValue("@id", studentId);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    exists = count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking student existence: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }

            return exists;
        }
        private bool IsStudentEnrolledInClass(long studentId, int classId)
        {
            MY_DB db = new MY_DB();
            bool isEnrolled = false;

            try
            {
                db.openConnection();

                string query = "SELECT COUNT(*) FROM student_class_enrolled WHERE student_id = @studentId AND class_id = @classId";
                using (MySqlCommand cmd = new MySqlCommand(query, db.getConnection))
                {
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    cmd.Parameters.AddWithValue("@classId", classId);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    isEnrolled = count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking enrollment: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }

            return isEnrolled;
        }

        private void button_change_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedCells.Count > 0)
            {
                // Get the row index of the selected cell
                int rowIndex = dataGridViewStudents.SelectedCells[0].RowIndex;

                // Check if the selected row is the placeholder row
                if (dataGridViewStudents.Rows[rowIndex].IsNewRow)
                {
                    MessageBox.Show("Cannot modify the placeholder row.", "Modify Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Proceed with the change operation for non-placeholder rows
                DataGridViewRow selectedRow = dataGridViewStudents.Rows[rowIndex];

                try
                {
                    // Extract the current data from the row
                    int studentId = Convert.ToInt32(selectedRow.Cells["Student ID"].Value);
                    string firstName = selectedRow.Cells["First Name"].Value.ToString();
                    string lastName = selectedRow.Cells["Last Name"].Value.ToString();
                    string userName = selectedRow.Cells["Username"].Value.ToString();

                    // Open Form_spsh_ch with current data for modification
                    Form_spsh_ch editForm = new Form_spsh_ch(studentId, firstName, lastName, userName);

                    // Subscribe to the event for confirming the changes
                    editForm.ChangeConfirmed += (newFirstName, newLastName, newId, newUserName) =>
                    {
                        // Attempt to update MySQL first
                        bool isUpdatedInMySQL = UpdateStudentInMySQL(newId, newFirstName, newLastName, newUserName);

                        // Only update DataGridView and CSV if MySQL update is successful
                        if (isUpdatedInMySQL)
                        {
                            // Update the selected row with the new data
                            selectedRow.Cells["Student ID"].Value = newId;
                            selectedRow.Cells["First Name"].Value = newFirstName;
                            selectedRow.Cells["Last Name"].Value = newLastName;
                            selectedRow.Cells["Username"].Value = newUserName;

                            // Refresh DataGridView to show updated data
                            dataGridViewStudents.Refresh();


                            // Show success message
                            MessageBox.Show("Student data successfully updated in MySQL and application.",
                                            "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    };

                    // Show the edit form as a modal dialog
                    editForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error modifying the selected row: " + ex.Message, "Modify Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a cell to modify.", "Modify Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (dataGridViewStudents.SelectedCells.Count > 0)
            {
                // Get the row index of the selected cell
                int rowIndex = dataGridViewStudents.SelectedCells[0].RowIndex;

                // Check if the selected row is the placeholder row
                if (dataGridViewStudents.Rows[rowIndex].IsNewRow)
                {
                    MessageBox.Show("Cannot delete the placeholder row.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Proceed with deletion for non-placeholder rows
                DataTable dataTable = (DataTable)dataGridViewStudents.DataSource;

                // Back up the row data before deletion by pushing it onto the stack
                DataRow deletedRow = dataTable.NewRow();
                deletedRow.ItemArray = dataTable.Rows[rowIndex].ItemArray.Clone() as object[]; // Clone row data
                

                // Delete from MySQL
                long studentId = Convert.ToInt64(deletedRow["Student ID"]);
                bool isDeletedFromMySQL = DeleteFromMySQL(studentId);

                // Only update DataGridView if successfully deleted from MySQL
                if (isDeletedFromMySQL)
                {
                    // Delete the row in the DataTable
                    dataTable.Rows[rowIndex].Delete();
                    dataTable.AcceptChanges(); // Commit the deletion to the DataTable

                    // Refresh the DataGridView to reflect the deleted row
                    dataGridViewStudents.DataSource = null;
                    dataGridViewStudents.DataSource = dataTable;

                    // Show success message
                    MessageBox.Show("Student data successfully deleted from MySQL and removed from the application.",
                                    "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a cell in the row you want to delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool DeleteFromMySQL(long studentId)
        {
            
            MY_DB db = new MY_DB();
            bool isDeleted = false;

            try
            {
                db.openConnection();

                // Proceed with deletion
                string deleteQuery = "DELETE FROM student WHERE id = @id";
                using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, db.getConnection))
                {
                    deleteCmd.Parameters.AddWithValue("@id", studentId);
                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                    // Check if a row was actually deleted
                    isDeleted = rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting student from MySQL: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }

            return isDeleted;
        }

        private void button_enroll_Click(object sender, EventArgs e)
        {
            // Ensure there is a displayed list in DataGridView
            if (dataGridViewStudents.DataSource == null || dataGridViewStudents.Rows.Count == 0)
            {
                MessageBox.Show("No student list available to enroll.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ensure a student is selected
            if (dataGridViewStudents.SelectedCells.Count == 0)
            {
                MessageBox.Show("Please select a student to enroll.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get selected student data
            int rowIndex = dataGridViewStudents.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridViewStudents.Rows[rowIndex];

            long studentId = Convert.ToInt64(selectedRow.Cells["Student ID"].Value);
            string firstName = selectedRow.Cells["First Name"].Value.ToString();
            string lastName = selectedRow.Cells["Last Name"].Value.ToString();
            string userName = selectedRow.Cells["Username"].Value.ToString();

            // Open Form_spsh_enroll with the selected student's information
            Form_spsh_enroll enrollForm = new Form_spsh_enroll(studentId, firstName, lastName, userName);
            enrollForm.EnrollConfirmed += (selectedClassId) =>
            {
                // Attempt to enroll the student in the selected class
                bool isEnrolled = EnrollStudentInClass(studentId, selectedClassId);

                if (isEnrolled)
                {
                    MessageBox.Show("Student successfully enrolled in the selected class.", "Enrollment Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Student is already enrolled in this class or an error occurred.", "Enrollment Skipped", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            enrollForm.ShowDialog();
        }
    }
}
