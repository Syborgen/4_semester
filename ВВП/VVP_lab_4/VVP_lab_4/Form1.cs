using System;
using System.Windows.Forms;
using System.IO;

namespace VVP_lab_4
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open = new FolderBrowserDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                string path = open.SelectedPath;
                SearchFiles(path, textBox1.Text);
                MessageBox.Show($"Поиск файлов, включающих в название \" {textBox1.Text} \" закончен." +
              $" Отчет о работе расположен в папке программы.");
            }
        }

        private void SearchFiles(string path,string text)
        {
            try
            {
                string[] directories = Directory.GetDirectories(path);//пути всех директорий внутри начальной
                foreach (string s in directories)
                {
                    SearchFiles(s,text);//рекурсивный поиск скрытых системных файлов в каждой поддиректории
                }
                //запись лога в корень программы
                using (var sw = new StreamWriter($@"{Environment.CurrentDirectory}\DIRECTORY_{path.Replace("\\", "-").Replace(":", "")}_LOG.txt"))
                {
                    sw.WriteLine($"Путь каталога: {path}");
                    sw.WriteLine($"Список файлов:\n");
                    string[] files = Directory.GetFiles(path);//все файлы директории
                    bool yes = false;//параметр отбора — подходит или нет
                    foreach (string s in files)
                    {
                        FileInfo fileInf = new FileInfo(s);//информация о файле

                        if (fileInf.Name.Contains(text))//если название файла содержит текст
                            yes = true;
                        else
                            yes = false;
                        sw.WriteLine($"Имя файла: {fileInf.Name,30} Подходит: {yes,6}");//вывод в отчет 
                    }
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
