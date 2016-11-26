using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Enums;

namespace SmartHouse.Interfaces
{
    interface IFreezable
    {
        void FreezUp();
        void FreezDown();
        void Defrost();
    }
}
