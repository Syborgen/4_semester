using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prints_Of_Donets_KursovayaBD_
{
    public partial class PrintsAddForm : Form
    {
        public bool isUpdate;
        public int id;
        public int maxId;
        public PrintsAddForm()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (isUpdate)//если изменение записи
                {
                    AuthorizationForm.mainForm.conn.Open();
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand(@"update prints set print ='" + printTextBox.Text +
                    "' , type_id = " + typeComboBox.Text.Split('.')[0] + ", district_id = " + districtComboBox.Text.Split('.')[0] +
                    ",address ='" + addressTextBox.Text + "',phone = '" + phoneTextBox.Text + "',year = " + yearTextBox.Text + " where id=" + (id), AuthorizationForm.mainForm.conn);
                    AuthorizationForm.mainForm.cmd.ExecuteReader();
                    AuthorizationForm.mainForm.conn.Close();
                }
                else//добавление записи
                {
                    AuthorizationForm.mainForm.conn.Open();
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand("INSERT INTO prints( print, type_id, district_id, address, phone, year) values" +
                            "('" +
                            printTextBox.Text + "', " +//print
                            int.Parse(typeComboBox.SelectedItem.ToString().Split('.')[0]) + ", " +//type id
                            int.Parse(districtComboBox.SelectedItem.ToString().Split('.')[0]) + ", '" +//district id
                            addressTextBox.Text + "', " +//address
                            "'" + phoneTextBox.Text + "', " +//phone
                            int.Parse(yearTextBox.Text) + ")"//year
                            , AuthorizationForm.mainForm.conn);

                    AuthorizationForm.mainForm.cmd.ExecuteScalar();
                    AuthorizationForm.mainForm.conn.Close();

                }



            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Ошибка ввода. Введите данные и повторите ввод. Error: " + ex.Message);
            }
            //select
            AuthorizationForm.mainForm.selectSql = @"select id,print as типография,(select type from types where id=type_id) as тип_собственности," +
                "(select district from districts where id=district_id) as район,address as адрес, phone as телефон, year as год_основания from prints";
            AuthorizationForm.mainForm.Select();
            //select

            //count
            AuthorizationForm.mainForm.sql = @"select count(*) from prints";
            AuthorizationForm.mainForm.ChangeLabel("Таблица Типографии");
            //count
        }

        private void backButton_Click(object sender, EventArgs e)
        {
           
            this.Visible = false;
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                
                addButton.Text = "Изменить";
                this.Text = "Изменение элемента";
                
                try
                {
                    AuthorizationForm.mainForm.conn.Open();

                    //заполнение формы информацией о строке
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand(@"select id,print as типография,(select type from types where id=type_id) as тип_собственности," +
                    "(select district from districts where id=district_id) as район,address as адрес, phone as телефон, year as год_основания, type_id,district_id from prints where id="
                    +(id), AuthorizationForm.mainForm.conn);
                    DataTable dt3 = new DataTable();
                    dt3.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                    printTextBox.Text = dt3.Rows[0].ItemArray[1].ToString();
                    typeComboBox.Text = dt3.Rows[0].ItemArray[7].ToString() +". "+ dt3.Rows[0].ItemArray[2].ToString();
                    districtComboBox.Text= dt3.Rows[0].ItemArray[8].ToString() + ". " + dt3.Rows[0].ItemArray[3].ToString();
                    addressTextBox.Text= dt3.Rows[0].ItemArray[4].ToString();
                    phoneTextBox.Text= dt3.Rows[0].ItemArray[5].ToString();
                    yearTextBox.Text= dt3.Rows[0].ItemArray[6].ToString();
                    //конец заполнение формы информацией о строке



                    AuthorizationForm.mainForm.conn.Close();
                }
                catch (Exception ex)
                {
                    AuthorizationForm.mainForm.conn.Close();
                    MessageBox.Show("Inserted fail 2. Error: " + ex.Message);
                }


            }
            else
            {
                addButton.Text = "Добавить";
                this.Text = "Добавление элемента";
            }
           
           
            
            AuthorizationForm.mainForm.conn.Open();
            
            //поиск количества записей в таблице типографии
            AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select count(*) from prints", AuthorizationForm.mainForm.conn);
            label7.Text = "Таблица Типографии (записей: " + AuthorizationForm.mainForm.cmd.ExecuteScalar()+")";
            //конец поиск количества записей в таблице типографии



            //заполнение выпадающего списка район
            AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select * from districts", AuthorizationForm.mainForm.conn);
            var reader = AuthorizationForm.mainForm.cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            foreach(DataRow dr in dt.Rows)
            {
                districtComboBox.Items.Add(dr["id"]+". "+dr["district"]);
            }
            //конец заполнение выпадающего списка район

            //заполнение выпадающего списка тип собственности
            AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select * from types", AuthorizationForm.mainForm.conn);
            reader = AuthorizationForm.mainForm.cmd.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(reader);
            foreach (DataRow dr in dt2.Rows)
            {
                typeComboBox.Items.Add(dr["id"] + ". " + dr["type"]);
            }
            //конец заполнение выпадающего списка тип собственности



            AuthorizationForm.mainForm.conn.Close();

        }
    }
}
