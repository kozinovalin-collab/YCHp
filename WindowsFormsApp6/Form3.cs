using System;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegistrationApp
{
    public partial class Form3 : Form
    {
        // Подключение к базе данных Access
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Database.mdb;");

        public Form3()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Валидация телефона
            if (!IsValidPhone(txtPhone.Text))
            {
                MessageBox.Show("Введите корректный номер телефона (формат: +7XXXXXXXXXX или 8XXXXXXXXXX)");
                return;
            }

            // Проверка совпадения паролей
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            // Проверка длины пароля
            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать минимум 6 символов!");
                return;
            }

            try
            {
                connection.Open();
                
                // Проверка существования пользователя с таким телефоном
                string checkQuery = "SELECT COUNT(*) FROM [User] WHERE Phone = ?";
                OleDbCommand checkCmd = new OleDbCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@Phone", FormatPhone(txtPhone.Text));
                int userCount = (int)checkCmd.ExecuteScalar();

                if (userCount > 0)
                {
                    MessageBox.Show("Пользователь с таким номером телефона уже зарегистрирован!");
                    return;
                }

                // Добавление нового пользователя
                string insertQuery = "INSERT INTO [User] (Phone, Password, RegistrationDate) VALUES (?, ?, ?)";
                OleDbCommand insertCmd = new OleDbCommand(insertQuery, connection);
                insertCmd.Parameters.AddWithValue("@Phone", FormatPhone(txtPhone.Text));
                insertCmd.Parameters.AddWithValue("@Password", HashPassword(txtPassword.Text)); // Хеширование пароля
                insertCmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);

                int rowsAffected = insertCmd.ExecuteNonQuery();
                
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Регистрация успешно завершена!");
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Ошибка при регистрации!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        // Валидация номера телефона
        private bool IsValidPhone(string phone)
        {
            // Убираем все нецифровые символы кроме +
            string cleanPhone = Regex.Replace(phone, @"[^\d+]", "");
            
            // Проверяем российские форматы номеров
            return Regex.IsMatch(cleanPhone, @"^(\+7|8)\d{10}$");
        }

        // Форматирование телефона в единый формат
        private string FormatPhone(string phone)
        {
            string cleanPhone = Regex.Replace(phone, @"[^\d]", "");
            
            if (cleanPhone.StartsWith("8") && cleanPhone.Length == 11)
            {
                return "+7" + cleanPhone.Substring(1);
            }
            else if (cleanPhone.StartsWith("7") && cleanPhone.Length == 11)
            {
                return "+" + cleanPhone;
            }
            
            return cleanPhone;
        }
        // Простое хеширование пароля (в реальном приложении используйте более безопасные методы)
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private void ClearForm()
        {
            txtPhone.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            // Автоматическое форматирование номера телефона при вводе
            if (txtPhone.Text.Length > 0)
            {
                string text = txtPhone.Text;
                if (text.Length == 1)
                {
                    if (text == "8") txtPhone.Text = "8 (";
                    else if (text == "7") txtPhone.Text = "+7 (";
                }
                else if (text.Length == 7)
                {
                    txtPhone.Text = text + ") ";
                }
                else if (text.Length == 12)
                {
                    txtPhone.Text = text + "-";
                }
                else if (text.Length == 15)
                {
                    txtPhone.Text = text + "-";
                }
            }
        }

        private TextBox txtPhone;
        private TextBox txtConfirmPassword;
        private TextBox txtPassword;
        private Button button1;

        private void InitializeComponent()
        {
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(158, 55);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(100, 22);
            this.txtPhone.TabIndex = 0;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(158, 111);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(100, 22);
            this.txtConfirmPassword.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(158, 83);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 22);
            this.txtPassword.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(164, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form3
            // 
            this.ClientSize = new System.Drawing.Size(672, 358);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.txtPhone);
            this.Name = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}