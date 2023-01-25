using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Childs_Polic
{
    public partial class Child_Registration : Form
    {
        Database database = new Database();
        public int ID_Client;
        public Child_Registration()
        {
            InitializeComponent();
            textBox1.MaxLength = 30;
            textBox4.MaxLength = 3;
        }
        private void Child_Registration_Load(object sender, EventArgs e)
        {
            fillType();
            fillGender();
        }
        private void fillGender()
        {
            database.OpenConnection();
            string query = $"SELECT TypesName FROM Gender";
            MySqlCommand command = new MySqlCommand(query, database.connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader.GetString(0));
            }
            database.CloseConnection();
        }
        private void fillType()
        {
            database.OpenConnection();
            string query = $"SELECT TypeName FROM type";
            MySqlCommand command = new MySqlCommand(query, database.connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0));
            }
            database.CloseConnection();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox4.Text != "" && comboBox2.Text != "")
            {
                var Child_Name = textBox1.Text;
                var Age = textBox4.Text;
                MySqlCommand cmd1 = new MySqlCommand("checkNumberCorrect", database.connection);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("chislo", Age);
                cmd1.Parameters.Add("@ireturnvalue", MySqlDbType.Int32);
                cmd1.Parameters["@ireturnvalue"].Direction = ParameterDirection.ReturnValue;
                cmd1.Connection.Open();
                cmd1.ExecuteNonQuery();
                int resOfFunc = Convert.ToInt32(cmd1.Parameters["@ireturnvalue"].Value);
                cmd1.Connection.Close();
                if (resOfFunc == 1)
                {
                    if (int.Parse(Age) >= 255)
                    {
                        MessageBox.Show("Извините, но возраст вашего ребёнка превышает максимально возможный в нашей системе, пожалуйтса, введите другой возраст!", "Максимальное значение!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string query;
                        if (comboBox1.Text!="")
                            query = $"INSERT INTO child (Child_Name,Age,ID_Type,ID_Gender,ID_Client) VALUES('{Child_Name}',{Age},{comboBox1.SelectedIndex + 6},{comboBox2.SelectedIndex + 4},{ID_Client})";
                        else
                            query = $"INSERT INTO CHILD (Child_Name,Age,ID_Gender,ID_Client) VALUES('{Child_Name}',{Age},{comboBox2.SelectedIndex + 4},{ID_Client})";
                       // MessageBox.Show($"{query}", "Неверный формат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        database.OpenConnection();
                        MySqlCommand cmd = new MySqlCommand(query, database.connection);
                        cmd.ExecuteNonQuery();
                        database.CloseConnection();
                        Client_Cabinet client_Cabinet = new Client_Cabinet();
                        client_Cabinet.ID_Client = ID_Client;
                        client_Cabinet.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Возраст указан в неверном формате!", "Неверный формат!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Для продолжения введите все данные!", "Неполные данные!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client_Cabinet client_Cabinet = new Client_Cabinet();
            client_Cabinet.ID_Client = ID_Client;
            client_Cabinet.Show();
            this.Hide();
        }
    }
}
