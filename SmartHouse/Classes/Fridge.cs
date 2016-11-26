using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Enums;

namespace SmartHouse.Classes
{
    class Fridge : IFridge
    {
        private bool state;
        private bool glaciate;
        private int glaciationLevel;
        private FreezeLevels freezeLevel;
        public ILamp currentLamp { get; set; }

        private void AddedGlaciate()
        {
            if (glaciationLevel >= 100)
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
        }
        public void Defrost()
        {
            glaciationLevel = 0;
            glaciate = false;
        }

        public void On()
        {
            if (!state)
            {
                AddedGlaciate();
                state = true;
            }
            else { }
        }
        public void Off()
        {
            if (state)
            {
                state = false;
            }
            else { }
        }

        public void Reset()
        {
            freezeLevel = FreezeLevels.Middle;
        }

        public void TurnOnLamp()
        {
            currentLamp.On();
        }

        public void TurnOffLamp()
        {
            currentLamp.Off();
        }

        public void BrightUpLamp()
        {
            currentLamp.BrightUp();
        }

        public void BrightDownLamp()
        {
            currentLamp.BrightDown();
        }
    }
}
