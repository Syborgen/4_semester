using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleOOP
{
    interface IStack
    {

        void Push(object item);
        object Pop();
        object Peek();
        string ToString();

    }
}
