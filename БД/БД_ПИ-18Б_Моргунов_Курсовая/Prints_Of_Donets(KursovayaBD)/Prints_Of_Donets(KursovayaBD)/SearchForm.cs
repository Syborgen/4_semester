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
    
    public partial class SearchForm : Form
    {
        public DataTable dt;
        public string sql;
        public string[] columns;
        public string table;
        public string label;
        public SearchForm(DataTable dt,ComboBox.ObjectCollection a,string table,string label,DataGridViewColumnCollection c)
        {
            InitializeComponent();
            this.dt = dt;
            foreach (String st in a)
            {
                comboBox1.Items.Add(st);
            }
            int i = 0;
            columns = new string[c.Count];
            foreach (DataGridViewColumn st in c)
            {
                
                columns[i++] = st.HeaderText;
            }
            this.table = table;
            this.label = label;
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            label1.Text = label;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (table)
            {
                case "prints":
                    {
                        switch (int.Parse(comboBox1.Text.Split(')')[0]) - 1)
                        {

                            case 2:
                                {
                                    try
                                    {
                                        AuthorizationForm.mainForm.conn.Open();
                                        sql = "select p.id,p.print,t.type,d.district,p.address,p.phone,p.year from prints as p,districts as d,types as t"+
                                            " where p.district_id=d.id and p.type_id=t.id and t.type='"+textBox1.Text+"'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        dataGridView1.DataSource = dt;
                                        sql = "select count(*) from prints as p,districts as d,types as t" +
                                            " where p.district_id=d.id and p.type_id=t.id and t.type='" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);

                                        label1.Text = label + " (выбрано " + int.Parse(AuthorizationForm.mainForm.cmd.ExecuteScalar().ToString()) + ")";
                                        for (int i = 0; i < columns.Length; i++)
                                        {
                                            dataGridView1.Columns[i].HeaderText = columns[i];
                                        }





                                        AuthorizationForm.mainForm.conn.Close();


                                    }
                                    catch (Exception ex)
                                    {
                                        AuthorizationForm.mainForm.conn.Close();
                                        MessageBox.Show("Error: " + ex.Message);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    try
                                    {
                                        AuthorizationForm.mainForm.conn.Open();
                                        sql = "select p.id,p.print,t.type,d.district,p.address,p.phone,p.year from prints as p,districts as d,types as t" +
                                            " where p.district_id=d.id and p.type_id=t.id and d.district='" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        dataGridView1.DataSource = dt;
                                        sql = "select count(*) from prints as p,districts as d,types as t" +
                                            " where p.district_id=d.id and p.type_id=t.id and d.district='" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);

                                        label1.Text = label + " (выбрано " + int.Parse(AuthorizationForm.mainForm.cmd.ExecuteScalar().ToString()) + ")";
                                        for (int i = 0; i < columns.Length; i++)
                                        {
                                            dataGridView1.Columns[i].HeaderText = columns[i];
                                        }





                                        AuthorizationForm.mainForm.conn.Close();


                                    }
                                    catch (Exception ex)
                                    {
                                        AuthorizationForm.mainForm.conn.Close();
                                        MessageBox.Show("Error: " + ex.Message);
                                    }
                                    break;
                                }
                            default:
                                {
                                    try
                                    {
                                        AuthorizationForm.mainForm.conn.Open();
                                        sql = "select * from " + table;
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        sql = "select * from " + table + " where " + dt.Columns[int.Parse(comboBox1.Text.Split(')')[0]) - 1] + " = '" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        dataGridView1.DataSource = dt;
                                        sql = "select count(*) from " + table + " where " + dt.Columns[int.Parse(comboBox1.Text.Split(')')[0]) - 1] + " = '" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);

                                        label1.Text = label + " (выбрано " + int.Parse(AuthorizationForm.mainForm.cmd.ExecuteScalar().ToString()) + ")";
                                        for (int i = 0; i < columns.Length; i++)
                                        {
                                            dataGridView1.Columns[i].HeaderText = columns[i];
                                        }





                                        AuthorizationForm.mainForm.conn.Close();


                                    }
                                    catch (Exception ex)
                                    {
                                        AuthorizationForm.mainForm.conn.Close();
                                        MessageBox.Show("Error: " + ex.Message);
                                    }
                                    break;
                                }

                        }

                        
                        break;
                    }
                case "orders":
                    {
                        switch (int.Parse(comboBox1.Text.Split(')')[0]) - 1)
                        {

                            case 2:
                                {
                                    try
                                    {
                                        AuthorizationForm.mainForm.conn.Open();
                                        sql = "'" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        dataGridView1.DataSource = dt;
                                        sql = "select count(*) from prints as p,districts as d,types as t" +
                                            " where p.district_id=d.id and p.type_id=t.id and t.type='" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);

                                        label1.Text = label + " (выбрано " + int.Parse(AuthorizationForm.mainForm.cmd.ExecuteScalar().ToString()) + ")";
                                        for (int i = 0; i < columns.Length; i++)
                                        {
                                            dataGridView1.Columns[i].HeaderText = columns[i];
                                        }





                                        AuthorizationForm.mainForm.conn.Close();


                                    }
                                    catch (Exception ex)
                                    {
                                        AuthorizationForm.mainForm.conn.Close();
                                        MessageBox.Show("Error: " + ex.Message);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    try
                                    {
                                        AuthorizationForm.mainForm.conn.Open();
                                        sql = "select p.id,p.print,t.type,d.district,p.address,p.phone,p.year from prints as p,districts as d,types as t" +
                                            " where p.district_id=d.id and p.type_id=t.id and d.district='" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        dataGridView1.DataSource = dt;
                                        sql = "select count(*) from prints as p,districts as d,types as t" +
                                            " where p.district_id=d.id and p.type_id=t.id and d.district='" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);

                                        label1.Text = label + " (выбрано " + int.Parse(AuthorizationForm.mainForm.cmd.ExecuteScalar().ToString()) + ")";
                                        for (int i = 0; i < columns.Length; i++)
                                        {
                                            dataGridView1.Columns[i].HeaderText = columns[i];
                                        }





                                        AuthorizationForm.mainForm.conn.Close();


                                    }
                                    catch (Exception ex)
                                    {
                                        AuthorizationForm.mainForm.conn.Close();
                                        MessageBox.Show("Error: " + ex.Message);
                                    }
                                    break;
                                }
                            default:
                                {
                                    try
                                    {
                                        AuthorizationForm.mainForm.conn.Open();
                                        sql = "select * from " + table;
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        sql = "select * from " + table + " where " + dt.Columns[int.Parse(comboBox1.Text.Split(')')[0]) - 1] + " = '" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        dataGridView1.DataSource = dt;
                                        sql = "select count(*) from " + table + " where " + dt.Columns[int.Parse(comboBox1.Text.Split(')')[0]) - 1] + " = '" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);

                                        label1.Text = label + " (выбрано " + int.Parse(AuthorizationForm.mainForm.cmd.ExecuteScalar().ToString()) + ")";
                                        for (int i = 0; i < columns.Length; i++)
                                        {
                                            dataGridView1.Columns[i].HeaderText = columns[i];
                                        }





                                        AuthorizationForm.mainForm.conn.Close();


                                    }
                                    catch (Exception ex)
                                    {
                                        AuthorizationForm.mainForm.conn.Close();
                                        MessageBox.Show("Error: " + ex.Message);
                                    }
                                    break;
                                }

                        }
                        break;
                    }

                case "workers": {
                        switch (int.Parse(comboBox1.Text.Split(')')[0]) - 1)
                        {

                            case 2:
                                {
                                    try
                                    {
                                        AuthorizationForm.mainForm.conn.Open();
                                        sql = "'" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        dataGridView1.DataSource = dt;
                                        sql = "select count(*) from prints as p,districts as d,types as t" +
                                            " where p.district_id=d.id and p.type_id=t.id and t.type='" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);

                                        label1.Text = label + " (выбрано " + int.Parse(AuthorizationForm.mainForm.cmd.ExecuteScalar().ToString()) + ")";
                                        for (int i = 0; i < columns.Length; i++)
                                        {
                                            dataGridView1.Columns[i].HeaderText = columns[i];
                                        }





                                        AuthorizationForm.mainForm.conn.Close();


                                    }
                                    catch (Exception ex)
                                    {
                                        AuthorizationForm.mainForm.conn.Close();
                                        MessageBox.Show("Error: " + ex.Message);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    try
                                    {
                                        AuthorizationForm.mainForm.conn.Open();
                                        sql = "select p.id,p.print,t.type,d.district,p.address,p.phone,p.year from prints as p,districts as d,types as t" +
                                            " where p.district_id=d.id and p.type_id=t.id and d.district='" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        dataGridView1.DataSource = dt;
                                        sql = "select count(*) from prints as p,districts as d,types as t" +
                                            " where p.district_id=d.id and p.type_id=t.id and d.district='" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);

                                        label1.Text = label + " (выбрано " + int.Parse(AuthorizationForm.mainForm.cmd.ExecuteScalar().ToString()) + ")";
                                        for (int i = 0; i < columns.Length; i++)
                                        {
                                            dataGridView1.Columns[i].HeaderText = columns[i];
                                        }





                                        AuthorizationForm.mainForm.conn.Close();


                                    }
                                    catch (Exception ex)
                                    {
                                        AuthorizationForm.mainForm.conn.Close();
                                        MessageBox.Show("Error: " + ex.Message);
                                    }
                                    break;
                                }
                            default:
                                {
                                    try
                                    {
                                        AuthorizationForm.mainForm.conn.Open();
                                        sql = "select * from " + table;
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        sql = "select * from " + table + " where " + dt.Columns[int.Parse(comboBox1.Text.Split(')')[0]) - 1] + " = '" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        dataGridView1.DataSource = dt;
                                        sql = "select count(*) from " + table + " where " + dt.Columns[int.Parse(comboBox1.Text.Split(')')[0]) - 1] + " = '" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);

                                        label1.Text = label + " (выбрано " + int.Parse(AuthorizationForm.mainForm.cmd.ExecuteScalar().ToString()) + ")";
                                        for (int i = 0; i < columns.Length; i++)
                                        {
                                            dataGridView1.Columns[i].HeaderText = columns[i];
                                        }





                                        AuthorizationForm.mainForm.conn.Close();


                                    }
                                    catch (Exception ex)
                                    {
                                        AuthorizationForm.mainForm.conn.Close();
                                        MessageBox.Show("Error: " + ex.Message);
                                    }
                                    break;
                                }

                        }
                        break;
                    }
                case "customers": {
                        switch (int.Parse(comboBox1.Text.Split(')')[0]) - 1)
                        {

                            case 2:
                                {
                                    try
                                    {
                                        AuthorizationForm.mainForm.conn.Open();
                                        sql = "'" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        dataGridView1.DataSource = dt;
                                        sql = "select count(*) from prints as p,districts as d,types as t" +
                                            " where p.district_id=d.id and p.type_id=t.id and t.type='" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);

                                        label1.Text = label + " (выбрано " + int.Parse(AuthorizationForm.mainForm.cmd.ExecuteScalar().ToString()) + ")";
                                        for (int i = 0; i < columns.Length; i++)
                                        {
                                            dataGridView1.Columns[i].HeaderText = columns[i];
                                        }





                                        AuthorizationForm.mainForm.conn.Close();


                                    }
                                    catch (Exception ex)
                                    {
                                        AuthorizationForm.mainForm.conn.Close();
                                        MessageBox.Show("Error: " + ex.Message);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    try
                                    {
                                        AuthorizationForm.mainForm.conn.Open();
                                        sql = "select p.id,p.print,t.type,d.district,p.address,p.phone,p.year from prints as p,districts as d,types as t" +
                                            " where p.district_id=d.id and p.type_id=t.id and d.district='" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        dataGridView1.DataSource = dt;
                                        sql = "select count(*) from prints as p,districts as d,types as t" +
                                            " where p.district_id=d.id and p.type_id=t.id and d.district='" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);

                                        label1.Text = label + " (выбрано " + int.Parse(AuthorizationForm.mainForm.cmd.ExecuteScalar().ToString()) + ")";
                                        for (int i = 0; i < columns.Length; i++)
                                        {
                                            dataGridView1.Columns[i].HeaderText = columns[i];
                                        }





                                        AuthorizationForm.mainForm.conn.Close();


                                    }
                                    catch (Exception ex)
                                    {
                                        AuthorizationForm.mainForm.conn.Close();
                                        MessageBox.Show("Error: " + ex.Message);
                                    }
                                    break;
                                }
                            default:
                                {
                                    try
                                    {
                                        AuthorizationForm.mainForm.conn.Open();
                                        sql = "select * from " + table;
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        sql = "select * from " + table + " where " + dt.Columns[int.Parse(comboBox1.Text.Split(')')[0]) - 1] + " = '" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                                        dt = new DataTable();
                                        dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                                        dataGridView1.DataSource = dt;
                                        sql = "select count(*) from " + table + " where " + dt.Columns[int.Parse(comboBox1.Text.Split(')')[0]) - 1] + " = '" + textBox1.Text + "'";
                                        AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);

                                        label1.Text = label + " (выбрано " + int.Parse(AuthorizationForm.mainForm.cmd.ExecuteScalar().ToString()) + ")";
                                        for (int i = 0; i < columns.Length; i++)
                                        {
                                            dataGridView1.Columns[i].HeaderText = columns[i];
                                        }





                                        AuthorizationForm.mainForm.conn.Close();


                                    }
                                    catch (Exception ex)
                                    {
                                        AuthorizationForm.mainForm.conn.Close();
                                        MessageBox.Show("Error: " + ex.Message);
                                    }
                                    break;
                                }

                        }
                        break;
                    }
                

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
