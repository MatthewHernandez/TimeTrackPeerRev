using System.Windows.Forms;

namespace Desktop_App_For_Professor
{
    partial class Form_main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_enroll = new System.Windows.Forms.Button();
            this.labelUsername = new System.Windows.Forms.Label();
            this.button_import = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_change = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.label_classection = new System.Windows.Forms.Label();
            this.comboBoxClasses = new System.Windows.Forms.ComboBox();
            this.button_save = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.login_top_logo = new System.Windows.Forms.PictureBox();
            this.dataGridViewStudents = new System.Windows.Forms.DataGridView();
            this.label_class = new System.Windows.Forms.Label();
            this.label_class_0 = new System.Windows.Forms.Label();
            this.button_logout = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.login_top_logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_enroll);
            this.panel1.Controls.Add(this.button_import);
            this.panel1.Controls.Add(this.button_delete);
            this.panel1.Controls.Add(this.button_change);
            this.panel1.Controls.Add(this.button_add);
            this.panel1.Controls.Add(this.label_classection);
            this.panel1.Controls.Add(this.comboBoxClasses);
            this.panel1.Controls.Add(this.button_save);
            this.panel1.Location = new System.Drawing.Point(0, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 593);
            this.panel1.TabIndex = 7;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button_enroll
            // 
            this.button_enroll.Location = new System.Drawing.Point(43, 287);
            this.button_enroll.Name = "button_enroll";
            this.button_enroll.Size = new System.Drawing.Size(110, 45);
            this.button_enroll.TabIndex = 11;
            this.button_enroll.Text = "Enroll";
            this.button_enroll.UseVisualStyleBackColor = true;
            this.button_enroll.Click += new System.EventHandler(this.button_enroll_Click);
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.labelUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelUsername.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsername.Location = new System.Drawing.Point(109, 34);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(2, 25);
            this.labelUsername.TabIndex = 9;
            this.labelUsername.Click += new System.EventHandler(this.labelUsername_Click);
            // 
            // button_import
            // 
            this.button_import.Location = new System.Drawing.Point(43, 185);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(110, 45);
            this.button_import.TabIndex = 7;
            this.button_import.Text = "Import";
            this.button_import.UseVisualStyleBackColor = true;
            this.button_import.Click += new System.EventHandler(this.button_import_Click);
            // 
            // button_delete
            // 
            this.button_delete.Location = new System.Drawing.Point(43, 389);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(110, 45);
            this.button_delete.TabIndex = 15;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_change
            // 
            this.button_change.Location = new System.Drawing.Point(43, 338);
            this.button_change.Name = "button_change";
            this.button_change.Size = new System.Drawing.Size(110, 45);
            this.button_change.TabIndex = 13;
            this.button_change.Text = "Change";
            this.button_change.UseVisualStyleBackColor = true;
            this.button_change.Click += new System.EventHandler(this.button_change_Click);
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(43, 236);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(110, 45);
            this.button_add.TabIndex = 9;
            this.button_add.Text = "Add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // label_classection
            // 
            this.label_classection.AutoSize = true;
            this.label_classection.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_classection.Location = new System.Drawing.Point(29, 22);
            this.label_classection.Name = "label_classection";
            this.label_classection.Size = new System.Drawing.Size(111, 23);
            this.label_classection.TabIndex = 11;
            this.label_classection.Text = "Your Class";
            this.label_classection.Click += new System.EventHandler(this.label1_Click);
            // 
            // comboBoxClasses
            // 
            this.comboBoxClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClasses.FormattingEnabled = true;
            this.comboBoxClasses.Location = new System.Drawing.Point(22, 59);
            this.comboBoxClasses.Name = "comboBoxClasses";
            this.comboBoxClasses.Size = new System.Drawing.Size(160, 24);
            this.comboBoxClasses.TabIndex = 3;
            this.comboBoxClasses.SelectedIndexChanged += new System.EventHandler(this.LoadProfessorClasses_SelectedIndexChanged);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(43, 134);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(110, 45);
            this.button_save.TabIndex = 5;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_logout);
            this.panel2.Controls.Add(this.login_top_logo);
            this.panel2.Controls.Add(this.labelUsername);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1262, 84);
            this.panel2.TabIndex = 10;
            // 
            // login_top_logo
            // 
            this.login_top_logo.Image = ((System.Drawing.Image)(resources.GetObject("login_top_logo.Image")));
            this.login_top_logo.Location = new System.Drawing.Point(22, 9);
            this.login_top_logo.Name = "login_top_logo";
            this.login_top_logo.Size = new System.Drawing.Size(68, 72);
            this.login_top_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.login_top_logo.TabIndex = 6;
            this.login_top_logo.TabStop = false;
            // 
            // dataGridViewStudents
            // 
            this.dataGridViewStudents.AllowUserToOrderColumns = true;
            this.dataGridViewStudents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewStudents.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewStudents.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewStudents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewStudents.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewStudents.Location = new System.Drawing.Point(255, 139);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.ReadOnly = true;
            this.dataGridViewStudents.RowHeadersWidth = 51;
            this.dataGridViewStudents.RowTemplate.Height = 24;
            this.dataGridViewStudents.Size = new System.Drawing.Size(974, 493);
            this.dataGridViewStudents.TabIndex = 17;
            this.dataGridViewStudents.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStudents_CellContentClick);
            // 
            // label_class
            // 
            this.label_class.AutoSize = true;
            this.label_class.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_class.Location = new System.Drawing.Point(311, 102);
            this.label_class.Name = "label_class";
            this.label_class.Size = new System.Drawing.Size(58, 23);
            this.label_class.TabIndex = 14;
            this.label_class.Text = "class";
            this.label_class.Click += new System.EventHandler(this.label_class_Click);
            // 
            // label_class_0
            // 
            this.label_class_0.AutoSize = true;
            this.label_class_0.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_class_0.Location = new System.Drawing.Point(251, 102);
            this.label_class_0.Name = "label_class_0";
            this.label_class_0.Size = new System.Drawing.Size(72, 23);
            this.label_class_0.TabIndex = 15;
            this.label_class_0.Text = "Class: ";
            // 
            // button_logout
            // 
            this.button_logout.Location = new System.Drawing.Point(1162, 22);
            this.button_logout.Name = "button_logout";
            this.button_logout.Size = new System.Drawing.Size(78, 37);
            this.button_logout.TabIndex = 99;
            this.button_logout.Text = "Log Out";
            this.button_logout.UseVisualStyleBackColor = true;
            this.button_logout.Click += new System.EventHandler(this.button_logout_Click);
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.label_class);
            this.Controls.Add(this.label_class_0);
            this.Controls.Add(this.dataGridViewStudents);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.IsMdiContainer = true;
            this.Name = "Form_main";
            this.Load += new System.EventHandler(this.Form_main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.login_top_logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.DataGridView dataGridViewStudents;
        private System.Windows.Forms.ComboBox comboBoxClasses;
        private System.Windows.Forms.Label label_classection;
        private Label label_class;
        private Label label_class_0;
        private Button button_delete;
        private Button button_change;
        private Button button_add;
        private Button button_import;
        private Button button_enroll;
        private PictureBox login_top_logo;
        private Button button_logout;
    }
}