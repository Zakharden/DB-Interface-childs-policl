using MySql.Data.MySqlClient;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Childs_Polic
{
    public partial class Client_Cabinet : Form
    {
        Database database = new Database();
        public int ID_Client;
        public Client_Cabinet()
        {
            InitializeComponent();
            textBox2.MaxLength = 7;
        }
        private void Client_Cabinet_Load(object sender, EventArgs e)
        {
            setHelloText();
            CreateColumns();
            RefreshDataGrid1(dataGridView1);
        }
        private void setHelloText()
        {
            database.OpenConnection();
            string query = $"SELECT Full_Name FROM Client WHERE ID_Client='{ID_Client}'";
            MySqlCommand command = new MySqlCommand(query, database.connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            label1.Text = $"Здравствуйте, {reader["Full_Name"]}";
            reader.Close();
            database.CloseConnection(); 
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID_Child", "ID_Ребёнка");
            dataGridView1.Columns.Add("Child_Name", "Имя ребёнка");
            dataGridView1.Columns.Add("Age", "Возраст");
            dataGridView1.Columns.Add("TypesName", "Пол");
            dataGridView1.Columns.Add("TypeName", "Тип");
        }
        private void ReadSingleRow1(DataGridView dgw, IDataRecord record)
        {
            if (record.IsDBNull(3)&&record.IsDBNull(4))
            {
                dgw.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2),'-', '-');
            }
            else if(record.IsDBNull(3))
            {
                dgw.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), '-', record.GetString(4));
            }
            else if (record.IsDBNull(4))
            {
                dgw.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), record.GetString(3), '-');
            }
            else   dgw.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4));
        }


        private void RefreshDataGrid1(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string query = $"SELECT ID_Child,Child_Name,Age,Gender.TypesName,type.TypeName FROM child LEFT JOIN gender using(ID_gender) LEFT JOIN type USING(ID_type) WHERE child.ID_Client='{ID_Client}'";
            database.OpenConnection();
            MySqlCommand command = new MySqlCommand(query, database.connection);

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow1(dgw, reader);
            }
            reader.Close();
            database.CloseConnection();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Main_Menu main_Menu= new Main_Menu();
            main_Menu.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Child_Registration pet_Registration = new Child_Registration();
            pet_Registration.ID_Client = ID_Client;
            pet_Registration.Show();
            this.Hide();
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                var ID_Child = textBox2.Text;
                MySqlCommand cmd1 = new MySqlCommand("checkNumberCorrect", database.connection);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("chislo", ID_Child);
                cmd1.Parameters.Add("@ireturnvalue", MySqlDbType.Int32);
                cmd1.Parameters["@ireturnvalue"].Direction = ParameterDirection.ReturnValue;
                cmd1.Connection.Open();
                cmd1.ExecuteNonQuery();
                int resOfFunc = Convert.ToInt32(cmd1.Parameters["@ireturnvalue"].Value);
                cmd1.Connection.Close();
                if (resOfFunc == 1)
                {
                    if (int.Parse(ID_Child) < 8388607)
                    {
                        MySqlCommand cmd2 = new MySqlCommand("checkChildOwner", database.connection);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("Child_ID", int.Parse(ID_Child));
                        cmd2.Parameters.AddWithValue("Client_ID", ID_Client);
                        cmd2.Parameters.Add("@ireturnvalue", MySqlDbType.Int32);
                        cmd2.Parameters["@ireturnvalue"].Direction = ParameterDirection.ReturnValue;
                        cmd2.Connection.Open();
                        cmd2.ExecuteNonQuery();
                        int resOfFunc2 = Convert.ToInt32(cmd2.Parameters["@ireturnvalue"].Value);
                        cmd2.Connection.Close();
                        if (resOfFunc2 == 1)
                        {
                            database.OpenConnection();
                            string query = $"SELECT * FROM Card WHERE ID_CHILD={ID_Child}";
                            //string query = $"INSERT INTO child (Child_Name,Age,ID_Type,ID_Gender,ID_Client) VALUES('{Child_Name}',{Age},{comboBox1.SelectedIndex + 1},{comboBox2.SelectedIndex + 1},{ID_Client})";
                            MySqlCommand cmd = new MySqlCommand(query, database.connection);
                            MySqlDataReader dataReader = cmd.ExecuteReader();

                            if (dataReader.Read())
                            {
                                database.CloseConnection();
                                CardForm cardForm = new CardForm();
                                cardForm.ID_Client = ID_Client;
                                cardForm.ID_Child = int.Parse(ID_Child);
                                cardForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Для указанного ребёнка еще не существет карта!", "Не существует карты!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Вы указали неверный ID ребёнка!", "Неверный ID!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы указали неверный ID ребёнка!", "Неверный ID!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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

        private void button2_Click(object sender, EventArgs e)
        {
            BookDoctor bookDoctor=new BookDoctor();
            bookDoctor.ID_Client = ID_Client;
            bookDoctor.Show();
            this.Hide();
          
        }
    }
}
