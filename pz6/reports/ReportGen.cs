using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pz6.reports
{
    public partial class ReportGen : Form
    {
        private SQLiteConnection sc = new SQLiteConnection();
        private SQLiteDataAdapter a;
        private DataSet tmp = new DataSet();
        private DataTable dt = new DataTable();
        private string query;


        public ReportGen(string choice, string connString)
        {
            InitializeComponent();
            sc = new SQLiteConnection();
            this.sc.ConnectionString = connString;
            tmp = new DataSet();

            if (choice == "warehouseLast")
            {
                query = "";

            }
            else if (choice == "warehouseAll")
            {
                query = "";
            }
            else if (choice == "warehouseRecent")
            {
                query = "";
            }

            dt.Clear();
            a = new SQLiteDataAdapter(query, sc);
            a.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
