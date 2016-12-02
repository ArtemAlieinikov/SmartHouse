using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;

namespace SmartHouse.Classes
{
    class Radio : Device, IEnablable, IVolume, IChannelable, IGetChannelList, IAddChannelable, IResetable
    {
        protected int actualChannel;
        protected int numberOfChannels;
        protected Dictionary<int, string[]> channelList;
        protected int volume;

        public int VolumeLevel
        {
            get
            {
                return volume;
            }

            set
            {
                if (value < 0)
                {
                    volume = 0;
                }
                else if (value > 100)
                {
                    volume = 100;
                }
                else
                {
                    volume = value;
                }
            }
        }
        public string CurrentChannel
        {
            get
            {
                if (channelList != null)
                {
                    string[] result = channelList[actualChannel];
                    return String.Format("{0} : name - {1}, frequency - {2}", actualChannel + 1, result[0], result[1]);
                }
                else
                {
                    return "There are no channels";
                }
            }
        }

        public Radio(string name)
            : base(name)
        { }

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
            VolumeLevel += 5;
        }
        public void VolumeDown()
        {
            VolumeLevel -= 5;
        }
        public void SetVolume(int number)
        {
            VolumeLevel = number;
        }

        public void ChannelUp()
        {
            if (actualChannel < channelList.Count - 1)
            {
                actualChannel++;
            }
        }
        public void ChannelDown()
        {
            if (actualChannel > 0)
            {
                actualChannel--;
            }
        }

        public List<string> GetChannelList()
        {
            List<string> result = new List<string>();
            if (channelList != null)
            {
                foreach (KeyValuePair<int, string[]> channel in channelList)
                {
                    result.Add(String.Format("{0} channel : name - {1}, frequency - {2}", channel.Key + 1, channel.Value[0], channel.Value[1]));
                }
            }
            else
            {
                throw new NullReferenceException("The channel list is missing.");
            }
            return result;
        }

        public void AddChannel(string nameOfChannel, double frequencie)
        {
            if (channelList == null)
            {
                channelList = new Dictionary<int, string[]>();
                channelList.Add(numberOfChannels, new string[] { nameOfChannel, frequencie.ToString() });
                numberOfChannels++;
            }
            else
            {
                channelList.Add(numberOfChannels, new string[] { nameOfChannel, frequencie.ToString() });
                numberOfChannels++;
            }
        }

        public void Reset()
        {
            numberOfChannels = 0;
            actualChannel = 0;
            VolumeLevel = 50;
            channelList = null;
        }

        public override string ToString()
        {
            string stringState = state ? "On" : "Off";
            return String.Format("Radio: \"{0}\", state - {1}, volume - {2}%, number of channels - {3}.\n\tCurrent channel - {4}.", Name, stringState, volume, numberOfChannels, CurrentChannel);
        }
    }
}
