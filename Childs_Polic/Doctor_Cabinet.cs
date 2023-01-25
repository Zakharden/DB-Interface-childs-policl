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
    public partial class Doctor_Cabinet : Form
    {
        Database database = new Database();
        public int ID_Doctor;
        public Doctor_Cabinet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main_Menu main_Menu=new Main_Menu();
            main_Menu.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Doctor_Reception doctor_Reception= new Doctor_Reception();
            doctor_Reception.ID_Doctor = ID_Doctor;
            doctor_Reception.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowAll showAll= new ShowAll();
            showAll.ID_Doctor= ID_Doctor;
            showAll.Show();
            this.Hide();
        }

        private void Doctor_Cabinet_Load(object sender, EventArgs e)
        {
            setHelloText();
        }
        private void setHelloText()
        {
            database.OpenConnection();
            string query = $"SELECT Full_Name FROM Doctor WHERE ID_Doctor='{ID_Doctor}'";
            MySqlCommand command = new MySqlCommand(query, database.connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            label1.Text = $"Здравствуйте, {reader["Full_Name"]}";
            reader.Close();
            database.CloseConnection();
        }
    }
}
