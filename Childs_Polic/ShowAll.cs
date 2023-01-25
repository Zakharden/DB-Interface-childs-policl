using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Childs_Polic
{
    public partial class ShowAll : Form
    {
        public int ID_Doctor;
        private int id_selected_card = -1;
        private string choosedDate="";
        Database database = new Database();
        public ShowAll()
        {
            InitializeComponent();
        }

        private void ShowAll_Load(object sender, EventArgs e)
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
            dataGridView1.Columns.Add("Client.Full_Name", "Имя клиента");//8
            dataGridView1.Columns.Add("Phone_Number", "Тел.Номер");//9
            dataGridView1.Columns.Add("Test_Date", "Дата анализа");//10
            dataGridView1.Columns.Add("Treatment", "Лечение");//11
            dataGridView1.Columns.Add("ID_Card", "ID Приёма");//12
            dataGridView1.Columns.Add("Doctor.Full_Name", "Принимающий врач");//13
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
            var s13 = "";
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
            if (record.IsDBNull(13))
                s13 = "-";
            else
                s13 = record.GetString(13);
            dgw.Rows.Add(s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12,s13);
        }
        private void RefreshDataGrid1(DataGridView dgw)
        {
            dgw.Rows.Clear();
            //var date = "2022-12-01";
            string query =
                $"SELECT ID_CHILD,Child_Name,Age,TypesName,TypeName,Complaints,Date(Dat),Tim,Client.Full_Name,Phone_Number, Test_Date, Treatment, ID_Card,Doctor.Full_Name FROM Card " +
                $"INNER JOIN Child Using(ID_Child) " +
                $"INNER JOIN Client Using(ID_Client) " +
                $"LEFT JOIN type Using(ID_type) " +
                $"INNER JOIN Gender Using(ID_Gender) " +
                $"INNER JOIN Doctor Using(ID_Doctor) " +
                $"LEFT JOIN Treatment Using(ID_Treatment); ";
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string[] strs = textBox1.Text.Split(';');
            string query = $"SELECT Distinct ID_Child,Child_Name,Age,TypesName,TypeName,Complaints,Date(Dat),Tim,Client.Full_Name,Phone_Number, Test_Date, Treatment, ID_Card,Doctor.Full_Name FROM Card " +
                $"INNER JOIN Child Using(ID_Child) " +
                $"INNER JOIN Client Using(ID_Client) " +
                $"LEFT JOIN type Using(ID_type) " +
                $"INNER JOIN Gender Using(ID_Gender) " +
                $"INNER JOIN Doctor Using(ID_Doctor) " +
                $"LEFT JOIN Treatment Using(ID_Treatment) WHERE ( ";
            for(int i=0;i<strs.Length;i++)
            {
                if(i!=strs.Length-1)
                {
                    query += $"ID_CHILD LIKE '%{strs[i]}%' OR CHILD_Name LIKE '%{strs[i]}%' OR " +
                        $"Age LIKE '%{strs[i]}%' OR TypesName LIKE '%{strs[i]}%' OR " +
                        $"TypeName LIKE '%{strs[i]}%' OR Complaints LIKE '%{strs[i]}%' OR " +
                        $"Date(Dat) LIKE '%{strs[i]}%' OR Tim LIKE '%{strs[i]}%' OR " +
                        $"Client.Full_Name LIKE '%{strs[i]}%' OR Phone_Number LIKE '%{strs[i]}%' OR " +
                        $"Test_Date LIKE '%{strs[i]}%' OR Treatment LIKE '%{strs[i]}%' OR " +
                        $"ID_Card LIKE '%{strs[i]}%' OR Doctor.Full_Name LIKE '%{strs[i]}%') AND ( ";
                }
                else
                {
                    query += $"ID_CHILD LIKE '%{strs[i]}%' OR CHILD_Name LIKE '%{strs[i]}%' OR " +
                        $"Age LIKE '%{strs[i]}%' OR TypesName LIKE '%{strs[i]}%' OR " +
                        $"TypeName LIKE '%{strs[i]}%' OR Complaints LIKE '%{strs[i]}%' OR " +
                        $"Date(Dat) LIKE '%{strs[i]}%' OR Tim LIKE '%{strs[i]}%' OR " +
                        $"Client.Full_Name LIKE '%{strs[i]}%' OR Phone_Number LIKE '%{strs[i]}%' OR " +
                        $"Test_Date LIKE '%{strs[i]}%' OR Treatment LIKE '%{strs[i]}%' OR " +
                        $"ID_Card LIKE '%{strs[i]}%' OR Doctor.Full_Name LIKE '%{strs[i]}%');";
                }
            }
            database.OpenConnection();
            MySqlCommand command = new MySqlCommand(query, database.connection);

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow1(dataGridView1, reader);
            }
            reader.Close();
            database.CloseConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (id_selected_card != -1 && choosedDate != "")
            {
                Doctor_Reception doctor_Reception = new Doctor_Reception();
                doctor_Reception.ID_Doctor = ID_Doctor;
                doctor_Reception.id_selected_card = id_selected_card;
                doctor_Reception.choosenDate = DateTime.Parse(choosedDate);
                doctor_Reception.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Для начала выберите ячеку!", "Данные не выбраны!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[selectedRow].Cells[0].Value != null && dataGridView1.Rows[selectedRow].Cells[0].Value != DBNull.Value && !String.IsNullOrWhiteSpace(dataGridView1.Rows[selectedRow].Cells[0].Value.ToString()))
                {
                    DataGridViewRow row = dataGridView1.Rows[selectedRow];
                    choosedDate = row.Cells[6].Value.ToString();
                    id_selected_card = int.Parse(row.Cells[12].Value.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Doctor_Cabinet doctor_Cabinet= new Doctor_Cabinet();
            doctor_Cabinet.ID_Doctor=ID_Doctor;
            doctor_Cabinet.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id_selected_card != -1 && choosedDate != "")
            {
                ///ПРОВЕРКА НА УНИКАЛЬНОСТЬ
                database.OpenConnection();
                string query = $"DELETE FROM CARD WHERE ID_CARD={id_selected_card}";
                MySqlCommand cmd = new MySqlCommand(query, database.connection);
                cmd.ExecuteReader();
                database.CloseConnection();
                MessageBox.Show("Указанная вами запись удалена, пожалуйста, обновите страницу!", "Запись удалена!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Для продолжения введите все данные!", "Неполные данные!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            RefreshDataGrid1(dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
