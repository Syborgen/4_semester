using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace VVP_lab_3
{


    public partial class Form1 : Form
    {

        
        
        public Form1()
        {

            InitializeComponent();
            Form2 a = new Form2();
            a.Show();

        }


        private void processingButton1_Click(object sender, EventArgs e)
        {
            MyLib.ProgressDelegate progress=method1;
            startWork(progressBar1, pictureBox1, progress,0);
        }

        private async void startWork(ProgressBar progressBar, PictureBox pictureBox,MyLib.ProgressDelegate progress,int number)
        {
            await Task.Run(() =>
            {
                if (pictureBox.Image != null)
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        progressBar.Maximum = pictureBox.Image.Height;
                        
                    }));
                    MyLib.ImageWorker imageWorker = new MyLib.ImageWorker();
                    imageWorker.Source = new Bitmap(pictureBox.Image);
                    imageWorker.startHandle(progress);
                    Invoke((MethodInvoker)(() =>
                    {
                        pictureBox.Image = imageWorker.Result;
                    }));
                }
            });
        }



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void method1(double yy)
        {

            new Thread(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    progressBar1.Value = (int)( yy);
                }));
            }).Start();
            
        }
        public void method2(double yy)
        {

            new Thread(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    progressBar2.Value = (int)( yy);
                }));
            }).Start();
        }
        public void method3(double yy)
        {

            new Thread(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    progressBar3.Value = (int)( yy);
                }));
            }).Start();
        }
        public void method4(double yy)
        {

            new Thread(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    progressBar4.Value = (int)( yy);
                }));
            }).Start();
        }
        public void method5(double yy)
        {

            new Thread(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    progressBar5.Value = (int)( yy);
                }));
            }).Start();
        }
        public void method6(double yy)
        {

            new Thread(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    progressBar6.Value = (int)( yy);
                }));
            }).Start();
        }
        public void method7(double yy)
        {

            new Thread(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    progressBar7.Value = (int)( yy);
                }));
            }).Start();
        }
        public void method8(double yy)
        {

            new Thread(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    progressBar8.Value = (int)( yy);
                }));
            }).Start();
        }
        public void method9(double yy)
        {

            new Thread(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    progressBar9.Value = (int)( yy);
                }));
            }).Start();
        }
        public void method10(double yy)
        {

            new Thread(() =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    progressBar10.Value = (int)( yy);
                }));
            }).Start();
        }



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
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

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void loadButton2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            ofd.Title = "Выбрать картинку для обработки";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox2.Image = new Bitmap(ofd.FileName);
                    pictureBox2.Bounds = new Rectangle(new Point(0, 0), new Size(pictureBox2.Image.Width, pictureBox2.Image.Height));

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void loadButton3_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            ofd.Title = "Выбрать картинку для обработки";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox3.Image = new Bitmap(ofd.FileName);
                    pictureBox3.Bounds = new Rectangle(new Point(0, 0), new Size(pictureBox3.Image.Width, pictureBox3.Image.Height));

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void loadButton4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            ofd.Title = "Выбрать картинку для обработки";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox4.Image = new Bitmap(ofd.FileName);
                    pictureBox4.Bounds = new Rectangle(new Point(0, 0), new Size(pictureBox4.Image.Width, pictureBox4.Image.Height));

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void loadButton5_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            ofd.Title = "Выбрать картинку для обработки";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox5.Image = new Bitmap(ofd.FileName);
                    pictureBox5.Bounds = new Rectangle(new Point(0, 0), new Size(pictureBox5.Image.Width, pictureBox5.Image.Height));

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void loadButton6_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            ofd.Title = "Выбрать картинку для обработки";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox6.Image = new Bitmap(ofd.FileName);
                    pictureBox6.Bounds = new Rectangle(new Point(0, 0), new Size(pictureBox6.Image.Width, pictureBox6.Image.Height));

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void loadButton7_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            ofd.Title = "Выбрать картинку для обработки";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox7.Image = new Bitmap(ofd.FileName);
                    pictureBox7.Bounds = new Rectangle(new Point(0, 0), new Size(pictureBox7.Image.Width, pictureBox7.Image.Height));

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void loadButton8_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            ofd.Title = "Выбрать картинку для обработки";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox8.Image = new Bitmap(ofd.FileName);
                    pictureBox8.Bounds = new Rectangle(new Point(0, 0), new Size(pictureBox8.Image.Width, pictureBox8.Image.Height));

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void loadButton9_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            ofd.Title = "Выбрать картинку для обработки";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox9.Image = new Bitmap(ofd.FileName);
                    pictureBox9.Bounds = new Rectangle(new Point(0, 0), new Size(pictureBox9.Image.Width, pictureBox9.Image.Height));

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void loadButton10_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            ofd.Title = "Выбрать картинку для обработки";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox10.Image = new Bitmap(ofd.FileName);
                    pictureBox10.Bounds = new Rectangle(new Point(0, 0), new Size(pictureBox10.Image.Width, pictureBox10.Image.Height));

                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        private void saveButton2_Click(object sender, EventArgs e)
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
                        pictureBox2.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }
        private void saveButton3_Click(object sender, EventArgs e)
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
                        pictureBox3.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }
        private void saveButton4_Click(object sender, EventArgs e)
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
                        pictureBox4.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }
        private void saveButton5_Click(object sender, EventArgs e)
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
                        pictureBox5.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }
        private void saveButton6_Click(object sender, EventArgs e)
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
                        pictureBox6.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }
        private void saveButton7_Click(object sender, EventArgs e)
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
                        pictureBox7.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }
        private void saveButton8_Click(object sender, EventArgs e)
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
                        pictureBox8.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }
        private void saveButton9_Click(object sender, EventArgs e)
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
                        pictureBox9.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }
        private void saveButton10_Click(object sender, EventArgs e)
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
                        pictureBox10.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void processingButton2_Click(object sender, EventArgs e)
        {
            MyLib.ProgressDelegate progress = method2;
            startWork(progressBar2, pictureBox2, progress,1);
        }

        private void processingButton3_Click(object sender, EventArgs e)
        {
            MyLib.ProgressDelegate progress = method3;
            startWork(progressBar3, pictureBox3, progress, 2);
        }

        private void processingButton4_Click(object sender, EventArgs e)
        {
            MyLib.ProgressDelegate progress = method4;
            startWork(progressBar4, pictureBox4, progress, 3);
        }

        private void processingButton5_Click(object sender, EventArgs e)
        {
            MyLib.ProgressDelegate progress = method5;
            startWork(progressBar5, pictureBox5,  progress, 4);
        }

        private void processingButton10_Click(object sender, EventArgs e)
        {
            MyLib.ProgressDelegate progress = method10;
            startWork(progressBar10, pictureBox10,  progress, 5);
        }

        private void processingButton9_Click(object sender, EventArgs e)
        {
            MyLib.ProgressDelegate progress = method9;
            startWork(progressBar9, pictureBox9, progress, 6);
        }

        private void processingButton8_Click(object sender, EventArgs e)
        {
            MyLib.ProgressDelegate progress = method8;
            startWork(progressBar8, pictureBox8,progress, 7);
        }

        private void processingButton7_Click(object sender, EventArgs e)
        {
            MyLib.ProgressDelegate progress = method7;
            startWork(progressBar7, pictureBox7,  progress, 8);
        }

        private void processingButton6_Click(object sender, EventArgs e)
        {
            MyLib.ProgressDelegate progress = method6;
            startWork(progressBar6, pictureBox6, progress, 9);
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }





















        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
