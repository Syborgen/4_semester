using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleOOP
{
    class Car
    {
        public enum CarType {LEFT, RIGHT}

        private int number;

        public int Number
        {
            get { return number; }
            set { if(value > 0 ) number = value; }
        }

        private CarType type;

        public CarType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Car(int number, CarType carType)
        {
            this.Number = number;
            this.Type = carType;

        }

        public override string ToString()
        {
            string s = "#"+this.Number.ToString()+"("+this.Type.ToString()+")";

            return s;           
        }

    }
}
