using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;

namespace SmartHouse.Classes
{
    class ValueModule : IVariable
    {
        protected int min;
        protected int max;
        protected int step;
        protected int valueField;

        public int Min
        {
            get
            {
                return min;
            }
        }
        public int Max
        {
            get
            {
                return max;
            }
        }
        public int Step
        {
            get
            {
                return step;
            }

            set
            {
                if (value > Max)
                {
                    step = Max;
                }
                else if (value < Min)
                {
                    step = Min;
                }
                else
                {
                    step = value;
                }
            }
        }
        public int Value
        {
            get
            {
                return valueField;
            }

            set
            {
                if (value > Max)
                {
                    valueField = Max;
                }
                else if (value < Min)
                {
                    valueField = Min;
                }
                else
                {
                    valueField = value;
                }
            }
        }

        public ValueModule(int min, int max, int step, int value)
        {
            if (max <= min)
            {
                throw new FormatException("Min value can not be largest that max value.");
            }
            else if ((max == 0) || (min == 0))
            {
                throw new FormatException("Max and min value can not be zero");
            }
            else if ((max < 0) || (min < 0))
            {
                throw new FormatException("Max and min value can not be smallar that zero");
            }
            else
            {
                this.min = min;
                this.max = max;
                Step = step;
                Value = value;
            }
        }

        public bool Up()
        {
            bool result;
            if (Value < Max)
            {
                Value += Step;
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
        public bool Down()
        {
            bool result;
            if (Value > Min)
            {
                Value -= Step;
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public void Reset()
        {
            Value /= 2;
        }
    }
}
