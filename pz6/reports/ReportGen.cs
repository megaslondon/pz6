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
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace pz6.reports
{
    public partial class ReportGen : Form
    {
        private DataTable dt = new DataTable();
        private string query;
        private string connString;


        public ReportGen(string choice, string connString, string text)
        {
            InitializeComponent();
            this.Name = choice;
            this.Text = text;
            this.connString = connString;

            switch (choice)
            {
                case "warehouseLast":
                    {
                        query = "SELECT \r\n    o.city AS \"Город доставки\",\r\n    p.name AS \"Название товара\",\r\n    p.product_type AS \"Категория товара\",\r\n    SUM(oi.quantity) AS \"Количество, шт\",\r\n    SUM(oi.quantity * p.sale_price) AS \"Сумма продаж, руб\"\r\nFROM ORDERS o\r\nJOIN ORDER_ITEM oi ON o.order_id = oi.order_id\r\nJOIN PRODUCT p ON oi.product_id = p.product_id\r\nGROUP BY o.city, p.name, p.product_type\r\nORDER BY o.city, \"Сумма продаж, руб\" DESC;";
                        break;
                    }
                case "saleGeo":
                    {
                        query = "WITH delivery_agg AS (\r\n    SELECT \r\n        product_id, \r\n        SUM(quantity) AS total_got\r\n    FROM delivery_item\r\n    GROUP BY product_id\r\n),\r\norder_agg AS (\r\n    SELECT \r\n        product_id, \r\n        SUM(quantity) AS total_prod\r\n    FROM order_item\r\n    GROUP BY product_id\r\n)\r\nSELECT \r\n    p.name AS \"Название товара\", \r\n    p.article AS \"Артикул товара\", \r\n    COALESCE(d.total_got, 0) AS \"Поступило\", \r\n    COALESCE(o.total_prod, 0) AS \"Продано\",\r\n    (COALESCE(d.total_got, 0) - COALESCE(o.total_prod, 0)) AS \"Остаток\",\r\n    p.sale_price AS \"Цена продажи\",\r\n    p.sale_price * (COALESCE(d.total_got, 0) - COALESCE(o.total_prod, 0)) AS \"Стоимость остатка\"\r\nFROM product p\r\nLEFT JOIN delivery_agg d ON d.product_id = p.product_id\r\nLEFT JOIN order_agg o ON o.product_id = p.product_id;";
                        break;
                    }
                case "employeeEfficiency":
                    {
                        query = "WITH sold_p AS (\r\n    SELECT \r\n        o.employee_id AS oed, \r\n        oi.product_id AS prod, \r\n        p.sale_price AS salep, \r\n        SUM(oi.quantity) AS sold \r\n    FROM order_item AS oi \r\n    JOIN product AS p ON p.product_id = oi.product_id \r\n    JOIN orders AS o ON o.order_id = oi.order_id\r\n    GROUP BY o.employee_id, oi.product_id, p.sale_price\r\n)\r\nSELECT \r\n    e.last_name || ' ' || SUBSTR(e.first_name, 1, 1) || '.' || SUBSTR(e.patronymic, 1, 1) || '.' AS \"ФИО\", \r\n    r.name AS \"Роль\", \r\n    SUM(sp.sold) AS \"Всего продано (шт)\", \r\n    SUM(sp.sold * sp.salep) AS \"Выручка (руб)\" \r\nFROM employee AS e \r\nJOIN role AS r ON r.role_id = e.role_id \r\nJOIN sold_p AS sp ON sp.oed = e.employee_id\r\nGROUP BY e.employee_id, e.last_name, e.first_name, e.patronymic, r.name\r\nORDER BY \"Выручка (руб)\" DESC;";
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Сформировать отчет для данной опции невозможно сформировать отчет!");
                        break;
                    }
            }

            Load += async (s, e) => await LoadReportAsync();
        }

        private async Task LoadReportAsync()
        {
            button1.Enabled = false;
            try
            {
                var result = await Task.Run(() =>
                {
                    var table = new DataTable();
                    using (var connection = new SQLiteConnection(connString))
                    using (var adapter = new SQLiteDataAdapter(query, connection))
                    {
                        adapter.Fill(table);
                    }
                    return table;
                });
                dt = result;
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки отчёта: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (this.Name)
            {
                case "warehouseLast":
                    {
                        ExportToWord(dt, this.Name);
                        break;
                    }
                case "saleGeo":
                    {
                        ExportToExcel(dt, this.Name);
                        break;
                    }
                case "employeeEfficiency":
                    {
                        ExportToWord(dt, this.Name, true);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Для данного вида отчетности не возможно создать документ!");
                        break;
                    }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExportToExcel(DataTable table, string name)
        {
            if (table.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для формирования отчета!");
                return;
            }
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Файлы Excel (*.xlsx)|*.xlsx";
                sfd.FileName = "Отчет_" + DateTime.Now.ToString("ddMMyyyy") + "_" + name;

                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string savePath = sfd.FileName;
                var snapshot = table.Copy();
                button1.Enabled = false;

                Excel.Application application = null;
                Excel.Workbook workbook = null;
                Excel.Worksheet worksheet = null;


                Task.Run(() =>
                {
                    try
                    {
                        application = new Excel.Application { Visible = false };
                        workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
                        worksheet = (Excel.Worksheet)workbook.Sheets[1];
                        for (int c = 0; c < snapshot.Columns.Count; c++)
                            worksheet.Cells[1, c + 1] = snapshot.Columns[c].ColumnName;
                        for (int r = 0; r < snapshot.Rows.Count; r++)
                        {
                            for (int c = 0; c < snapshot.Columns.Count; c++)
                                worksheet.Cells[r + 2, c + 1] = snapshot.Rows[r][c] == DBNull.Value ? string.Empty : snapshot.Rows[r][c].ToString();
                        }
                        workbook.SaveAs(savePath);
                    }
                    finally { application.Quit(); }
                }).ContinueWith(t =>
                {
                    button1.Enabled = true;
                    MessageBox.Show(t.IsFaulted ? "Произошла ошибка при экспорте: " + t.Exception.GetBaseException().Message : "Файл успешно сохранен!", t.IsFaulted ? "Ошибка" : "Успех", MessageBoxButtons.OK, t.IsFaulted ? MessageBoxIcon.Error : MessageBoxIcon.Information);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void ExportToWord(DataTable table, string name, bool isPDF = false)
        {
            if (table.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для формирования отчета!");
                return;
            }
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = isPDF?"Документ PDF (*.pdf)|*.pdf" : "Документ Word (*.docx)|*.docx";
                sfd.FileName = "Отчет_" + DateTime.Now.ToString("ddMMyyyy") + "_" + name;
                object saveChanges = null;

                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                string savePath = sfd.FileName;
                var snapshot = table.Copy();
                button1.Enabled = false;

                Word.Application wordApp = null;
                Word.Document wordDoc = null;

                Task.Run(() =>
                {
                    try
                    {
                        wordApp = new Word.Application { Visible = false };
                        wordDoc = wordApp.Documents.Add();
                        Word.Table wordTable = wordDoc.Tables.Add(wordDoc.Range(0, 0), snapshot.Rows.Count + 1, snapshot.Columns.Count);
                        wordTable.Borders.Enable = 1;
                        for (int c = 0; c < snapshot.Columns.Count; c++)
                            wordTable.Cell(1, c + 1).Range.Text = snapshot.Columns[c].ColumnName;
                        for (int r = 0; r < snapshot.Rows.Count; r++)
                        {
                            for (int c = 0; c < snapshot.Columns.Count; c++)
                                wordTable.Cell(r + 2, c + 1).Range.Text = snapshot.Rows[r][c] == DBNull.Value ? string.Empty : snapshot.Rows[r][c].ToString();
                        }
                        if (isPDF) { object fileFormat = Word.WdSaveFormat.wdFormatPDF; wordDoc.SaveAs2(savePath, ref fileFormat); }
                        else wordDoc.SaveAs2(savePath);
                    }
                    finally
                    {
                        if (wordDoc != null) { wordDoc.Close(); System.Runtime.InteropServices.Marshal.ReleaseComObject(wordDoc); }
                        wordApp.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                    }
                }).ContinueWith(t =>
                {
                    button1.Enabled = true;
                    MessageBox.Show(t.IsFaulted ? "Произошла ошибка при экспорте: " + t.Exception.GetBaseException().Message : "Файл успешно сохранен!", t.IsFaulted ? "Ошибка" : "Успех", MessageBoxButtons.OK, t.IsFaulted ? MessageBoxIcon.Error : MessageBoxIcon.Information);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
