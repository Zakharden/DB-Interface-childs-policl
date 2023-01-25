using System;
using System.Windows.Forms;

namespace Childs_Polic
{
    public partial class Main_Menu : Form
    {
        public Main_Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client_Login client_Login = new Client_Login();
            client_Login.Show();
            this.Hide();

        }

        private void Main_Menu_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Doctor_Login doctor_Login = new Doctor_Login();
            doctor_Login.Show();
            this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
