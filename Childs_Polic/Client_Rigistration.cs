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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Childs_Polic
{
    public partial class Client_Rigistration : Form
    {
        Database database = new Database();
        public Client_Rigistration()
        {
            InitializeComponent();
            textBox1.MaxLength = 20;
            textBox2.MaxLength = 20;
            textBox6.MaxLength = 12;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client_Login client_login=new Client_Login();
            client_login.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "")
            {
                var Login = textBox1.Text;
                var Pass = textBox2.Text;
                var Name = textBox4.Text;
                var Surname = textBox3.Text;
                var FatherName = textBox5.Text;
                var Phone = textBox6.Text;
                if ((Name + Surname + FatherName).Length > 150)
                {
                    MessageBox.Show("Извините, но ваше полное имя превышает максимально возможную длину, пожалуйста, попробуйте сократить его!", "Превышена длина!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    MySqlCommand cmd1 = new MySqlCommand("checkPhoneCorrect", database.connection);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("phone", Phone);
                    cmd1.Parameters.Add("@ireturnvalue", MySqlDbType.Int32);
                    cmd1.Parameters["@ireturnvalue"].Direction = ParameterDirection.ReturnValue;
                    cmd1.Connection.Open();
                    cmd1.ExecuteNonQuery();
                    int resOfFunc = Convert.ToInt32(cmd1.Parameters["@ireturnvalue"].Value);
                    cmd1.Connection.Close();
                    if (resOfFunc == 1)
                    {
                        if (checkInsert())
                        {
                            string query = $"INSERT INTO Client (Login,Pass,Full_Name,Phone_Number) VALUES('{Login}','{Pass}','{Name + " " + Surname + " " + FatherName}','{Phone}')";
                            database.OpenConnection();
                            MySqlCommand cmd = new MySqlCommand(query, database.connection);
                            cmd.ExecuteNonQuery();
                            database.CloseConnection();
                            Client_Login client_Login = new Client_Login();
                            client_Login.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Извините, но аккаунт с таким логином или номером телефона уже существует!", "Аккаунт занят!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Телефон указан в неверном формате!", "Неверный формат!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Для продолжения введите все данные!", "Неполные данные!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool checkInsert()
        {
            var phone1 = textBox6.Text;
            var login = textBox1.Text;
            MySqlCommand cmd1 = new MySqlCommand("InsertClient", database.connection);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("Login1", login);
            cmd1.Parameters.AddWithValue("phone1", phone1);;
            cmd1.Parameters.Add("@ireturnvalue", MySqlDbType.Int32);
            cmd1.Parameters["@ireturnvalue"].Direction = ParameterDirection.ReturnValue;
            cmd1.Connection.Open();
            cmd1.ExecuteNonQuery();
            int resOfFunc = Convert.ToInt32(cmd1.Parameters["@ireturnvalue"].Value);
            cmd1.Connection.Close();
            if (resOfFunc == 0)
                return true;
            else
                return false;
        }
        private void Client_Rigistration_Load(object sender, EventArgs e)
        {

        }
    }
}
