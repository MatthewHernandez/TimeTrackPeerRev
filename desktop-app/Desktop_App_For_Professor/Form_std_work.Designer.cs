using System.Drawing;
using System.Windows.Forms;

namespace Desktop_App_For_Professor
{
    partial class Form_std_work
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
            this.label_stdinfo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView_stdwork = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_stdwork)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_stdinfo
            // 
            this.label_stdinfo.AutoSize = true;
            this.label_stdinfo.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_stdinfo.Location = new System.Drawing.Point(38, 65);
            this.label_stdinfo.Name = "label_stdinfo";
            this.label_stdinfo.Size = new System.Drawing.Size(45, 23);
            this.label_stdinfo.TabIndex = 0;
            this.label_stdinfo.Text = "test";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 34);
            this.label2.TabIndex = 1;
            this.label2.Text = "Work Hours";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // dataGridView_stdwork
            // 
            this.dataGridView_stdwork.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_stdwork.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_stdwork.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_stdwork.Location = new System.Drawing.Point(125, 130);
            this.dataGridView_stdwork.Name = "dataGridView_stdwork";
            this.dataGridView_stdwork.ReadOnly = true;
            this.dataGridView_stdwork.RowHeadersWidth = 51;
            this.dataGridView_stdwork.RowTemplate.Height = 24;
            this.dataGridView_stdwork.Size = new System.Drawing.Size(857, 464);
            this.dataGridView_stdwork.TabIndex = 2;
            
            //this.dataGridView_stdwork.Columns[0].DefaultCellStyle.Font = new Font("Verdana", 16, FontStyle.Bold);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.label_stdinfo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1005, 100);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(106, 506);
            this.panel2.TabIndex = 4;
            // 
            // Form_std_work
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 606);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView_stdwork);
            this.Name = "Form_std_work";
            this.Text = "Form_std_work";
            this.Load += new System.EventHandler(this.Form_std_work_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_stdwork)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_stdinfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView_stdwork;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}