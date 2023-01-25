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
    public partial class Change_Analysis : Form
    {
        public int ID_Doctor;
       // private int id_selected_card;
        public string Test_Date;
        Database database = new Database();
        public Change_Analysis()
        {
            InitializeComponent();
            textBox1.MaxLength =150;
            textBox2.MaxLength =40;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Doctor_Reception doctor_Reception=new Doctor_Reception();
            doctor_Reception.ID_Doctor= ID_Doctor;
            doctor_Reception.Show();
            this.Hide();
        }

        private void Change_Analysis_Load(object sender, EventArgs e)
        {
            Test_Date = Test_Date.Substring(6, 4) + "-" + Test_Date.Substring(3, 2) + "-" + Test_Date.Substring(0, 2) + " " + Test_Date.Substring(11);
            MessageBox.Show($"{Test_Date}", "Аккаунта не существует!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            string query = $"SELECT Status, Parameter,Results FROM Analysis WHERE Test_Date='{Test_Date}'";
            database.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(query, database.connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            label8.Text=Test_Date.Substring(11);
            label7.Text = Test_Date.Substring(0,10);
            textBox2.Text = dataReader["Status"] + "";
            label9.Text = dataReader["Parameter"] + "";
            if (dataReader.IsDBNull(2))
                textBox1.Text ="-";
            else
                textBox1.Text = dataReader["Results"] + "";
            dataReader.Close();
            database.CloseConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                var status = textBox2.Text;
                var result = textBox1.Text;
                ///ПРОВЕРКА НА УНИКАЛЬНОСТЬ
                database.OpenConnection();
                string query = $"UPDATE Analysis SET Status='{status}' WHERE Test_Date='{Test_Date}'";
                MySqlCommand cmd = new MySqlCommand(query, database.connection);
                cmd.ExecuteReader();
                database.CloseConnection();
                database.OpenConnection();
                string query1 = $"UPDATE Analysis SET Results='{result}' WHERE Test_Date='{Test_Date}'";
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
                MessageBox.Show("Для продолжения введите все данные!", "Неполные данные!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
