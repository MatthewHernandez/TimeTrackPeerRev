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
    //gxk220025
    public partial class Form_std_work_edit : Form
    {
        // Define a delegate and event for notifying the parent form
        public delegate void WorkHoursUpdatedHandler();
        public event WorkHoursUpdatedHandler WorkHoursUpdated;

        public Form_std_work_edit()
        {
            InitializeComponent();
        }

        //gxk220025
        private void Form_std_work_edit_Load(object sender, EventArgs e)
        {
            // Display information in labels or read-only text boxes
            labelStudentId.Text = "ID: " + STUDENT.Id.ToString();
            labelName.Text = STUDENT.FirstName + " " + STUDENT.LastName;
            labelEmail.Text = "Email: " + STUDENT.Email;

            //display original value. none editable
            dateTimePickerWorkDate_origin.Value = STUDENT.workDate;
            textBoxWorkHour_origin.Text = STUDENT.workHour.ToString();
            textBoxWorkDescription_origin.Text = STUDENT.workDescrip;
            // Set editable fields with current values
            dateTimePickerWorkDate.Value = STUDENT.workDate;
            textBoxWorkHour.Text = STUDENT.workHour.ToString();
            textBoxWorkDescription.Text = STUDENT.workDescrip;
        }

        //gxk220025
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            MY_DB db = new MY_DB();
            try
            {
                db.openConnection();

                // SQL query to update the selected work record in the database
                string query = @"
            UPDATE time_entry
            SET date = @date, hours = @hours, comments = @comments
            WHERE student_id = @student_id"; //work_id = @work_id AND

                MySqlCommand command = new MySqlCommand(query, db.getConnection);

                // Update with new values from the form
                command.Parameters.AddWithValue("@date", dateTimePickerWorkDate.Value);
                command.Parameters.AddWithValue("@hours", Convert.ToDecimal(textBoxWorkHour.Text));
                command.Parameters.AddWithValue("@comment", textBoxWorkDescription.Text);
                //command.Parameters.AddWithValue("@work_id", STUDENT.workId);
                command.Parameters.AddWithValue("@student_id", STUDENT.Id);

                // Execute the update command
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record updated successfully.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Trigger the event to notify the parent form //refresh work hour table for new data
                    WorkHoursUpdated?.Invoke();

                    this.Close();  // Close the form after successful update
                }
                else
                {
                    MessageBox.Show("No record was updated.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();  // Close the form without saving changes
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
