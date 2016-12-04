using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Interfaces.ChannelModule;

namespace SmartHouse.Classes
{
    class TelevisionSet : Device, IEnablable, IVolume, IChannelable, IGetChannelList, IAddChannelable, IResetable
    {
        private IVariable volumeModule;
        private IChannelableModule channelModule;

        public IVariable VolumeModule 
        {
            get 
            {
                return volumeModule;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Value module is null");
                }
                else
                {
                    volumeModule = value;
                }
            }
        }
        public IChannelableModule ChannelModule
        {
            get
            {
                return channelModule;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Channel module is null");
                }
                else
                {
                    channelModule = value;
                }
            }
        }
        public string CurrentChannel
        {
            get
            {
                return ChannelModule.Current;
            }
        }
        public int VolumeLevel
        {
            get
            {
                return volumeModule.Value;
            }

            set
            {
                volumeModule.Value = value;
            }
        }

        public TelevisionSet(string name, IChannelableModule channelModule, IVariable volumeModule)
            : base(name)
        {
            VolumeModule = volumeModule;
            ChannelModule = channelModule;
        }

        public void On()
        {
            if (!State)
            {
                State = true;
            }
        }
        public void Off()
        {
            if (State)
            {
                State = false;
            }
        }

        public void VolumeUp()
        {
            VolumeModule.Up();
        }
        public void VolumeDown()
        {
            VolumeModule.Down();
        }
        public void SetVolume(int number)
        {
            VolumeModule.Value = number;
        }

        public void ChannelUp()
        {
            ChannelModule.Up();
        }
        public void ChannelDown()
        {
            ChannelModule.Down();
        }

        public List<string> GetChannelList()
        {
            return ChannelModule.ReadChannelList();
        }

        public void AddChannel(string nameOfChannel, double frequencie)
        {
            ChannelModule.WriteNewChannel(nameOfChannel, frequencie);
        }

        public void Reset()
        {
            IResetable channelModule = (IResetable)ChannelModule;
            channelModule.Reset();
            VolumeModule.Reset();
        }

        public override string ToString()
        {
            string stringState = state ? "On" : "Off";
            return String.Format("Television: \"{0}\", state - {1}, volume - {2}%, number of channels - {3}.\n\tCurrent channel - {4}.", Name, stringState, VolumeLevel, ChannelModule.NumberOfChannels, CurrentChannel);
        }
    }
}
