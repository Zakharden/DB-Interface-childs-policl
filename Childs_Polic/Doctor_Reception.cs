using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Childs_Polic
{
    public partial class Doctor_Reception : Form
    {
        public int ID_Doctor;
        public int id_selected_card = 0;
        public DateTime choosenDate = DateTime.Today;
        Database database = new Database();
        public Doctor_Reception()
        {
            InitializeComponent();
            textBox1.MaxLength= 65535;
        }

        private void label3_Click(object sender, EventArgs e)
        {
       
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Doctor_Reception_Load(object sender, EventArgs e)
        {
            CreateColumns();
            dateTimePicker1.Value = choosenDate;
            var date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            RefreshDataGrid1(dataGridView1,date);
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
            var s3 ="";
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
                s3=record.GetString(3);
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
                s6 = record.GetString(6).Substring(0,10);
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
            dgw.Rows.Add(s0,s1,s2,s3,s4,s5,s6,s7,s8,s9,s10,s11,s12);
        }
        private void RefreshDataGrid1(DataGridView dgw, String date)
        {
            dgw.Rows.Clear();
            //var date = "2022-12-01";
            string query =
                $"SELECT ID_CHILD,Child_Name,Age,TypesName,TypeName,Complaints,Date(Dat),Tim,Full_Name,Phone_Number, Test_Date, Treatment, ID_Card FROM Card " +
                $"INNER JOIN Child Using(ID_Child) " +
                $"INNER JOIN Client Using(ID_Client) " +
                $"LEFT JOIN Type Using(ID_Type) " +
                $"INNER JOIN Gender Using(ID_Gender) " +
                $"LEFT JOIN Treatment Using(ID_Treatment) " +
                $"WHERE ID_Doctor={ID_Doctor} " +
                $"AND Date(Dat) ='{date}';";
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[selectedRow].Cells[0].Value != null && dataGridView1.Rows[selectedRow].Cells[0].Value != DBNull.Value && !String.IsNullOrWhiteSpace(dataGridView1.Rows[selectedRow].Cells[0].Value.ToString()))
                {
                    DataGridViewRow row = dataGridView1.Rows[selectedRow];
                    //Проверку на не ноль
                    label6.Text = row.Cells[1].Value.ToString();
                    label7.Text = row.Cells[8].Value.ToString();//Имя
                    label8.Text = row.Cells[2].Value.ToString();//Возраст
                    label9.Text = row.Cells[9].Value.ToString();//Телефон
                    label10.Text = row.Cells[3].Value.ToString();//Пол
                    label11.Text = row.Cells[4].Value.ToString();//тип
                    label14.Text = row.Cells[6].Value.ToString();//Дата
                    label15.Text = row.Cells[7].Value.ToString();//Время
                    label19.Text = row.Cells[5].Value.ToString();//Жалобы
                    label22.Text = row.Cells[11].Value.ToString();//Лечение
                    if (row.Cells[10].Value.ToString() == "-")
                        linkLabel1.Text = "Записать на анализ";
                    else
                        linkLabel1.Text = row.Cells[10].Value.ToString();
                    id_selected_card = int.Parse(row.Cells[12].Value.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Doctor_Cabinet doctor_Cabinet = new Doctor_Cabinet();
            doctor_Cabinet.ID_Doctor = ID_Doctor;
            doctor_Cabinet.Show();
            this.Hide();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            RefreshDataGrid1(dataGridView1, date);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label6.Text != "*выберите приём*"&&textBox1.Text!="")
            {
                var new_treat = textBox1.Text;
                label22.Text = new_treat;//Лечение
                MySqlCommand cmd1 = new MySqlCommand("checkTreatment", database.connection);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("Treat", (new_treat));
                cmd1.Parameters.Add("@ireturnvalue", MySqlDbType.Int32);
                cmd1.Parameters["@ireturnvalue"].Direction = ParameterDirection.ReturnValue;
                cmd1.Connection.Open();
                cmd1.ExecuteNonQuery();
                int resOfFunc = Convert.ToInt32(cmd1.Parameters["@ireturnvalue"].Value);
                cmd1.Connection.Close();

                string query = $"UPDATE Card SET ID_Treatment={resOfFunc} WHERE ID_Card={id_selected_card}";
                database.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, database.connection);
                cmd.ExecuteNonQuery();
                database.CloseConnection();
                MessageBox.Show("Указанное вами лечение было добавлено!", "Лечение добавлено!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                var date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                RefreshDataGrid1(dataGridView1, date);
            }
            else
            {
                MessageBox.Show("Для продолжения введите все данные!", "Неполные данные!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (label6.Text != "*выберите приём*")
            {
                if (linkLabel1.Text == "Записать на анализ")
                {
                    Add_Analysis add_Analysis = new Add_Analysis();
                    add_Analysis.ID_Doctor = ID_Doctor;
                    add_Analysis.selected_Id_Card = id_selected_card;
                    add_Analysis.Show();
                    this.Hide();
                }
                else
                {
                    Change_Analysis change_Analysis = new Change_Analysis();
                    change_Analysis.ID_Doctor = ID_Doctor;
                    change_Analysis.Test_Date = linkLabel1.Text;
                    change_Analysis.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Для продолжения введите все данные!", "Неполные данные!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }

}
