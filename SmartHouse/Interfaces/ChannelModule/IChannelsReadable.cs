using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Interfaces.ChannelModule
{
    interface IChannelsReadable
    {
        List<string> ReadChannelList();
    }
}
