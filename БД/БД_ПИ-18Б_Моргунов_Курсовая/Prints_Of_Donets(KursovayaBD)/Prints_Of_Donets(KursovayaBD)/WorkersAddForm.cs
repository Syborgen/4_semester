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
    public partial class WorkersAddForm : Form
    {
        public bool isUpdate;
        public int id;
        public int maxId;
        public WorkersAddForm()
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
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand(@"update workers set first_name  ='" + first_nameTextBox.Text +
                    "' , second_name  = '" + second_nameTextBox.Text + "', third_name = '" + third_nameTextBox.Text +
                    "',print_id ='" + printComboBox.Text.Split('.')[0] + "' where id=" + id, AuthorizationForm.mainForm.conn);
                    AuthorizationForm.mainForm.cmd.ExecuteReader();
                    AuthorizationForm.mainForm.conn.Close();
                }
                else//добавление записи
                {
                    AuthorizationForm.mainForm.conn.Open();
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand("INSERT INTO workers( print_id,first_name, second_name , third_name) values" +
                            "(" +
                            printComboBox.Text.Split('.')[0] + ", '" +//print_id
                            first_nameTextBox.Text + "', '" +//fname
                            second_nameTextBox.Text + "', '" +//sname
                            third_nameTextBox.Text + "')"//tname
                            , AuthorizationForm.mainForm.conn); ;

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
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand(@"select id,print_id,(select print from prints where id=print_id)," +
                    "first_name, second_name , third_name from workers where id="
                    + (id), AuthorizationForm.mainForm.conn);


                    DataTable dt3 = new DataTable();
                    dt3.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                    printComboBox.Text = dt3.Rows[0].ItemArray[1].ToString() + ". " + dt3.Rows[0].ItemArray[2].ToString();
                    first_nameTextBox.Text = dt3.Rows[0].ItemArray[3].ToString();
                    second_nameTextBox.Text = dt3.Rows[0].ItemArray[4].ToString();
                    third_nameTextBox.Text = dt3.Rows[0].ItemArray[5].ToString();
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
            AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select count(*) from workers", AuthorizationForm.mainForm.conn);
            label7.Text = "Таблица Работники (записей: " + AuthorizationForm.mainForm.cmd.ExecuteScalar() + ")";
            //конец поиск количества записей в таблице работники



            //заполнение выпадающего списка типографии
            AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select * from prints", AuthorizationForm.mainForm.conn);
            var reader = AuthorizationForm.mainForm.cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            foreach (DataRow dr in dt.Rows)
            {
                printComboBox.Items.Add(dr["id"] + ". " + dr["print"]);
            }
            //конец заполнение выпадающего списка типографии

            



            AuthorizationForm.mainForm.conn.Close();

        }
    }
}
