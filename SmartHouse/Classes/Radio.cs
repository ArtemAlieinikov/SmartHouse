using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Interfaces.ChannelModule;

namespace SmartHouse.Classes
{
    class Radio : Device, IEnablable, IVolume, IChannelable, IGetChannelList, IAddChannelable, IResetable
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
                    InvokeEventStatusChanged(String.Format("Radio: \"{0}\" has changed the volume module.", name));
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
                    InvokeEventStatusChanged(String.Format("Radio: \"{0}\" has changed the volume module.", name));
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
                InvokeEventStatusChanged(String.Format("Radio: \"{0}\" has changed the volume level to {1}.", name, value));
            }
        }

        public Radio(string name, IChannelableModule channelModule, IVariable volumeModule)
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
                InvokeEventStatusChanged(String.Format("Radio: \"{0}\" has turned on.", name));
            }
            else { }
        }
        public void Off()
        {
            if (State)
            {
                State = false;
                InvokeEventStatusChanged(String.Format("Radio: \"{0}\" has turned off.", name));
            }
        }

        public void VolumeUp()
        {
            if (VolumeModule.Up())
            {
                InvokeEventStatusChanged(String.Format("Radio: \"{0}\" has changed the volume level to {1}.", name, VolumeLevel));
            }
            else { }
        }
        public void VolumeDown()
        {
            if (VolumeModule.Down())
            {
                InvokeEventStatusChanged(String.Format("Radio: \"{0}\" has changed the volume level to {1}.", name, VolumeLevel));
            }
            else { }
        }
        public void SetVolume(int number)
        {
            VolumeModule.Value = number;
            InvokeEventStatusChanged(String.Format("Radio: \"{0}\" has changed volume level to {1}.", name, number));
        }

        public void ChannelUp()
        {
            if (ChannelModule.Up())
            {
                InvokeEventStatusChanged(String.Format("Radio: \"{0}\" has changed channel to {1}.", name, ChannelModule.Current));
            }
            else { }
        }
        public void ChannelDown()
        {
            if (ChannelModule.Up())
            {
                InvokeEventStatusChanged(String.Format("Radio: \"{0}\" has changed channel to {1}.", name, ChannelModule.Current));
            }
            else { }
        }

        public List<string> GetChannelList()
        {
            return ChannelModule.ReadChannelList();
        }

        public void AddChannel(string nameOfChannel, double frequencie)
        {
            ChannelModule.WriteNewChannel(nameOfChannel, frequencie);
            InvokeEventStatusChanged(String.Format("Radio: \"{0}\" has added new {1} channel.", name, nameOfChannel));
        }

        public void Reset()
        {
            IResetable channelModule = (IResetable)ChannelModule;
            channelModule.Reset();
            VolumeModule.Reset();
            InvokeEventStatusChanged(String.Format("Radio: \"{0}\" has reseted.", name));
        }

        public override string ToString()
        {
            string stringState = state ? "On" : "Off";
            return String.Format("Radio: \"{0}\", state - {1}, volume - {2}%, number of channels - {3}.\n\tCurrent channel - {4}.", Name, stringState, VolumeLevel, ChannelModule.NumberOfChannels, CurrentChannel);
        }
    }
}
