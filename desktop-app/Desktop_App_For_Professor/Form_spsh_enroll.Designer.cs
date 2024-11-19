namespace Desktop_App_For_Professor
{
    partial class Form_spsh_enroll
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
            this.textBox_user = new System.Windows.Forms.TextBox();
            this.textBox_last = new System.Windows.Forms.TextBox();
            this.textBox_first = new System.Windows.Forms.TextBox();
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxClasses = new System.Windows.Forms.ComboBox();
            this.button_confirm = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.label_class = new System.Windows.Forms.Label();
            this.label_enroll = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_user
            // 
            this.textBox_user.Location = new System.Drawing.Point(369, 82);
            this.textBox_user.Name = "textBox_user";
            this.textBox_user.ReadOnly = true;
            this.textBox_user.Size = new System.Drawing.Size(158, 22);
            this.textBox_user.TabIndex = 1;
            // 
            // textBox_last
            // 
            this.textBox_last.Location = new System.Drawing.Point(207, 82);
            this.textBox_last.Name = "textBox_last";
            this.textBox_last.ReadOnly = true;
            this.textBox_last.Size = new System.Drawing.Size(156, 22);
            this.textBox_last.TabIndex = 1;
            // 
            // textBox_first
            // 
            this.textBox_first.Location = new System.Drawing.Point(55, 82);
            this.textBox_first.Name = "textBox_first";
            this.textBox_first.ReadOnly = true;
            this.textBox_first.Size = new System.Drawing.Size(146, 22);
            this.textBox_first.TabIndex = 1;
            // 
            // textBox_id
            // 
            this.textBox_id.Location = new System.Drawing.Point(533, 82);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.ReadOnly = true;
            this.textBox_id.Size = new System.Drawing.Size(163, 22);
            this.textBox_id.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(530, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Student ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(366, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Last Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "First Name";
            // 
            // comboBoxClasses
            // 
            this.comboBoxClasses.FormattingEnabled = true;
            this.comboBoxClasses.Location = new System.Drawing.Point(207, 144);
            this.comboBoxClasses.Name = "comboBoxClasses";
            this.comboBoxClasses.Size = new System.Drawing.Size(195, 24);
            this.comboBoxClasses.TabIndex = 14;
            // 
            // button_confirm
            // 
            this.button_confirm.Location = new System.Drawing.Point(189, 220);
            this.button_confirm.Name = "button_confirm";
            this.button_confirm.Size = new System.Drawing.Size(132, 40);
            this.button_confirm.TabIndex = 15;
            this.button_confirm.Text = "Confirm";
            this.button_confirm.UseVisualStyleBackColor = true;
            this.button_confirm.Click += new System.EventHandler(this.button_confirm_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(395, 220);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(132, 40);
            this.button_cancel.TabIndex = 15;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // label_class
            // 
            this.label_class.AutoSize = true;
            this.label_class.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_class.ForeColor = System.Drawing.Color.Black;
            this.label_class.Location = new System.Drawing.Point(97, 141);
            this.label_class.Name = "label_class";
            this.label_class.Size = new System.Drawing.Size(66, 23);
            this.label_class.TabIndex = 16;
            this.label_class.Text = "Class:";
            // 
            // label_enroll
            // 
            this.label_enroll.AutoSize = true;
            this.label_enroll.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_enroll.Location = new System.Drawing.Point(23, 12);
            this.label_enroll.Name = "label_enroll";
            this.label_enroll.Size = new System.Drawing.Size(163, 34);
            this.label_enroll.TabIndex = 17;
            this.label_enroll.Text = "Class Enroll";
            // 
            // Form_spsh_enroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 301);
            this.Controls.Add(this.label_enroll);
            this.Controls.Add(this.label_class);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_confirm);
            this.Controls.Add(this.comboBoxClasses);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_id);
            this.Controls.Add(this.textBox_first);
            this.Controls.Add(this.textBox_last);
            this.Controls.Add(this.textBox_user);
            this.Name = "Form_spsh_enroll";
            this.Text = "Form_spsh_enroll";
            this.Load += new System.EventHandler(this.Form_spsh_enroll_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_user;
        private System.Windows.Forms.TextBox textBox_last;
        private System.Windows.Forms.TextBox textBox_first;
        private System.Windows.Forms.TextBox textBox_id;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxClasses;
        private System.Windows.Forms.Button button_confirm;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label_class;
        private System.Windows.Forms.Label label_enroll;
    }
}