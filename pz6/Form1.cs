using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace pz6
{
    public partial class Form1 : Form
    {
        private SQLiteConnection sqliteConn;

        public Form1()
        {
            InitializeComponent();
            sqliteConn = new SQLiteConnection("Data Source=sklad_t_o.db;Version=3;");
            sqliteConn.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            sqliteConn.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void окноToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
