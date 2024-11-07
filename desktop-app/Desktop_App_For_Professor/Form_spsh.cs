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


namespace Desktop_App_For_Professor
{
    public partial class Form_spsh : Form
    {
        private string csvFilePath; // Track the CSV file path for saving

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

        private void button_delete_Click(object sender, EventArgs e)
        {
            /*
            if (dataGridViewStudents.SelectedRows.Count > 0)
            {
                DataTable dataTable = (DataTable)dataGridViewStudents.DataSource;

                // Delete selected rows from the DataTable
                foreach (DataGridViewRow row in dataGridViewStudents.SelectedRows)
                {
                    dataTable.Rows[row.Index].Delete(); // Mark row as deleted
                }
                dataTable.AcceptChanges(); // Apply deletions to the DataTable

                // Refresh DataGridView to show changes
                dataGridViewStudents.DataSource = null;
                dataGridViewStudents.DataSource = dataTable;

                // Rewrite the CSV file with updated data
                SaveToCsv(csvFilePath, dataTable);
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            */
            if (dataGridViewStudents.SelectedCells.Count > 0)
            {
                // Get the row index of the selected cell
                int rowIndex = dataGridViewStudents.SelectedCells[0].RowIndex;

                // Check if the selected row is the new (placeholder) row
                if (dataGridViewStudents.Rows[rowIndex].IsNewRow)
                {
                    MessageBox.Show("Cannot delete the placeholder row.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Proceed with deletion for non-placeholder rows
                DataTable dataTable = (DataTable)dataGridViewStudents.DataSource;

                // Delete the row in the DataTable
                dataTable.Rows[rowIndex].Delete();
                dataTable.AcceptChanges(); // Commit the deletion to the DataTable

                // Refresh the DataGridView to reflect the deleted row
                dataGridViewStudents.DataSource = null;
                dataGridViewStudents.DataSource = dataTable;

                // Save the updated DataTable to the CSV file
                SaveToCsv(csvFilePath, dataTable);
            }
            else
            {
                MessageBox.Show("Please select a cell in the row you want to delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_change_Click(object sender, EventArgs e)
        {
            /*
            if (dataGridViewStudents.SelectedCells.Count > 0)
            {
                // Get the selected cell's row
                DataGridViewRow selectedRow = dataGridViewStudents.Rows[dataGridViewStudents.SelectedCells[0].RowIndex];


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
                    // Update the selected row with the new data
                    selectedRow.Cells["Student ID"].Value = newId;
                    selectedRow.Cells["First Name"].Value = newFirstName;
                    selectedRow.Cells["Last Name"].Value = newLastName;
                    selectedRow.Cells["Username"].Value = newUserName;

                    // Refresh DataGridView to show updated data
                    dataGridViewStudents.Refresh();

                    // Rewrite the CSV file with updated data
                    SaveToCsv(csvFilePath, (DataTable)dataGridViewStudents.DataSource);
                };

                editForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a cell to modify.", "Modify Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
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
                        // Update the selected row with the new data
                        selectedRow.Cells["Student ID"].Value = newId;
                        selectedRow.Cells["First Name"].Value = newFirstName;
                        selectedRow.Cells["Last Name"].Value = newLastName;
                        selectedRow.Cells["Username"].Value = newUserName;

                        // Refresh DataGridView to show updated data
                        dataGridViewStudents.Refresh();

                        // Rewrite the CSV file with updated data
                        SaveToCsv(csvFilePath, (DataTable)dataGridViewStudents.DataSource);
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
    }
    }
