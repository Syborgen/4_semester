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
using System.Windows.Forms.DataVisualization.Charting;

namespace Prints_Of_Donets_KursovayaBD_
{
    public partial class DiagramForm : Form
    {
        public int type;
        public DiagramForm()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void DiagramForm_Load(object sender, EventArgs e)
        {

            chart1.Series.Clear();
            // Форматировать диаграмму
            //Сhart1.BackColor = Color.Gray;
            //Сhart1.BackSecondaryColor = Color.WhiteSmoke;
           // Сhart1.BackGradientStyle = GradientStyle.DiagonalRight;

            chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            chart1.BorderlineColor = Color.Gray;
            chart1.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            // Форматировать область диаграммы
            //Сhart1.ChartAreas[0].BackColor = Color.Wheat;

            // Добавить и форматировать заголовок
            
            

            




            switch (type)
            {

                case 1://гистограмма
                    {
                        DataTable dt3 = new DataTable();
                        chart1.Titles.Add("Соотношение количества заказов оплаченных с помощью различных банков");
                        chart1.Series.Add(new Series("ColumnSeries")
                        {
                            
                            ChartType = SeriesChartType.Column

                        }) ;
                        
                        try
                        {
                            AuthorizationForm.mainForm.conn.Open();

                            //заполнение формы информацией о строке
                            AuthorizationForm.mainForm.cmd = new NpgsqlCommand(@"select b.bank, count(o.*) from banks as b, accounts as a,"+
                                " orders as o where o.account=a.account and  a.bank_id = b.id group by 1 ", AuthorizationForm.mainForm.conn);



                            
                            dt3.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                            //конец заполнение формы информацией о строке



                            AuthorizationForm.mainForm.conn.Close();
                        }
                        
                        catch (Exception ex)
                        {
                            AuthorizationForm.mainForm.conn.Close();
                            MessageBox.Show("Inserted fail 2. Error: " + ex.Message);
                        }
                        int i = 0;
                        string[] valuesx= new string[dt3.Rows.Count];
                        long[] valuesy= new long[dt3.Rows.Count];    //= (int[])dt3.Rows[0].ItemArray[1];
                        foreach (DataRow dr in dt3.Rows)
                        {
                            valuesx[i] = dr.ItemArray[0].ToString();
                            valuesy[i++] = (long)dr.ItemArray[1];
                        }
                        chart1.Series["ColumnSeries"].Points.DataBindXY(valuesx , valuesy);
                       
                        

                        break;
                    }
                case 2://круговая
                    {
                        DataTable dt3 = new DataTable();
                        chart1.Titles.Add("Частота использования форматов в заказах");
                        chart1.Series.Add(new Series("ColumnSeries")
                        {
                            ChartType = SeriesChartType.Pie
                        });


                        try
                        {
                            AuthorizationForm.mainForm.conn.Open();

                            //заполнение формы информацией о строке
                            AuthorizationForm.mainForm.cmd = new NpgsqlCommand(@"select f.format,count (o.*) from formats as f,orders as o where f.id=o.format_id group by 1 order by 1", AuthorizationForm.mainForm.conn);




                            dt3.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                            //конец заполнение формы информацией о строке



                            AuthorizationForm.mainForm.conn.Close();
                        }
                        catch (Exception ex)
                        {
                            AuthorizationForm.mainForm.conn.Close();
                            MessageBox.Show("Inserted fail 2. Error: " + ex.Message);
                        }
                        int i = 0;
                        string[] valuesx = new string[dt3.Rows.Count];
                        long[] valuesy = new long[dt3.Rows.Count];    //= (int[])dt3.Rows[0].ItemArray[1];
                        chart1.Legends.Add(new Legend("a"));
                        chart1.Legends["a"].Title ="Форматы";
                        foreach (DataRow dr in dt3.Rows)
                        {
                            valuesx[i] = dr.ItemArray[0].ToString();
                            valuesy[i++] = (long)dr.ItemArray[1];
                            chart1.Series["ColumnSeries"].Points.AddXY(dr.ItemArray[0].ToString()+" ("+ dr.ItemArray[1].ToString()+")", (long)dr.ItemArray[1]);
                            
                                
                        }
                        //chart1.Series["ColumnSeries"].Points.DataBindXY(valuesx, valuesy);


                        break;
                    }
                case 3://объемная
                    {
                        DataTable dt3 = new DataTable();
                        chart1.Titles.Add("Отношение количества заказчиков в разных городах");
                        chart1.Series.Add(new Series("ColumnSeries")
                        {
                            ChartType = SeriesChartType.Column
                        });


                        try
                        {
                            AuthorizationForm.mainForm.conn.Open();

                            //заполнение формы информацией о строке
                            AuthorizationForm.mainForm.cmd = new NpgsqlCommand(@"select c.city,count(cu.*) from cities as c,customers as cu where c.id=cu.city_id group by 1 ", AuthorizationForm.mainForm.conn);




                            dt3.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());
                            //конец заполнение формы информацией о строке



                            AuthorizationForm.mainForm.conn.Close();
                        }
                        catch (Exception ex)
                        {
                            AuthorizationForm.mainForm.conn.Close();
                            MessageBox.Show("Inserted fail 2. Error: " + ex.Message);
                        }
                        int i = 0;
                        string[] valuesx = new string[dt3.Rows.Count];
                        long[] valuesy = new long[dt3.Rows.Count];    //= (int[])dt3.Rows[0].ItemArray[1];
                        foreach (DataRow dr in dt3.Rows)
                        {
                            valuesx[i] = dr.ItemArray[0].ToString();
                            valuesy[i++] = (long)dr.ItemArray[1];
                        }
                        chart1.Series["ColumnSeries"].Points.DataBindXY(valuesx, valuesy);

                        chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
                        break;
                    }

                default: { break; }
            }
           

        }
    }
}
