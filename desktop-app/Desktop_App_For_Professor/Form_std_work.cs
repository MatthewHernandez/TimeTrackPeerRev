using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Desktop_App_For_Professor
{
    public partial class Form_std_work : Form
    {

        public Form_std_work()
        {
            InitializeComponent();

            //Cell double click then edit form open
            dataGridView_stdwork.CellDoubleClick += DataGridView_stdwork_CellDoubleClick;
        }

        //Work Hour Cell Double Click
        private void DataGridView_stdwork_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            

            //version 10/31/2024
            // Ensure the row is valid (not a header or empty)
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView_stdwork.Rows[e.RowIndex];

                // Retrieve the selected work hour data from the row
                STUDENT.workId = Convert.ToInt32(selectedRow.Cells["work_id"].Value);          // Assuming "work_id" is the column name
                STUDENT.workDate = Convert.ToDateTime(selectedRow.Cells["date"].Value);        // Assuming "date" is the column name
                STUDENT.workHour = Convert.ToDecimal(selectedRow.Cells["hours"].Value);        // Assuming "hours" is the column name
                STUDENT.workDescrip = selectedRow.Cells["description"].Value.ToString();       // Assuming "description" is the column name
                

                // Open the edit form
                Form_std_work_edit workStdInfo = new Form_std_work_edit();

                // Subscribe to the WorkHoursUpdated event
                workStdInfo.WorkHoursUpdated += LoadStudentWorkHours;

                // Show the edit form as a modal dialog
                workStdInfo.ShowDialog();
            }
        }

        private void Form_std_work_Load(object sender, EventArgs e)
        {
            //gxk220025 display logged in user
            // Check if the user is logged in and display the username
            if (!(STUDENT.Id<=0))
            {
                // Set the label to display the current professor's username
                label_stdinfo.Text = STUDENT.FirstName + " " + STUDENT.LastName;

                LoadStudentWorkHours();
            }
            else
            {
                // If no user is logged in, show a default message
                label_stdinfo.Text = "No student information.";
            }
        }

        // Method to load and display the student's work hours in the DataGridView
        private void LoadStudentWorkHours()
        {
            MY_DB db = new MY_DB();
            try
            {
                db.openConnection();

                // SQL query to retrieve work hours for the selected student and class
                string query = @"
                    SELECT work_id, date, hours, description
                    FROM student_hours
                    WHERE student_id = @student_id AND class_id = @class_id";

                MySqlCommand command = new MySqlCommand(query, db.getConnection);

                // Use the student ID from STUDENT and class ID from ProfessorSession
                command.Parameters.AddWithValue("@student_id", STUDENT.Id);
                command.Parameters.AddWithValue("@class_id", ProfessorSession.curClass);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable workHoursTable = new DataTable();

                // Fill the DataTable with the result of the query
                adapter.Fill(workHoursTable);

                // Bind the DataTable to the DataGridView
                dataGridView_stdwork.DataSource = workHoursTable;

                db.closeConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading student work hours: " + ex.Message);
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
