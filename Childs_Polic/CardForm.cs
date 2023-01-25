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
    public partial class CardForm : Form
    {
        public int ID_Client;
        public int ID_Child;
        Database database = new Database();
        public CardForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void CardForm_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid1(dataGridView1);
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID_CHILD", "ID Ребёнка");//0
            dataGridView1.Columns.Add("Child_Name", "Имя ребёнка");//1
            dataGridView1.Columns.Add("Age", "Возраст ребёнка");//2
            dataGridView1.Columns.Add("TypesName", "Пол");//3
            dataGridView1.Columns.Add("TypeName", "Тип");//4
            dataGridView1.Columns.Add("Complaints", "Жалобы");//5
            dataGridView1.Columns.Add("Date(Dat)", "Дата приёма");//6
            dataGridView1.Columns.Add("Tim", "Время приёма");//7
            dataGridView1.Columns.Add("Full_Name", "Имя клиента");//8
            dataGridView1.Columns.Add("Phone_Number", "Тел.Номер");//9
            dataGridView1.Columns.Add("Test_Date", "Дата анализа");//10
            dataGridView1.Columns.Add("Treatment", "Лечение");//11
            dataGridView1.Columns.Add("ID_Card", "ID Приёма");//12
        }
        private void ReadSingleRow1(DataGridView dgw, IDataRecord record)
        {
            var s0 = record.GetString(0);
            var s1 = record.GetString(1);
            var s2 = record.GetString(2);
            var s3 = "";
            var s4 = "";
            var s5 = "";
            var s6 = "";
            var s7 = "";
            var s8 = "";
            var s9 = "";
            var s10 = "";
            var s11 = "";
            var s12 = "";
            if (record.IsDBNull(3))
                s3 = "-";
            else
                s3 = record.GetString(3);
            if (record.IsDBNull(4))
                s4 = "-";
            else
                s4 = record.GetString(4);
            if (record.IsDBNull(5))
                s5 = "-";
            else
                s5 = record.GetString(5);
            if (record.IsDBNull(6))
                s6 = "-";
            else
                s6 = record.GetString(6).Substring(0, 10);
            if (record.IsDBNull(7))
                s7 = "-";
            else
                s7 = record.GetString(7);
            if (record.IsDBNull(8))
                s8 = "-";
            else
                s8 = record.GetString(8);
            if (record.IsDBNull(9))
                s9 = "-";
            else
                s9 = record.GetString(9);
            if (record.IsDBNull(10))
                s10 = "-";
            else
                s10 = record.GetString(10);
            if (record.IsDBNull(11))
                s11 = "-";
            else
                s11 = record.GetString(11);
            if (record.IsDBNull(12))
                s12 = "-";
            else
                s12 = record.GetString(12);
            dgw.Rows.Add(s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12);
        }
        private void RefreshDataGrid1(DataGridView dgw)
        {
            dgw.Rows.Clear();
            //var date = "2022-12-01";
            string query =
                $"SELECT ID_CHILD,Child_Name,Age,TypesName,TypeName,Complaints,Date(Dat),Tim,Full_Name,Phone_Number, Test_Date, Treatment, ID_Card FROM Card " +
                $"INNER JOIN Child Using(ID_child) " +
                $"INNER JOIN Client Using(ID_Client) " +
                $"LEFT JOIN Type Using(ID_Type) " +
                $"INNER JOIN Gender Using(ID_Gender) " +
                $"LEFT JOIN Treatment Using(ID_Treatment) " +
                $"WHERE Card.ID_Child={ID_Child};";
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
            Client_Cabinet client_Cabinet=new Client_Cabinet();
            client_Cabinet.ID_Client = ID_Client;
            client_Cabinet.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
