using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VVP_lab_2
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            ofd.Title = "Выбрать картинку для обработки";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                    pictureBox1.Bounds = new Rectangle(new Point(0,0),new Size(pictureBox1.Image.Width, pictureBox1.Image.Height));

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if(pictureBox2.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить картинку как...";
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;
                sfd.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.PNG)|*.PNG|All Files(*.*)|*.*";
                sfd.ShowHelp = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox2.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void processingButton_Click(object sender, EventArgs e)
        {

            Console.WriteLine("------------------------------");
            count = 0;
            if (pictureBox1.Image != null)
            {
                
                progressBar1.Value = 0;
                MyLib.ImageWorker imageWorker = new MyLib.ImageWorker();
                MyLib.ProgressDelegate progress;
                progress =  method;
                imageWorker.Source= new Bitmap(pictureBox1.Image);
                progressBar1.Maximum = pictureBox1.Image.Height;
                imageWorker.startHandle(progress);
                pictureBox2.Image = imageWorker.Result;
                pictureBox2.Bounds = new Rectangle(new Point(0, 0), new Size(pictureBox1.Image.Width, pictureBox1.Image.Height));
            }
        }
        int count = 0;
        public void method(double yy)
        {

            progressBar1.Value = (int)yy;
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }


    
}
