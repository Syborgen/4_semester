using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Npgsql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prints_Of_Donets_KursovayaBD_
{
    public partial class OrdersAddForm : Form
    {
        public bool isUpdate;
        public int id;
        public int maxId;
        public OrdersAddForm()
        {
            InitializeComponent();
            dateTimePicker1.MaxDate = dateTimePicker2.MaxDate = dateTimePicker3.MaxDate = DateTime.Today;
            dateTimePicker1.CustomFormat = dateTimePicker2.CustomFormat = dateTimePicker3.CustomFormat = "yyyy-MM-dd";
            dateTimePicker1.Format = dateTimePicker2.Format = dateTimePicker3.Format = DateTimePickerFormat.Custom;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void priceNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (isUpdate)//если изменение записи
                {
                    AuthorizationForm.mainForm.conn.Open();
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand(@"update customers set customer_id =" + customerComboBox.Text
                        + " ,producttype_id =" + producttypeComboBox.Text
                        + ", publication='" + publicationTextBox.Text
                        + "',  worker_id=" + workerComboBox.Text
                        + ", price=" + priceNumericUpDown.Value
                        + ", calculation=" + calculationNumericUpDown.Value
                        + ", count=" + countNumericUpDown.Value
                        + ", format_id=" + formatComboBox.Text
                        + ", papertype_id=" + papertypeComboBox.Text
                        + ", datastart='" + dateTimePicker1.Text
                        + "', dataplan='" + dateTimePicker2.Text
                        + "', datafact='" + dateTimePicker3.Text
                        + "', cost=" + costNumericUpDown.Value
                        + ",  account=" + accountComboBox.Text
                        + " where id=" + id, AuthorizationForm.mainForm.conn);
                    AuthorizationForm.mainForm.cmd.ExecuteReader();
                    AuthorizationForm.mainForm.conn.Close();
                }
                else//добавление записи
                {
                    AuthorizationForm.mainForm.conn.Open();
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand("INSERT INTO orders(customer_id,producttype_id, publication,  worker_id, price, calculation," +
                        " count, format_id, papertype_id, datastart, dataplan, datafact, cost,  account)  VALUES" +
                                    "(" + " " +//id
                                     customerComboBox.Text + ", " +//customer id
                                     producttypeComboBox.Text + ", '" +//producttype id
                                     publicationTextBox.Text + "', " +//publication
                                     workerComboBox.Text + ", " +//worker id
                                     priceNumericUpDown.Value + ", " +//price
                                     calculationNumericUpDown.Value + ", " +//calculation
                                     countNumericUpDown.Value + ", " +//count
                                     formatComboBox.Text + ", " +//format id
                                     papertypeComboBox.Text + ", '" +//papertype id
                                     dateTimePicker1.Text + "', '" +//datastart
                                     dateTimePicker2.Text + "', '" +//dataplan
                                     dateTimePicker3.Text + "', " +//datafact
                                     costNumericUpDown.Value + ", '" +//cost
                                     accountComboBox.Text + "')"//account

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
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand(@"select customer_id,(select second_name from customers where customer_id=id)" +
                        ",producttype_id,(select producttype from producttypes where producttype_id = id), publication,  worker_id,(select second_name from workers where worker_id = id), price, calculation," +
                        " count, format_id,(select format from formats where format_id = id), papertype_id,(select papertype from papertypes where papertype_id=id), datastart," +
                        " dataplan, datafact, cost,  account from orders where id="
                    + (id), AuthorizationForm.mainForm.conn);


                    DataTable dt3 = new DataTable();
                    dt3.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                    customerComboBox.Text = dt3.Rows[0].ItemArray[0].ToString() + ". " + dt3.Rows[0].ItemArray[1].ToString();
                    producttypeComboBox.Text = dt3.Rows[0].ItemArray[2].ToString() + ". " + dt3.Rows[0].ItemArray[3].ToString();
                    publicationTextBox.Text = dt3.Rows[0].ItemArray[4].ToString();
                    workerComboBox.Text = dt3.Rows[0].ItemArray[5].ToString() + ". " + dt3.Rows[0].ItemArray[6].ToString();
                    priceNumericUpDown.Value = int.Parse(dt3.Rows[0].ItemArray[7].ToString());
                    calculationNumericUpDown.Value = int.Parse(dt3.Rows[0].ItemArray[8].ToString());
                    countNumericUpDown.Value = int.Parse(dt3.Rows[0].ItemArray[9].ToString());
                    formatComboBox.Text = dt3.Rows[0].ItemArray[10].ToString() + ". " + dt3.Rows[0].ItemArray[11].ToString();
                    papertypeComboBox.Text = dt3.Rows[0].ItemArray[12].ToString() + ". " + dt3.Rows[0].ItemArray[13].ToString();
                    dateTimePicker1.Text = dt3.Rows[0].ItemArray[14].ToString();
                    dateTimePicker2.Text = dt3.Rows[0].ItemArray[15].ToString();
                    dateTimePicker3.Text = dt3.Rows[0].ItemArray[16].ToString();
                    costNumericUpDown.Value = int.Parse(dt3.Rows[0].ItemArray[17].ToString());
                    accountComboBox.Text = dt3.Rows[0].ItemArray[18].ToString();




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
            label7.Text = "Таблица Заказы (записей: " + AuthorizationForm.mainForm.cmd.ExecuteScalar() + ")";
            //конец поиск количества записей в таблице работники

            //заполнение выпадающего списка заказчик
            AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select * from customers", AuthorizationForm.mainForm.conn);
            var reader = AuthorizationForm.mainForm.cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            foreach (DataRow dr in dt.Rows)
            {
                customerComboBox.Items.Add(dr["id"] + ". " + dr["second_name"]);
            }
            //конец заполнение выпадающего списка заказчик

            //заполнение выпадающего списка изделия
            AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select * from producttypes", AuthorizationForm.mainForm.conn);
            reader = AuthorizationForm.mainForm.cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            foreach (DataRow dr in dt.Rows)
            {
                producttypeComboBox.Items.Add(dr["id"] + ". " + dr["producttype"]);
            }
            //конец заполнение выпадающего списка изделия

            //заполнение выпадающего списка работник
            AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select * from workers", AuthorizationForm.mainForm.conn);
            reader = AuthorizationForm.mainForm.cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            foreach (DataRow dr in dt.Rows)
            {
                workerComboBox.Items.Add(dr["id"] + ". " + dr["second_name"]);
            }
            //конец заполнение выпадающего списка работник

            //заполнение выпадающего списка формат
            AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select * from formats", AuthorizationForm.mainForm.conn);
            reader = AuthorizationForm.mainForm.cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            foreach (DataRow dr in dt.Rows)
            {
                formatComboBox.Items.Add(dr["id"] + ". " + dr["format"]);
            }
            //конец заполнение выпадающего списка формат

            //заполнение выпадающего списка вид бумаги
            AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select * from papertypes", AuthorizationForm.mainForm.conn);
            reader = AuthorizationForm.mainForm.cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            foreach (DataRow dr in dt.Rows)
            {
                papertypeComboBox.Items.Add(dr["id"] + ". " + dr["papertype"]+"("+dr["density"]+")");
            }
            //конец заполнение выпадающего списка вид бумаги

            //заполнение выпадающего списка банковский счет
            AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select * from accounts", AuthorizationForm.mainForm.conn);
            reader = AuthorizationForm.mainForm.cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            foreach (DataRow dr in dt.Rows)
            {
                accountComboBox.Items.Add(dr["account"]);
            }
            //конец заполнение выпадающего списка банковский счет





            AuthorizationForm.mainForm.conn.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            ofd.Title = "Выбрать картинку для обработки";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}


