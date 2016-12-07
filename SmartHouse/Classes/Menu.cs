using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Interfaces;
using SmartHouse.Interfaces.Lamp;
using SmartHouse.Interfaces.ChannelModule;
using System.IO;

namespace SmartHouse.Classes
{
    class Menu
    {
        private Dictionary<string, Device> listOfDevices = new Dictionary<string, Device>();
        private SimpleDeviceCreator lamp = new LampCreator();
        private SimpleDeviceCreator fridge = new FridgeCreator();
        private SimpleDeviceCreator airConditionin = new AirConditioningCreator();
        private ComplexDeviceCreator televisionSet = new TelevisionSetCreator();
        private ComplexDeviceCreator radio = new RadioCreator();
        private TextLoggingModule loggingModule;

        public void Run()
        {
            CreateStartState();
            while (true)
            {
                ShowInfoAboutDevices();
                Console.Write("I wander if you could write your command : ");
                string command = Console.ReadLine();

                //if ()
                //else
                //{
                //    ShowCommands();
                //}
            }
        }

        private void CreateStartState()
        {
            //Create a fridge
            ILampable lampInFridge = (ILampable)lamp.Create("FridgeLamp");
            Fridge homeFridge = new Fridge("SAMSUNG", lampInFridge);

            listOfDevices.Add("HallWay", lamp.Create("HallWay"));
            listOfDevices.Add("ORISTON", airConditionin.Create("ORISTON"));
            listOfDevices.Add("SAMSUNG", homeFridge);
            listOfDevices.Add("BEREZA", televisionSet.Create("BEREZA", (IChannelableModule)new ChannelModule(), (IVariable)new ValueModule(1, 100, 5, 50)));
            listOfDevices.Add("SVOBODA", radio.Create("SVOBODA", (IChannelableModule)new ChannelModule(), (IVariable)new ValueModule(1, 100, 5, 50)));

            if (IsLogging())
            {
                SignEvents();
            }
            else { }
        }

        private void ShowInfoAboutDevices()
        {
            Console.SetWindowSize(100, 40);
            foreach(KeyValuePair<string, Device> device in listOfDevices)
            {
                Console.WriteLine(device.Value.ToString());
                Console.WriteLine();
            }
        }

        private bool IsLogging()
        {
            bool result;
            while (true)
            {
                Console.Write("Do you want save the log file? (Yes/No) : ");
                string command = Console.ReadLine();
                if (command.ToUpper() == "YES")
                {
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Please write diretcory to save the log file (C:\\YourFolder\\...)");
                            string path = Console.ReadLine();
                            loggingModule = new TextLoggingModule(path);
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
                    result = true;
                    break;
                }
                else if (command.ToUpper() == "NO")
                {
                    result = false;
                    break;
                }
                else 
                {
                    result = false;
                }
            }
            return result;
        }

        private void SignEvents()
        {
            if (loggingModule != null)
            {
                foreach (KeyValuePair<string, Device> device in listOfDevices)
                {
                    device.Value.EventStateChanged += loggingModule.Write;
                }
            }
        }

        private void ShowCommands()
        {
            Console.WriteLine("Turn ON / OFF device");
            Console.WriteLine("\t- DeviseName_On");
            Console.WriteLine("\t- DeviseName_Off");
            Console.WriteLine("RESET settings");
            Console.WriteLine("\t- DeviseName_Reset");
            Console.WriteLine("Change the volume, available for RADIO and TV");
            Console.WriteLine("\t- DeviseName_set_volume_number");
            Console.WriteLine("\t- DeviseName_volume_up");
            Console.WriteLine("\t- DeviseName_volume_down");
            Console.WriteLine("Change the bright, available for LAMP and FRIDGE");
            Console.WriteLine("\t- DeviseName_bright_up");
            Console.WriteLine("\t- DeviseName_bright_down");
            Console.WriteLine("Change the temperature, available for AIR CONDITIONING and FRIDGE");
            Console.WriteLine("\t- DeviseName_temp_up");
            Console.WriteLine("\t- DeviseName_temp_down");
            Console.WriteLine("Defrost the FRIDGE");
            Console.WriteLine("\t- DeviseName_defrost");
            Console.WriteLine("Add new channel, available for RADIO and TV");
            Console.WriteLine("\t- DeviseName_add_channelName_frequencie");
            Console.WriteLine("\tfor example : LG_add_TestChannel_33.3");
            Console.WriteLine("Change the channel, available for RADIO and TV");
            Console.WriteLine("\t- DeviseName_channel_up");
            Console.WriteLine("\t- DeviseName_channel_down");
            Console.WriteLine("Write the channel list, available for RADIO and TV");
            Console.WriteLine("\t- DeviseName_write");
        }
    }
}
