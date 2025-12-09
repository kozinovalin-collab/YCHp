using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form2 : Form
    {
        private DataTable dataTable = new DataTable();
        private OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\Database.mdb;");
        private OleDbDataAdapter adapter;
        private DataView dataView;

        public Form2()
        {
            InitializeComponent();
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            InitializeDatabase();  //Инициализация при загрузке формы
        }

        private void InitializeDatabase()
        {
            try
            {
                connection.Open();
                adapter = new OleDbDataAdapter("SELECT * FROM bd2", connection);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter); // Создаем объект OleDbCommandBuilder
                adapter.Fill(dataTable);

                dataView = new DataView(dataTable);
                dataGridView1.DataSource = dataView;
            }
            catch (OleDbException ex)
            {
                MessageBox.Show($"Ошибка при работе с базой данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //обработка нажатий на ячейки таблицы
        }



        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form0 form0 = new Form0();
            form0.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите текст для поиска");
                return;
            }

            string searchText = textBox1.Text.Trim();

            try
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    dataView.RowFilter = "";
                    MessageBox.Show("Показаны все записи");
                }
                else
                {
                    dataView.RowFilter = $"Название LIKE '%{searchText.Replace("'", "''")}%'";

                    if (dataView.Count == 0)
                    {
                        MessageBox.Show("Записи с похожим именем не найдены");
                    }
                    else
                    {
                        MessageBox.Show($"Найдено записей: {dataView.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске: {ex.Message}");
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            dataView.RowFilter = "";
            textBox1.Clear();
            MessageBox.Show("Фильтр сброшен, показаны все записи");
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
