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
    public partial class DirectoryAddForm : Form
    {
        public bool isUpdate;
        public int id;
        public int maxId;
        public string directory;
        public string field;
        public string sql;
        public string ss,acc;
        public DirectoryAddForm(bool a, bool b, bool c,string sa,string sb,string sc,string ss,string directory,int id,string acc)
        {
            InitializeComponent();
            textBox1.Visible = a;
            comboBox1.Visible = b;
            textBox2.Visible = c;
            label1.Visible = a;
            label2.Visible = b;
            label3.Visible = c;
            label1.Text = sa;
            label2.Text = sb;
            label3.Text = sc;
            this.ss = ss;
            this.directory = directory;
            this.id = id;
            this.acc = acc;
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (isUpdate)//если изменение записи
                {
                    switch (directory)
                    {
                        case "accounts":
                            {
                                sql = @"update accounts set bank_id="+comboBox1.Text.Split('.')[0] + "   where account='" + acc+"'";
                                break;
                            }
                        case "banks":
                            {
                                sql = @"update banks  set bank='" + textBox1.Text + "'   where id=" + id;
                                break;
                            }
                        case "cities":
                            {
                                sql = @"update cities set  city='" + textBox1.Text + "'   where id=" + id;
                                break;
                            }
                        case "districts":
                            {
                                sql = @"update districts set  district='" + textBox1.Text + "'   where id=" + id;
                                break;
                            }
                        case "formats":
                            { 
                                sql = @"update formats  set format='" + textBox1.Text + "'   where id=" + id;
                                break;
                            }
                        case "papertypes":
                            {
                                sql = @"update papertypes  set papertype='" + textBox1.Text + "' ,density=" + int.Parse(textBox2.Text) + "   where id=" + id;
                                break;
                            }
                        case "producttypes":
                            {
                                sql = @"update  producttypes  set producttype='" + textBox1.Text + "'  where id=" + id;
                                break;
                            }
                        case "types":
                            {
                                sql = @"update types set  type='" + textBox1.Text + "'   where id=" + id;
                                break;
                            }
                    }
                    AuthorizationForm.mainForm.conn.Open();
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                    AuthorizationForm.mainForm.cmd.ExecuteReader();
                    AuthorizationForm.mainForm.conn.Close();
                }
                else//добавление записи
                {
                    switch (directory)
                    {
                        case "accounts":
                            {
                                sql = @"insert into accounts ( account) values('" + textBox1.Text + "')";
                                break;
                            }
                        case "banks":
                            {
                                sql = @"insert into banks  ( bank) values('" + textBox1.Text + "')";
                                break;
                            }
                        case "cities":
                            {
                                sql = @"insert into cities (  city) values('" + textBox1.Text + "')";
                                break;
                            }
                        case "districts":
                            {
                                sql = @"insert into districts (  district) values('" + textBox1.Text + "')";
                                break;
                            }
                        case "formats":
                            {
                                sql = @"insert into formats  ( format) values('" + textBox1.Text + "')";
                                break;
                            }
                        case "papertypes":
                            {
                                sql = @"insert into papertypes  (papertype,density) values('"+textBox1.Text+"'," + int.Parse(textBox2.Text) + ")";
                                break;
                            }
                        case "producttypes":
                            {
                                sql = @"insert into  producttypes  ( producttype) values('" + textBox1.Text + "')";
                                break;
                            }
                        case "types":
                            {
                                sql = @"insert into types ( type) values('" + textBox1.Text + "')";
                                break;
                            }
                    }
                    AuthorizationForm.mainForm.conn.Open();
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn); ;

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
                    switch (directory)
                    {
                        case "accounts":
                            {
                                sql = @"select account, bank_id,(select bank from banks where bank_id=id) from accounts  where account='" + acc + "'";
                                textBox1.Enabled = false;
                                break;
                            }
                        case "banks":
                            {
                                sql = @"select bank from banks where id=" + id;
                                break;
                            }
                        case "cities":
                            {
                                sql = @"select city from cities where id=" + id;
                                break;
                            }
                        case "districts":
                            {
                                sql = @"select district from districts where id=" + id;
                                break;
                            }
                        case "formats":
                            {
                                sql = @"select format from formats where id=" + id;
                                break;
                            }
                        case "papertypes":
                            {
                                sql = @"select papertype,density from papertypes where id=" + id;
                                break;
                            }
                        case "producttypes":
                            {
                                sql = @"select producttype from producttypes where id=" + id;
                                break;
                            }
                        case "types":
                            {
                                sql = @"select type from types where id=" + id;
                                break;
                            }
                    }
                    AuthorizationForm.mainForm.conn.Open();

                    //заполнение формы информацией о строке
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);


                    DataTable dt3 = new DataTable();
                    dt3.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                    textBox1.Text = dt3.Rows[0].ItemArray[0].ToString();
                    if(comboBox1.Visible)
                        comboBox1.Text = dt3.Rows[0].ItemArray[1].ToString() + ". " + dt3.Rows[0].ItemArray[2].ToString();
                    if(textBox2.Visible)
                    textBox2.Text = dt3.Rows[0].ItemArray[1].ToString();
                    
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


            try
            {
                AuthorizationForm.mainForm.conn.Open();

                //поиск количества записей в таблице 
                AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select count(*) from " + directory, AuthorizationForm.mainForm.conn);
                label7.Text = "Таблица " + ss + " (записей: " + AuthorizationForm.mainForm.cmd.ExecuteScalar() + ")";
                //конец поиск количества записей в таблице 



                //заполнение выпадающего списка банки
                if (comboBox1.Visible)
                {
                    AuthorizationForm.mainForm.cmd = new NpgsqlCommand("select * from banks", AuthorizationForm.mainForm.conn);
                    var reader = AuthorizationForm.mainForm.cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    foreach (DataRow dr in dt.Rows)
                    {
                        comboBox1.Items.Add(dr["id"] + ". " + dr["bank"]);
                    }
                }
                //конец заполнение выпадающего списка банки


                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Inserted fail 3. Error: " + ex.Message);
            }
            
        }
    }
}
