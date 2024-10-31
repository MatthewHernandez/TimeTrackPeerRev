namespace Desktop_App_For_Professor
{
    partial class Form_std_work_edit
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
            this.label_work_edit = new System.Windows.Forms.Label();
            this.labelStudentId = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.dateTimePickerWorkDate_origin = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerWorkDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxWorkHour = new System.Windows.Forms.TextBox();
            this.textBoxWorkHour_origin = new System.Windows.Forms.TextBox();
            this.textBoxWorkDescription = new System.Windows.Forms.TextBox();
            this.textBoxWorkDescription_origin = new System.Windows.Forms.TextBox();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label_date = new System.Windows.Forms.Label();
            this.label_hour = new System.Windows.Forms.Label();
            this.label_description = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_work_edit
            // 
            this.label_work_edit.AutoSize = true;
            this.label_work_edit.Location = new System.Drawing.Point(30, 35);
            this.label_work_edit.Name = "label_work_edit";
            this.label_work_edit.Size = new System.Drawing.Size(104, 16);
            this.label_work_edit.TabIndex = 0;
            this.label_work_edit.Text = "Edit Work Hours";
            // 
            // labelStudentId
            // 
            this.labelStudentId.AutoSize = true;
            this.labelStudentId.Location = new System.Drawing.Point(42, 63);
            this.labelStudentId.Name = "labelStudentId";
            this.labelStudentId.Size = new System.Drawing.Size(18, 16);
            this.labelStudentId.TabIndex = 1;
            this.labelStudentId.Text = "id";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(42, 101);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(41, 16);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "name";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(42, 136);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(40, 16);
            this.labelEmail.TabIndex = 4;
            this.labelEmail.Text = "email";
            // 
            // dateTimePickerWorkDate_origin
            // 
            this.dateTimePickerWorkDate_origin.Enabled = false;
            this.dateTimePickerWorkDate_origin.Location = new System.Drawing.Point(76, 208);
            this.dateTimePickerWorkDate_origin.Name = "dateTimePickerWorkDate_origin";
            this.dateTimePickerWorkDate_origin.Size = new System.Drawing.Size(254, 22);
            this.dateTimePickerWorkDate_origin.TabIndex = 5;
            // 
            // dateTimePickerWorkDate
            // 
            this.dateTimePickerWorkDate.Location = new System.Drawing.Point(76, 248);
            this.dateTimePickerWorkDate.Name = "dateTimePickerWorkDate";
            this.dateTimePickerWorkDate.Size = new System.Drawing.Size(253, 22);
            this.dateTimePickerWorkDate.TabIndex = 6;
            // 
            // textBoxWorkHour
            // 
            this.textBoxWorkHour.Location = new System.Drawing.Point(353, 248);
            this.textBoxWorkHour.Name = "textBoxWorkHour";
            this.textBoxWorkHour.Size = new System.Drawing.Size(100, 22);
            this.textBoxWorkHour.TabIndex = 7;
            // 
            // textBoxWorkHour_origin
            // 
            this.textBoxWorkHour_origin.Location = new System.Drawing.Point(353, 208);
            this.textBoxWorkHour_origin.Name = "textBoxWorkHour_origin";
            this.textBoxWorkHour_origin.ReadOnly = true;
            this.textBoxWorkHour_origin.Size = new System.Drawing.Size(100, 22);
            this.textBoxWorkHour_origin.TabIndex = 8;
            // 
            // textBoxWorkDescription
            // 
            this.textBoxWorkDescription.Location = new System.Drawing.Point(477, 250);
            this.textBoxWorkDescription.Name = "textBoxWorkDescription";
            this.textBoxWorkDescription.Size = new System.Drawing.Size(301, 22);
            this.textBoxWorkDescription.TabIndex = 9;
            // 
            // textBoxWorkDescription_origin
            // 
            this.textBoxWorkDescription_origin.Location = new System.Drawing.Point(477, 208);
            this.textBoxWorkDescription_origin.Name = "textBoxWorkDescription_origin";
            this.textBoxWorkDescription_origin.ReadOnly = true;
            this.textBoxWorkDescription_origin.Size = new System.Drawing.Size(301, 22);
            this.textBoxWorkDescription_origin.TabIndex = 10;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(188, 359);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(116, 34);
            this.buttonConfirm.TabIndex = 11;
            this.buttonConfirm.Text = "CONFRIM";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(521, 359);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(110, 34);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "CANCEL";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label_date
            // 
            this.label_date.AutoSize = true;
            this.label_date.Location = new System.Drawing.Point(185, 177);
            this.label_date.Name = "label_date";
            this.label_date.Size = new System.Drawing.Size(44, 16);
            this.label_date.TabIndex = 13;
            this.label_date.Text = "DATE";
            // 
            // label_hour
            // 
            this.label_hour.AutoSize = true;
            this.label_hour.Location = new System.Drawing.Point(379, 177);
            this.label_hour.Name = "label_hour";
            this.label_hour.Size = new System.Drawing.Size(43, 16);
            this.label_hour.TabIndex = 14;
            this.label_hour.Text = "Hours";
            this.label_hour.Click += new System.EventHandler(this.label1_Click);
            // 
            // label_description
            // 
            this.label_description.AutoSize = true;
            this.label_description.Location = new System.Drawing.Point(587, 177);
            this.label_description.Name = "label_description";
            this.label_description.Size = new System.Drawing.Size(75, 16);
            this.label_description.TabIndex = 15;
            this.label_description.Text = "Description";
            // 
            // Form_std_work_edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_description);
            this.Controls.Add(this.label_hour);
            this.Controls.Add(this.label_date);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.textBoxWorkDescription_origin);
            this.Controls.Add(this.textBoxWorkDescription);
            this.Controls.Add(this.textBoxWorkHour_origin);
            this.Controls.Add(this.textBoxWorkHour);
            this.Controls.Add(this.dateTimePickerWorkDate);
            this.Controls.Add(this.dateTimePickerWorkDate_origin);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelStudentId);
            this.Controls.Add(this.label_work_edit);
            this.Name = "Form_std_work_edit";
            this.Text = "Form_std_work_edit";
            this.Load += new System.EventHandler(this.Form_std_work_edit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_work_edit;
        private System.Windows.Forms.Label labelStudentId;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.DateTimePicker dateTimePickerWorkDate_origin;
        private System.Windows.Forms.DateTimePicker dateTimePickerWorkDate;
        private System.Windows.Forms.TextBox textBoxWorkHour;
        private System.Windows.Forms.TextBox textBoxWorkHour_origin;
        private System.Windows.Forms.TextBox textBoxWorkDescription;
        private System.Windows.Forms.TextBox textBoxWorkDescription_origin;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.Label label_hour;
        private System.Windows.Forms.Label label_description;
    }
}