namespace Desktop_App_For_Professor
{
    partial class Form_spsh_ch
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
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.textBoxFirstNameIn = new System.Windows.Forms.TextBox();
            this.textBoxLastNameIn = new System.Windows.Forms.TextBox();
            this.textBoxUsernameIn = new System.Windows.Forms.TextBox();
            this.textBoxIDIn = new System.Windows.Forms.TextBox();
            this.button_confirm = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(73, 89);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.ReadOnly = true;
            this.textBoxLastName.Size = new System.Drawing.Size(152, 22);
            this.textBoxLastName.TabIndex = 0;
            this.textBoxLastName.TabStop = false;
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(241, 89);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.ReadOnly = true;
            this.textBoxFirstName.Size = new System.Drawing.Size(152, 22);
            this.textBoxFirstName.TabIndex = 0;
            this.textBoxFirstName.TabStop = false;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(418, 89);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.ReadOnly = true;
            this.textBoxUsername.Size = new System.Drawing.Size(152, 22);
            this.textBoxUsername.TabIndex = 0;
            this.textBoxUsername.TabStop = false;
            this.textBoxUsername.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(587, 89);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.ReadOnly = true;
            this.textBoxID.Size = new System.Drawing.Size(152, 22);
            this.textBoxID.TabIndex = 0;
            this.textBoxID.TabStop = false;
            // 
            // textBoxFirstNameIn
            // 
            this.textBoxFirstNameIn.Location = new System.Drawing.Point(73, 148);
            this.textBoxFirstNameIn.Name = "textBoxFirstNameIn";
            this.textBoxFirstNameIn.Size = new System.Drawing.Size(152, 22);
            this.textBoxFirstNameIn.TabIndex = 1;
            // 
            // textBoxLastNameIn
            // 
            this.textBoxLastNameIn.Location = new System.Drawing.Point(241, 148);
            this.textBoxLastNameIn.Name = "textBoxLastNameIn";
            this.textBoxLastNameIn.Size = new System.Drawing.Size(152, 22);
            this.textBoxLastNameIn.TabIndex = 2;
            // 
            // textBoxUsernameIn
            // 
            this.textBoxUsernameIn.Location = new System.Drawing.Point(418, 148);
            this.textBoxUsernameIn.Name = "textBoxUsernameIn";
            this.textBoxUsernameIn.Size = new System.Drawing.Size(152, 22);
            this.textBoxUsernameIn.TabIndex = 3;
            this.textBoxUsernameIn.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBoxIDIn
            // 
            this.textBoxIDIn.Location = new System.Drawing.Point(587, 148);
            this.textBoxIDIn.Name = "textBoxIDIn";
            this.textBoxIDIn.Size = new System.Drawing.Size(152, 22);
            this.textBoxIDIn.TabIndex = 4;
            // 
            // button_confirm
            // 
            this.button_confirm.Location = new System.Drawing.Point(206, 305);
            this.button_confirm.Name = "button_confirm";
            this.button_confirm.Size = new System.Drawing.Size(130, 51);
            this.button_confirm.TabIndex = 5;
            this.button_confirm.Text = "Confirm";
            this.button_confirm.UseVisualStyleBackColor = true;
            this.button_confirm.Click += new System.EventHandler(this.button_confirm_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(440, 305);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(130, 51);
            this.button_cancel.TabIndex = 6;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // Form_spsh_ch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_confirm);
            this.Controls.Add(this.textBoxIDIn);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.textBoxUsernameIn);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.textBoxLastNameIn);
            this.Controls.Add(this.textBoxFirstName);
            this.Controls.Add(this.textBoxFirstNameIn);
            this.Controls.Add(this.textBoxLastName);
            this.Name = "Form_spsh_ch";
            this.Text = "Form_spsh_ch";
            this.Load += new System.EventHandler(this.Form_spsh_ch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.TextBox textBoxFirstNameIn;
        private System.Windows.Forms.TextBox textBoxLastNameIn;
        private System.Windows.Forms.TextBox textBoxUsernameIn;
        private System.Windows.Forms.TextBox textBoxIDIn;
        private System.Windows.Forms.Button button_confirm;
        private System.Windows.Forms.Button button_cancel;
    }
}