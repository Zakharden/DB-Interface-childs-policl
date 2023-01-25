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
    public partial class Client_Login : Form
    {
        Database database = new Database();
        public Client_Login()
        {
            InitializeComponent();
            textBox1.MaxLength = 20;
            textBox2.MaxLength = 20;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                var LoginUser = textBox1.Text;
                var PassUser = textBox2.Text;

                string query = $"SELECT Login,Pass,ID_Client FROM Client WHERE Client.Login='{LoginUser}' AND Client.Pass='{PassUser}'";
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
                        list[2].Add(dataReader["ID_Client"] + "");
                    }
                    if (list[0].Count == 1)
                    {
                        Client_Cabinet client_Cabinet = new Client_Cabinet();
                        MessageBox.Show("Добро пожаловать! Вход успешен.", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        client_Cabinet.ID_Client = (int)(dataReader["ID_Client"]);
                        client_Cabinet.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Аккаунта не существует! Проверьте введенные данные или зарегистрируйтесь!", "Аккаунта не существует!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    dataReader.Close();
                    database.CloseConnection();
                }
                else
                {
                    MessageBox.Show("Не удалось подключиться к базе данных!", "Проблема с подключением!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Введите все данные, чтобы продолжить!", "Не все поля заполнены!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Client_Rigistration client_Registration=new Client_Rigistration();
            client_Registration.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main_Menu main_Menu=new Main_Menu();
            main_Menu.Show();
            this.Hide();
        }

        private void Client_Login_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
