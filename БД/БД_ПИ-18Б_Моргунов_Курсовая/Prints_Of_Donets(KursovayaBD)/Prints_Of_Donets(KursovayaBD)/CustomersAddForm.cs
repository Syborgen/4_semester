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
    public partial class CustomersAddForm : Form
    {
        public bool isUpdate;
        public int id;
        public int maxId;
        public CustomersAddForm()
        {
            InitializeComponent();
            dateTimePicker.MaxDate = DateTime.Today;
            dateTimePicker.CustomFormat = "yyyy-MM-dd";
            dateTimePicker.Format = DateTimePickerFormat.Custom;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (isUpdate)//если изменение записи
                {
                    AuthorizationForm.mainForm.conn.Open();
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand(@"update customers set first_name  ='" + first_nameTextBox.Text +
                    "' , second_name  = '" + second_nameTextBox.Text + "', third_name = '" + third_nameTextBox.Text +
                    "',city_id ='" + cityComboBox.Text.Split('.')[0] + "', address='"+addressTextBox.Text+"',birthday = '"+dateTimePicker.Text+
                    "',phone='"+phoneTextBox.Text +"' where id=" + id, AuthorizationForm.mainForm.conn);
                    AuthorizationForm.mainForm.cmd.ExecuteReader();
                    AuthorizationForm.mainForm.conn.Close();
                }
                else//добавление записи
                {
                    AuthorizationForm.mainForm.conn.Open();
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand("INSERT INTO customers( first_name, second_name, third_name, city_id, address, birthday, phone)	VALUES" +
                            "('" +
                            first_nameTextBox.Text + "', '" +//fname
                            second_nameTextBox.Text + "', '" +//sname
                            third_nameTextBox.Text + "'," +//tname
                            cityComboBox.Text.Split('.')[0] + ", '" +// city_id
                            addressTextBox.Text + "','" +//address
                            dateTimePicker.Text+ "','" +//birthday
                            phoneTextBox.Text + "')"//phone

                            , AuthorizationForm.mainForm.conn);

                    AuthorizationForm.mainForm.cmd.ExecuteScalar();
                    AuthorizationForm.mainForm.conn.Close();

                    

                }



            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Inserted fail 1. Error: " + ex.Message);
            }
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
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand(@"select first_name, second_name, third_name, city_id,(select city from cities where city_id=id),"+
                        " address, birthday, phone from customers where id="
                    + (id), AuthorizationForm.mainForm.conn);


                    DataTable dt3 = new DataTable();
                    dt3.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                    first_nameTextBox.Text = dt3.Rows[0].ItemArray[0].ToString();
                    second_nameTextBox.Text = dt3.Rows[0].ItemArray[1].ToString();
                    third_nameTextBox.Text = dt3.Rows[0].ItemArray[2].ToString();
                    cityComboBox.Text= dt3.Rows[0].ItemArray[3].ToString() + ". " + dt3.Rows[0].ItemArray[4].ToString();
                    addressTextBox.Text= dt3.Rows[0].ItemArray[5].ToString();
                    dateTimePicker.Text= dt3.Rows[0].ItemArray[6].ToString();
                    phoneTextBox.Text= dt3.Rows[0].ItemArray[7].ToString();
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

            //поиск количества записей в таблице работники
            AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select count(*) from customers", AuthorizationForm.mainForm.conn);
            label7.Text = "Таблица Заказчики (записей: " + AuthorizationForm.mainForm.cmd.ExecuteScalar() + ")";
            //конец поиск количества записей в таблице работники



            //заполнение выпадающего списка города
            AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select * from cities", AuthorizationForm.mainForm.conn);
            var reader = AuthorizationForm.mainForm.cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            foreach (DataRow dr in dt.Rows)
            {
               cityComboBox.Items.Add(dr["id"] + ". " + dr["city"]);
            }
            //конец заполнение выпадающего списка города





            AuthorizationForm.mainForm.conn.Close();

        }

        private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
