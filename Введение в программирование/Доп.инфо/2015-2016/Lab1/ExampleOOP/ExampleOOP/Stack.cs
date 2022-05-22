using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleOOP
{

    class Stack : IStack
    {
        
        private const int maxCapacity = 100;


        private int currentIndex;

        private object[] items;



        public Stack()
        {
            
            items = new object[maxCapacity];
            currentIndex = -1;

        }


        public object Pop()
        {
            object item = null;

            if (currentIndex > -1)
            {
                item = items[currentIndex];
                currentIndex--;
            }

            return item;
        }


        public void Push(object item)
        {
            
            
            if (currentIndex < maxCapacity-1)
            {
                currentIndex++;
                items[currentIndex] = item;
            }
           
        }


        public object Peek()
        {

            return items[currentIndex];
            
        }


        public override string ToString()
        {

            string s="STACK=";

            for (int i = 0; i < currentIndex; i++)
            {
                s += items[i].ToString() + "->";

            }

            s += "TOP";

            return s;
        }


    }
}
