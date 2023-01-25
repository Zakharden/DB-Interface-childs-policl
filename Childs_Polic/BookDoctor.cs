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
    public partial class BookDoctor : Form
    {
        public int ID_Client;
        private int selected_ID_Doctor=1;
        Database database = new Database();
        String pn="-";
        String vt = "-";
        String sr = "-";
        String cht = "-";
        String pt = "-";
        String sb = "-";
        public BookDoctor()
        {
            InitializeComponent();
            textBox3.MaxLength = 7;
            textBox4.MaxLength = 65535;
        }
        private void BookDoctor_Load(object sender, EventArgs e)
        {
            fillTimeBox();
            CreateColumns();
            RefreshDataGrid1(dataGridView1);
        }
        private void fillTimeBox()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("09:00-09:30");
            comboBox1.Items.Add("09:30-10:00");
            comboBox1.Items.Add("10:00-10:30");
            comboBox1.Items.Add("10:30-11:00");
            comboBox1.Items.Add("11:00-11:30");
            comboBox1.Items.Add("11:30-12:00");
            comboBox1.Items.Add("11:30-12:00");
            comboBox1.Items.Add("12:00-12:30");
            comboBox1.Items.Add("12:30-13:00");
            comboBox1.Items.Add("13:00-13:30");
            comboBox1.Items.Add("13:30-14:00");
            comboBox1.Items.Add("14:00-14:30");
            comboBox1.Items.Add("14:30-15:00");
            comboBox1.Items.Add("15:00-15:30");
            comboBox1.Items.Add("15:30-16:00");
            comboBox1.Items.Add("16:00-16:30");
            comboBox1.Items.Add("16:30-17:00");
            comboBox1.Items.Add("17:00-17:30");
            comboBox1.Items.Add("17:30-18:00");

        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("ID_Doctor", "ID Врача");
            dataGridView1.Columns.Add("Full_Name", "Имя Врача");
            dataGridView1.Columns.Add("Specialisation", "Специализация");
            dataGridView1.Columns.Add("Понедельник", "Понедельник");
            dataGridView1.Columns.Add("Вторник", "Вторник");
            dataGridView1.Columns.Add("Среда", "Среда");
            dataGridView1.Columns.Add("Четверг", "Четверг");
            dataGridView1.Columns.Add("Пятница", "Пятница");
            dataGridView1.Columns.Add("Суббота", "Суббота");
        }
        private void ReadSingleRow1(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetString(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6), record.GetString(7), record.GetString(8));
        }
        private void RefreshDataGrid1(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string query = $"SELECT ID_Doctor,Full_Name,Specialisation,Понедельник,Вторник,Среда,Четверг,Пятница,Суббота FROM Doctor INNER JOIN Shedules USING(ID_Shedule)";
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
        private bool checkChild(string ID_Child)
        {
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
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Вы указали неверный ID ребёнка!", "Неверный ID!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Вы указали неверный ID ребёнка!", "Неверный ID!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Возраст указан в неверном формате!", "Неверный формат!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (label1.Text != "*Выберите врача*" && textBox3.Text != "" && textBox4.Text != "" && comboBox1.Text != "")
            {
                var time = comboBox1.SelectedItem.ToString().Substring(0, 5) + ":00";
                var date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                var complaints = textBox4.Text;
                var ID_Child = textBox3.Text;
                DateTime dateTime = DateTime.Parse(date + " " + time);
                if (DateTime.Compare(DateTime.Now, dateTime) <= 0)
                {
                    if (checkChild(ID_Child) == true)
                    {
                        DateTime date1 = dateTimePicker1.Value;
                        int day = (int)date1.DayOfWeek;
                        //if (checkTime(day))
                        if(checkTime(day))
                        {
                            ///----------------------------------------------------------------------------------------
                            ///Нужна проверка на то, есть ли уже такая запись
                            ///----------------------------------------------------------------------------------------
                            ///if
                            if (checkInsert())
                            {
                                if (checkForCompl())
                                {
                                    database.OpenConnection();
                                    string query = $"INSERT INTO Card (ID_Child,ID_Doctor,Complaints,Dat,Tim) VALUES({int.Parse(ID_Child)},{selected_ID_Doctor},'{complaints}','{date}','{time}');";
                                    MySqlCommand cmd = new MySqlCommand(query, database.connection);
                                    cmd.ExecuteReader();
                                    database.CloseConnection();
                                    Client_Cabinet client_Cabinet = new Client_Cabinet();
                                    client_Cabinet.ID_Client = ID_Client;
                                    client_Cabinet.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    DialogResult dialogResult = MessageBox.Show("Вы уже обращались с такой просьбой, вы можете опробовать прописанное вам ранее лечение. Хотите ли вы записаться к врачу?", "Вы уже обращались с такой просьбой", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        database.OpenConnection();
                                        string query = $"INSERT INTO Card (ID_Child,ID_Doctor,Complaints,Dat,Tim) VALUES({int.Parse(ID_Child)},{selected_ID_Doctor},'{complaints}','{date}','{time}');";
                                        MySqlCommand cmd = new MySqlCommand(query, database.connection);
                                        cmd.ExecuteReader();
                                        database.CloseConnection();
                                        Client_Cabinet client_Cabinet = new Client_Cabinet();
                                        client_Cabinet.ID_Client = ID_Client;
                                        client_Cabinet.Show();
                                        this.Hide();
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Извините, но выбранная запись уже занята, пожалуйста, выберите другую дату или время!", "Занятное время!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Доктор не принимает в выбранное время!", "Неверное время!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Указанная вами дата уже прошла!", "Неверная дата!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Для продолжения введите все данные!", "Неполные данные!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                    selected_ID_Doctor = int.Parse(row.Cells[0].Value.ToString());
                    label9.Text = row.Cells[1].Value.ToString();
                    //label10.Text = row.Cells[2].Value.ToString();
                    pn= row.Cells[3].Value.ToString();
                    vt = row.Cells[4].Value.ToString();
                    sr = row.Cells[5].Value.ToString();
                    cht = row.Cells[6].Value.ToString();
                    pt = row.Cells[7].Value.ToString();
                    sb = row.Cells[8].Value.ToString();
                }
            }
        }
        private bool checkInsert()
        {
            var time = comboBox1.SelectedItem.ToString().Substring(0, 5) + ":00";
            var date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            MySqlCommand cmd1 = new MySqlCommand("InsertDoctor", database.connection);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("ID", selected_ID_Doctor);
            cmd1.Parameters.AddWithValue("tim2", time);
            cmd1.Parameters.AddWithValue("dat2", date);
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
        private bool checkForCompl()
        {
            var id = int.Parse(textBox3.Text);
            var compl = textBox4.Text;
            MySqlCommand cmd1 = new MySqlCommand("checkForComplaints", database.connection);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("CHILD_ID", id);
            cmd1.Parameters.AddWithValue("Compl", compl);
            cmd1.Parameters.Add("@ireturnvalue", MySqlDbType.Text);
            cmd1.Parameters["@ireturnvalue"].Direction = ParameterDirection.ReturnValue;
            cmd1.Connection.Open();
            cmd1.ExecuteNonQuery();
            string resOfFunc = ""; 
            resOfFunc = Convert.ToString(cmd1.Parameters["@ireturnvalue"].Value);
            cmd1.Connection.Close();
            if (resOfFunc == "")
                return true;
            else
                return false;
        }
        private bool checkTime(int day)
        {
            int start=-1;
            int startMin = -1;
            int end=-1;
            int endMin = -1;
            switch(day)
            {
                case 0:
                    return false;
                case 1:
                    if (pn != "-")
                    {
                        if (pn[2] == ':')
                        {
                            start = int.Parse(pn.Substring(0, 2));
                            startMin = int.Parse(pn.Substring(3, 2));
                        }
                        else
                        {
                            start = int.Parse(pn.Substring(0, 1));
                            startMin = int.Parse(pn.Substring(2, 2));
                        }
                        end = int.Parse(pn.Substring(pn.Length - 5, 2));
                        endMin = int.Parse(pn.Substring(pn.Length - 2, 2));
                        break;
                    }
                    else
                        return false;
                case 2:
                    if (vt != "-")
                    {
                        if (vt[2] == ':')
                        {
                            start = int.Parse(vt.Substring(0, 2));
                            startMin = int.Parse(vt.Substring(3, 2));
                        }
                        else
                        {
                            start = int.Parse(vt.Substring(0, 1));
                            startMin = int.Parse(vt.Substring(2, 2));
                        }
                        end = int.Parse(vt.Substring(vt.Length - 5, 2));
                        endMin = int.Parse(vt.Substring(vt.Length - 2, 2));
                        break;
                    }
                    else
                        return false;
                case 3:
                    if (sr != "-")
                    {
                        if (sr[2] == ':')
                        {
                            start = int.Parse(sr.Substring(0, 2));
                            startMin = int.Parse(sr.Substring(3, 2));
                        }
                        else
                        {
                            start = int.Parse(sr.Substring(0, 1));
                            startMin = int.Parse(sr.Substring(2, 2));
                        }
                        end = int.Parse(sr.Substring(sr.Length - 5, 2));
                        endMin = int.Parse(sr.Substring(sr.Length - 2, 2));
                        break;
                    }
                    else
                        return false;
                case 4:
                    if (cht != "-")
                    {
                        if (cht[2] == ':')
                        {
                            start = int.Parse(cht.Substring(0, 2));
                            startMin = int.Parse(cht.Substring(3, 2));
                        }
                        else
                        {
                            start = int.Parse(cht.Substring(0, 1));
                            startMin = int.Parse(cht.Substring(2, 2));
                        }
                        end = int.Parse(cht.Substring(cht.Length - 5, 2));
                        endMin = int.Parse(cht.Substring(cht.Length - 2, 2));
                        break;
                    }
                    else
                        return false;
                case 5:
                    if (pt != "-")
                    {
                        if (pt[2] == ':')
                        {
                            start = int.Parse(pt.Substring(0, 2));
                            startMin = int.Parse(pt.Substring(3, 2));
                        }
                        else
                        {
                            start = int.Parse(pt.Substring(0, 1));
                            startMin = int.Parse(pt.Substring(2, 2));
                        }
                        end = int.Parse(pt.Substring(pt.Length - 5, 2));
                        endMin = int.Parse(pt.Substring(pt.Length - 2, 2));
                        break;
                    }
                    else
                        return false;
                case 6:
                    if (sb != "-")
                    {
                        if (sb[2] == ':')
                        {
                            start = int.Parse(sb.Substring(0, 2));
                            startMin = int.Parse(sb.Substring(3, 2));
                        }
                        else
                        {
                            start = int.Parse(sb.Substring(0, 1));
                            startMin = int.Parse(sb.Substring(2, 2));
                        }
                        end = int.Parse(sb.Substring(sb.Length - 5, 2));
                        endMin = int.Parse(sb.Substring(sb.Length - 2, 2));
                        break;
                    }
                    else
                        return false;
            }
            int startCombo;
            int startComboMin;
            if (comboBox1.SelectedItem.ToString()[2] == ':')
            {
                startCombo = int.Parse(comboBox1.SelectedItem.ToString().Substring(0, 2));
                startComboMin = int.Parse(comboBox1.SelectedItem.ToString().Substring(3, 2));
            }
            else
            {

                startCombo = int.Parse(comboBox1.SelectedItem.ToString().Substring(0, 1));
                startComboMin = int.Parse(comboBox1.SelectedItem.ToString().Substring(2, 2));
            }
            int endCombo;
            int endComboMin;
            endCombo = int.Parse(comboBox1.SelectedItem.ToString().Substring(comboBox1.SelectedItem.ToString().Length - 5, 2));
            endComboMin = int.Parse(comboBox1.SelectedItem.ToString().Substring(comboBox1.SelectedItem.ToString().Length - 2, 2));
            if (start < startCombo && end > endCombo)
            {
                return true;
            }
            else if (start > startCombo || end < endCombo)
            {
                return false;
            }
            else if (start == startCombo && startMin <= startComboMin || end == endCombo && endMin >= endComboMin)
            {
                return true;
            }
            else return false;
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client_Cabinet client_Cabinet = new Client_Cabinet();
            client_Cabinet.ID_Client= ID_Client;
            client_Cabinet.Show();
            this.Hide();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
