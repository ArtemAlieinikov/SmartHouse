using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Enums;
using SmartHouse.Interfaces.Lamp;
using SmartHouse.Interfaces.LampHolder;

namespace SmartHouse.Classes
{
    class Fridge : Device, IEnablable, IFreezable, IDefrostable, IResetable, ILampHolderable, ILampHolderBrightable
    {
        protected bool glaciate;
        protected int glaciationLevel;
        protected FreezeLevels freezeLevel;
        protected ILampable currentLamp;

        public ILampable CurrentLamp
        {
            get
            {
                return currentLamp;
            }

            set
            {
                currentLamp = value;
                InvokeEventStatusChanged(String.Format("Fridge: \"{0}\" has changed the lamp to {1}.", name, ((Device)value).Name));
            }
        }

        public Fridge(string name) : base (name)
        { }
        public Fridge(string name, ILampable currentLamp) : base (name)
        {
            CurrentLamp = currentLamp;
        }

        private void AddedGlaciate()
        {
            if (glaciationLevel <= 100)
            {
                if (freezeLevel == FreezeLevels.Low)
                {
                    glaciationLevel += 5;
                }
                else if (freezeLevel == FreezeLevels.Middle)
                {
                    glaciationLevel += 10;
                }
                else if (freezeLevel == FreezeLevels.High)
                {
                    glaciationLevel += 15;
                }
                else
                {
                    glaciationLevel += 20;
                }
            }
            else
            {
                glaciate = true;
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
            InvokeEventStatusChanged(String.Format("Fridge: \"{0}\" has changed the temperature to {1}.", name, freezeLevel));
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
            InvokeEventStatusChanged(String.Format("Fridge: \"{0}\" has changed the temperature to {1}.", name, freezeLevel));
        }

        public void Defrost()
        {
            glaciationLevel = 0;
            glaciate = false;
            InvokeEventStatusChanged(String.Format("Fridge: \"{0}\" has defrosted.", name));
        }

        public void On()
        {
            if (!state)
            {
                AddedGlaciate();
                state = true;
                InvokeEventStatusChanged(String.Format("Fridge: \"{0}\" has turned on.", name));
            }
            else { }
        }
        public void Off()
        {
            if (state)
            {
                state = false;
                InvokeEventStatusChanged(String.Format("Fridge: \"{0}\" has turned off.", name));
            }
            else { }
        }

        public void Reset()
        {
            freezeLevel = FreezeLevels.Low;
            InvokeEventStatusChanged(String.Format("Fridge: \"{0}\" has reseted.", name));
        }

        public void TurnOnLamp()
        {
            if (CurrentLamp != null)
            {
                InvokeEventStatusChanged(String.Format("Fridge: \"{0}\" has turned on the {1} lamp", name, ((Device)currentLamp).Name));
                CurrentLamp.On();
            }
            else { }
        }
        public void TurnOffLamp()
        {
            if (CurrentLamp != null)
            {
                InvokeEventStatusChanged(String.Format("Fridge: \"{0}\" has turned off the {1} lamp", name, ((Device)currentLamp).Name));
                CurrentLamp.Off();
            }
            else { }
        }

        public void BrightUpLamp()
        {
            if (CurrentLamp != null)
            {
                InvokeEventStatusChanged(String.Format("Fridge: \"{0}\" has changed the {1} lapm brightness to {2}.", name, ((Device)currentLamp).Name, CurrentLamp.BrightnessLevel));
                CurrentLamp.BrightUp();
            }
            else { }
        }
        public void BrightDownLamp()
        {
            if (CurrentLamp != null)
            {
                InvokeEventStatusChanged(String.Format("Fridge: \"{0}\" has changed the {1} lapm brightness to {2}.", name, ((Device)currentLamp).Name, CurrentLamp.BrightnessLevel));
                CurrentLamp.BrightDown();
            }
            else { }
        }

        public override string ToString()
        {
            string stringState = state ? "On" : "Off";

            string firstPartResult = String.Format("Fridge: \"{0}\", state - {1}, freezing level - {2}, ", Name, stringState, freezeLevel);
            string secondPartResult = String.Format("glaciate level - {0}%, is glaciate - {1}.\n\t{2}", glaciationLevel, glaciate, CurrentLamp.ToString());

            return String.Concat(firstPartResult, secondPartResult);
        }
    }
}
