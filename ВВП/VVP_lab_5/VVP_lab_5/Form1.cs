using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace VVP_lab_5
{
    public partial class Form1 : Form
    {
        private Ser ser;
        //private SortedList<int, int> slist;
        private List<int> list;
        private XmlSerializer xml;

        public Form1()
        {
            InitializeComponent();
            ser = new Ser();
            //slist = new SortedList<int, int>();
            list = new List<int>();
            // передаем в конструктор тип класса
            xml = new XmlSerializer(typeof(Ser));

        }

        private void button1_Click(object sender, EventArgs e)//сериализация
        {
            try
            {
                int[] tmp = new int[5];
                tmp[0] = (int)arr1.Value;
                arr1.Value = 0;
                tmp[1] = (int)arr2.Value;
                arr2.Value = 0;
                tmp[2] = (int)arr3.Value;
                arr3.Value = 0;
                tmp[3] = (int)arr4.Value;
                arr4.Value = 0;
                tmp[4] = (int)arr5.Value;
                arr5.Value = 0;
                ser.Int = tmp;
                ser.Name = name.Text;
                name.Clear();
                ser.List = list;
                slistL.Clear();
                key.Value = 0;
                ser.Class = new A((int)aint.Value, (double)adou.Value);
                aint.Value = 0;
                adou.Value = 0;

                using (MemoryStream ms1 = new MemoryStream())
                {
                    if (pictureBox1.Image != null)
                    {
                        ((Bitmap)pictureBox1.Image).Save(ms1, ImageFormat.Bmp);
                        ser.bitmap = ms1.ToArray();
                        pictureBox1.Image.Dispose();
                    }
                }
                pictureBox1.Image = null;
                if(File.Exists($@"{Environment.CurrentDirectory}\ser.xml"))
                File.Delete($@"{Environment.CurrentDirectory}\ser.xml");
                // получаем поток, куда будем записывать сериализованный объект
                using (FileStream fs = new FileStream($@"{Environment.CurrentDirectory}\ser.xml", FileMode.OpenOrCreate))
                {
                    xml.Serialize(fs, ser);

                    MessageBox.Show("Сериализация успешна");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
                MessageBox.Show(e1.StackTrace);
                MessageBox.Show(e1.InnerException.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // десериализация
                using (FileStream fs = new FileStream($@"{Environment.CurrentDirectory}\ser.xml", FileMode.Open))
                {
                    ser = (Ser)xml.Deserialize(fs);

                    MessageBox.Show("Десериализация успешна");
                }
                arr1.Value = ser.Int[0];
                arr2.Value = ser.Int[1];
                arr3.Value = ser.Int[2];
                arr4.Value = ser.Int[3];
                arr5.Value = ser.Int[4];
                name.Text = ser.Name;
                string tmp = "";
                int[] keys = ser.List.ToArray();
                for (int i = 0; i < keys.Length; i++)
                {
                    tmp += $"{keys[i]},";
                }
                tmp.TrimEnd(',');
                slistL.Text = tmp;
                aint.Value = ser.Class.Int;
                adou.Value = (int)ser.Class.Double;
                using (MemoryStream ms = new MemoryStream(ser.bitmap))
                {
                    
                        pictureBox1.Image = new Bitmap(ms);
                    
                }

            }
            catch (Exception e4)
            {
                MessageBox.Show(e4.Message);
                MessageBox.Show(e4.StackTrace);
                MessageBox.Show(e4.InnerException.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)//добавление
        {
            try
            {
                
                list.Add((int)key.Value);
                string tmp = "";
                int[] keys = list.ToArray();
                for (int i = 0; i < keys.Length; i++)
                {
                    tmp += $"{keys[i]},";
                }
                tmp.TrimEnd(',');
                slistL.Text = tmp;
            }
            catch
            {
                MessageBox.Show("Ключ не должен повторяться!");
            }

        }

        private void button4_Click(object sender, EventArgs e)
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


    [Serializable]
    public class Ser
    {
        public string Name { get; set; }
        public int[] Int { get; set; }
        public List<int> List { get; set; }
        public A Class { get; set; }
        public Ser()
        {

        }
        public byte[] bitmap { get; set; }
    }

    [Serializable]
    public class A
    {
        public int Int { get; set; }
        public double Double { get; set; }
        public A()
        {

        }
        public A(int INT, double DOUBLE)
        {
            Int = INT;
            Double = DOUBLE;
        }
    }


}
