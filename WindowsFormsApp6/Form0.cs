using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Form0 : Form
    {
        public Form0()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form0_Load(object sender, EventArgs e)
        {

        }
        private void Exit(object sender, MouseEventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }
        private void EExit(object sender, MouseEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void txtComment_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        { 
           string comment = txtComment.Text.Trim();

            if (string.IsNullOrEmpty(comment))
            {
                MessageBox.Show("Пожалуйста, введите комментарий.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO CommentsTable (Comment) VALUES (@Comment)";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Comment", comment);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Комментарий успешно сохранен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtComment.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении комментария: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
