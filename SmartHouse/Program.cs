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
            //Добавил паттерн Фабричній метод.
            Menu consoleMenu = new Menu();
            consoleMenu.Run();
        }
    }
}
