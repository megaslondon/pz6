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

namespace pz6.forms.employee
{
    public partial class InsEditEmployee : Form
    {
        private SQLiteConnection sc;
        private SQLiteCommand command;
        private SQLiteDataAdapter a;
        private DataSet tmp;
        private DataRow dr;
        private bool isEdit = false;

        public InsEditEmployee(string name, string connString)
        {
            InitializeComponent();
            this.changePasswordButton.Visible = isEdit;
            this.Text = name;
            sc = new SQLiteConnection();
            this.sc.ConnectionString = connString;
            tmp = new DataSet();

            tmp.Tables.Add(new DataTable("role"));
            a = new SQLiteDataAdapter("select * from role", sc);
            a.Fill(tmp.Tables["role"]);

            roleComboBox.DisplayMember = tmp.Tables["role"].Columns["name"].ToString();
            roleComboBox.ValueMember = tmp.Tables["role"].Columns["role_id"].ToString();
            roleComboBox.DataSource = tmp.Tables["role"];
        }
        public InsEditEmployee(string name, string connString, DataRow dr)
        {
            InitializeComponent();
            this.changePasswordButton.Visible = isEdit;
            this.Text = name;
            sc = new SQLiteConnection();
            this.sc.ConnectionString = connString;
            tmp = new DataSet();

            tmp.Tables.Add(new DataTable("role"));
            a = new SQLiteDataAdapter("select * from role", sc);
            a.Fill(tmp.Tables["role"]);

            roleComboBox.DisplayMember = tmp.Tables["role"].Columns["name"].ToString();
            roleComboBox.ValueMember = tmp.Tables["role"].Columns["role_id"].ToString();
            roleComboBox.DataSource = tmp.Tables["role"];

            firstNameTextBox.Text = dr["first_name"].ToString();
            lastNameTextBox.Text = dr["last_name"].ToString();
            loginTextBox.Text = dr["login1"].ToString();
            roleComboBox.SelectedValue = dr["role_id"];
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
