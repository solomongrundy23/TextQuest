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
            public string Input()
            {
                Console.CursorVisible = true;
                return Console.ReadLine();
            }
            public void Print(string text = "")
            {
                Console.CursorVisible = false;
                Console.WriteLine(text);
            }
        }
    }
}