using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Classes
{
    class LampCreator : SimpleDeviceCreator
    {
        public override Device Create(string name)
        {
            return new Lamp(name);
        }
    }
}
