using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Interfaces.ChannelModule;

namespace SmartHouse.Classes
{
    class Radio : Device, IVolume, IChannelable, IGetChannelList, IAddChannelable, IResetable
    {
        private IVariable volumeModule;
        private IChannelableModule channelModule;

        /// <summary>
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Thrown when argument is null</exception>
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
                    InvokeEventStatusChanged(String.Format("{0}:\t \"{1}\" has changed the volume module.", this.GetType(), name));
                }
            }
        }
        /// <summary>
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Thrown when argument is null</exception>
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
                    InvokeEventStatusChanged(String.Format("{0}:\t \"{1}\" has changed the volume module.", this.GetType(), name));
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
                InvokeEventStatusChanged(String.Format("{0}:\t \"{1}\" has changed the volume level to {2}.", this.GetType(), name, value));
            }
        }

        public Radio(string name, IChannelableModule channelModule, IVariable volumeModule)
            : base(name)
        {
            VolumeModule = volumeModule;
            ChannelModule = channelModule;
        }

        public void VolumeUp()
        {
            if (VolumeModule.Up())
            {
                InvokeEventStatusChanged(String.Format("{0}:\t \"{1}\" has changed the volume level to {2}.", this.GetType(), name, VolumeLevel));
            }
            else { }
        }
        public void VolumeDown()
        {
            if (VolumeModule.Down())
            {
                InvokeEventStatusChanged(String.Format("{0}:\t \"{1}\" has changed the volume level to {2}.", this.GetType(), name, VolumeLevel));
            }
            else { }
        }
        public void SetVolume(int number)
        {
            VolumeModule.Value = number;
            InvokeEventStatusChanged(String.Format("{0}:\t \"{1}\" has changed volume level to {2}.", this.GetType(), name, number));
        }

        public void ChannelUp()
        {
            if (ChannelModule.Up())
            {
                InvokeEventStatusChanged(String.Format("{0}:\t \"{1}\" has changed channel to {2}.", this.GetType(), name, ChannelModule.Current));
            }
            else { }
        }
        public void ChannelDown()
        {
            if (ChannelModule.Up())
            {
                InvokeEventStatusChanged(String.Format("{0}:\t \"{1}\" has changed channel to {2}.", this.GetType(), name, ChannelModule.Current));
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
            InvokeEventStatusChanged(String.Format("{0}:\t \"{1}\" has added new {2} channel.", this.GetType(), name, nameOfChannel));
        }

        public void Reset()
        {
            IResetable channelModule = (IResetable)ChannelModule;
            channelModule.Reset();
            VolumeModule.Reset();
            InvokeEventStatusChanged(String.Format("{0}:\t \"{1}\" has reseted.", this.GetType(), name));
        }

        public override string ToString()
        {
            string stringState = state ? "On" : "Off";
            return String.Format("Radio: \"{0}\", state - {1}, volume - {2}%, number of channels - {3}.\n\tCurrent channel - {4}.", Name, stringState, VolumeLevel, ChannelModule.NumberOfChannels, CurrentChannel);
        }
    }
}
