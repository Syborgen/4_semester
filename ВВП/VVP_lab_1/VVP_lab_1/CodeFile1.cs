using System.Collections.Generic;
using System;
class VVP_lab_1//класс для мейна
{
    public static void Main()
    {
        System.Console.WriteLine("Моргунов А.\nгруппа: ПИ-18б\nВариант:18 \nЗадание: Создать алгоритм, который ищет путь от начала лабиринта(левая верхняя ячейка) до финиша(правая нижняя ячейка)");
        Stack stack;
        StackSecond stackSecond;
        char a;
        bool goFlag = true;
        Console.WriteLine("Нажмите ENTER чтобы продолжить...");
        Console.ReadLine();
        Console.Clear();
        while (goFlag)
        {
            Console.Clear();
            Console.WriteLine("1. Работа с базовым классом");
            Console.WriteLine("2. Работа с производным классом");
            Console.WriteLine("3. Выход");
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    {
                        bool param = true;
                        stack = new Stack();
                        while (param)
                        {
                            Console.Clear();
                            Console.WriteLine("1. Изменить лабиринт");
                            Console.WriteLine("2. Найти маршрут");
                            Console.WriteLine("3. Вывести лабиринт");
                            Console.WriteLine("4. Назад");
                            switch (Console.ReadKey().KeyChar)
                            {
                                case '1':
                                    {
                                        bool param2 = true;
                                        while (param2)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("1. Задать новый лабиринт вручную ");
                                            Console.WriteLine("2. Автоматически сгенерировать новый лабиринт");
                                            Console.WriteLine("3. Вывести лабиринт");
                                            Console.WriteLine("4. Назад");
                                            switch (Console.ReadKey().KeyChar)
                                            {
                                                case '1':
                                                    {

                                                        int n, m;
                                                        bool aa, bb,cc;
                                                        Console.Clear();
                                                        Console.WriteLine("Введите размеры лабиринта(MxN):");
                                                        do
                                                        {
                                                            do
                                                            {
                                                                Console.Write("Введите M = ");
                                                                aa = int.TryParse(Console.ReadLine(), out m);
                                                            } while (!aa);
                                                        } while (m < 2);
                                                        do
                                                        {
                                                            do
                                                            {
                                                                Console.Write("Введите N = ");
                                                                bb = int.TryParse(Console.ReadLine(), out n);
                                                            } while (!bb);
                                                        } while (n < 2);


                                                        int[,] l = new int[m, n];
                                                        for (int i = 0; i < m; i++)
                                                        {
                                                            for (int j = 0; j < n; j++)
                                                            {
                                                                do
                                                                {
                                                                    do
                                                                    {
                                                                        Console.Write("Введите элемент масива [{0},{1}] (ноль или единица):",i,j);
                                                                        cc = int.TryParse(Console.ReadLine(), out l[i,j]);
                                                                    } while (!cc);
                                                                } while (l[i,j] < 0|| l[i, j] > 1);
                                                            }
                                                        }








                                                        stack.SetLabirintHand(l);
                                                        

                                                        break;
                                                    }//case1
                                                case '2':
                                                    {
                                                        int n,m;
                                                        bool aa, bb;
                                                        Console.Clear();
                                                        Console.WriteLine("Введите размеры лабиринта(MxN):");
                                                        do
                                                        {
                                                            do
                                                            {
                                                                Console.Write("Введите M = ");
                                                                aa = int.TryParse(Console.ReadLine(), out m);
                                                            } while (!aa);
                                                        } while (m < 2);
                                                        do
                                                        {
                                                            do
                                                            {
                                                                Console.Write("Введите N = ");
                                                                bb = int.TryParse(Console.ReadLine(), out n);
                                                            } while (!bb);
                                                        } while (n < 2);
                                                        stack.SetLabirintAuto(m, n);

                                                        break;
                                                    }//case2
                                                case '3':
                                                    {
                                                        try { 
                                                        stack.GetLabirint();
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            Console.WriteLine("\nДля начала создайте лабиринт!");
                                                        }
                                                        Console.WriteLine("Нажмите на любую кнопку...");
                                                        Console.ReadKey();
                                                        break;
                                                    }//case3
                                                case '4':
                                                    {
                                                        param2 = false;
                                                        break;
                                                    }//case4




                                            }//switch
                                        }//while


                                        break;
                                    }//case1
                                case '2':
                                    {
                                        try
                                        {
                                            stack.FindWay(0, 0);
                                        }catch(Exception e)
                                        {
                                            Console.WriteLine("\nДля начала создайте лабиринт!");
                                        }
                                            Console.WriteLine("Нажмите на любую кнопку...");
                                        Console.ReadKey();
                                        break;
                                    }//ase2
                                case '3':
                                    {
                                        try { 
                                        stack.GetLabirint();
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine("\nДля начала создайте лабиринт!");
                                        }
                                        Console.WriteLine("Нажмите на любую кнопку...");
                                        Console.ReadKey();
                                        break;
                                    }//case3
                                case '4':
                                    {
                                        param = false;
                                        break;
                                    }//case4
                            }//switch
                        }//while
                        break;

                    }//case
                case '2':
                    {
                        bool param = true;
                        stackSecond = new StackSecond();
                        while (param)
                        {
                            Console.Clear();
                            Console.WriteLine("1. Изменить лабиринт");
                            Console.WriteLine("2. Найти маршрут");
                            Console.WriteLine("3. Вывести лабиринт");
                            Console.WriteLine("4. Назад");
                            switch (Console.ReadKey().KeyChar)
                            {
                                case '1':
                                    {
                                        bool param2 = true;
                                        while (param2)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("1. Задать новый лабиринт вручную ");
                                            Console.WriteLine("2. Автоматически сгенерировать новый лабиринт");
                                            Console.WriteLine("3. Вывести лабиринт");
                                            Console.WriteLine("4. Назад");
                                            switch (Console.ReadKey().KeyChar)
                                            {
                                                case '1':
                                                    {

                                                        int n, m;
                                                        bool aa, bb, cc;
                                                        Console.Clear();
                                                        Console.WriteLine("Введите размеры лабиринта(MxN):");
                                                        do
                                                        {
                                                            do
                                                            {
                                                                Console.Write("Введите M = ");
                                                                aa = int.TryParse(Console.ReadLine(), out m);
                                                            } while (!aa);
                                                        } while (m < 2);
                                                        do
                                                        {
                                                            do
                                                            {
                                                                Console.Write("Введите N = ");
                                                                bb = int.TryParse(Console.ReadLine(), out n);
                                                            } while (!bb);
                                                        } while (n < 2);


                                                        int[,] l = new int[m, n];
                                                        for (int i = 0; i < m; i++)
                                                        {
                                                            for (int j = 0; j < n; j++)
                                                            {
                                                                do
                                                                {
                                                                    do
                                                                    {
                                                                        Console.Write("Введите элемент масива [{0},{1}] (ноль или единица):", i, j);
                                                                        cc = int.TryParse(Console.ReadLine(), out l[i, j]);
                                                                    } while (!cc);
                                                                } while (l[i, j] < 0 || l[i, j] > 1);
                                                            }
                                                        }
                                                        stackSecond.SetLabirintHand(l);
                                                        break;
                                                    }//case1
                                                case '2':
                                                    {
                                                        int n, m;
                                                        bool aa, bb;
                                                        Console.Clear();
                                                        Console.WriteLine("Введите размеры лабиринта(MxN):");
                                                        do
                                                        {
                                                            do
                                                            {
                                                                Console.Write("Введите M = ");
                                                                aa = int.TryParse(Console.ReadLine(), out m);
                                                            } while (!aa);
                                                        } while (m < 2);
                                                        do
                                                        {
                                                            do
                                                            {
                                                                Console.Write("Введите N = ");
                                                                bb = int.TryParse(Console.ReadLine(), out n);
                                                            } while (!bb);
                                                        } while (n < 2);
                                                        stackSecond.SetLabirintAuto(m, n);

                                                        break;
                                                    }//case2
                                                case '3':
                                                    {
                                                        try
                                                        {
                                                            stackSecond.GetLabirint();
                                                        }
                                                        catch (Exception e)
                                                        {
                                                            Console.WriteLine("\nДля начала создайте лабиринт!");
                                                        }
                                                        Console.WriteLine("Нажмите на любую кнопку...");
                                                        Console.ReadKey();
                                                        break;
                                                    }//case3
                                                case '4':
                                                    {
                                                        param2 = false;
                                                        break;
                                                    }//case4




                                            }//switch
                                        }//while


                                        break;
                                    }//case1
                                case '2':
                                    {
                                        try
                                        {
                                            stackSecond.FindWay(0, 0);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine("\nДля начала создайте лабиринт!");
                                        }
                                        Console.WriteLine("Нажмите на любую кнопку...");
                                        Console.ReadKey();
                                        break;
                                    }//ase2
                                case '3':
                                    {
                                        try
                                        {
                                            stackSecond.GetLabirint();
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine("\nДля начала создайте лабиринт!");
                                        }
                                        Console.WriteLine("Нажмите на любую кнопку...");
                                        Console.ReadKey();
                                        break;
                                    }//case3
                                case '4':
                                    {
                                        param = false;
                                        break;
                                    }//case4
                            }//switch
                        }//while
                        break;

                    }//case
                case '3':
                    {
                        goFlag = false;
                        break;
                    }//case


            }//switch
                
            




        }//while













    }//main
}
interface LabirintInterface//интерфейс работы с лабиринтом
{
    void SetLabirintAuto(int m,int n);//задать лабиринт
    void SetLabirintHand(int[,] l);
    void FindWay(int a, int b);//найти путь
    void GetLabirint();//вывод матрицы в консоль
}
class Node//звено стека(путь к выходу)
{
    public Node(int xx,int yy)
    {
        x = xx;
        y = yy;
    }
    static Node()
    {
        Console.WriteLine("Вызван статический конструктор класса Node");
    }
    public Node()
    {
        x = 0;
        y = 0;
    }
    private int x;
    private int y;
    public int GetY()
    {
        return y;
    }
    public int GetX()
    {
        return x;
    }
    public override string ToString()
    {
        return x + " " + y;
    }
}//Node
class Stack : LabirintInterface//класс, реализующий интерфейс(базовый класс) + реализация стека
{
    int m, n;//размер матрицы
    private int[,] labirint;
    static Stack()
    {
        Console.WriteLine("Вызван статический конструктор класса Stack");
    }
    public Stack()
    {
        m = 10;
        n = 10;
    }
    public Stack(int m, int n)
    {
        this.m = m;//ширина
        this.n = n;//высота
    }
    private List<Node> items = new List<Node>();
    virtual public void AddItem(int x, int y)//добавление элемента в стек
    {
        items.Add(new Node(x, y));
    }
    virtual public Node DeleteItem()//удаление последнего элемента в стеке
    {
        Node a = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return a;
    }
    virtual public void GetItems()//вывод пути для прохождения лабиранта
    {
        foreach (Node i in items)
        {
            System.Console.WriteLine(i);
        }
    }
    virtual public void SetLabirintHand(int[,] l)
    {
        m = l.GetLength(0);
        n = l.GetLength(1);
        labirint = l;
        labirint[m - 1, n - 1] = 0;//отчистка финиша
    }
    virtual public void GetLabirint()//вывод лабиринта на экран
    {
        System.Console.WriteLine();
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                System.Console.Write(labirint[i, j] + " ");
            }
            System.Console.WriteLine();
        }//for
    }//getlbirint
    virtual public void SetLabirintAuto(int m, int n)//задать лабиринт
    {
        this.m = m;
        this.n = n;
        labirint = new int[m, n];
        Random rnd = new Random();
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (rnd.Next(0, 3) > 1)
                    labirint[i, j] = 1;
                else labirint[i, j] = 0;
            }
        }//for
        labirint[m - 1, n - 1] = 0;//отчистка финиша
    }//SetLabirint
    virtual public void FindWay(int a, int b)//a,b = start position
    {
        int curi = a, curj = b;
        Node deletedNode;
        bool param = true;
        if (labirint[a, b] == 2)
        {
            System.Console.WriteLine("Поля, которые прошел алгоритм в поисках верного пути(двойки):");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    System.Console.Write(labirint[i, j] + " ");
                }
                System.Console.WriteLine();
            }//for
            System.Console.WriteLine();
            System.Console.WriteLine("Путь, ведущий к финишу:");
            if (items.Count == 1) { System.Console.WriteLine("Не найден (финиш закрыт наглухо)"); }
            else
            {
                foreach (Node i in items)
                    System.Console.Write(i + " -> ");
                System.Console.WriteLine("end");
            }//else
            return;
        }
            
        if (labirint[a, b] == 1)
        { System.Console.WriteLine("\nСтарт заблокирован, пересоздайте лабиринт"); }
        else
        if (labirint[m - 1, n - 1] == 1)
        {
            System.Console.WriteLine("\nФиниш заблокирован, пересоздайте лабиринт");
        }//if
        else
        {
            labirint[curi, curj] = 2;
            items.Add(new Node(curi, curj));
            while (param)
            {
                if (curj != n - 1 && labirint[curi, curj + 1] == 0)
                {
                    labirint[curi, 1+curj] = 2;
                    items.Add(new Node(curi, ++curj));
                }//if
                else
                {
                    if (curi != m - 1 && labirint[curi + 1, curj] == 0)
                    {
                        labirint[1+curi, curj] = 2;
                        items.Add(new Node(++curi, curj));
                    }//if
                    else
                    {
                        if (curj != 0 && labirint[curi, curj - 1] == 0)
                        {
                            labirint[curi, curj-1] = 2;
                            items.Add(new Node(curi, --curj));

                        }//if
                        else
                        {
                            if (curi != 0 && labirint[curi - 1, curj] == 0)
                            {
                                labirint[curi-1, curj] = 2;
                                items.Add(new Node(--curi, curj));
                            }//if
                            else
                            {
                                if (items.Count == 1)
                                {
                                    param = false;
                                }//if
                                else
                                {
                                    deletedNode = DeleteItem();
                                    curi = deletedNode.GetX();
                                    curj = deletedNode.GetY();
                                }//else
                            }//else
                        }//else
                    }//else
                }//else
                if (curi == m - 1 & curj == n - 1)
                {
                    param = false;
                }
            }//while
            System.Console.WriteLine("Поля, которые прошел алгоритм в поисках верного пути(двойки):");
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    System.Console.Write(labirint[i, j] + " ");
                }
                System.Console.WriteLine();
            }//for
            System.Console.WriteLine();
            System.Console.WriteLine("Путь, ведущий к финишу:");
            if (items.Count == 1) { System.Console.WriteLine("Не найден (финиш закрыт наглухо)"); }
            else
            {
                foreach (Node i in items)
                    System.Console.Write(i + " -> ");
                System.Console.WriteLine("end");
            }//else
        }//else
    }//FindWay
    public override string ToString()
    {
        String s = "Размер лабиринта " + m + "x" + n + ", для его просмотра вызовите метод GetLabirint()";
        return s;
    }
}//stack
class StackSecond : Stack//класс наследник
{
    public StackSecond(int m,int n) : base(m,n)
    {
        Console.WriteLine("(Производный класс)Контруктор базового класса с параметрами был вызван");
    }
    public StackSecond() : base()
    {
        Console.WriteLine("(Производный класс)Вызван конструктор по умолчанию класса Stack");
    }
    static StackSecond()
    {
        Console.WriteLine("Вызван статический конструктор класса StackSecond");
    }

    private List<Node> items = new List<Node>();
    override public void AddItem(int x, int y)//добавление элемента в стек
    {
        System.Console.WriteLine("(Производный класс) AddItem");
        base.AddItem(x,y);
    }
    override public Node DeleteItem()//удаление последнего элемента в стеке
    {
        System.Console.WriteLine("(Производный класс) DeleteItem");
        return base.DeleteItem();
    }
    override public void GetItems()//вывод пути для прохождения лабиранта
    {
        System.Console.WriteLine("(Производный класс) GetItems");
        base.GetItems();
    }
    override public void GetLabirint()//вывод лабиринта
    {
        System.Console.WriteLine("(Производный класс) GetLabirint");
        base.GetLabirint();
    }
    override public void SetLabirintAuto(int m,int n)
    {
        System.Console.WriteLine("(Производный класс) SetLabirint");
        base.SetLabirintAuto(m,n);
    }//SetLabirint
    override public void FindWay(int a, int b)//a,b = start position
    {
        System.Console.WriteLine("(Производный класс) FindWay");
        base.FindWay(a, b);
    }//FindWay
    public override string ToString()
    {
        return base.ToString();
    }
}//stack