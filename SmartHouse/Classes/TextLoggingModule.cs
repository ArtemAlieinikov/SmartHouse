using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SmartHouse.Interfaces;

namespace SmartHouse.Classes
{
    class TextLoggingModule : IWritable
    {
        private string pathToDIrectory;
        private string pathToFile;

        /// <summary>
        /// </summary>
        /// <param name="adress">Path to the log files</param>
        /// <exception cref="System.DirectoryNotFoundException">Thrown when Directory not found.</exception>
        public TextLoggingModule(string adress)
        {

            pathToDIrectory = adress.Trim('\\') + "\\SmartHouseLogs";
            string stringDate = String.Format(@"\{0}{1}{2}_", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            pathToFile = pathToDIrectory + stringDate + "SmartHouseLog.txt";

            if (!Directory.Exists(adress))
            {
                throw new DirectoryNotFoundException("Directory not found, please write exists directory.");
            }
            else if (!Directory.Exists(pathToDIrectory) && Directory.Exists(adress))
            {
                try
                {
                    DirectoryInfo logDirectory = new DirectoryInfo(pathToDIrectory);
                    logDirectory.Create();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            { }
        }
    
        public void Write(string message)
        {
            string actionTime = String.Format("{0}:{1}:{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            try
            {
                using(StreamWriter writer = new StreamWriter(pathToFile, true, Encoding.Default))
                {
                    writer.WriteLine(actionTime + " \t\t " + message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
