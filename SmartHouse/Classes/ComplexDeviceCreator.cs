using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces.ChannelModule;
using SmartHouse.Interfaces;

namespace SmartHouse.Classes
{
    abstract class ComplexDeviceCreator
    {
        public abstract Device Create(string name, IChannelableModule channelModule, IVariable valueModule);
    }
}
