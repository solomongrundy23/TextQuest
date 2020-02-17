using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    class WeaponsData
    {
        public class Sword : BaseData.Weapon
        {
            public Sword()
            {
                Name = "Меч";
                Weight = 5;
                Damage = new BaseData.Damage(physic: new BaseData.Range(5, 10));
                Accuracy = 60;
            }
            public override void Hit()
            {
                Console.WriteLine(Damage.Physic.RandomValue);
            }
        }

        public class Axe : BaseData.Weapon
        {
            public Axe()
            {
                Name = "Топор";
                Weight = 8;
                Damage = new BaseData.Damage(physic: new BaseData.Range(5, 10));
                Accuracy = 40;
            }
            public override void Hit() 
            {
                Console.WriteLine(Damage.Physic.RandomValue);
            }
        }
    }
}
