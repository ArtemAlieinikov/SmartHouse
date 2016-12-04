using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Interfaces.ChannelModule
{
    interface IChannelsActionable
    {
        int NumberOfChannels { get;  }
        int ActualChannel { get; }
        string Current { get; }
        bool Up();
        bool Down();
    }
}
