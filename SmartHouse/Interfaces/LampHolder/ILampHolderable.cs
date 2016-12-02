using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Classes;
using SmartHouse.Interfaces.Lamp;

namespace SmartHouse.Interfaces.LampHolder
{
    interface ILampHolderable
    {
        ILampable CurrentLamp { get; set; }

        void TurnOnLamp();
        void TurnOffLamp();
    }
}
