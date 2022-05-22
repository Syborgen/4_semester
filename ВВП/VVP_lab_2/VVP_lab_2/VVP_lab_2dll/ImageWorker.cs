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
        public string HandlerName { get; }
        public Bitmap Source { set; get; }
        public Bitmap Result { set; get; }
        public void init(SortedList<string, object> parameters)
        {

        }
      //  public void setSource(Bitmap a)
      //  {
      //      Source = a;
      //  }
     //   public Bitmap getResult()
     //   {
     //      return Result;
     //   }
        
        public void startHandle(ProgressDelegate progress)//K-Mean method
        {
            Result = new Bitmap(Source.Width, Source.Height);
            Cluster[] clusters;//массив кластеров
            clusters = new Cluster[Cluster.scount];
            for (int i = 0; i < Cluster.scount; i++)//создание массива кластеров
            {
                clusters[i] = new Cluster(i + 1);
            }
            float param=0;
            do
            {

                for (int j = 0; j < Source.Height; j++)//перебор пикселей изображения
                {
                    for (int i = 0; i < Source.Width; i++)
                    {
                        UInt32 pixel = (UInt32)(Source.GetPixel(i, j).ToArgb());
                        float minR=257, minG = 257, minB = 257,min=1000;
                        float R = (float)((pixel & 0x00FF0000) >> 16);
                        float G = (float)((pixel & 0x0000FF00) >> 8);
                        float B = (float)((pixel & 0x000000FF));
                        int k=-1,mink=0;

                        foreach (Cluster o in clusters)
                        {
                            k += 1;
                            float ifminR = Math.Abs(R - o.curR);
                            if (ifminR < minR)
                                minR = ifminR;
                            float ifminG = Math.Abs(G - o.curG);
                            if (ifminG < minG)
                                minG = ifminG;
                            float ifminB = Math.Abs(B - o.curB);
                            if (ifminB < minB)
                                minB = ifminB;
                            float ifmin= minR + minG + minB;
                            if (ifmin < min)
                            {
                                min = ifmin;
                                mink = k;
                            }
                        }
                        clusters[mink].points.Add(new Point(i, j));
                       
                        UInt32 newPixel = 0xFF000000 | ((UInt32)clusters[mink].curR << 16) | ((UInt32)clusters[mink].curG << 8) | ((UInt32)clusters[mink].curB);


                        Result.SetPixel(i, j, Color.FromArgb((int)newPixel));
                        //progress(100 * ((double)i / Source.Width));

                    }
                    progress(100 * ((double)j / Source.Height));

                }
                //progress(100);


                param = 0;
                foreach (Cluster o in clusters)
                {
                    o.SetCenter();
                    o.points.Clear();
                    param += Math.Abs(o.curR - o.lastR) + Math.Abs(o.curG - o.lastG) + Math.Abs(o.curB - o.lastB);

                }


            } while (param>100);
            progress(100);
            





        }
    }
    public class Cluster
    {
        public float curR, curG, curB;//текущие значения центрального цвета
        public float lastR, lastG, lastB;//предыдущие значения центрального цвета
        public List<Point> points;//точки в кластере
        public static ImageWorker a { get; set; }
        public static int scount { get; set; }

        public void SetCenter()//найти среднее значение цвета в кластере
        {
            lastR = curR;
            lastG = curG;
            lastB = curB;
            curR = curG = curB = 0;
            foreach (Point i in points)
            {
                UInt32 pixel = (UInt32)a.Source.GetPixel(i.X,i.Y).ToArgb();
                curR += (float)((pixel & 0x00FF0000) >> 16);
                curG += (float)((pixel & 0x0000FF00) >> 8);
                curB += (float)((pixel & 0x000000FF));

            }
            if (points.Count != 0)
            {
                curR = curR / points.Count;
                curG = curG / points.Count;
                curB = curB / points.Count;
            }
            else
            {
                curR = 0;
                curG = 0;
                curB = 0;
            }
        }
        
        public Cluster(int k)//конструктор
        {
            points = new List<Point>();
            Random rnd = new Random();




            if (scount == 1)
            {
                curR = (float)((float)((0x00FF0000) >> 16) / 2);
                curG = (float)((float)((0x0000FF00) >> 8) / 2);
                curB = (float)((float)(0x000000FF) / 2);
            }
            else
            {
                curR = (float)(((float)((0x00FF0000) >> 16) / (scount - 1)) * (k - 1));
                curG = (float)(((float)((0x0000FF00) >> 8) / (scount - 1)) * (k - 1));
                curB = (float)(((float)(0x000000FF) / (scount - 1)) * (k - 1));
            }
        }



    }
}
