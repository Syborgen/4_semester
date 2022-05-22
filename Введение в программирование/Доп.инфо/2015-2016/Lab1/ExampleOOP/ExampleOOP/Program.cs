using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleOOP
{
    class Program
    {
        

        static void Main(string[] args)
        {


            Stack stack = new Stack();

            stack.Push(new Car(1,Car.CarType.LEFT));
            stack.Push(new Car(2,Car.CarType.RIGHT));
            stack.Push(new Car(3,Car.CarType.RIGHT));
            stack.Push(new Car(4,Car.CarType.LEFT));
            stack.Push(new Car(5,Car.CarType.LEFT));
            stack.Push(new Car(6,Car.CarType.RIGHT));
            stack.Push(new Car(7,Car.CarType.RIGHT));
            stack.Push(new Car(8,Car.CarType.LEFT));
            stack.Push(new Car(9,Car.CarType.LEFT));

            Console.WriteLine(stack.ToString());

            object item;

            Stack leftStack = new Stack();
            Stack rightStack = new Stack();


            while((item = stack.Pop())!=null)
            {

                Car car = (Car)item;

                if (car.Type == Car.CarType.LEFT)
                {
                    leftStack.Push(car);
                }
                else
                    if (car.Type == Car.CarType.RIGHT)
                    {
                        rightStack.Push(car);
                    }
            }


            
            Console.WriteLine(leftStack.ToString());
            Console.WriteLine(rightStack.ToString());
            Console.ReadLine();

        }
    }
}
