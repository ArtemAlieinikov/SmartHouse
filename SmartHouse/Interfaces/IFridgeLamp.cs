using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Interfaces
{
    interface IFridgeLamp
    {
        ILamp currentLamp { get; set; }
        void TurnOnLamp();
        void TurnOffLamp();
        void BrightUpLamp();
        void BrightDownLamp();
    }
}
