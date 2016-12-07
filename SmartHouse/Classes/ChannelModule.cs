using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Interfaces.ChannelModule;

namespace SmartHouse.Classes
{
    class ChannelModule : IChannelableModule, IResetable
    {
        protected int actualChannel;
        protected int numberOfChannels;
        protected Dictionary<int, string[]> channelList;

        public int NumberOfChannels
        {
            get
            {
                return numberOfChannels;
            }
        }
        public int ActualChannel
        {
            get
            {
                return actualChannel;
            }
        }
        public string Current
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

        public bool Up()
        {
            bool result;
            if (actualChannel < channelList.Count - 1)
            {
                actualChannel++;
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public bool Down()
        {
            bool result;
            if (actualChannel > 0)
            {
                actualChannel--;
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// </summary>
        /// <returns>List of the channels</returns>
        /// <exception cref="System.NullReferenceException">Thrown when Dictionary of channels is null</exception>
        public List<string> ReadChannelList()
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

        public void WriteNewChannel(string name, double frequencie)
        {
            if (channelList == null)
            {
                channelList = new Dictionary<int, string[]>();
                channelList.Add(numberOfChannels, new string[] { name, frequencie.ToString() });
                numberOfChannels++;
            }
            else
            {
                channelList.Add(numberOfChannels, new string[] { name, frequencie.ToString() });
                numberOfChannels++;
            }
        }

        public void Reset()
        {
            actualChannel = 0;
            numberOfChannels = 0;
            channelList = null;
        }
    }
}
