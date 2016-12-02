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
                InvokeEventStatusChanged("Air conditioning: \"{0}\" has turned on.");
            }
        }
        public void Off()
        {
            if (State)
            {
                State = false;
                InvokeEventStatusChanged("Air conditioning: \"{0}\" has turned off.");
            }
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
            InvokeEventStatusChanged(String.Format("Air conditioning: \"{0}\" has increased the temperature to {0}.", freezeLevel));
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
            InvokeEventStatusChanged(String.Format("Air conditioning: \"{0}\" has reduce the temperature to {0}.", freezeLevel));
        }

        public void Reset()
        {
            freezeLevel = FreezeLevels.Low;
            InvokeEventStatusChanged("Air conditioning: \"{0}\" has reseted.");
        }

        public override string ToString()
        {
            string stringState = state ? "On" : "Off";
            return String.Format("Air conditioning: \"{0}\", state - {1}, freezing level - {2}.", Name, stringState, freezeLevel);
        }
    }
}
