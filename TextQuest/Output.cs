using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    class ConsoleOut : Game.Output
    {
        public string Input() => Console.ReadLine();
        public void Print(string text) => Console.WriteLine();
    }
}