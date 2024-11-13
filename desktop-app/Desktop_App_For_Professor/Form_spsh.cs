using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;


namespace Desktop_App_For_Professor
{
    public partial class Form_spsh : Form
    {
        private string csvFilePath; // Track the CSV file path for saving
                                    // Stack to hold deleted rows
        private Stack<DataRow> deletedRowsStack = new Stack<DataRow>();
        public Form_spsh()
        {
            InitializeComponent();
        }

        private void Form_spsh_Load(object sender, EventArgs e)
        {

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing data: " + ex.Message, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void button_add_Click(object sender, EventArgs e)
        {
            // Create a new instance of Form_spsh_add
            Form_spsh_add addForm = new Form_spsh_add();

            // Subscribe to the StudentAdded event to handle new student data
            addForm.StudentAdded += (firstName, lastName, id, userName) =>
            {
                // Attempt to add to MySQL first
                bool isAddedToMySQL = AddStudentToMySQL(id, firstName, lastName, userName);

                // Only update DataGridView and CSV if added to MySQL successfully
                if (isAddedToMySQL)
                {
                    // Get the current data source
                    DataTable dataTable = (DataTable)dataGridViewStudents.DataSource;

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

                    // Append the new student data to the CSV file
                    AppendToCsv(csvFilePath, id, firstName, lastName, userName);

                    // Show success message
                    MessageBox.Show("Student data successfully added to MySQL and updated in the application.",
                                    "Add Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            // Show the Form_spsh_add form as a modal dialog
            addForm.ShowDialog();
        }

        // Method to append a new row to the CSV file
        private void AppendToCsv(string filePath, long id, string firstName, string lastName, string userName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    // Write the new line in CSV format
                    //lstname, firstname, usernmae, id
                    sw.WriteLine($"{lastName},{firstName},{userName},{id}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving to CSV file: " + ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveToCsv(string filePath, DataTable dataTable)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false)) // Overwrite file
                {
                    // Write headers
                    string[] columnNames = dataTable.Columns.Cast<DataColumn>()
                                           .Select(column => column.ColumnName)
                                           .ToArray();
                    sw.WriteLine(string.Join(",", columnNames));

                    // Write rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();
                        sw.WriteLine(string.Join(",", fields));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving to CSV file: " + ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                    // If ID exists, skip the addition
                    if (count > 0)
                    {
                        return false; // Skip insertion if ID is not unique
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
                MessageBox.Show("Error adding student to MySQL: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection(); // Ensure the connection is closed
            }

            return isAdded;
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
                deletedRowsStack.Push(deletedRow); // Push the backup data onto the stack

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

                    // Save the updated DataTable to the CSV file
                    SaveToCsv(csvFilePath, dataTable);

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
            /*
            MY_DB db = new MY_DB();
            bool isDeleted = false;

            try
            {
                db.openConnection();

                // Define the DELETE query with a parameter
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
            */
            MY_DB db = new MY_DB();
            bool isDeleted = false;

            try
            {
                db.openConnection();

                // Check for dependencies in student_class_enrolled
                string checkClassEnrolledQuery = "SELECT COUNT(*) FROM student_class_enrolled WHERE student_id = @studentId";
                using (MySqlCommand checkCmd1 = new MySqlCommand(checkClassEnrolledQuery, db.getConnection))
                {
                    checkCmd1.Parameters.AddWithValue("@studentId", studentId);
                    int dependentCount1 = Convert.ToInt32(checkCmd1.ExecuteScalar());

                    // If dependencies exist, show error and cancel the deletion
                    if (dependentCount1 > 0)
                    {
                        MessageBox.Show("Cannot delete this student record because there are dependent entries in 'student_class_enrolled'.",
                                        "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                // Check for dependencies in student_hours
                string checkStudentHoursQuery = "SELECT COUNT(*) FROM student_hours WHERE student_id = @studentId";
                using (MySqlCommand checkCmd2 = new MySqlCommand(checkStudentHoursQuery, db.getConnection))
                {
                    checkCmd2.Parameters.AddWithValue("@studentId", studentId);
                    int dependentCount2 = Convert.ToInt32(checkCmd2.ExecuteScalar());

                    // If dependencies exist, show error and cancel the deletion
                    if (dependentCount2 > 0)
                    {
                        MessageBox.Show("Cannot delete this student record because there are dependent entries in 'student_hours'.",
                                        "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                // Proceed with deletion if no dependencies are found
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

                            // Rewrite the CSV file with updated data
                            SaveToCsv(csvFilePath, (DataTable)dataGridViewStudents.DataSource);

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

        private void dataGridViewStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_intoserver_Click(object sender, EventArgs e)
        {
            // Initialize MY_DB to handle the database connection
            MY_DB db = new MY_DB();

            try
            {
                db.openConnection(); // Open the database connection

                foreach (DataGridViewRow row in dataGridViewStudents.Rows)
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

        private void button_recovery_Click(object sender, EventArgs e)
        {
            if (deletedRowsStack.Count > 0)
            {
                // Pop the latest deleted row from the stack
                DataRow restoredRow = deletedRowsStack.Pop();

                // Add the restored row back to the DataTable
                DataTable dataTable = (DataTable)dataGridViewStudents.DataSource;
                dataTable.Rows.Add(restoredRow.ItemArray);
                dataTable.AcceptChanges(); // Commit the addition to the DataTable

                // Restore the row in MySQL
                long studentId = Convert.ToInt64(restoredRow["Student ID"]);
                string firstName = restoredRow["First Name"].ToString();
                string lastName = restoredRow["Last Name"].ToString();
                string userName = restoredRow["Username"].ToString();
                

                AddStudentToMySQL(studentId, firstName, lastName, userName);

                // Refresh the DataGridView to reflect the restored row
                dataGridViewStudents.DataSource = null;
                dataGridViewStudents.DataSource = dataTable;

                MessageBox.Show("Row successfully restored.", "Recovery Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No rows to restore.", "Recovery Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
    }
