using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Interfaces
{
    interface IVariable : IResetable
    {
        int Min { get; }
        int Max { get; }
        int Step { get; set; }
        int Value { get; set; }
        bool Up();
        bool Down();
    }
}
