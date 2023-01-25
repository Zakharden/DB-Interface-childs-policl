using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Childs_Polic
{
    public partial class Add_Analysis : Form
    {
        public int ID_Doctor;
        public int selected_Id_Card;
        Database database = new Database();
        public Add_Analysis()
        {
            InitializeComponent();
            textBox3.MaxLength= 100; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Doctor_Reception doctor_Reception=new Doctor_Reception();
            doctor_Reception.ID_Doctor = ID_Doctor;
            doctor_Reception.Show();
            this.Hide();
        }
        private bool checkInsert()
        {
            var time = comboBox1.SelectedItem.ToString() + ":00";
            var date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            var dateTime = date + " " + time;
            DateTime dateTime2 = DateTime.Parse(date + " " + time);
            MySqlCommand cmd1 = new MySqlCommand("InsertAnalysis", database.connection);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("dateT", dateTime2);
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
        private void Add_Analysis_Load(object sender, EventArgs e)
        {
            fillTimeBox();
        }
        private void fillTimeBox()
        {
            comboBox1.Items.Add("09:00");
            comboBox1.Items.Add("09:30");
            comboBox1.Items.Add("10:00");
            comboBox1.Items.Add("10:30");
            comboBox1.Items.Add("11:00");
            comboBox1.Items.Add("11:30");
            comboBox1.Items.Add("13:00");
            comboBox1.Items.Add("13:30");
            comboBox1.Items.Add("14:00");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && comboBox1.Text != "")
            {
                var parameter = textBox3.Text;
                var time = comboBox1.SelectedItem.ToString() + ":00";
                var date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                var dateTime = date + " " + time;
                DateTime dateTime2 = DateTime.Parse(date + " " + time);
                if (DateTime.Compare(DateTime.Now, dateTime2) <= 0)
                {
                    ///ПРОВЕРКА НА УНИКАЛЬНОСТЬ
                    if (checkInsert())
                    {
                        database.OpenConnection();
                        string query = $"INSERT INTO Analysis (Test_Date,Parameter,Status) VALUES" +
                            $"('{dateTime}','{parameter}','Not Ready')";
                        MySqlCommand cmd = new MySqlCommand(query, database.connection);
                        cmd.ExecuteReader();
                        database.CloseConnection();
                        database.OpenConnection();
                        string query1 = $"UPDATE Card Set Test_Date='{dateTime}' WHERE ID_Card={selected_Id_Card}";
                        MySqlCommand cmd1 = new MySqlCommand(query1, database.connection);
                        cmd1.ExecuteReader();
                        database.CloseConnection();
                        Doctor_Reception doctor_Reception = new Doctor_Reception();
                        doctor_Reception.ID_Doctor = ID_Doctor;
                        doctor_Reception.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Указанное вами окно для записи уже выбрано другим клиентом.", "К сожалению, выбранное время занято!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Запись на выбранное время невозможна! (Окно устарело).", "Неверная дата!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Для продолжения введите все данные", "Неполные данные!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
