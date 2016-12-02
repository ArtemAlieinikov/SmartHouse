using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHouse.Classes;

namespace SmartHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            TelevisionSet tv = new TelevisionSet("TV");
            tv.AddChannel("Ortron", 19.5);
            tv.AddChannel("Ortron", 19.5);
            tv.ChannelUp();
            tv.ChannelUp();
            tv.ChannelUp();
            Console.WriteLine(tv.ToString());
        }
    }
}
