using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Classes
{
    class AirConditioningCreator : SimpleDeviceCreator
    {
        public override Device Create(string name)
        {
            return new AirConditioning(name);
        }
    }
}
