namespace Desktop_App_For_Professor
{
    partial class Form_std_add
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
            this.stdadd_username = new System.Windows.Forms.Label();
            this.stdadd_id = new System.Windows.Forms.Label();
            this.stdadd_first = new System.Windows.Forms.Label();
            this.stdadd_last = new System.Windows.Forms.Label();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.textBox_first = new System.Windows.Forms.TextBox();
            this.textBox_last = new System.Windows.Forms.TextBox();
            this.stdadd_button_confirm = new System.Windows.Forms.Button();
            this.stdadd_button_cancle = new System.Windows.Forms.Button();
            this.textBox_email = new System.Windows.Forms.TextBox();
            this.stdadd_email = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // stdadd_username
            // 
            this.stdadd_username.AutoSize = true;
            this.stdadd_username.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stdadd_username.Location = new System.Drawing.Point(50, 102);
            this.stdadd_username.Name = "stdadd_username";
            this.stdadd_username.Size = new System.Drawing.Size(134, 27);
            this.stdadd_username.TabIndex = 0;
            this.stdadd_username.Text = "User Name";
            // 
            // stdadd_id
            // 
            this.stdadd_id.AutoSize = true;
            this.stdadd_id.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stdadd_id.Location = new System.Drawing.Point(55, 147);
            this.stdadd_id.Name = "stdadd_id";
            this.stdadd_id.Size = new System.Drawing.Size(124, 27);
            this.stdadd_id.TabIndex = 1;
            this.stdadd_id.Text = "Student ID";
            // 
            // stdadd_first
            // 
            this.stdadd_first.AutoSize = true;
            this.stdadd_first.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stdadd_first.Location = new System.Drawing.Point(50, 190);
            this.stdadd_first.Name = "stdadd_first";
            this.stdadd_first.Size = new System.Drawing.Size(128, 27);
            this.stdadd_first.TabIndex = 1;
            this.stdadd_first.Text = "First Name";
            // 
            // stdadd_last
            // 
            this.stdadd_last.AutoSize = true;
            this.stdadd_last.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stdadd_last.Location = new System.Drawing.Point(50, 234);
            this.stdadd_last.Name = "stdadd_last";
            this.stdadd_last.Size = new System.Drawing.Size(129, 27);
            this.stdadd_last.TabIndex = 1;
            this.stdadd_last.Text = "Last Name";
            // 
            // textBox_username
            // 
            this.textBox_username.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_username.Location = new System.Drawing.Point(190, 102);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(233, 32);
            this.textBox_username.TabIndex = 1;
            // 
            // textBox_id
            // 
            this.textBox_id.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_id.Location = new System.Drawing.Point(190, 147);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.Size = new System.Drawing.Size(233, 32);
            this.textBox_id.TabIndex = 2;
            // 
            // textBox_first
            // 
            this.textBox_first.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_first.Location = new System.Drawing.Point(190, 190);
            this.textBox_first.Name = "textBox_first";
            this.textBox_first.Size = new System.Drawing.Size(233, 32);
            this.textBox_first.TabIndex = 3;
            // 
            // textBox_last
            // 
            this.textBox_last.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_last.Location = new System.Drawing.Point(190, 229);
            this.textBox_last.Name = "textBox_last";
            this.textBox_last.Size = new System.Drawing.Size(233, 32);
            this.textBox_last.TabIndex = 4;
            this.textBox_last.TextChanged += new System.EventHandler(this.addlast_TextChanged);
            // 
            // stdadd_button_confirm
            // 
            this.stdadd_button_confirm.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stdadd_button_confirm.Location = new System.Drawing.Point(327, 325);
            this.stdadd_button_confirm.Name = "stdadd_button_confirm";
            this.stdadd_button_confirm.Size = new System.Drawing.Size(96, 37);
            this.stdadd_button_confirm.TabIndex = 6;
            this.stdadd_button_confirm.Text = "Add";
            this.stdadd_button_confirm.UseVisualStyleBackColor = true;
            this.stdadd_button_confirm.Click += new System.EventHandler(this.stdadd_button_confirm_Click);
            // 
            // stdadd_button_cancle
            // 
            this.stdadd_button_cancle.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stdadd_button_cancle.Location = new System.Drawing.Point(190, 325);
            this.stdadd_button_cancle.Name = "stdadd_button_cancle";
            this.stdadd_button_cancle.Size = new System.Drawing.Size(96, 37);
            this.stdadd_button_cancle.TabIndex = 7;
            this.stdadd_button_cancle.Text = "Cancel";
            this.stdadd_button_cancle.UseVisualStyleBackColor = true;
            this.stdadd_button_cancle.Click += new System.EventHandler(this.stdadd_button_cancle_Click);
            // 
            // textBox_email
            // 
            this.textBox_email.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_email.Location = new System.Drawing.Point(190, 272);
            this.textBox_email.Name = "textBox_email";
            this.textBox_email.Size = new System.Drawing.Size(233, 32);
            this.textBox_email.TabIndex = 5;
            this.textBox_email.TextChanged += new System.EventHandler(this.addlast_TextChanged);
            // 
            // stdadd_email
            // 
            this.stdadd_email.AutoSize = true;
            this.stdadd_email.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stdadd_email.Location = new System.Drawing.Point(96, 272);
            this.stdadd_email.Name = "stdadd_email";
            this.stdadd_email.Size = new System.Drawing.Size(82, 27);
            this.stdadd_email.TabIndex = 1;
            this.stdadd_email.Text = "E-Mail";
            // 
            // Form_std_add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 450);
            this.Controls.Add(this.stdadd_button_cancle);
            this.Controls.Add(this.stdadd_button_confirm);
            this.Controls.Add(this.textBox_email);
            this.Controls.Add(this.textBox_last);
            this.Controls.Add(this.textBox_first);
            this.Controls.Add(this.textBox_id);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.stdadd_email);
            this.Controls.Add(this.stdadd_last);
            this.Controls.Add(this.stdadd_first);
            this.Controls.Add(this.stdadd_id);
            this.Controls.Add(this.stdadd_username);
            this.Name = "Form_std_add";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_add_std";
            this.Load += new System.EventHandler(this.Form_std_add_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label stdadd_username;
        private System.Windows.Forms.Label stdadd_id;
        private System.Windows.Forms.Label stdadd_first;
        private System.Windows.Forms.Label stdadd_last;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.TextBox textBox_id;
        private System.Windows.Forms.TextBox textBox_first;
        private System.Windows.Forms.TextBox textBox_last;
        private System.Windows.Forms.Button stdadd_button_confirm;
        private System.Windows.Forms.Button stdadd_button_cancle;
        private System.Windows.Forms.TextBox textBox_email;
        private System.Windows.Forms.Label stdadd_email;
    }
}