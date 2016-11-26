using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Interfaces
{
    interface IRadio<T> : IEnable, IVolume, IChannel, IGetChannelList<T>, ISetChannelList<T>, IResetable
    {
    }
}
