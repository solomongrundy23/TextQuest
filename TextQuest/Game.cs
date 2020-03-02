using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextQuest.BaseData;

namespace TextQuest
{
    class Game
    {
        public interface IOutput
        {
            void Print(string text = "");
            string Input();
        }

        public void Initialization()
        {
            GameConsole = new Output.ConsoleWithTextLog();
        }

        public static class Dialogs
        {
            private static string GetAnswer(string text, string variants = "")
            {
                if (variants != string.Empty) GameConsole.Print(text);
                if (variants != string.Empty) GameConsole.Print(variants);
                return GameConsole.Input().Trim();
            }
            public static bool YesNo(string text = "")
            {
                string result;
                do
                {
                    result = GetAnswer(text, "[0 - нет, 1 - да]");
                    if (result == "0" || result == "1") return result == "1";
                }
                while (true);
            }
        }
        public static void Print(string s = "") => GameConsole.Print(s);
        private static IOutput GameConsole;
        public static class Input
        {
            public static string StringNotEmpty(string text)
            {
                GameConsole.Print(text);
                string s;
                do
                {
                    s = GameConsole.Input();
                }
                while (s.Trim() == "");
                return s;
            }
            public static string String(string text)
            {
                GameConsole.Print(text);
                return String();
            }
            public static string String() => GameConsole.Input();
            public static int Integer(int min, int max)
            {
                do
                {
                    GameConsole.Print($"Ваш выбор [{min}-{max}]");
                    string s = GameConsole.Input();
                    if (int.TryParse(s, out int x))
                        if (min <= x && x <= max) return x;
                    GameConsole.Print("Не верный ввод");
                }
                while (true);
            }
        }

        public async void Start()
        {
            Initialization();
            var c1 = new CharsData.Ork();
            c1.Weapon = new WeaponsData.Sword();
            var c2 = new CharsData.Troll();
            c2.Weapon = new WeaponsData.Axe();
            var fight = new Fight(c1, c2);
            await fight.StartAsync();
            while (true) { }
        }
    }
}
