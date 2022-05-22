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
    public partial class SelectForm : Form
    {
        public DataTable dt;
        public string sql;
        public SelectForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0://Типографии с работником с ИМЕНЕМ
                    {
                        string result = Microsoft.VisualBasic.Interaction.InputBox("Введите имя:", "Ввод данных для запроса", "");
                        sql = @"select" +
                            " p.print as типография," +
                            " d.district as район,p.year as год_основания " +
                            " from prints as p," +
                            "  workers as w,districts as d " +
                            " where p.id=w.print_id and " +
                            " w.first_name='" + result + "' and d.id = p.district_id" +
                            " order by 2,1";
                        break;
                    }
                case 1://Заказчики, живущие в ГОРОДЕ
                    {
                        string result = Microsoft.VisualBasic.Interaction.InputBox("Введите город:", "Ввод данных для запроса", "");
                        sql = @"select" +
                            " cust.first_name as имя," +
                            " cust.second_name as фамилия," +
                            " cust.third_name as отчество," +
                            "cust.phone as телефон, cust.birthday as дата_рождения" +
                            " from customers as cust," +
                            " cities as city" +
                            " where cust.city_id=city.id and " +
                            "city.city='" + result + "'" +
                            "order by 1,2,3";
                        break;
                    }
                case 2://Заказчики с ГОДОМ рождения, совпадающим с основанием типографии
                    {
                        string result = Microsoft.VisualBasic.Interaction.InputBox("Введите город:", "Ввод данных для запроса", "");
                        sql = @"select" + " cust.first_name as имя," + " cust.second_name as фамилия," + " cust.third_name as отчество, " +
                            " p.print as типография " +
                            "from customers as cust," +
                            " orders as o," +
                            " workers as w, " +
                            "prints as p " +
                            "where cust.id = o.customer_id and " +
                            " o.worker_id=w.id and " +
                            " w.print_id=p.id and " +
                            " extract(year from cust.birthday)=p.year and " +
                            " p.year = " + result +
                            " order by p.print";
                        break;
                    }
                case 3://Заказы, принятые в ГОД, принятые через 20 лет после основания типографии
                    {
                        string result = Microsoft.VisualBasic.Interaction.InputBox("Введите год:", "Ввод данных для запроса", "");
                        sql = @"select " +
                            " o.id as номер_заказа," +
                            "o.publication as название,o.datastart as заказ_принят," +
                            "p.print as типография," +
                            "w.second_name as работник" +
                            " from workers as w," +
                            " prints as p," +
                            " orders as o " +
                            "where p.id=w.print_id and" +
                            " o.worker_id=w.id and" +
                            " (extract(year from o.datastart)-20)=p.year and" +
                            " extract(year from o.datastart)=" + result +
                            " order by p.year ";
                        break;
                    }
                case 4://Работники с типографиями и районом
                    {
                        sql = @"select " + " w.first_name as имя, " +
                            "w.second_name as фамилия," +
                            " w.third_name as отчество, " +
                            "p.print as типография ," +
                            " d.district as район " +
                            "from workers as w," +
                            " prints as p," +
                            " districts as d" +
                            " where p.id=w.print_id and " +
                            "  p.district_id=d.id" +
                            " order by 5,1,2,3";
                        break;
                    }
                case 5://Заказчики с городом и заказами
                    {
                        sql = @"select" +
                            " cust.id as номер_заказчика, " +
                            "cust.first_name as имя ," +
                            "cust.second_name as фамилия, " +
                            "cust.third_name as отчество," +
                            " c.city as город," +
                            " o.publication as заказ" +
                            " from customers as cust," +
                            " cities as c," +
                            " orders as o where cust.city_id=c.id and" +
                            " o.customer_id=cust.id " +
                            "" +
                            "order by 5,1 desc";
                        break;
                    }
                case 6://Банковские счета с банком и заказом
                    {
                        sql = @"select " +
                            "a.account as банковский_счет, " +
                            "b.bank as банк," +
                            " o.publication as заказ" +
                            " from accounts as a," +
                            " banks as b," +
                            " orders as o " +
                            "where a.account=o.account and " +
                            " a.bank_id = b.id " +
                            "" +
                            "order by 2,3 desc";
                        break;
                    }
                case 7://Банковские счета и заказы оплаченные ими
                    {
                        sql = @"select" +
                            " b.bank as банк,a.account as банковский_счет," +
                            " o.id as номер_заказа , o.publication as заказ " +
                            "from accounts as a " +
                            "left join orders as o" +
                            " on o.account=a.account join banks as b on a.bank_id=b.id" +
                            " order by 1,4 desc";
                        break;
                    }
                case 8://Города с заказчиками
                    {

                        sql = @"select " +
                            "c.id as номер_города," +
                            " c.city as Город, " +
                            "cust.first_name as имя," +
                            " cust.second_name as фамилия, " +
                            "cust.third_name as отчество " +
                            "from customers as cust" +
                            "  right join cities as c " +
                            "on c.id=cust.city_id" +
                            " order by 1 desc";
                        break;
                    }
                case 9://Банковские счета, включающие последовательность цифр 1234
                    {

                        sql = @"select" +
                            " aa.account as банковский_счет," +
                            " b.bank as банк " +
                            "from (select account,bank_id from accounts where account like '%1234%') as aa " +
                            "left join banks as b " +
                            "on aa.bank_id=b.id ";
                        break;
                    }
                case 10://Заказчики и итоговая стоимость их заказов
                    {
                        sql = @"select" +
                            " cust.id номер_заказчика, " +
                            "cust.first_name as имя, " +
                            "cust.second_name as фамилия," +
                            " cust.third_name as отчество," +
                            " sum(o.cost*o.calculation) as денег_потрачено" +
                            "  from customers as cust," +
                            "orders as o" +
                            " where o.customer_id=cust.id " +
                            "group by 1,2,3,4" +
                            " order by 5";
                        break;
                    }
                case 11://Количество работников РАЙОНА
                    {
                        string result = Microsoft.VisualBasic.Interaction.InputBox("Введите район:", "Ввод данных для запроса", "");
                        sql = @"select" +
                            " d.district as район," +
                            " count (w.*) as работников " +
                            "from prints as p, " +
                            "workers as w, " +
                            "districts as d" +
                            " where d.id=p.district_id and" +
                            " p.id=w.print_id and " +
                            "d.district='" + result + "'" +
                            "group by 1 " +
                            "order by 2";
                        break;
                    }
                case 12://Типографии, количество заказов которых больше ЧИСЛА
                    {
                        string result = Microsoft.VisualBasic.Interaction.InputBox("Введите число заказов:", "Ввод данных для запроса", "");
                        sql = @"select" +
                            " p.id as номер_типографии," +
                            " p.print as типография," +
                            " count(o.*) as количество_заказов " +
                            "from prints as p," +
                            " workers as w," +
                            " orders as o" +
                            " where p.id=w.print_id and " +
                            "o.worker_id=w.id " +
                            "group by 1,2 " +
                            "having(count(o.*)>" + result + ") " +
                            "order by 3";
                        break;
                    }
                case 13://Банковские счета БАНКА, на которых денег потрачено больше ЧИСЛА
                    {
                        string result = Microsoft.VisualBasic.Interaction.InputBox("Введите банк:", "Ввод данных для запроса", "");
                        string result2 = Microsoft.VisualBasic.Interaction.InputBox("Введите количество денег:", "Ввод данных для запроса", "");
                        sql = @"select" +
                            " a.account as банковский_счет," +
                            "  " +
                            "sum(o.cost*o.calculation) as денег_потрачено " +
                            "from accounts as a," +
                            " banks as b, " +
                            "orders as o" +
                            " where o.account=a.account and" +
                            " a.bank_id = b.id and " +
                            "b.bank='" + result + "'" +
                            " group by 1" +
                            " having (sum(o.cost*o.calculation)>" + result2 + ")" +
                            " order by 2";
                        break;
                    }
                case 14://Количество работников и заказов типографий
                    {
                        sql = @"select" +
                            " p.id as номер_типографии," +
                            " p.print as типография, " +
                            "count(w.*) as количество_работников," +
                            " sum(sel.c) as количество_заказов " +
                            " from prints as p," +
                            " workers as w," +
                            " (select wo.id as id,count(o.*) as c " +
                            "from workers as wo," +
                            " orders as o" +
                            " where wo.id=o.worker_id " +
                            "group by 1) as sel" +
                            " where p.id=w.print_id and" +
                            " sel.id=w.id " +
                            "group by 1,2 " +
                            "order by 4 desc,3";
                        break;
                    }
                case 15://Количество работников типографий, существующих больше 30 лет
                    {
                        sql = @"select p.id as номер_типографии, p.print as типография, count (w.*) as количество_работников, p.возраст, p.год_основания from workers as w, " +
                            "(select id,print,year as год_основания,(extract(year from current_date)-year) as возраст from prints" +
                            " where (extract(year from current_date)-year>=30)) as p where p.id=w.print_id group by 1,2,4,5 order by 4 desc,3";
                        break;
                    }
                case 16://Средний тираж по типографиям, районам, городу
                    {
                        sql = @"select p.print,avg(o.calculation) as средний_тираж_типография,d.district,dist.средний_тираж_район,cit.средний_тираж_город"+
                            " from districts as d, prints as p, workers as w, orders as o, ( 	select d.district,avg(o.calculation) as средний_тираж_район 	from districts as d,"+
                            " 	prints as p, workers as w, 	orders as o 	where p.id=w.print_id and 	d.id=p.district_id and 	o.worker_id = w.id 	group by 1 	order by 1) as dist,"+
                            " 	(select avg(o.calculation) as средний_тираж_город 	from districts as d, 	prints as p, workers as w, 	orders as o 	where p.id=w.print_id and "+
                            "	d.id=p.district_id and 	o.worker_id = w.id 	order by 1) as cit where p.id=w.print_id and d.id=p.district_id and o.worker_id = w.id and dist.district = d.district"+
                            "group by 1,3,4,5 order by 3,1";
                        break;
                    }

                case 17://Лучшая типография по району, городу и самое популярное изделие
                    {
                        sql = @"select p.print as типография_район,o.cost*o.calculation as сумма_заказов_район,d.district as район,maxxx.print as типография_город,"+
                            "maxxx.maxcd as сумма_заказов_город,prmax.изделие as популярное_изделие , prmax.количество_заказов_изделия 	from districts as d,"+
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
                        break;
                    }

                case 18://Стоимость типографий за ГОД не выполненные в срок
                    {
                        string result = Microsoft.VisualBasic.Interaction.InputBox("Введите год:", "Ввод данных для запроса", "");
                        sql = @"select id as номер_заказа,publication заказ,datafact дата_выполнения_факт,dataplan as дата_выполнения_план from orders"+
                            " where datafact>dataplan and extract(year from datafact) ="+ result+" order by 2";
                        break;
                    }

                default:
                    {
                        MessageBox.Show(comboBox1.SelectedIndex + "");
                        break;
                    }
            }


            try
            {
                AuthorizationForm.mainForm.conn.Open();

                AuthorizationForm.mainForm.cmd = new NpgsqlCommand(sql, AuthorizationForm.mainForm.conn);
                dt = new DataTable();
                dt.Load(AuthorizationForm.mainForm.cmd.ExecuteReader());

                AuthorizationForm.mainForm.conn.Close();

                dgvData.DataSource = null;
                dgvData.DataSource = dt;
            }
            catch (Exception ex)
            {
                AuthorizationForm.mainForm.conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void backButton_Click(object sender, EventArgs e)
        {

            this.Visible = false;
        }
    }

   

}
