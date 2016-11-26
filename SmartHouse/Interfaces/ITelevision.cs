using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Interfaces
{
    interface ITelevision<T> : IEnable, IVolume, IChannel, IBrightable, IGetChannelList<T>, ISetChannelList<T>, IResetable
    {
    }
}
