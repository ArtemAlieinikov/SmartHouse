using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Interfaces
{
    interface ISetChannelList<T>
    {
        void SetChannelList(List<T> channelList);
    }
}
