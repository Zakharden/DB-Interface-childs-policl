using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Childs_Polic
{
    public partial class Doctor_Login : Form
    {
        Database database = new Database();
        public Doctor_Login()
        {
            InitializeComponent();
            textBox1.MaxLength = 20;
            textBox2.MaxLength = 20;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main_Menu main_Menu= new Main_Menu();
            main_Menu.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                var Login = textBox1.Text;
                var Pass = textBox2.Text;

                string query = $"SELECT Login,Pass,ID_Doctor FROM Doctor WHERE Doctor.Login='{Login}' AND Doctor.Pass='{Pass}'";
                List<string>[] list = new List<string>[3];
                list[0] = new List<string>();
                list[1] = new List<string>();
                list[2] = new List<string>();
                if (database.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, database.connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        list[0].Add(dataReader["Login"] + "");
                        list[1].Add(dataReader["Pass"] + "");
                        list[2].Add(dataReader["ID_Doctor"] + "");
                    }
                    if (list[2].Count == 1)
                    {
                        Doctor_Cabinet doctor_Cabinet = new Doctor_Cabinet();
                        MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        doctor_Cabinet.ID_Doctor = (int)dataReader["ID_Doctor"];
                        doctor_Cabinet.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Такого аккаунта не существует!", "Аккаунта не существует!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    dataReader.Close();
                    database.CloseConnection();
                }
                else
                {
                    MessageBox.Show("Не удалось подключиться к базе данных :(", "Проблема с подключением!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Для продолжения введите все данные!", "Неполные данные!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Doctor_Login_Load(object sender, EventArgs e)
        {

        }
    }
}