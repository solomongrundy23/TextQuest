using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextQuest.BaseData;

namespace TextQuest
{
    class WeaponsData
    {
        public class Sword : Weapon
        {
            public override string Title => "Меч";
            public Sword()
            {
                Weight = 5;
                Damage = new Damage(Damage.Types.Physic, new Range(5, 10));
                Accuracy = 60;
            }
            public override void Hit()
            {
                Console.WriteLine(Damage.Points.RandomValue);
            }
        }

        public class Axe : Weapon
        {
            public override string Title => "Топор";
            public Axe()
            {
                Weight = 8;
                Damage = new Damage(Damage.Types.Physic, new Range(5, 10));
                Accuracy = 40;
            }
            public override void Hit() 
            {
                Console.WriteLine(Damage.Points.RandomValue);
            }
        }
    }
}
