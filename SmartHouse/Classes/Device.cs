﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;

namespace SmartHouse.Classes
{
    abstract class Device : IEnablable
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

            protected set
            {
                state = value;
            }
        }

        public Device(string name)
        {
            Name = name;
        }

        protected void InvokeEventStatusChanged(string message)
        {
            if (EventStateChanged != null)
            {
                EventStateChanged(message);
            }
        }

        public virtual bool On()
        {
            bool result;
            if (!State)
            {
                result = true;
                State = true;
                InvokeEventStatusChanged(String.Format("{0}:\t \"{1}\" has turned on.", this.GetType(), name));
            }
            else
            {
                result = false;
            }
            return result;
        }
        public virtual bool Off()
        {
            bool result;
            if (State)
            {
                result = true;
                State = false;
                InvokeEventStatusChanged(String.Format("{0}:\t \"{1}\" has turned on.", this.GetType(), name));
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
