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
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Diagnostics;

namespace Prints_Of_Donets_KursovayaBD_
{
    public partial class MainForm : Form
    {

        private string connstring; 
        public NpgsqlConnection conn;
        public string sql;
        public string selectSql;
        public NpgsqlCommand cmd;
        public DataTable dt;
        private int rowIndex = -1;
        public MainForm()
        {
            InitializeComponent();
           string password = Microsoft.VisualBasic.Interaction.InputBox("Введите пароль:", "Ввод данных для подключения к серверу", "");
            string database = Microsoft.VisualBasic.Interaction.InputBox("Введите базу данных:", "Ввод данных для подключения к серверу", "");

            if ((password == "") && (database == ""))
            {
                connstring = String.Format("Server={0};Port={1};"
                + "User Id={2};Password={3};Database={4}", "localhost", 5432, "postgres", "sava000", "prints");
            }
            else
            {
                connstring = String.Format("Server={0};Port={1};"
               + "User Id={2};Password={3};Database={4}", "localhost", 5432, "postgres", password, database);
            }
        }

        
        public bool FindTableType()//показывает тип таблицы показанной пользователю true - таблица false - справочник
        {
            string[] words = label1.Text.Split(' ');
            switch (words[0])
            {
                case "Таблица":
                    {
                        return true;
                        break;
                    }
                case "Справочник":
                    {
                        return false;
                        break;
                    }
                

            }
            return true;


        }
        public string FindTable() //показывает какая таблица показана пользователю
        {
            string[] words = label1.Text.Split(' ');
            switch (words[1])
            {
                case "Типографии":
                    {
                        return "prints";
                        break;
                    }
                case "Заказы":
                    {
                        return "orders";
                        break;
                    }
                case "Заказчики":
                    {
                        return "customers";
                        break;
                    }
                case "Работники":
                    {
                        return "workers";
                        break;
                    }
                case "Банковские":
                    {
                        return "accounts";
                        break;
                    }
                case "Типы":
                    {
                        return "types";
                        break;
                    }
                case "Районы":
                    {
                        return "districts";
                        break;
                    }
                case "Города":
                    {
                        return "cities";
                        break;
                    }
                case "Банки":
                    {
                        return "banks";
                        break;
                    }
                case "Изделия":
                    {
                        return "producttypes";
                        break;
                    }
                case "Виды":
                    {
                        return "papertypes";
                        break;
                    }
                case "Форматы":
                    {
                        return "formats";
                        break;
                    }
                    
            }
            return "";


        }
        private void Form1_Load(object sender, EventArgs e)//при загрузке главной формы
        {
            conn = new NpgsqlConnection(connstring);//присоединение к БД

            //select
            selectSql = @"select id,print as типография,(select type from types where id=type_id) as тип_собственности," +
               "(select district from districts where id=district_id) as район,address as адрес, phone as телефон, year as год_основания from prints";
            Select();
            //select

            //вывод количества записей и названия таблицы с которой работаем на форму
            sql = @"select count(*) from prints";
            ChangeLabel("Таблица Типографии");
            //конец вывод количества записей и названия таблицы с которой работаем на форму
            //заполнение ComboBox
            int i = 1;
            cellComboBox.Items.Clear();
            foreach (DataColumn item in dt.Columns)
            {
                if (CheckBytea(i - 1))
                {
                    cellComboBox.Items.Add(i + ") " + item);
                }
                i++;
            }
            //заполнение ComboBox
        }
        public void ChangeLabel(string tableName)//записывает с какой таблицей работаем и сколько в ней записей
        {
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand(sql, conn);
                label1.Text = tableName +" (записей: " + cmd.ExecuteScalar() + ")";
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Ошибка при вычислении количества записей. Error: " + ex.Message);
            }
        }
        
