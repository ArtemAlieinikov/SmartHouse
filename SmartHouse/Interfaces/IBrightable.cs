using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Enums;

namespace SmartHouse.Interfaces
{
    interface IBrightable
    {
        Brightness BrightnessLevel { get; }
        void BrightUp();
        void BrightDown();
    }
}
