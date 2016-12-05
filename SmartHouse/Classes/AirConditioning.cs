using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Enums;

namespace SmartHouse.Classes
{
    class AirConditioning : Device, IEnablable, IFreezable, IResetable
    {
        protected FreezeLevels freezeLevel;

        public AirConditioning(string name) : base (name)
        { }

        public void On()
        {
            if (!State)
            {
                State = true;
                InvokeEventStatusChanged(String.Format("Air conditioning: \"{0}\" has turned on.", name));
            }
            else { }
        }
        public void Off()
        {
            if (State)
            {
                State = false;
                InvokeEventStatusChanged(String.Format("Air conditioning: \"{0}\" has turned off.", name));
            }
            else { }
        }

        public void FreezUp()
        {
            if (freezeLevel == FreezeLevels.SuperHigh)
            {
                return;
            }
            else if (freezeLevel == FreezeLevels.High)
            {
                freezeLevel = FreezeLevels.SuperHigh;
            }
            else if (freezeLevel == FreezeLevels.Middle)
            {
                freezeLevel = FreezeLevels.High;
            }
            else
            {
                freezeLevel = FreezeLevels.Middle;
            }
            InvokeEventStatusChanged(String.Format("Air conditioning: \"{0}\" has changed the temperature to {1}.",name, freezeLevel));
        }
        public void FreezDown()
        {
            if (freezeLevel == FreezeLevels.Low)
            {
                return;
            }
            else if (freezeLevel == FreezeLevels.Middle)
            {
                freezeLevel = FreezeLevels.Low;
            }
            else if (freezeLevel == FreezeLevels.High)
            {
                freezeLevel = FreezeLevels.Middle;
            }
            else
            {
                freezeLevel = FreezeLevels.High;
            }
            InvokeEventStatusChanged(String.Format("Air conditioning: \"{0}\" has changed the temperature to {1}.", name, freezeLevel));
        }

        public void Reset()
        {
            freezeLevel = FreezeLevels.Low;
            InvokeEventStatusChanged(String.Format("Air conditioning: \"{0}\" has reseted.", name));
        }

        public override string ToString()
        {
            string stringState = state ? "On" : "Off";
            return String.Format("Air conditioning: \"{0}\", state - {1}, freezing level - {2}.", Name, stringState, freezeLevel);
        }
    }
}
