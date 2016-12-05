using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;

namespace SmartHouse.Classes
{
    abstract class Device
    {
        protected string name;
        protected bool state;

        public event Action<string> EventStateChanged;

        /*Виртуальные свойства для случая, если потребуется реализовывать настандартную
         * логику в наследниках (При переопределении см. подстановки Лисков )*/ 
        public virtual string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        public virtual bool State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        public Device(string name)
        {
            Name = name;
        }

        public void InvokeEventStatusChanged(string message)
        {
            if (EventStateChanged != null)
            {
                EventStateChanged(message);
            }
        }
    }
}
