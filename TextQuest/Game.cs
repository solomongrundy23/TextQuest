using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    class Game
    {
        public int day = 1;
        public static string DayInfo(int day) => $"неделя: {day / 7}\nдень: {day % 7 + 1}";
        public int way = 10000;

        public interface Output
        {
            void Print(string text);
            string Input();
        }

        public Output GameConsole;

        public void Start()
        {
            var hero = new BaseData.Hero();
            hero.Bag.Add(new WeaponsData.Sword());
            hero.Weapon = (BaseData.Weapon)hero.Bag[0];
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(hero.Bag[0].Name);
                hero.Weapon.Hit();
                Console.WriteLine("\n");
            }
            Console.ReadLine();
        }
    }
}
