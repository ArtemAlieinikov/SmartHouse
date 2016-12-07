using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Interfaces.ChannelModule;

namespace SmartHouse.Classes
{
    class RadioCreator : ComplexDeviceCreator
    {
        public override Device Create(string name, IChannelableModule channelModule, IVariable valueModule)
        {
            return new Radio(name, channelModule, valueModule);
        }
    }
}
