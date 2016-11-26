using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Enums;

namespace SmartHouse.Classes
{
    class Lamp : ILamp
    {
        private bool state;
        private Brightness brightnessLevel;

        public bool State
        {
            get
            {
                return state;
            }
        }
        public Brightness BrightnessLevel
        {
            get
            {
                return brightnessLevel;
            }
        }

        public void On()
        {
            if (!State)
            {
                state = true;
            }
            else { }
        }
        public void Off()
        {
            if (State)
            {
                state = false;
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
                brightnessLevel = Brightness.High;
                return;
            }
            else
            {
                brightnessLevel = Brightness.Middle;
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
            string stringBrightness;
            string stringState = state ? "On" : "Off";

            if (BrightnessLevel == Brightness.High)
            {
                stringBrightness = "High";
            }
            else if (BrightnessLevel == Brightness.Middle)
            {
                stringBrightness = "Middle";
            }
            else
            {
                stringBrightness = "Low";
            }
            return String.Format("State {0}, {1} brightness", state, stringBrightness);
        }
    }
}
