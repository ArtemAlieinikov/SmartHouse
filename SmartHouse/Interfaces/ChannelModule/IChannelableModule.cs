using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces.ChannelModule;

namespace SmartHouse.Interfaces.ChannelModule
{
    interface IChannelableModule : IChannelsActionable, IChannelWriteable, IChannelsReadable
    {
    }
}
