using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Interfaces.Lamp;
using SmartHouse.Interfaces.ChannelModule;
using SmartHouse.Interfaces.LampHolder;
using System.IO;

namespace SmartHouse.Classes
{
    class Menu
    {
        private DeviceBuilder builder = new DeviceBuilder();
        private IWritable loggingModule;
        private Dictionary<string, Device> deviceList = new Dictionary<string, Device>();
        private string[] command = null;

        public void Run()
        {
            IsLogging();
            CreateStartState();
            while (true)
            {
                Console.Clear();
                ShowInfoAboutDevices();
                if (command == null)
                {
                    command = Console.ReadLine().Split(new char[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries);
                }

                try
                {
                    if (command.Length == 0)
                    {
                        throw new ArgumentException("Command is wrong");
                    }
                    else if ("exit" == command[0].ToLower())
                    {
                        break;
                    }
                    else if (!deviceList.ContainsKey(command[0]) && !CheckDeviseType(command[0]))
                    {
                        throw new ArgumentException("Wrong device name or device type");
                    }
                    else if (command.Length == 2)
                    {
                        GoTwoWordsCommands();
                        command = null;
                    }
                    else if ("lamp" == command[1].ToLower())
                    {
                        GoToLampCommands();
                        command = null;
                    }
                    else if ("volume" == command[1].ToLower())
                    {
                        GoToVolumeCommands();
                        command = null;
                    }
                    else if ("bright" == command[1].ToLower())
                    {
                        GoToBrightCommands();
                        command = null;
                    }
                    else if ("temp" == command[1].ToLower())
                    {
                        GoToTempCommands();
                        command = null;
                    }
                    else if ("channel" == command[1].ToLower())
                    {
                        GoToChannelCommands();
                        command = null;
                    }
                    else if ("add" == command[1].ToLower())
                    {
                        GoToAddChannels();
                        command = null;
                    }
                    else
                    {
                        command = null;
                        throw new ArgumentException("Command is wrong");
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    ShowInfoAboutDevices();
                    ShowCommands();
                    Console.WriteLine(e.Message.ToUpper());
                    command = Console.ReadLine().Split('_', ' ');
                }
            }
        }

        /// <summary>
        /// Creates start devices and signs the events. Works with console.
        /// </summary>
        private void CreateStartState()
        {
            SignBuilderEvent();

            Device lampForFridge = builder.CreateLamp("FridgeLamp");

            IVariable volumeModuleRadio = builder.CreateVariableModule(1, 100, 10, 50);
            IVariable volumeModuleTV = builder.CreateVariableModule(1, 100, 5, 50);

            IChannelableModule channelModuleRadio = builder.CreateChannelModule();
            IChannelableModule channelModuleTV = builder.CreateChannelModule();

            deviceList.Add("HallWay", builder.CreateLamp("HallWay"));
            deviceList.Add("SAMSUNG", builder.CreateAirConditioning("SAMSUNG"));
            deviceList.Add("ORISTON", builder.CreateFridge("ORISTON", (ILampable)lampForFridge));
            deviceList.Add("BEREZA", builder.CreateTV("BEREZA", channelModuleTV, volumeModuleTV));
            deviceList.Add("SVOBODA", builder.CreateRadio("SVOBODA", channelModuleRadio, volumeModuleRadio));

            foreach (KeyValuePair<string, Device> item in deviceList)
            {
                SignEvent(item.Value);
            }
        }

        /// <summary>
        /// Signs logging module on the object builder event
        /// </summary>
        private void SignBuilderEvent()
        {
            if (loggingModule != null)
            {
                builder.DeviceCreated += loggingModule.Write;
            }
            else { }
        }

        private void ShowInfoAboutDevices()
        {
            Console.SetWindowSize(100, 55);
            foreach (KeyValuePair<string, Device> device in deviceList)
            {
                Console.WriteLine(device.Value.ToString());
                Console.WriteLine();
            }
            Console.WriteLine(new String('*', 95) + "\n");
        }

        private bool CheckDeviseType(string type)
        {
            if ("fridge" == type.ToLower() || "lamp" == type.ToLower() || "conditioning" == type.ToLower() || "tv" == type.ToLower() || "radio" == type.ToLower())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Asks the client about logging and created logging folder
        /// </summary>
        private void IsLogging()
        {
            while (true)
            {
                Console.Write("Do you want save the log? (Yes/No) : ");
                string command = Console.ReadLine();
                if ("yes" == command.ToLower())
                {
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Please write diretcory to save the log file (C:\\YourFolder...)");
                            string path = Console.ReadLine();
                            loggingModule = builder.CreateLoggingModule(path);
                            break;
                        }
                        catch (DirectoryNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    }
                    break;
                }
                else if ("no" == command.ToLower())
                {
                    break;
                }
                else
                { }
            }
        }

        /// <summary>
        /// Signs logging module on the device event
        /// </summary>
        private void SignEvent(Device device)
        {
            if (loggingModule != null)
            {
                device.EventStateChanged += loggingModule.Write;
            }
            else { }
        }

        private void ShowCommands()
        {
            Console.WriteLine("Create device");
            Console.WriteLine("\t- DeviseType_create");
            Console.WriteLine("Delete device");
            Console.WriteLine("\t- DeviceName_delete");
            Console.WriteLine("Turn on / off device");
            Console.WriteLine("\t- DeviseName_on");
            Console.WriteLine("\t- DeviseName_off");
            Console.WriteLine("Reset setting");
            Console.WriteLine("\t- DeviseName_Reset");
            Console.WriteLine("Turn on / off device lamp");
            Console.WriteLine("\t- DeviseName_lamp_on");
            Console.WriteLine("\t- DeviseName_lamp_off");
            Console.WriteLine("Change the volume");
            Console.WriteLine("\t- DeviseName_volume_set_number");
            Console.WriteLine("\t- DeviseName_volume_up");
            Console.WriteLine("\t- DeviseName_volume_down");
            Console.WriteLine("Change the bright");
            Console.WriteLine("\t- DeviseName_bright_up");
            Console.WriteLine("\t- DeviseName_bright_down");
            Console.WriteLine("Change the temperature");
            Console.WriteLine("\t- DeviseName_temp_up");
            Console.WriteLine("\t- DeviseName_temp_down");
            Console.WriteLine("Defrost the fridge");
            Console.WriteLine("\t- DeviseName_defrost");
            Console.WriteLine("Add new channel");
            Console.WriteLine("\t- DeviseName_add_channelName_frequencie");
            Console.WriteLine("\tfor example : LG_add_TestChannel_33.3");
            Console.WriteLine("Change the channel");
            Console.WriteLine("\t- DeviseName_channel_up");
            Console.WriteLine("\t- DeviseName_channel_down");
            Console.WriteLine("Write the channel list");
            Console.WriteLine("\t- DeviseName_write");
            Console.WriteLine("Close the application");
            Console.WriteLine("\t- Exit");
        }

        private void GoTwoWordsCommands()
        {
            if (CheckDeviseType(command[0]))
            {
                if ("create" == command[1].ToLower())
                {
                    CreateNewDevice();
                }
                else
                {
                    command = null;
                    throw new ArgumentException("Command is wrong");
                }
            }
            else if ("delete" == command[1].ToLower())
            {
                if (deviceList.ContainsKey(command[0]))
                {
                    deviceList.Remove(command[0]);
                    Console.WriteLine("Device {0} has been removed", command[0]);
                }
                else
                {
                    throw new ArgumentException("Wrong device name");
                }
            }
            else if ("on" == command[1].ToLower())
            {
                IEnablable device = deviceList[command[0]];
                device.On();
            }
            else if ("off" == command[1].ToLower())
            {
                IEnablable device = deviceList[command[0]];
                device.Off();
            }
            else if ("defrost" == command[1].ToLower())
            {
                if (deviceList[command[0]] is IDefrostable)
                {
                    IDefrostable device = (IDefrostable)deviceList[command[0]];
                    device.Defrost();
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else if ("reset" == command[1].ToLower())
            {
                if (deviceList[command[0]] is IResetable)
                {
                    IResetable device = (IResetable)deviceList[command[0]];
                    device.Reset();
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else if ("write" == command[1].ToLower())
            {
                if (deviceList[command[0]] is IGetChannelList)
                {
                    IGetChannelList device = (IGetChannelList)deviceList[command[0]];
                    List<string> channelList = device.GetChannelList();
                    Console.WriteLine("Channel list of {0}", command[0]);
                    foreach (string item in channelList)
                    {
                        Console.WriteLine(item);
                    }
                    Console.ReadLine();
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else
            {
                throw new ArgumentException("Wrong command");
            }
        }

        private void GoToLampCommands()
        {
            if ("on" == command[2].ToLower())
            {
                if (deviceList[command[0]] is ILampHolderable)
                {
                    ILampHolderable device = (ILampHolderable)deviceList[command[0]];
                    device.TurnOnLamp();
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else if ("off" == command[2].ToLower())
            {
                if (deviceList[command[0]] is ILampHolderable)
                {
                    ILampHolderable device = (ILampHolderable)deviceList[command[0]];
                    device.TurnOffLamp();
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else
            {
                throw new ArgumentException("Wrong command");
            }
        }

        private void CreateNewDevice()
        {
            string deviceName;
            Console.Clear();
            Console.WriteLine("Write device name");
            deviceName = Console.ReadLine().Trim();
            deviceName = deviceName.Replace(' ', '-');
            Console.Clear();

            if ("conditioning" == command[0].ToLower())
            {
                Device newDevice = builder.CreateAirConditioning(deviceName);
                deviceList.Add(deviceName, newDevice);

                SignEvent(newDevice);
            }
            else if ("lamp" == command[0].ToLower())
            {
                Device newDevice = builder.CreateLamp(deviceName);
                deviceList.Add(deviceName, newDevice);

                SignEvent(newDevice);
            }
            else if ("fridge" == command[0].ToLower())
            {
                Console.WriteLine("Write lamp name");
                string lampName = Console.ReadLine().Trim();
                Console.Clear();

                Device newDevice = builder.CreateFridge(deviceName, (ILampable)builder.CreateLamp(lampName));
                deviceList.Add(deviceName, newDevice);

                SignEvent(newDevice);
            }
            else if ("radio" == command[0].ToLower())
            {
                Console.WriteLine("Write min volume");
                int min = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Write max volume");
                int max = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Write step of the volume change");
                int step = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Write start value");
                int value = Int32.Parse(Console.ReadLine());

                Device newDevice = builder.CreateRadio(deviceName, (IChannelableModule)builder.CreateChannelModule(), (IVariable)builder.CreateVariableModule(min, max, step, value));
                deviceList.Add(deviceName, newDevice);

                SignEvent(newDevice);
            }
            else if ("tv" == command[0].ToLower())
            {
                Console.WriteLine("Write min volume");
                int min = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Write max volume");
                int max = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Write step of the volume change");
                int step = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Write start value");
                int value = Int32.Parse(Console.ReadLine());

                Device newDevice = builder.CreateTV(deviceName, (IChannelableModule)builder.CreateChannelModule(), (IVariable)builder.CreateVariableModule(min, max, step, value));
                deviceList.Add(deviceName, newDevice);

                SignEvent(newDevice);
            }
        }

        private void GoToVolumeCommands()
        {
            if (command.Length > 4)
            {
                throw new ArgumentException("Wrong command");
            }
            else if ("up" == command[2].ToLower())
            {
                if (deviceList[command[0]] is IVolume)
                {
                    IVolume device = (IVolume)deviceList[command[0]];
                    device.VolumeUp();
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else if ("down" == command[2].ToLower())
            {
                if (deviceList[command[0]] is IVolume)
                {
                    IVolume device = (IVolume)deviceList[command[0]];
                    device.VolumeDown();
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else if ("set" == command[2].ToLower())
            {
                int value = Int32.Parse(command[3]);
                if (deviceList[command[0]] is IVolume)
                {
                    IVolume device = (IVolume)deviceList[command[0]];
                    device.VolumeLevel = value;
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else
            {
                throw new ArgumentException("Wrong command");
            }
        }

        private void GoToBrightCommands()
        {
            if (command.Length > 3)
            {
                throw new ArgumentException("Wrong command");
            }
            else if ("down" == command[2].ToLower())
            {
                if (deviceList[command[0]] is IBrightable)
                {
                    IBrightable device = (IBrightable)deviceList[command[0]];
                    device.BrightDown();
                }
                else if (deviceList[command[0]] is ILampHolderBrightable)
                {
                    ILampHolderBrightable device = (ILampHolderBrightable)deviceList[command[0]];
                    device.BrightDownLamp();
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else if ("up" == command[2].ToLower())
            {
                if (deviceList[command[0]] is IBrightable)
                {
                    IBrightable device = (IBrightable)deviceList[command[0]];
                    device.BrightUp();
                }
                else if (deviceList[command[0]] is ILampHolderBrightable)
                {
                    ILampHolderBrightable device = (ILampHolderBrightable)deviceList[command[0]];
                    device.BrightUpLamp();
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else
            {
                throw new ArgumentException("Wrong command");
            }
        }

        private void GoToTempCommands()
        {
            if (command.Length > 3)
            {
                throw new ArgumentException("Wrong command");
            }
            else if ("down" == command[2].ToLower())
            {
                if (deviceList[command[0]] is IFreezable)
                {
                    IFreezable device = (IFreezable)deviceList[command[0]];
                    device.FreezDown();
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else if ("up" == command[2].ToLower())
            {
                if (deviceList[command[0]] is IFreezable)
                {
                    IFreezable device = (IFreezable)deviceList[command[0]];
                    device.FreezUp();
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else
            {
                throw new ArgumentException("Wrong command");
            }
        }

        private void GoToChannelCommands()
        {
            if (command.Length > 3)
            {
                throw new ArgumentException("Wrong command");
            }
            else if ("down" == command[2].ToLower())
            {
                if (deviceList[command[0]] is IChannelable)
                {
                    IChannelable device = (IChannelable)deviceList[command[0]];
                    device.ChannelDown();
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else if ("up" == command[2].ToLower())
            {
                if (deviceList[command[0]] is IChannelable)
                {
                    IChannelable device = (IChannelable)deviceList[command[0]];
                    device.ChannelUp();
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else
            {
                throw new ArgumentException("Wrong command");
            }
        }

        private void GoToAddChannels()
        {
            if (command.Length == 4)
            {
                if (deviceList[command[0]] is IAddChannelable)
                {
                    IAddChannelable device = (IAddChannelable)deviceList[command[0]];
                    device.AddChannel(command[2], Double.Parse(command[3]));
                }
                else
                {
                    throw new ArgumentException("Wrong device");
                }
            }
            else
            {
                throw new ArgumentException("Wrong command");
            }
        }

    }
}
