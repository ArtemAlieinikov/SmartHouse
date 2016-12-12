using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SmartHouse.Classes;
using SmartHouse.Interfaces;

namespace SmartHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Run();
        }
    }
}
