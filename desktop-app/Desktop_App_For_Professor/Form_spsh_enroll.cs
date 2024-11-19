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
    public partial class Form_spsh_enroll : Form
    {
        public event Action<int> EnrollConfirmed; // Event to notify the parent form
        private long studentId;
        private string firstName;
        private string lastName;
        private string userName;
        public Form_spsh_enroll(long studentId, string firstName, string lastName, string userName)
        {
            InitializeComponent();

            this.studentId = studentId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.userName = userName;
        }

        private void Form_spsh_enroll_Load(object sender, EventArgs e)
        {
            // Display selected student's information
            textBox_first.Text = firstName;
            textBox_last.Text = lastName;  
            textBox_user.Text = userName;
            textBox_id.Text = studentId.ToString();

            // Load professor's available classes into ComboBox
            LoadProfessorClasses();
        }

        private void LoadProfessorClasses()
        {
            MY_DB db = new MY_DB();

            try
            {
                db.openConnection();

                string query = "SELECT id, class_name FROM class WHERE professor_id = @prof_id";
                MySqlCommand command = new MySqlCommand(query, db.getConnection);
                command.Parameters.AddWithValue("@prof_id", ProfessorSession.ProfessorId);

                MySqlDataReader reader = command.ExecuteReader();

                comboBoxClasses.Items.Clear();

                while (reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem
                    {
                        Value = Convert.ToInt32(reader["id"]), // class_id
                        Text = reader["class_name"].ToString() // class_name
                    };

                    comboBoxClasses.Items.Add(item);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading classes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                db.closeConnection();
            }
        }

        private void comboBoxClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxClasses.SelectedItem is ComboBoxItem selectedItem)
            {
                ProfessorSession.curClass = selectedItem.Value; // Set selected class ID
            }
            else
            {
                ProfessorSession.curClass = -1; // Reset if no valid selection
            }
        }
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (comboBoxClasses.SelectedItem is ComboBoxItem selectedItem)
            {
                EnrollConfirmed?.Invoke(selectedItem.Value);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a class to enroll the student.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            if (comboBoxClasses.SelectedItem is ComboBoxItem selectedItem)
            {
                EnrollConfirmed?.Invoke(selectedItem.Value);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a class to enroll the student.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
