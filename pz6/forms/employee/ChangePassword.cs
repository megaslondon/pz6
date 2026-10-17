using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace pz6.forms.employee
{
    public partial class ChangePassword : Form
    {
        private string connString;
        private int employeeId;

        public ChangePassword(string connString, int employeeId)
        {
            InitializeComponent();
            this.connString = connString;
            this.employeeId = employeeId;
            
            try { this.cancelButton.Click += (s, e) => this.Close(); } catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var newPass = this.textBox1.Text;
            var confirm = this.textBox2.Text;
            if (newPass != confirm)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                var hashStr = HashPassword(newPass);
                using (var sc = new SQLiteConnection(connString))
                using (var cmd = new SQLiteCommand("UPDATE employee SET password_hash = @h WHERE employee_id = @id;", sc))
                {
                    cmd.Parameters.AddWithValue("@h", hashStr);
                    cmd.Parameters.AddWithValue("@id", employeeId);
                    sc.Open(); cmd.ExecuteNonQuery(); sc.Close();
                }
                MessageBox.Show("Пароль обновлён.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении пароля: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string HashPassword(string password)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password ?? string.Empty);
                var hash = sha.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
