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
                InvokeEventStatusChanged(String.Format("{0}:\t \"{1}\" has changed the brightness to {2}.",this.GetType(), name, value));
            }
        }

        public Lamp(string name) : base (name)
        { }

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
            return String.Format("Lamp: \"{0}\", state - {1}, brightness - {2}.", name, stringState, brightnessLevel);
        }
    }
}
