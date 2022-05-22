using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace VVP_lab_3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void loadButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            ofd.Title = "Выбрать картинку для обработки";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                    pictureBox1.Bounds = new Rectangle(new Point(0, 0), new Size(pictureBox1.Image.Width, pictureBox1.Image.Height));
                    label1.Text = $"{pictureBox1.Image.Width} x {pictureBox1.Image.Height} = {pictureBox1.Image.Width*pictureBox1.Image.Height}";

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void saveButton1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
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
                        pictureBox1.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        

        private void processingButton1_Click(object sender, EventArgs e)
        {
            MyLib.ProgressDelegate progress = method1;


            startWork(progressBar1, pictureBox1, progress, 0, label3) ;
            

        }


        private async void startWork(ProgressBar progressBar, PictureBox pictureBox, MyLib.ProgressDelegate progress, int number,Label label)
        {


            await Task.Run(() =>
            {

                if (pictureBox.Image != null)
                {
                    var sw = new Stopwatch();
                    
                    Invoke((MethodInvoker)(() =>
                    {
                        progressBar.Maximum = pictureBox.Image.Height;

                    }));
                    MyLib.ImageWorker imageWorker = new MyLib.ImageWorker();


                    imageWorker.Source = new Bitmap(pictureBox.Image);
                    sw.Start();
                    imageWorker.startHandle(progress);
                    sw.Stop();
                    



                    Invoke((MethodInvoker)(() =>
                    {
                        label.Text = $"{sw.ElapsedMilliseconds} milliseconds";
                        chart1.Series[0].Points.AddXY($"{pictureBox.Image.Width} x {pictureBox.Image.Height}", sw.ElapsedMilliseconds);
                        pictureBox.Image = imageWorker.Result;

                    }));
                }
            });

        }
        public void method1(double yy)
        {

            new Thread(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    progressBar1.Value = (int)(yy);
                }));
            }).Start();

        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
