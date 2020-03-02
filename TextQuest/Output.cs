using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleToad.Logs;

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

        public class ConsoleWithTextLog : Game.IOutput
        {
            private Log Log = Log.GetInstance();

            public string Input()
            {
                Console.CursorVisible = true;
                string text = Console.ReadLine();
                Log.Write(text, "Ввод");
                return text;
            }
            public void Print(string text = "")
            {
                Console.CursorVisible = false;
                Console.WriteLine(text);
                Log.Write(text, "Вывод");
            }
        }
    }
}