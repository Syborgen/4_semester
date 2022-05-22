using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    //Указатель на функцию обработки прогресса выполнения задачи
    public delegate void ProgressDelegate(double percent);

    public interface ImageHandler
    {
        //получение осмысленного имени обработчика
        string HandlerName { get; }

        //Инициализация параметров обработчика
        void init(SortedList<string, object> parameters);

        //Установка изображения-источника
        Bitmap Source { set; }

        //Получение изображения-результата
        Bitmap Result { get; }

        //Запуск обработки
        void startHandle(ProgressDelegate progress);
    }

    public class ImageWorker : ImageHandler
    {
        public string HandlerName { set; get; }
        public Bitmap Source { set; get; }
        public Bitmap Result { set; get; }
        public void init(SortedList<string, object> parameters)
        {

        }

        public void startHandle(ProgressDelegate progress)//K-Mean method
        {
            Result = new Bitmap(Source.Width, Source.Height);
            
            
            

                for (int j = 0; j < Source.Height; j++)//перебор пикселей изображения
                {
                    for (int i = 0; i < Source.Width; i++)
                    {
                        UInt32 pixel = (UInt32)(Source.GetPixel(i, j).ToArgb());
                        double R = (double)((pixel & 0x00FF0000) >> 16);
                        double G = (double)((pixel & 0x0000FF00) >> 8);
                        double B = (double)((pixel & 0x000000FF));
                    double c = (R+G+B)/ 3;

                        UInt32 newPixel = 0xFF000000 | ((UInt32)c << 16) | ((UInt32)c << 8) | ((UInt32)c);


                        Result.SetPixel(i, j, Color.FromArgb((int)newPixel));

                    
                    }

                progress(j);
                }
                
        }

    }
    
}
