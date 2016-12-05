using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Interfaces.Lamp;
using SmartHouse.Enums;

namespace SmartHouse.Classes
{
    class Lamp : Device, ILampable
    {
        protected Brightness brightnessLevel;

        public Brightness BrightnessLevel
        {
            get
            {
                return brightnessLevel;
            }

            set
            {
                brightnessLevel = value;
                InvokeEventStatusChanged(String.Format("Lamp: \"{0}\" has changed the brightness to {1}.", name, value));
            }
        }

        public Lamp(string name) : base (name)
        { }

        public void On()
        {
            if (!State)
            {
                state = true;
                InvokeEventStatusChanged(String.Format("Lamp: \"{0}\" has turned on.", name));
            }
            else { }
        }
        public void Off()
        {
            if (State)
            {
                state = false;
                InvokeEventStatusChanged(String.Format("Lamp: \"{0}\" has turned off.", name));
            }
            else { }
        }

        public void BrightUp()
        {
            if (brightnessLevel == Brightness.High)
            {
                return;
            }
            else if (brightnessLevel == Brightness.Middle)
            {
                BrightnessLevel = Brightness.High;
                return;
            }
            else
            {
                BrightnessLevel = Brightness.Middle;
            }
        }
        public void BrightDown()
        {
            if (brightnessLevel == Brightness.Low)
            {
                return;
            }
            else if (brightnessLevel == Brightness.High)
            {
                brightnessLevel = Brightness.Middle;
                return;
            }
            else
            {
                brightnessLevel = Brightness.Low;
                return;
            }
        }

        public override string ToString()
        {
            string stringState = state ? "On" : "Off";
            return String.Format("Lamp: \"{0}\", state - {1}, brightness - {2}.", name, state, brightnessLevel);
        }
    }
}
