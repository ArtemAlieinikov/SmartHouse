using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Interfaces.ChannelModule
{
    interface IChannelWriteable
    {
        void WriteNewChannel(string name, double frequencie);
    }
}
