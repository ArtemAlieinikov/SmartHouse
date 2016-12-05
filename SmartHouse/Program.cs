using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SmartHouse.Classes;

namespace SmartHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            AirConditioning testCond = new AirConditioning("Cond");
            TextLoggingModule logger = new TextLoggingModule("D:\\");
            testCond.EventStateChanged += logger.Write;

            testCond.On();
            testCond.Off();
            testCond.FreezUp();
        }
    }
}
