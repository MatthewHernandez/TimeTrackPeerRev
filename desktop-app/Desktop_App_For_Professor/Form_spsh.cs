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
    }
    }
