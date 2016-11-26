using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;

namespace SmartHouse.Interfaces
{
    interface IFridge : IEnable, IFreezable, IResetable, IFridgeLamp
    { }
}
