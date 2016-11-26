using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Interfaces
{
    interface IVolume
    {
        int VolumeLevel { get; set; }
        void VolumeUp();
        void VolumeDown();
    }
}