        public void Select()
        {
            

            try
            {

                conn.Open();
                
                cmd = new NpgsqlCommand(selectSql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                
                conn.Close();
                
                dgvData.DataSource = null;
                dgvData.DataSource = dt;
            }
            catch(Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public string NameConverterForDelete(string column)
        {
            switch (column)
            {
                case "типография":
                    {
                        try
                        {
                            conn.Open();

                            cmd = new NpgsqlCommand(@"select count(*) filter (where print_id="+ ""+") from workers ", conn);
                            
                            

                            conn.Close();

                            
                        }
                        catch (Exception ex)
                        {
                            conn.Close();
                            MessageBox.Show("Error: " + ex.Message);
                        }
                        return "print";
                    }
                        case "тип_собственности":
                    return "type_id";
                case "год_основания":
                    return "year";

                default:
                    break;
            }
            return column;

        }
        public bool CheckBytea(int i)//проверка типа столбца на bytea
        {
            switch (dt.Columns[i].DataType.ToString())
            {
                case "System.Int64":
                    {
                        
                        break;
                    }
                case "System.Int32":
                    {
                        
                        break;
                    }
                case "System.String":
                    {
                        
                        break;
                    }
                case "System.DateTime":
                    {
                        
                        break;
                    }

                default:
                    {
                        
                        return false;
                    }
                    break;
            }
            return true;
        }
        public string CheckComboBox(int i)//проверка типа вводимого значения для ComboBox на MainForm
        {
            //valueTextBox.Text = dt.Columns[i].DataType.ToString();
            switch (dt.Columns[i].DataType.ToString())
            {
                case "System.Int64":
                    {
                        
                        return valueTextBox.Text;
                        break;
                    }
                case "System.Int32":
                    {
                        
                        return valueTextBox.Text;
                        break;
                    }
                case "System.String":
                    {
                        
                        return "'"+ valueTextBox.Text+"'";
                        break;
                    }
                case "System.DateTime":
                    {
                        
                        return "'" + valueTextBox.Text + "'";
                        break;
                    }

                default:
                    {
                        valueTextBox.Text = dt.Columns[i].DataType.ToString();
                        return "error";
                    }
                    break;
            }

            //return dt.Columns[i].DataType.ToString();
        } 
        
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)//при нажатии на строку в таблице
        {
            if (e.RowIndex >= 0)
            {
                

                rowIndex = e.RowIndex;
                
                //nameTextBox.Text = dgvData.Rows[e.RowIndex].Cells["name"].Value.ToString();
            }
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            
            PrintsAddForm addForm = new PrintsAddForm();
            addForm.Visible = true;
            //int result = 0;
            //rowIndex = -1;
            //try
            //{
            //    conn.Open();
            //    sql = @"select * from print_insert(:_name)";
            //    cmd = new NpgsqlCommand(sql, conn);
            //   // cmd.Parameters.AddWithValue("_name", nameTextBox.Text);
            //    result = (int)cmd.ExecuteScalar();
            //    conn.Close();

            //    if (result == 1)
            //    {
            //        MessageBox.Show("Inserted succesfully");
            //        Select();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Inserted fail");
            //    }


            //}
            //catch (Exception ex)
            //{
            //    conn.Close();
            //    MessageBox.Show("Inserted fail. Error: " + ex.Message);
            //}
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            //int result = 0;
            //if (rowIndex < 0)
            //{
            //    MessageBox.Show("Please choose student to update");
            //    return;
            //}
            //try
            //{
            //    conn.Open();
            //    sql = @"select * from print_update(:_id,:_name)";
            //    cmd = new NpgsqlCommand(sql, conn);
            //    cmd.Parameters.AddWithValue("_id", int.Parse(dgvData.Rows[rowIndex].Cells["id"].Value.ToString()));
            //    //cmd.Parameters.AddWithValue("_name", nameTextBox.Text);
            //    result = (int)cmd.ExecuteScalar();
            //    conn.Close();

            //    if (result == 1)
            //    {
            //        MessageBox.Show("Updated succesfully");
            //        Select();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Updated fail");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    conn.Close();
            //    MessageBox.Show("Update fail. Error: " + ex.Message);
            //}
            //result = 0;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (rowIndex < 0)
            {
                MessageBox.Show("Выберите строку для удаления");
                return;
            }
            try
            {
                conn.Open();
                sql = @"select * from st_delete(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", int.Parse(dgvData.Rows[rowIndex].Cells["id"].Value.ToString()));
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Удалено успешно");
                    rowIndex = -1;

                }
                conn.Close();
                Select();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Ошибка при удалении. Error: " + ex.Message);
            }
        }



        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)//при закрытии главной формы
        {
            Application.Exit();
        }

        private void buttonSearch_Click_1(object sender, EventArgs e)
        {
            string[] words = label1.Text.Split(' ');
            SearchForm searchForm;
            switch (words[1])
            {
                case "Типографии":
                    {

                        searchForm = new SearchForm(dt,cellComboBox.Items, FindTable(),label1.Text,dgvData.Columns);
                        searchForm.Show();
                        break;
                    }
                case "Заказы":
                    {
                        searchForm = new SearchForm(dt, cellComboBox.Items, FindTable(), label1.Text, dgvData.Columns);
                        searchForm.Show();
                        break;
                    }
                case "Заказчики":
                    {
                        searchForm = new SearchForm(dt, cellComboBox.Items, FindTable(), label1.Text, dgvData.Columns);
                        searchForm.Show();
                        break;
                    }
                case "Работники":
                    {
                        searchForm = new SearchForm(dt, cellComboBox.Items,FindTable(), label1.Text, dgvData.Columns);
                        searchForm.Show();
                        break;
                    }
                
                default:
                    {
                        MessageBox.Show("Поиск выполняется только для таблиц");
                        break;
                    }

            }
            
            
        }

        private void типографииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //select
            selectSql = @"select id,print as типография,(select type from types where id=type_id) as тип_собственности,"+
                "(select district from districts where id=district_id) as район,address as адрес, phone as телефон, year as год_основания from prints";
            Select();
            //select

            //count
            sql = @"select count(*) from prints";
            ChangeLabel("Таблица Типографии");
            //count

            //заполнение ComboBox 
            int i = 1;
            cellComboBox.Items.Clear();
            foreach (DataColumn item in dt.Columns)
            {
                if (CheckBytea(i-1))
                {
                    cellComboBox.Items.Add(i + ") " + item);
                }
                i++;
            }
            //конец заполнение ComboBox 

        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //select
            selectSql = @"select id, customer_id as индекс_заказчика, (select producttype from producttypes where id=producttype_id) as изделие," +
                                " publication as название_издания,picture as титульная_страница, worker_id as индекс_работника, price as цена_экземпляра, " +
                              "calculation as тираж, count as количество_листов, (select format from formats where id=format_id) as формат, (select papertype from papertypes where id = papertype_id) as вид_бумаги" +
                              ", datastart as дата_принятия, dataplan as дата_выполнения_план, datafact as дата_выполнения_факт, cost as предоплата, comment as доп_сведения" +
                              ",account as банковский_номер from orders limit 1000";
            Select();
            //select

            //count
            sql = @"select count(*) from orders";
            ChangeLabel("Таблица Заказы");
            //count

            //заполнение ComboBox
            int i = 1;
            cellComboBox.Items.Clear();
            foreach (DataColumn item in dt.Columns)
            {
                if (CheckBytea(i-1))
                {
                    cellComboBox.Items.Add(i + ") " + item);
                }
                i++;
            }
            //заполнение ComboBox
            
        }

        private void районыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //select
            selectSql = @"select id,district as Район from districts";
            Select();
            //select
            cellComboBox.Items.Clear();
            //count
            sql = @"select count(*) from districts";
            ChangeLabel("Справочник Районы");
            //count
        }

        private void типыСобственностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //select
            selectSql = @"select id,type as Тип_Собственности from types";
            Select();
            //select
            cellComboBox.Items.Clear();
            //count
            sql = @"select count(*) from types";
            ChangeLabel("Справочник Типы собственности");
            //count
        }

        private void изделияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //select
            selectSql = @"select id,producttype as Изделие from producttypes";
            Select();
            //select
            cellComboBox.Items.Clear();
            //count
            sql = @"select count(*) from producttypes";
            ChangeLabel("Справочник Изделия");
            //count
        }

        private void городаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //select
            selectSql = @"select id,city as Город from cities";
            Select();
            //select
            cellComboBox.Items.Clear();
            //count
            sql = @"select count(*) from cities";
            ChangeLabel("Справочник Города");
            //count
        }

        private void банкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //select
            selectSql = @"select id,bank as Банк from banks";
            Select();
            //select
            cellComboBox.Items.Clear();
            //count
            sql = @"select count(*) from banks";
            ChangeLabel("Справочник Банки");
            //count
        }

        private void банковскиеНомераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //select
            selectSql = @"select * from accounts_select()";
            Select();
            //select
            cellComboBox.Items.Clear();
            //count
            sql = @"select count(*) from accounts";
            ChangeLabel("Справочник Банковские номера");
            //count
        }

        private void видыБумагиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //select
            selectSql = @"select id,papertype as Вид_бумаги,density from papertypes";
            Select();
            //select
            cellComboBox.Items.Clear();
            //count
            sql = @"select count(*) from papertypes";
            ChangeLabel("Справочник Виды бумаги");
            //count
        }

        private void форматыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //select
            selectSql = @"select id,format as Формат from formats";
            Select();
            //select
            cellComboBox.Items.Clear();
            //count
            sql = @"select count(*) from formats";
            ChangeLabel("Справочник Форматы");
            //count
        }

        private void заказчикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //select
            selectSql = @"select id,first_name as имя, second_name as фамилия, third_name as отчество,(select city from cities where id=city_id) as город," +
                                "address as адрес, birthday as дата_рождения, phone as телефон  from customers";
            Select();
            //select

            //count
            sql = @"select count(*) from customers";
            ChangeLabel("Таблица Заказчики");
            //count
            //заполнение ComboBox 
            int i = 1;
            cellComboBox.Items.Clear();
            foreach (DataColumn item in dt.Columns)
            {
                if (CheckBytea(i - 1))
                {
                    cellComboBox.Items.Add(i + ") " + item);
                }
                i++;
            }
            //конец заполнение ComboBox 
        }

        private void работникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //select
            selectSql = @"select id, first_name as имя, second_name as фамилия, third_name as отчество,(select print from prints where id = print_id) as типография from workers";
            Select();
            //select

            //count
            sql = @"select count(*) from workers";
            ChangeLabel("Таблица Работники");
            //count
            //заполнение ComboBox 
            int i = 1;
            cellComboBox.Items.Clear();
            foreach (DataColumn item in dt.Columns)
            {
                
                if (CheckBytea(i - 1))
                {
                    cellComboBox.Items.Add(i + ") " + item);
                }
                i++;
            }
            //конец заполнение ComboBox 
        }



        private void insertButton_Click_1(object sender, EventArgs e)
        {
            string[] words = label1.Text.Split(' ');
            switch (words[1])
            {
                case "Типографии":
                    {
                        //return "prints";
                        PrintsAddForm printsAdd = new PrintsAddForm();
                        printsAdd.isUpdate = false;
                        printsAdd.Visible = true;
                        //MessageBox.Show(""+rowIndex);
                        break;
                    }
                case "Заказы":
                    {
                        //return "orders";
                        OrdersAddForm ordersAdd = new OrdersAddForm();
                        ordersAdd.isUpdate = false;
                        ordersAdd.Visible = true;
                        break;
                    }
                case "Заказчики":
                    {
                        //return "customers";
                        
                        CustomersAddForm customersAdd = new CustomersAddForm();
                        customersAdd.isUpdate = false;
                        customersAdd.Visible = true;


                        break;
                    }
                case "Работники":
                    {
                        //return "workers";
                        WorkersAddForm workersAdd = new WorkersAddForm();
                        workersAdd.isUpdate = false;
                        workersAdd.Visible = true;
                        break;
                    }
                case "Банковские":
                    {
                        //return "accounts";
                        DirectoryAddForm directoryAdd = new DirectoryAddForm(true,true,false,"Банковский счет","Банк","", words[1], FindTable(),0,"");
                        directoryAdd.isUpdate = false;
                        directoryAdd.Visible = true;
                        directoryAdd.directory = FindTable();
                        break;
                    }
                case "Типы":
                    {
                        //return "types";
                        DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, false, "Тип собственности", "", "", words[1], FindTable(), 0, "");
                        directoryAdd.isUpdate = false;
                        directoryAdd.Visible = true;
                        directoryAdd.directory = FindTable();
                        break;
                    }
                case "Районы":
                    {
                        //return "districts";
                        DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, false, "Район", "", "", words[1], FindTable(), 0, "");
                        directoryAdd.isUpdate = false;
                        directoryAdd.Visible = true;
                        directoryAdd.directory = FindTable();
                        break;
                    }
                case "Города":
                    {
                        //return "cities";
                        DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, false, "Город", "", "", words[1], FindTable(), 0, "");
                        directoryAdd.isUpdate = false;
                        directoryAdd.Visible = true;
                        directoryAdd.directory = FindTable();
                        break;
                    }
                case "Банки":
                    {
                        //return "banks";
                        DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, false, "Банк", "", "", words[1], FindTable(), 0, "");
                        directoryAdd.isUpdate = false;
                        directoryAdd.Visible = true;
                        directoryAdd.directory = FindTable();
                        break;
                    }
                case "Изделия":
                    {
                        //return "producttypes";
                        DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, false, "Изделие", "", "", words[1], FindTable(), 0, "");
                        directoryAdd.isUpdate = false;
                        directoryAdd.Visible = true;
                        directoryAdd.directory = FindTable();
                        break;
                    }
                case "Виды":
                    {
                        //return "papertypes";
                        DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, true, "Вид бумаги", "", "Плотность", words[1], FindTable(), 0, "");
                        directoryAdd.isUpdate = false;
                        directoryAdd.Visible = true;
                        directoryAdd.directory = FindTable();
                        break;
                    }
                case "Форматы":
                    {
                        //return "formats";
                        DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, false, "Формат", "", "", words[1], FindTable(), 0, "");
                        directoryAdd.isUpdate = false;
                        directoryAdd.Visible = true;
                        directoryAdd.directory = FindTable();
                        break;
                    }
            }
        }

        private void deleteButton_Click_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)//если помечено удаление текущей записи
            {
                if (rowIndex < 0)//если не выбрана запись
                {
                    MessageBox.Show("Выберите запись которую хотите удалить");
                    return;
                }
                
                try
                {

                    conn.Open();

                    //проверка каскадного удаления для таблицы работники
                    cmd = new NpgsqlCommand("select count(*) from workers as w, prints as p where p.id=w.print_id and p.id=" +
                          dgvData.Rows[rowIndex].Cells["id"].Value.ToString(), conn);
                    string a = "";
                    a += "Из таблицы работники будет удалено "+cmd.ExecuteScalar()+" записей. ";
                    //конец проверка каскадного удаления для таблицы работники

                    //проверка каскадного удаления для таблицы заказы
                    cmd = new NpgsqlCommand("select count(*) from workers as w, prints as p,orders as o where p.id=w.print_id and w.id=o.worker_id and p.id="+
                         dgvData.Rows[rowIndex].Cells["id"].Value.ToString(), conn);
                    a+= "Из таблицы заказы будет удалено " + cmd.ExecuteScalar() + " записей.";

                    MessageBox.Show(a);
                    //конец проверка каскадного удаления для таблицы заказы

                    //удаление записи
                    //sql = @"delete from prints where id=" + dgvData.Rows[rowIndex].Cells["id"].Value.ToString();
                    //cmd = new NpgsqlCommand(sql, conn);
                    //dt.Load(cmd.ExecuteReader());
                    //MessageBox.Show("Удаление успешно");
                    //конец удаление записи
                    
                    conn.Close();
                    
                    rowIndex = -1;//переход к состоянию "ни одна запись не выбрана"

                    //select
                    selectSql = @"select id,print as типография,(select type from types where id=type_id) as тип_собственности," +
                "(select district from districts where id=district_id) as район,address as адрес, phone as телефон, year as год_основания from prints";
                    Select();
                    //select
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Ошибка при удалении. Error: " + ex.Message);
                }
            }
            else if (radioButton2.Checked == true)//если помечено удаление по номеру записи
            {
                try
                {
                    conn.Open();

                    //удаление записи по id
                    sql = @"delete from prints where id=" + numericUpDown1.Value;
                    cmd = new NpgsqlCommand(sql, conn);
                    dt.Load(cmd.ExecuteReader());
                    MessageBox.Show("Удаление успешно");
                    //конец удаление записи по id

                    conn.Close();

                    //select
                    selectSql = @"select id,print as типография,(select type from types where id=type_id) as тип_собственности," +
                "(select district from districts where id=district_id) as район,address as адрес, phone as телефон, year as год_основания from prints";
                    Select();
                    //select
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Ошибка при удалении. Error: " + ex.Message);
                }
            }
            else//если помечено удаление со значением поля
            {
                try
                {
                    conn.Open();

                    //удаление поля со значением
                    MessageBox.Show("delete from prints where " + NameConverterForDelete( dt.Columns[cellComboBox.SelectedIndex].ColumnName) + 
                        "=" + CheckComboBox(cellComboBox.SelectedIndex));
                    sql = @"delete from prints where " + NameConverterForDelete(dt.Columns[cellComboBox.SelectedIndex].ColumnName) +
                        "=" + CheckComboBox(cellComboBox.SelectedIndex);
                    cmd = new NpgsqlCommand(sql, conn);
                    dt.Load(cmd.ExecuteReader());
                    MessageBox.Show("Удаление успешно");
                    //конец удаление поля со значением

                    conn.Close();

                    CheckComboBox(cellComboBox.SelectedIndex);

                    //select
                    selectSql = @"select id,print as типография,(select type from types where id=type_id) as тип_собственности," +
                "(select district from districts where id=district_id) as район,address as адрес, phone as телефон, year as год_основания from prints";
                    Select();
                    //select
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Ошибка при удалении. Error: " + ex.Message);
                }
            }
        }

        private void генерацияToolStripMenuItem_Click(object sender, EventArgs e)//----------------------------генерация--------------------------------
        {
            string[] streets = { "просп. Мира", "ул. Артема", "ул. Кирова", "ул. Победы", "ул. Проса", "ул. Верти", "просп. Ворона",
                "пер. Тихона","ул. Боевая", "ул. Гоголя", "ул. Гастелло", "ул. Крылова" };//улицы для случайной генерации

            string[] first_names = { "Борис", "Абрам", "Альберт", "Богдан", "Вадим", "Василий", "Гавриил", "Виктор", "Густав", "Геннадий", "Егор", "Игорь", "Заур", "Игнат", "Иван", "Жерар"
            ,"Захар","Илья","Карл","Назар","Николай","Артур","Михаил","Марат"};//имена 

            string[] second_names = { "Рябов", "Смирнов", "Иванов", "Петров", "Морозов", "Зайцев", "Тарасов", "Федоров", "Орлов", "Бобров", "Фролов", "Ершов", "Карпов", "Шаров", "Панов",
                "Суворов", "Осипов", "Петухов", "Калинин", "Макаров" };//фамилии

            string[] third_names = { "Борисович", "Абрамович", "Альбертович", "Богданович", "Вадимович", "Васильевич", "Гавриилович", "Викторович", "Густавович", "Геннадиевич",
                "Егорович", "Игоревич", "Заурович", "Игнатович", "Иванович", "Жерарович","Захарович","Карлович","Назарович","Николаевич","Артурович","Михаилович","Маратович"};//отчества

            string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };//Англ алфавит

            string[] greekAlphabet = { "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Kappa", "Lambda",
                "Mu", "Nu", "Xi", "Omicron", "Pi", "Rho", "Sigma", "Tau", "Upsilon", "Omega"};//греческий алфавит

            string[] nameParts = { "ta-da", "so-so", "ready", "loved", "lover", "order", "slick", "sharp", "shark", "civil", "wowed", "eased", "adore", "roomy", "oh my"
                        , "gusto", "peppy", "vigor", "comic", "dress", "heart", "light", "exact", "index", "peace", "noble", "eases","gains", "award", "quick", "prime"
                        , "eases", "there", "yummy", "crisp", "jazzy", "saucy", "greet", "proto", "clean", "avant", "defix", "asset", "comfy", "spicy", "nurse", "manly"};//части названий

            string[] words = label1.Text.Split(' ');//проверка названия таблицы
            var random = new Random();
            switch (words[1])
            {
                case "Банковские":
                    {
                        string result = Microsoft.VisualBasic.Interaction.InputBox("Сколько записей сгенерировать:", "Генерация справочника банковские номера", "");
                        if (result != "")//если нажали отмена и строка не пустая
                        {

                            //формирование sql запроса insert
                            if (true)
                            {
                                sql = @"insert into accounts( account,bank_id) values";
                                sql += "('" + random.Next(11111, 99999) + random.Next(11111, 99999) +
                                    random.Next(11111, 99999) + random.Next(11111, 99999) + "'," + random.Next(1, 5) + ")";
                                for (int i = 1; i < int.Parse(result); i++)
                                {
                                    sql += ",('" + random.Next(11111, 99999) + random.Next(11111, 99999) +
                                        random.Next(11111, 99999) + random.Next(11111, 99999) + "'," + random.Next(1, 5) + ")";
                                }
                            }
                            //конец формирование sql запроса insert

                            try
                            {
                                conn.Open();

                                //запрос insert
                                cmd = new NpgsqlCommand(sql, conn);
                                dt.Load(cmd.ExecuteReader());
                                //конец запрос insert

                                conn.Close();

                                //select
                                selectSql = @"select account as банковский_номер, (select bank from banks where id=bank_id) as bank from accounts";
                                Select();
                                //select
                            }
                            catch (Exception ex)
                            {
                                conn.Close();
                                MessageBox.Show("Ошибка при удалении. Error: " + ex.Message);
                            }
                        }
                        break;
                    }
                case "Типографии":
                    {
                       
                        
                        string result = Microsoft.VisualBasic.Interaction.InputBox("Сколько записей сгенерировать:", "Генерация таблицы Типографии", "");
                        if (result != "")//если нажали отмена и строка не пустая
                        {




                            //формирование sql запроса insert
                            if (true)
                            {
                                sql = "INSERT INTO prints( print, type_id, district_id, address, phone, year) values" +
                                    "('" +
                                    alphabet[random.Next(0, 26)] + nameParts[random.Next(1, nameParts.Length)] + nameParts[random.Next(1, nameParts.Length)] + "', " +//print
                                    random.Next(1, 4) + ", " +//type id
                                    random.Next(1, 9) + ", '" +//district id
                                    streets[random.Next(0, streets.Length)] + " " + random.Next(1, 150) + "', " +//address
                                    "'071" + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + "', " +//phone
                                    random.Next(1981, 2000) + ")";//year
                                for (int i = 2; i < int.Parse(result) + 1; i++)
                                {
                                    sql += ",('" +
                                    alphabet[random.Next(0, 26)] + nameParts[random.Next(1, nameParts.Length)] + nameParts[random.Next(1, nameParts.Length)] + "', " +//print
                                    random.Next(1, 4) + ", " +//type id
                                    random.Next(1, 9) + ", '" +//district id
                                    streets[random.Next(0, streets.Length)] + " " + random.Next(1, 150) + "', " +//address
                                    "'071" + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + "', " +//phone
                                    random.Next(1981, 2000) + ")";//year
                                }
                            }
                            //конец формирование sql запроса insert

                            try
                            {
                                conn.Open();

                                //запрос insert
                                sql = @"" + sql + ";";
                                cmd = new NpgsqlCommand(sql, conn);
                                dt.Load(cmd.ExecuteReader());
                                //конец запрос insert

                                conn.Close();

                                //select
                                selectSql = @"select id,print as типография,(select type from types where id=type_id) as тип_собственности," +
                                    "(select district from districts where id=district_id) as район, address as адрес,phone as телефон, year as год from prints";
                                Select();
                                //select 

                                //count
                                sql = "select count(*) from prints";
                                ChangeLabel("Таблица Заказы");
                                //count

                                MessageBox.Show("Генерация успешно выполнена");
                            }
                            catch (Exception ex)
                            {
                                conn.Close();
                                MessageBox.Show("Ошибка при генерации. Error: " + ex.Message);
                            }
                        }
                        break;
                    }
                case "Заказы":
                    {
                        string[] acc = { "a" };//номера аккаунтов
                        DataTable dataTable;//промежуточная таблица данных
                        int[] customers= { 0};//массив индексов пользователей
                        int[] producttypes = { 0 };//массив видов изделий
                        int[] workers = { 0 };//массив работников
                        int[] formats = { 0 };//форматов
                        int[] papertypes = { 0 };//видов бумаги
                        int o;//счетчик для циклов foreach
                        string result = Microsoft.VisualBasic.Interaction.InputBox("Сколько записей сгенерировать?", "Генерация таблицы Заказы", "");
                        if (result != "")//если нажали ок(и строка не пустая)
                        {


                            try
                            {
                                if (true)//заполнение массивов с индексами
                                {
                                    conn.Open();

                                    //вытягивание аккаунотов из справочника аккаунты
                                    dataTable = new DataTable();
                                    sql = @"select account from accounts";
                                    cmd = new NpgsqlCommand(sql, conn);
                                    dataTable.Load(cmd.ExecuteReader());
                                    acc = new string[dataTable.Rows.Count];
                                    o = 0;
                                    foreach (DataRow dr in dataTable.Rows)
                                    {
                                        acc[o] = dr.ItemArray[0].ToString();
                                        //MessageBox.Show("acc["+i+"]: "+acc[i]);
                                        o += 1;
                                    }
                                    //конец вытягивание аккаунотов из справочника аккаунты

                                    //вытягивание id из таблицы заказчики
                                    cmd = new NpgsqlCommand("select id from customers", conn);
                                    dataTable = new DataTable();
                                    dataTable.Load(cmd.ExecuteReader());
                                    customers = new int[dataTable.Rows.Count];
                                    o = 0;
                                    foreach (DataRow dr in dataTable.Rows)
                                    {
                                        customers[o] = int.Parse(dr.ItemArray[0].ToString());
                                        //MessageBox.Show("customers[" + o + "]: " + customers[o]);
                                        o += 1;
                                    }
                                    //конец вытягивание id из таблицы заказчики

                                    //вытягивание id из таблицы изделия
                                    cmd = new NpgsqlCommand("select id from producttypes", conn);
                                    dataTable = new DataTable();
                                    dataTable.Load(cmd.ExecuteReader());
                                    producttypes = new int[dataTable.Rows.Count];
                                    o = 0;
                                    foreach (DataRow dr in dataTable.Rows)
                                    {
                                        producttypes[o] = int.Parse(dr.ItemArray[0].ToString());
                                        //MessageBox.Show("producttypes[" + o + "]: " + producttypes[o]);
                                        o += 1;
                                    }
                                    //конец вытягивание id из таблицы изделия

                                    //вытягивание id из таблицы работники
                                    cmd = new NpgsqlCommand("select id from workers", conn);
                                    dataTable = new DataTable();
                                    dataTable.Load(cmd.ExecuteReader());
                                    workers = new int[dataTable.Rows.Count];
                                    o = 0;
                                    foreach (DataRow dr in dataTable.Rows)
                                    {
                                        workers[o] = int.Parse(dr.ItemArray[0].ToString());
                                        //MessageBox.Show("worekrs[" + o + "]: " + workers[o]);
                                        o += 1;
                                    }
                                    //конец вытягивание id из таблицы работники

                                    //вытягивание id из таблицы форматы
                                    cmd = new NpgsqlCommand("select id from formats", conn);
                                    dataTable = new DataTable();
                                    dataTable.Load(cmd.ExecuteReader());
                                    formats = new int[dataTable.Rows.Count];
                                    o = 0;
                                    foreach (DataRow dr in dataTable.Rows)
                                    {
                                        formats[o] = int.Parse(dr.ItemArray[0].ToString());
                                        //MessageBox.Show("formats[" + o + "]: " + formats[o]);
                                        o += 1;
                                    }
                                    //конец вытягивание id из таблицы форматы

                                    //вытягивание id из таблицы виды бумаги
                                    cmd = new NpgsqlCommand("select id from papertypes", conn);
                                    dataTable = new DataTable();
                                    dataTable.Load(cmd.ExecuteReader());
                                    papertypes = new int[dataTable.Rows.Count];
                                    o = 0;
                                    foreach (DataRow dr in dataTable.Rows)
                                    {
                                        papertypes[o] = int.Parse(dr.ItemArray[0].ToString());
                                        //MessageBox.Show("papertypes[" + o + "]: " + papertypes[o]);
                                        o += 1;
                                    }
                                    //конец вытягивание id из таблицы виды бумаги

                                    conn.Close();
                                }//заполнение массивов с индексами
                            }
                            catch (Exception ex)
                            {
                                conn.Close();
                                MessageBox.Show("Deleted fail. Error: " + ex.Message);
                            }


                            //формирование sql запроса insert
                            if (true)
                            {
                                sql = "INSERT INTO orders(customer_id, producttype_id, publication,  worker_id, price, calculation, count, format_id, papertype_id, datastart, dataplan, datafact, cost,  account)  VALUES" +
                                    "(" + " " +//id
                                    customers[random.Next(0, customers.Length)] + ", " +//customer id
                                    producttypes[random.Next(0, producttypes.Length)] + ", '" +//producttype id
                                    greekAlphabet[random.Next(0, greekAlphabet.Length)] + nameParts[random.Next(0, nameParts.Length)] + "  " +
                                    alphabet[random.Next(0, alphabet.Length)] + "', " +//publication
                                    workers[random.Next(0, workers.Length)] + ", " +//worker id
                                    random.Next(100, 5000) + ", " +//price
                                    random.Next(100, 5000) + ", " +//calculation
                                    random.Next(100, 5000) + ", " +//count
                                    formats[random.Next(0, formats.Length)] + ", " +//format id
                                    papertypes[random.Next(0, papertypes.Length)] + ", '" +//papertype id
                                    random.Next(2015, 2017) + "-0" + random.Next(1, 10) + "-" + random.Next(10, 29) + "', '" +//datastart
                                    random.Next(2017, 2020) + "-0" + random.Next(1, 10) + "-" + random.Next(10, 29) + "', '" +//dataplan
                                    random.Next(2017, 2020) + "-0" + random.Next(1, 10) + "-" + random.Next(10, 29) + "', " +//datafact
                                    random.Next(0, 10000) + ", '" +//cost
                                    acc[random.Next(0, acc.Length)] + "')";//account
                                for (int i = 2; i < int.Parse(result) + 1; i++)
                                {
                                    sql += ",(" + " " +//id
                                    customers[random.Next(0, customers.Length)] + ", " +//customer id
                                    producttypes[random.Next(0, producttypes.Length)] + ", '" +//producttype id
                                    greekAlphabet[random.Next(0, greekAlphabet.Length)] + nameParts[random.Next(0, nameParts.Length)] + "  " +
                                    alphabet[random.Next(0, alphabet.Length)] + "', " +//publication
                                    workers[random.Next(0, workers.Length)] + ", " +//worker id
                                    random.Next(100, 5000) + ", " +//price
                                    random.Next(100, 5000) + ", " +//calculation
                                    random.Next(100, 5000) + ", " +//count
                                    formats[random.Next(0, formats.Length)] + ", " +//format id
                                    papertypes[random.Next(0, papertypes.Length)] + ", '" +//papertype id
                                    random.Next(2015, 2017) + "-0" + random.Next(1, 10) + "-" + random.Next(10, 29) + "', '" +//datastart
                                    random.Next(2017, 2020) + "-0" + random.Next(1, 10) + "-" + random.Next(10, 29) + "', '" +//dataplan
                                    random.Next(2017, 2020) + "-0" + random.Next(1, 10) + "-" + random.Next(10, 29) + "', " +//datafact
                                    random.Next(0, 10000) + ", '" +//cost
                                    acc[random.Next(0, acc.Length)] + "')";//account
                                }
                            }
                            //конец формирование sql запроса insert

                            try
                            {

                                conn.Open();

                                //запрос insert
                                sql = @"" + sql + ";";
                                cmd = new NpgsqlCommand(sql, conn);
                                dt.Load(cmd.ExecuteReader());
                                //конец запрос insert

                                conn.Close();

                                //select
                                selectSql = @"select id, customer_id as индекс_заказчика, (select producttype from producttypes where id=producttype_id) as изделие," +
                                    " publication as название_издания,picture as титульная_страница," +
                                  "calculation as тираж, count as количество_листов, (select format from formats where id=format_id) as формат, (select papertype from papertypes where id = papertype_id) as вид_бумаги" +
                                  ", datastart as дата_принятия, dataplan as дата_выполнения_план, datafact as дата_выполнения_факт, cost as предоплата, comment as доп_сведения" +
                                  ",account as банковский_номер from orders limit 1000";
                                Select();
                                //select

                                //count
                                sql = "select count(*) from orders";
                                ChangeLabel("Таблица Заказы");
                                //count
                                
                                MessageBox.Show("Генерация успешно выполнена");
                            }
                            catch (Exception ex)
                            {
                                conn.Close();
                                MessageBox.Show("Ошибка при генерации. Error: " + ex.Message);
                            }
                        }
                        else//если нажали отмена или строка пустая
                        {
                            MessageBox.Show("Генерация отменена!", "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    }
                case "Заказчики":
                    {
                        //"prints";
                        string result = Microsoft.VisualBasic.Interaction.InputBox("Сколько записей сгенерировать:", "Генерация таблицы Заказчики", "");
                        if (result != "")//если нажали отмена и строка не пустая
                        {
                            try
                            {
                                selectSql = @"select * from customers";
                                Select();
                            }
                            catch (Exception ex)
                            {
                                conn.Close();
                                MessageBox.Show("Ошибка при отчистке таблицы. Error: " + ex.Message);
                            }

                            //формирование sql запроса insert
                            if (true)
                            {
                                sql = "INSERT INTO customers( first_name, second_name, third_name, city_id, address, birthday, phone)	VALUES" +
                                "(" + " '" +//id
                                first_names[random.Next(0, first_names.Length)] + "', '" +//first name
                                second_names[random.Next(0, second_names.Length)] + "', '" +//second name
                                third_names[random.Next(0, third_names.Length)] + "', " +// third name
                                random.Next(1, 7) + ", '" +//city id
                                streets[random.Next(0, streets.Length)] + " " + random.Next(1, 150) + "', " +//address
                                "'" + random.Next(1950, 2002) + "-0" + random.Next(1, 10) + "-0" + random.Next(1, 28) + "', " +//birthday
                                "'071" + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + "') ";//phone
                                for (int i = 2; i < int.Parse(result) + 1; i++)
                                {
                                    sql += ",(" + " '" +//id
                                    first_names[random.Next(0, first_names.Length)] + "', '" +//first name
                                    second_names[random.Next(0, second_names.Length)] + "', '" +//second name
                                    third_names[random.Next(0, third_names.Length)] + "', " +// third name
                                    random.Next(1, 7) + ", '" +//city id
                                    streets[random.Next(0, streets.Length)] + " " + random.Next(1, 150) + "', " +//address
                                    "'" + random.Next(1950, 2002) + "-0" + random.Next(1, 10) + "-0" + random.Next(1, 28) + "', " +//birthday
                                    "'071" + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + random.Next(0, 10) + "') ";//phone

                                }
                            }
                            //конец формирование sql запроса insert

                            try
                            {
                                conn.Open();

                                //запрос insert
                                sql = @"" + sql + ";";
                                cmd = new NpgsqlCommand(sql, conn);
                                dt.Load(cmd.ExecuteReader());
                                //конец запрос insert

                                conn.Close();
                                
                                //select
                                selectSql = @"select id,first_name as имя, second_name as фамилия, third_name as отчество,(select city from cities where id=city_id) as город," +
                                    "address as адрес, birthday as дата_рождения, phone as телефон  from customers";
                                Select();
                                //select

                                //count
                                sql = "select count(*) from customers";
                                ChangeLabel("Таблица Заказчики");
                                //count

                                MessageBox.Show("Генерация успешно выполнена");
                            }
                            catch (Exception ex)
                            {
                                conn.Close();
                                MessageBox.Show("Ошибка при генерации. Error: " + ex.Message);
                            }
                        }
                        else//если нажали отмена или строка пустая
                        {
                            MessageBox.Show("Генерация отменена!", "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    }
                case "Работники":
                    {
                        
                        string result = Microsoft.VisualBasic.Interaction.InputBox("Сколько записей сгенерировать:", "Генерация таблицы Работники", "");
                        if (result != "")//если нажали отмена и строка не пустая
                        {
                            int printsCount=0;
                            try
                            {
                                conn.Open();
                                //sql = @"select count(*) from workers";
                                //cmd = new NpgsqlCommand(sql, conn);
                                //MessageBox.Show("Будет удалено " + cmd.ExecuteScalar() + " записей из таблицы Работники", "Предупреждение", MessageBoxButtons.OKCancel);
                                sql = @"select count(*) from prints";
                                cmd = new NpgsqlCommand(sql, conn);
                                printsCount = int.Parse(cmd.ExecuteScalar().ToString());
                                //MessageBox.Show(printsCount.ToString());
                                conn.Close();
                                //conn.Open();
                                //sql = @"delete from workers";
                                //cmd = new NpgsqlCommand(sql, conn);
                                //dt.Load(cmd.ExecuteReader());
                                ////MessageBox.Show("Delete succesfully");
                                //conn.Close();

                                //select
                                selectSql = @"select * from workers";
                                Select();
                                //select
                            }
                            catch (Exception ex)
                            {
                                conn.Close();
                                MessageBox.Show("Ошибка при отчистке таблицы. Error: " + ex.Message);
                            }

                            //формирование sql запроса insert
                            if (true)
                            {
                                sql = "INSERT INTO public.workers( first_name, second_name, third_name, print_id) VALUES " +
                                    "(" + " '" +//id
                                    first_names[random.Next(0, first_names.Length)] + "', '" +//first name
                                    second_names[random.Next(0, second_names.Length)] + "', '" +//second name
                                    third_names[random.Next(0, third_names.Length)] + "', " +// third name
                                    random.Next(32, 32 + printsCount) + ")";//print id
                                for (int i = 2; i < int.Parse(result) + 1; i++)
                                {
                                    sql += ",(" + "'" +//id
                                    first_names[random.Next(0, first_names.Length)] + "', '" +//first name
                                    second_names[random.Next(0, second_names.Length)] + "', '" +//second name
                                    third_names[random.Next(0, third_names.Length)] + "', " +// third name
                                    random.Next(32, 32 + printsCount) + ")";//print id                                
                                }
                            }
                            //конец формирование sql запроса insert

                            try
                            {
                                conn.Open();

                                //запрос insert
                                sql = @"" + sql + ";";
                                cmd = new NpgsqlCommand(sql, conn);
                                dt.Load(cmd.ExecuteReader());
                                //конец запрос insert
                                
                                conn.Close();

                                //select
                                selectSql = @"select id,first_name as имя, second_name as фамилия, third_name as отчество,(select print from prints where id=print_id) as типография from workers";
                                Select();
                                //select

                                //count
                                sql = "select count(*) from workers";
                                ChangeLabel("Таблица Работники");
                                //count

                                MessageBox.Show("Генерация успешно выполнена");
                            }
                            catch (Exception ex)
                            {
                                conn.Close();
                                MessageBox.Show("Ошибка при генерации. Error: " + ex.Message);
                            }
                        }
                        else//если нажали отмена или строка пустая
                        {
                            MessageBox.Show("Генерация отменена!", "Оповещение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                        
                    }
                default:
                    {
                        {
                            MessageBox.Show("Нельзя сгенерировать справочник");
                        }
                        break;
                    }


            }
                
                






            
            
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cellComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckComboBox(cellComboBox.SelectedIndex);
        }

        private void updateButton_Click_1(object sender, EventArgs e)
        {
            string[] words = label1.Text.Split(' ');
            if (rowIndex >= 0)
                switch (words[1])

                {

                    case "Типографии":
                        {
                            //return "prints";
                            PrintsAddForm addForm = new PrintsAddForm();
                            addForm.isUpdate = true;
                            addForm.id = int.Parse(dt.Rows[rowIndex].ItemArray[0].ToString());
                            addForm.Visible = true;
                            break;
                        }
                    case "Заказы":
                        {
                            //return "orders";
                            OrdersAddForm ordersAdd = new OrdersAddForm();
                            ordersAdd.isUpdate = true;
                            ordersAdd.id = int.Parse(dt.Rows[rowIndex].ItemArray[0].ToString());
                            ordersAdd.Visible = true;

                            break;
                        }
                    case "Заказчики":
                        {
                            //return "customers";
                            CustomersAddForm customersAdd = new CustomersAddForm();
                            customersAdd.isUpdate = true;
                            customersAdd.id = int.Parse(dt.Rows[rowIndex].ItemArray[0].ToString());
                            customersAdd.Visible = true;


                            break;
                        }
                    case "Работники":
                        {
                            //return "workers";
                            WorkersAddForm workersAdd = new WorkersAddForm();
                            workersAdd.isUpdate = true;
                            workersAdd.id= int.Parse(dt.Rows[rowIndex].ItemArray[0].ToString());
                            workersAdd.Visible = true;
                            break;
                        }
                    case "Банковские":
                        {
                            //return "accounts";
                            DirectoryAddForm directoryAdd = new DirectoryAddForm(true, true, false, "Банковский счет", "Банк", "", words[1], FindTable(),0, dt.Rows[rowIndex].ItemArray[0].ToString());
                            directoryAdd.isUpdate = true;
                            directoryAdd.Visible = true;
                            
                            directoryAdd.directory = FindTable();
                            break;
                        }
                    case "Типы":
                        {
                            //return "types";
                            DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, false, "Тип собственности", "", "", words[1], FindTable(), int.Parse(dt.Rows[rowIndex].ItemArray[0].ToString()), "");
                            directoryAdd.isUpdate = true;
                            directoryAdd.Visible = true;
                            
                            directoryAdd.directory = FindTable();
                            break;
                        }
                    case "Районы":
                        {
                            //return "districts";
                            DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, false, "Район", "", "", words[1], FindTable(), int.Parse(dt.Rows[rowIndex].ItemArray[0].ToString()), "");
                            directoryAdd.isUpdate = true;
                            directoryAdd.Visible = true;
                            
                            directoryAdd.directory = FindTable();
                            break;
                        }
                    case "Города":
                        {
                            //return "cities";
                            DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, false, "Город", "", "", words[1], FindTable(), int.Parse(dt.Rows[rowIndex].ItemArray[0].ToString()), "");
                            directoryAdd.isUpdate = true;
                            directoryAdd.Visible = true;
                            
                            directoryAdd.directory = FindTable();
                            break;
                        }
                    case "Банки":
                        {
                            //return "banks";
                            DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, false, "Банк", "", "", words[1], FindTable(), int.Parse(dt.Rows[rowIndex].ItemArray[0].ToString()), "");
                            directoryAdd.isUpdate = true;
                            directoryAdd.Visible = true;
                            directoryAdd.directory = FindTable();
                            break;
                        }
                    case "Изделия":
                        {
                            //return "producttypes";
                            DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, false, "Изделие", "", "", words[1], FindTable(), int.Parse(dt.Rows[rowIndex].ItemArray[0].ToString()), "");
                            directoryAdd.isUpdate = true;
                            directoryAdd.Visible = true;
                            directoryAdd.directory = FindTable();
                            break;
                        }
                    case "Виды":
                        {
                            //return "papertypes";
                            DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, true, "Вид бумаги", "", "Плотность", words[1], FindTable(), int.Parse(dt.Rows[rowIndex].ItemArray[0].ToString()), "");
                            directoryAdd.isUpdate = true;
                            directoryAdd.Visible = true;
                            directoryAdd.directory = FindTable();
                            break;
                        }
                    case "Форматы":
                        {
                            //return "formats";
                            DirectoryAddForm directoryAdd = new DirectoryAddForm(true, false, false, "Формат", "", "", words[1], FindTable(), int.Parse(dt.Rows[rowIndex].ItemArray[0].ToString()),"");
                            directoryAdd.isUpdate = true;
                            directoryAdd.Visible = true;
                            directoryAdd.directory = FindTable();
                            break;
                        }
                }
            else MessageBox.Show("Выберите запись");

        }

        private void запросыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectForm selectForm = new SelectForm();
            selectForm.Show();
        }

        private void изделияToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DiagramForm diagramForm = new DiagramForm();
            diagramForm.type = 3;
            diagramForm.Show();
        }

        private void частотаИспользованияФорматовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiagramForm diagramForm = new DiagramForm();
            diagramForm.type = 2;
            diagramForm.Show();
        }

        private void количестоЗаказовНаБанкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiagramForm diagramForm = new DiagramForm();
            diagramForm.type = 1;
            diagramForm.Show();
            
        }

        private void городаСЗаказчикамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + @"\" + "Save_value.xlsx";

            Excel.Application excelapp = new Excel.Application();
            Excel.Workbook workbook = excelapp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            for (int i= 1; i < dgvData.RowCount + 1; i++)
            {
                for (int j = 1; j < dgvData.ColumnCount+1; j++)
                {
                    worksheet.Rows[i].Columns[j] = dgvData.Rows[i - 1].Cells[j - 1].Value;
                }
            }
            excelapp.AlertBeforeOverwriting = false;
            workbook.SaveAs(path);
            excelapp.Quit();
        }

        private void средийРазмерТиражейПоТипографииToolStripMenuItem_Click(object sender, EventArgs e)//Средний тираж по типографиям, районам, городу
        {
            DataTable dt4 = new DataTable();
            string path = System.IO.Directory.GetCurrentDirectory() + @"\" + "Средний_тираж.xlsx";

            sql = @"select p.print as типография,avg(o.calculation) as средний_тираж_типография,d.district as район,dist.средний_тираж_район,cit.средний_тираж_город" +
                            " from districts as d, prints as p, workers as w, orders as o, ( 	select d.district,avg(o.calculation) as средний_тираж_район 	from districts as d," +
                            " 	prints as p, workers as w, 	orders as o 	where p.id=w.print_id and 	d.id=p.district_id and 	o.worker_id = w.id 	group by 1 	order by 1) as dist," +
                            " 	(select avg(o.calculation) as средний_тираж_город 	from districts as d, 	prints as p, workers as w, 	orders as o 	where p.id=w.print_id and " +
                            "	d.id=p.district_id and 	o.worker_id = w.id 	order by 1) as cit where p.id=w.print_id and d.id=p.district_id and o.worker_id = w.id and dist.district = d.district" +
                            " group by 1,3,4,5 order by 3,1";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt4 = new DataTable();
                dt4.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            Excel.Application excelapp = new Excel.Application();
            Excel.Workbook workbook = excelapp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            for (int j = 1; j < dt4.Columns.Count + 1; j++)
            {
                worksheet.Rows[1].Columns[j] = dt4.Columns[j - 1].ColumnName;
            }
            for (int i = 2; i < dt4.Rows.Count + 2; i++)
            {
                for (int j = 1; j < dt4.Columns.Count + 1; j++)
                {
                    worksheet.Rows[i].Columns[j] = dt4.Rows[i - 2].ItemArray[j - 1];
                }
            }
            excelapp.AlertBeforeOverwriting = false;
            workbook.SaveAs(path);
            excelapp.Quit();
        }

        private void стреднийРазмерТиражейПоРайонуToolStripMenuItem_Click(object sender, EventArgs e)//Лучшая типография по району, городу и самое популярное изделие
        {
            DataTable dt4 = new DataTable();
            string path = System.IO.Directory.GetCurrentDirectory() + @"\" + "Лучшая_типография.xlsx";

            sql = @"select p.print as типография_район,o.cost*o.calculation as сумма_заказов_район,d.district as район,maxxx.print as типография_город," +
                            "maxxx.maxcd as сумма_заказов_город,prmax.изделие as популярное_изделие , prmax.количество_заказов_изделия 	from districts as d," +
                            "	prints as p, workers as w, 	orders as o, ( 		select d.district,max(o.cost*o.calculation) as maxcd 		from districts as d," +
                            "        prints as p, workers as w, 		orders as o 		where p.id=w.print_id and 		d.id=p.district_id and 		o.worker_id = w.id  " +
                            "        group by 1 		order by 1) as maxd, 		( 		select p.print,max(o.cost*o.calculation) as maxcd 		from districts as d, " +
                            "         prints as p, workers as w, 		orders as o,( 			select max(o.cost*o.calculation) as maxcc 			from districts as d," +
                            "             prints as p, workers as w, 			orders as o 			where p.id=w.print_id and 			d.id=p.district_id and" +
                            "             o.worker_id = w.id  			order by 1) as maxc 		where p.id=w.print_id and 		d.id=p.district_id and 		o.worker_id = w.id and" +
                            "         maxc.maxcc=(o.cost*o.calculation) 		group by 1 		order by 1) as maxxx, 		(select pr.producttype as изделие,o.producttype_id,count(o.*) as количество_заказов_изделия 			from districts as d, " +
                            "            prints as p, workers as w, 			orders as o, producttypes as pr 			where p.id=w.print_id and 			d.id=p.district_id and 			o.worker_id = w.id and" +
                            "          o.producttype_id=pr.id 		 	group by 1,2 			order by 3 desc 		limit 1) as prmax 	 " +
                            "     where p.id=w.print_id and 	d.id=p.district_id and 	o.worker_id = w.id and 	maxd.maxcd=(o.cost*o.calculation) and 	maxd.district=d.district 	order by 1";

            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt4 = new DataTable();
                dt4.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            Excel.Application excelapp = new Excel.Application();
            Excel.Workbook workbook = excelapp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            for (int j = 1; j < dt4.Columns.Count + 1; j++)
            {
                worksheet.Rows[1].Columns[j] = dt4.Columns[j - 1].ColumnName;
            }
            for (int i = 2; i < dt4.Rows.Count + 2; i++)
            {
                for (int j = 1; j < dt4.Columns.Count + 1; j++)
                {
                    worksheet.Rows[i].Columns[j] = dt4.Rows[i - 2].ItemArray[j - 1];
                }
            }
            excelapp.AlertBeforeOverwriting = false;
            workbook.SaveAs(path);
            excelapp.Quit();
        }

        private void стреднийРазмерТиражейПоГородуToolStripMenuItem_Click(object sender, EventArgs e)//Стоимость заказов типографий за ГОД не выполненные в срок
        {
            DataTable dt4 = new DataTable();
            string path = System.IO.Directory.GetCurrentDirectory() + @"\" + "Стоимость_заказов.xlsx";

            string result = Microsoft.VisualBasic.Interaction.InputBox("Введите год:", "Ввод данных для запроса", "");
            sql = @"select id as номер_заказа,publication заказ,datafact дата_выполнения_факт,dataplan as дата_выполнения_план from orders" +
                " where datafact>dataplan and extract(year from datafact) =" + result + " order by 2";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt4 = new DataTable();
                dt4.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            Excel.Application excelapp = new Excel.Application();
            Excel.Workbook workbook = excelapp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;

            for (int j = 1; j < dt4.Columns.Count + 1; j++)
            {
                worksheet.Rows[1].Columns[j] = dt4.Columns[j - 1].ColumnName;
            }
            for (int i = 2; i < dt4.Rows.Count + 2; i++)
            {
                for (int j = 1; j < dt4.Columns.Count + 1; j++)
                {
                    worksheet.Rows[i].Columns[j] = dt4.Rows[i - 2].ItemArray[j - 1];
                }
            }
            excelapp.AlertBeforeOverwriting = false;
            workbook.SaveAs(path);
            excelapp.Quit();
        }

        public void CreateHTMLFile(string table,string name)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + @"\" +name +".html";
            var sbHTML = new StringBuilder();
            var sw = new StreamWriter(System.IO.Directory.GetCurrentDirectory() + @"\" +name +".html");
            sbHTML.AppendLine("<DOCTYPE html>");
            sbHTML.AppendLine("<html>");
            sbHTML.AppendLine("<head>");
            sbHTML.AppendLine("<title>");
            sbHTML.AppendLine(name);
            sbHTML.AppendLine("</title>");
            sbHTML.AppendLine("</head>");
            sbHTML.AppendLine("<body>");
            sbHTML.AppendLine(table);
            sbHTML.AppendLine("</body>");
            sbHTML.AppendLine("</html>");
            sw.Write(sbHTML);
            sw.Close();
            Process.Start(System.IO.Directory.GetCurrentDirectory() + @"\" + name + ".html");
        }

        private void типографииToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            sql = @"select id,print as типография,(select type from types where id=type_id) as тип_собственности," +
    "(select district from districts where id=district_id) as район,address as адрес, phone as телефон, year as год_основания from prints";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt = new DataTable();
                dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            CreateHTMLFile(Extensions.GetHtml(dt),"типографии");
        }

        private void заToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            sql = @"select id, customer_id as индекс_заказчика, (select producttype from producttypes where id=producttype_id) as изделие," +
                                " publication as название_издания,picture as титульная_страница, worker_id as индекс_работника, price as цена_экземпляра, " +
                              "calculation as тираж, count as количество_листов, (select format from formats where id=format_id) as формат, (select papertype from papertypes where id = papertype_id) as вид_бумаги" +
                              ", datastart as дата_принятия, dataplan as дата_выполнения_план, datafact as дата_выполнения_факт, cost as предоплата, comment as доп_сведения" +
                              ",account as банковский_номер from orders limit 1000";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt = new DataTable();
                dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            CreateHTMLFile(Extensions.GetHtml(dt), "заказы");
        }

        private void заказчикиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            sql = @"select id,first_name as имя, second_name as фамилия, third_name as отчество,(select city from cities where id=city_id) as город," +
                                "address as адрес, birthday as дата_рождения, phone as телефон  from customers";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt = new DataTable();
                dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            CreateHTMLFile(Extensions.GetHtml(dt), "заказчики");
        }

        private void работникиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            sql = @"select id, first_name as имя, second_name as фамилия, third_name as отчество,(select print from prints where id = print_id) as типография from workers";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt = new DataTable();
                dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            CreateHTMLFile(Extensions.GetHtml(dt), "работники");
        }

        private void типыСобственностиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            sql = @"select id,type as Тип_Собственности from types";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt = new DataTable();
                dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            CreateHTMLFile(Extensions.GetHtml(dt), "типы_собственности");
        }

        private void районыToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            sql = @"select id,district as Район from districts";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt = new DataTable();
                dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            CreateHTMLFile(Extensions.GetHtml(dt), "районы");
        }

        private void изделияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            sql = @"select id,format as Формат from formats";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt = new DataTable();
                dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            CreateHTMLFile(Extensions.GetHtml(dt), "изделия");
        }

        private void городаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            sql = @"select id,city as Город from cities";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt = new DataTable();
                dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            CreateHTMLFile(Extensions.GetHtml(dt), "города");
        }

        private void банкиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            sql = @"select id,bank as Банк from banks";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt = new DataTable();
                dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            CreateHTMLFile(Extensions.GetHtml(dt), "банки");
        }

        private void банковскиеНомераToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            sql = @"select * from accounts_select()";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt = new DataTable();
                dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            CreateHTMLFile(Extensions.GetHtml(dt), "банковские_ноиера");
        }

        private void видыБумагиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            sql = @"select id,papertype as Вид_бумаги,density from papertypes";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt = new DataTable();
                dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            CreateHTMLFile(Extensions.GetHtml(dt), "виды_бумаги");
        }

        private void форматыToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            sql = @"select id,format as Формат from formats";
            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt = new DataTable();
                dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
            CreateHTMLFile(Extensions.GetHtml(dt), "форматы");
        }
    }
    public static class Extensions
    {
        public static String GetHtml(this DataTable dataTable)
        {
            StringBuilder sbControlHtml = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter())
            {
                using (HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter))
                {
                    using (var htmlTable = new HtmlTable())
                    {
                        htmlTable.Border = 1;
                        // Add table header row  
                        using (var headerRow = new HtmlTableRow())
                        {
                            foreach (DataColumn dataColumn in dataTable.Columns)
                            {
                                using (var htmlColumn = new HtmlTableCell())
                                {
                                    htmlColumn.InnerText = dataColumn.ColumnName;
                                    headerRow.Cells.Add(htmlColumn);
                                }
                            }
                            htmlTable.Rows.Add(headerRow);
                        }
                        // Add data rows  
                        foreach (DataRow row in dataTable.Rows)
                        {
                            using (var htmlRow = new HtmlTableRow())
                            {
                                foreach (DataColumn column in dataTable.Columns)
                                {
                                    using (var htmlColumn = new HtmlTableCell())
                                    {
                                        htmlColumn.InnerText = row[column].ToString();
                                        htmlRow.Cells.Add(htmlColumn);
                                    }
                                }
                                htmlTable.Rows.Add(htmlRow);
                            }
                        }
                        htmlTable.RenderControl(htmlWriter);
                        sbControlHtml.Append(stringWriter.ToString());
                    }
                }
            }
            return sbControlHtml.ToString();
        }
    }
}  

