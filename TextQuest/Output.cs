using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Output
    {
        public class ConsoleOut : Game.IOutput
        {
            public string Input() => Console.ReadLine();
            public void Print(string text = "") => Console.WriteLine(text);
        }
    }
}