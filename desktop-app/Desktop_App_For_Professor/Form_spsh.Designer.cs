namespace Desktop_App_For_Professor
{
    partial class Form_spsh
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_change = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.button_import = new System.Windows.Forms.Button();
            this.dataGridViewStudents = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1151, 78);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_delete);
            this.panel2.Controls.Add(this.button_change);
            this.panel2.Controls.Add(this.button_add);
            this.panel2.Controls.Add(this.button_import);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 78);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(109, 572);
            this.panel2.TabIndex = 2;
            // 
            // button_delete
            // 
            this.button_delete.Location = new System.Drawing.Point(19, 219);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(78, 41);
            this.button_delete.TabIndex = 4;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_change
            // 
            this.button_change.Location = new System.Drawing.Point(19, 172);
            this.button_change.Name = "button_change";
            this.button_change.Size = new System.Drawing.Size(78, 41);
            this.button_change.TabIndex = 3;
            this.button_change.Text = "Change";
            this.button_change.UseVisualStyleBackColor = true;
            this.button_change.Click += new System.EventHandler(this.button_change_Click);
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(19, 125);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(78, 41);
            this.button_add.TabIndex = 2;
            this.button_add.Text = "Add";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_import
            // 
            this.button_import.Location = new System.Drawing.Point(19, 78);
            this.button_import.Name = "button_import";
            this.button_import.Size = new System.Drawing.Size(78, 41);
            this.button_import.TabIndex = 1;
            this.button_import.Text = "Import";
            this.button_import.UseVisualStyleBackColor = true;
            this.button_import.Click += new System.EventHandler(this.button_import_Click);
            // 
            // dataGridViewStudents
            // 
            this.dataGridViewStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStudents.Location = new System.Drawing.Point(163, 112);
            this.dataGridViewStudents.Name = "dataGridViewStudents";
            this.dataGridViewStudents.RowHeadersWidth = 51;
            this.dataGridViewStudents.RowTemplate.Height = 24;
            this.dataGridViewStudents.Size = new System.Drawing.Size(932, 506);
            this.dataGridViewStudents.TabIndex = 3;
            this.dataGridViewStudents.TabStop = false;
            // 
            // Form_spsh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 650);
            this.Controls.Add(this.dataGridViewStudents);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form_spsh";
            this.Text = "Form_spsh";
            this.Load += new System.EventHandler(this.Form_spsh_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStudents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_change;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_import;
        private System.Windows.Forms.DataGridView dataGridViewStudents;
    }
}