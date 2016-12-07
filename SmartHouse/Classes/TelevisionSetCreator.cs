using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Interfaces.ChannelModule;

namespace SmartHouse.Classes
{
    class TelevisionSetCreator : ComplexDeviceCreator
    {
        public override Device Create(string name, IChannelableModule channelModule, IVariable valueModule)
        {
            return new TelevisionSet(name, channelModule, valueModule);
        }
    }
}
