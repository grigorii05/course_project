using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB
{
    public partial class Library : Form
    {
        SqlConnection sqlConnection;

        public Library()
        {
            InitializeComponent();
        }

        private void BOOKBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bOOKBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dBLibDataSet);

        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBLibDataSet.BOOK". При необходимости она может быть перемещена или удалена.
            //this.bOOKTableAdapter.Fill(this.dBLibDataSet.BOOK);
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Desktop\Курсач\DB\DB\DBLib.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command1 = new SqlCommand("SELECT * FROM [BOOK]", sqlConnection);
            SqlCommand command2 = new SqlCommand("SELECT * FROM [Students]", sqlConnection);
            SqlCommand command3 = new SqlCommand("SELECT * FROM [Выдача_книг]", sqlConnection);

            // Вывод таблицы книг в библиотеке
            try
            {
                sqlReader = await command1.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(Convert.ToString(sqlReader["Номер"]) + "   " + sqlReader["Название_книги"]) + ",  Всего шутук: " + Convert.ToString(sqlReader["Количество"]) +
                        ", Автор книги: " + Convert.ToString(sqlReader["Автор"]) + ", год издания: " + Convert.ToString(sqlReader["Год_издания"]));

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }

            // Вывод таблицы студентов
            try
            {
                sqlReader = await command2.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox2.Items.Add(Convert.ToString(Convert.ToString(sqlReader["Номер"]) + "   " + sqlReader["ФИО"]));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }

            // Вывод таблицы, которая показывает наличие книг у студентов
            try
            {
                sqlReader = await command3.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox3.Items.Add(Convert.ToString("Номер выдвннной книги:  " + Convert.ToString(sqlReader["Номер_выданной_книги"]) + "   " + sqlReader["Название_книги"]) + ", выдана студенту: " + sqlReader["ФИО_Студента"] + "; Выдано: " + Convert.ToString(sqlReader["Дата_выдачи"]) + ", Дата Возврата: " + Convert.ToString(sqlReader["Дата_возврата"]));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void БиблиотекаToolStripMenuItem_Click(object sender, EventArgs e) // Обновить изменения
        {

        }

        private void TabPage2_Click(object sender, EventArgs e) 
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Library_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [BOOKS] (Название_книги, Номер книги, Количество, Автор, Год_издания)VALUES(@Название_книги, @Номер, @Количество, @Автор, @Год_издания",
                sqlConnection);

            command.Parameters.AddWithValue("Название_книги", textBox1.Text);
            command.Parameters.AddWithValue("Номер", textBox2.Text);
            command.Parameters.AddWithValue("Количество", textBox3.Text);
            command.Parameters.AddWithValue("Автор", textBox4.Text);
            command.Parameters.AddWithValue("Год_издания", textBox5.Text);

            await command.ExecuteNonQueryAsync();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [Students] (Номер, ФИО)VALUES(@Номер, @ФИО", sqlConnection);

            command.Parameters.AddWithValue("Номер", textBox16.Text);
            command.Parameters.AddWithValue("Количество", textBox15.Text);
  
            await command.ExecuteNonQueryAsync();
        }


        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {   
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
