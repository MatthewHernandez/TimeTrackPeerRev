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
    public partial class Form_main : Form
    {
        public Form_main()
        {
            InitializeComponent();
        }

        private void Form_main_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void studentInfomationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void edittempToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gxk220025
            //when click edit student infomation button, then open edit form 
            Form_std_edit editStdInfo = new Form_std_edit();
            editStdInfo.Show(this);
        }

        private void mailTempToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gxk220025
            //when click add student infomation button, then open add form 
            Form_std_add addStdInfo = new Form_std_add();
            addStdInfo.Show(this);

        }

        private void button_read_Click(object sender, EventArgs e)
        {

        }
    }
}
