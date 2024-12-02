namespace Desktop_App_For_Professor
{
    partial class Form_login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_login));
            this.login_top_logo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.login_title = new System.Windows.Forms.Label();
            this.label_account = new System.Windows.Forms.Label();
            this.label_account_password = new System.Windows.Forms.Label();
            this.textBox_account = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.button_login = new System.Windows.Forms.Button();
            this.button_login_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.login_top_logo)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // login_top_logo
            // 
            this.login_top_logo.Image = ((System.Drawing.Image)(resources.GetObject("login_top_logo.Image")));
            this.login_top_logo.Location = new System.Drawing.Point(12, 12);
            this.login_top_logo.Name = "login_top_logo";
            this.login_top_logo.Size = new System.Drawing.Size(68, 72);
            this.login_top_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.login_top_logo.TabIndex = 0;
            this.login_top_logo.TabStop = false;
            this.login_top_logo.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.login_title);
            this.panel1.Controls.Add(this.login_top_logo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(582, 100);
            this.panel1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(102, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "for Professor";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // login_title
            // 
            this.login_title.AutoSize = true;
            this.login_title.Font = new System.Drawing.Font("Century Gothic", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_title.ForeColor = System.Drawing.SystemColors.Highlight;
            this.login_title.Location = new System.Drawing.Point(86, 12);
            this.login_title.Name = "login_title";
            this.login_title.Size = new System.Drawing.Size(293, 49);
            this.login_title.TabIndex = 1;
            this.login_title.Text = "Desktop App";
            this.login_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_account
            // 
            this.label_account.AutoSize = true;
            this.label_account.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_account.ForeColor = System.Drawing.Color.Gray;
            this.label_account.Location = new System.Drawing.Point(102, 175);
            this.label_account.Name = "label_account";
            this.label_account.Size = new System.Drawing.Size(115, 27);
            this.label_account.TabIndex = 2;
            this.label_account.Text = "Account:";
            this.label_account.Click += new System.EventHandler(this.label_account_id_Click);
            // 
            // label_account_password
            // 
            this.label_account_password.AutoSize = true;
            this.label_account_password.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_account_password.ForeColor = System.Drawing.Color.Gray;
            this.label_account_password.Location = new System.Drawing.Point(102, 229);
            this.label_account_password.Name = "label_account_password";
            this.label_account_password.Size = new System.Drawing.Size(121, 27);
            this.label_account_password.TabIndex = 3;
            this.label_account_password.Text = "Password:";
            // 
            // textBox_account
            // 
            this.textBox_account.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_account.Location = new System.Drawing.Point(229, 175);
            this.textBox_account.Name = "textBox_account";
            this.textBox_account.Size = new System.Drawing.Size(192, 32);
            this.textBox_account.TabIndex = 0;
            this.textBox_account.TextChanged += new System.EventHandler(this.textBox_account_TextChanged);
            // 
            // textBox_password
            // 
            this.textBox_password.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_password.Location = new System.Drawing.Point(229, 229);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(192, 32);
            this.textBox_password.TabIndex = 1;
            this.textBox_password.UseSystemPasswordChar = true;
            // 
            // button_login
            // 
            this.button_login.Location = new System.Drawing.Point(427, 175);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(105, 86);
            this.button_login.TabIndex = 2;
            this.button_login.Text = "Login";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // button_login_cancel
            // 
            this.button_login_cancel.Location = new System.Drawing.Point(440, 274);
            this.button_login_cancel.Name = "button_login_cancel";
            this.button_login_cancel.Size = new System.Drawing.Size(82, 38);
            this.button_login_cancel.TabIndex = 3;
            this.button_login_cancel.Text = "Cancel";
            this.button_login_cancel.UseVisualStyleBackColor = true;
            this.button_login_cancel.Click += new System.EventHandler(this.button_login_cancel_Click);
            // 
            // Form_login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(582, 353);
            this.Controls.Add(this.button_login_cancel);
            this.Controls.Add(this.button_login);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.textBox_account);
            this.Controls.Add(this.label_account_password);
            this.Controls.Add(this.label_account);
            this.Controls.Add(this.panel1);
            this.Name = "Form_login";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form_login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.login_top_logo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox login_top_logo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_account;
        private System.Windows.Forms.Label label_account_password;
        private System.Windows.Forms.TextBox textBox_account;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Button button_login_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label login_title;
    }
}