using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Classes
{
    abstract class SimpleDeviceCreator
    {
        public abstract Device Create(string name);
    }
}
