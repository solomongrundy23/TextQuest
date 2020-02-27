using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    class Main
    {
        public int day = 1;
        public static string DayInfo(int day) => $"неделя: {day / 7}\nдень: {day % 7 + 1}";
        public int way = 10000;
    }
}