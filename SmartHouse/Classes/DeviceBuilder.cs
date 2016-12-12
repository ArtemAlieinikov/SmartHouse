using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Interfaces.Lamp;
using SmartHouse.Interfaces.ChannelModule;

namespace SmartHouse.Classes
{
    class DeviceBuilder
    {
        public event Action<string> DeviceCreated;

        private void EventCall(string message)
        {
            if (DeviceCreated != null)
            {
                DeviceCreated(message);
            }
            else { }
        }

        public Device CreateLamp(string name)
        {
            Lamp newDevice = new Lamp(name);
            EventCall(String.Format("{0}:\t \"{1}\" has created.", this.GetType(), name));

            return newDevice;
        }

        public Device CreateAirConditioning(string name)
        {
            AirConditioning newDevice = new AirConditioning(name);
            EventCall(String.Format("{0}:\t \"{1}\" has created.", this.GetType(), name));

            return newDevice;
        }

        public Device CreateFridge(string name, ILampable lamp)
        {
            Fridge newDevice = new Fridge(name, lamp);
            EventCall(String.Format("{0}:\t \"{1}\" has created.", this.GetType(), name));

            return newDevice;
        }

        public Device CreateRadio(string name, IChannelableModule channelModule, IVariable volumeModule)
        {
            Radio newDevice = new Radio(name, channelModule, volumeModule);
            EventCall(String.Format("{0}:\t \"{1}\" has created.", this.GetType(), name));

            return newDevice;
        }

        public Device CreateTV(string name, IChannelableModule channelModule, IVariable volumeModule)
        {
            TelevisionSet newDevice = new TelevisionSet(name, channelModule, volumeModule);
            EventCall(String.Format("{0}:\t \"{1}\" has created.", this.GetType(), name));

            return newDevice;
        }

        public IChannelableModule CreateChannelModule()
        {
            return new ChannelModule();
        }

        public IVariable CreateVariableModule(int min, int max, int step, int value)
        {
            return new ValueModule(min, max, step, value);
        }

        public IWritable CreateLoggingModule(string pathToFile)
        {
            return new TextLoggingModule(pathToFile);
        }
    }
}
