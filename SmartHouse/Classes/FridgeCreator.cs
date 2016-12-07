using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Classes
{
    class FridgeCreator : SimpleDeviceCreator
    {
        public override Device Create(string name)
        {
            return new Fridge(name);
        }
    }
}
