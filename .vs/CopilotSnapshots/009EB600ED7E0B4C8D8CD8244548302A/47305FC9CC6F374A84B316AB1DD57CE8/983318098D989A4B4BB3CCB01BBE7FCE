using pz6.forms.employee;
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

namespace pz6.forms
{
    public partial class Table : Form
    {
        private SQLiteConnection sc = new SQLiteConnection();
        private SQLiteDataAdapter a;
        private SQLiteCommand command = new SQLiteCommand();
        private DataTable dt = new DataTable();

        private string nameTable;
        private string query;

        private int CurrentIndex = 0;

        public Table(string nameF, string connString)
        {
            InitializeComponent();
            this.Text = nameF;
            this.Name = nameF;
            this.sc.ConnectionString = connString;
            switch (this.Name)
            {
                case "Сотрудники":
                    {
                        query = "SELECT e.last_name AS 'Фамилия', e.first_name AS 'Имя', e.patronymic AS 'Отчество', e.login AS 'Логин', r.name AS 'Роль' FROM employee e JOIN role r ON e.role_id = r.role_id;";
                        this.nameTable = "Employee";
                        break;
                    }

                case "Клиенты":
                    {
                        query = "select name, phone, email from client;";
                        this.nameTable = "Client";
                        break;
                    }

                case "Товары":
                    {
                        query = "SELECT p.name AS 'Название', p.article AS 'Артикул', p.product_type AS 'Тип товара', s.name AS 'Поставщик', p.sale_price AS 'Цена продажи' FROM product p LEFT JOIN supplier s ON p.supplier_id = s.supplier_id;";
                        this.nameTable = "Product";
                        break;
                    }

                case "Склады":
                    {
                        query = "SELECT\r\n    name AS 'Название',\r\n    city AS 'Город',\r\n    street AS 'Улица',\r\n    house_number AS 'Номер дома'\r\nFROM warehouse;\r\n";
                        this.nameTable = "Warehouse";
                        break;
                    }

                case "Заказы":
                    {
                        query = "select * from orders;";
                        this.nameTable = "Order";
                        break;
                    }

                case "Поставщики":
                    {
                        query = "SELECT     name AS 'Название',     phone AS 'Телефон',     email AS 'Электронная почта' FROM supplier; ";
                        this.nameTable = "Supplier";
                        break;
                    }

                case "Поставки":
                    {
                        query = "select * from delivery";
                        this.nameTable = "Delivery";
                        break;
                    }

                default:
                    {
                        MessageBox.Show("Ошибка! Выбранная таблица не может быть отражена!");
                        break;
                    }
            }
            a = new SQLiteDataAdapter(query, sc);
            a.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void InsEdit(int param)
        {
            int rowCount = dataGridView1.RowCount;
            if (rowCount != 0)
            {
                CurrentIndex = dataGridView1.CurrentRow.Index;
            }
            else if (param == 1)
            {
                    MessageBox.Show("Нет данных для редактирования!");
                    return;
            }
            switch (this.Name)
            {
                case "Сотрудники":
                    {
                        InsEditEmployee ed;
                        if (param == 0) ed = new InsEditEmployee("Добавление сотрудника", this.sc.ConnectionString);
                        else ed = new InsEditEmployee("Изменение данных о сотруднике", this.sc.ConnectionString, dt.Rows[dataGridView1.CurrentRow.Index]);
                        ed.Tag = param;
                        ed.ShowDialog();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Ошибка! Для данной таблицы запрошенное действие не может быть выполнено!");
                        break;
                    }
            }
            dt.Clear();
            a = new SQLiteDataAdapter(query, sc);
            a.Fill(dt);
            dataGridView1.DataSource = dt;

            if(rowCount != 0)
            {
                dataGridView1.CurrentCell = rowCount == dataGridView1.RowCount ?
                    dataGridView1.Rows[CurrentIndex].Cells[dataGridView1.ColumnCount - 1] :
                    dataGridView1.Rows[rowCount].Cells[dataGridView1.ColumnCount - 1];

            }
        }
    }
}
