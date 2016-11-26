using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Interfaces
{
    interface IChannel
    {
        int Cnannel { get; set; }
        void ChannelUp();
        void ChannelDown();
    }
}
