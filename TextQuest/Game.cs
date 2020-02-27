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
            GameConsole = new Output.ConsoleOut();
        }

        public static class Dialogs
        {
            public static bool YesNo()
            {
                string result;
                do
                {
                    GameConsole.Print("[0 - нет, 1 - да]");
                    result = GameConsole.Input().Trim();
                }
                while (result != "0" && result != "1");
                return result == "1";
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
                    GameConsole.Print("Выбирайте...");
                    string s = GameConsole.Input();
                    if (int.TryParse(s, out int x))
                        if (min <= x && x <= max) return x;
                    GameConsole.Print("Не верный ввод");
                }
                while (true);
            }
        }

        public void Start()
        {
            Initialization();
            var hero = new CharsData.Hero();
            for (int i = 0; i < 100; i++)
            {
                hero.Bag.Add(new WeaponsData.Sword());
                hero.Bag.Add(new WeaponsData.Axe());
            }
            while (true)
            {
                Item item = hero.Bag.SelectDialog(ItemType.Food);
                hero.Bag.Remove(item);
            }
            Console.ReadLine();
        }
    }
}
