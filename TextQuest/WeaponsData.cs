using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextQuest.BaseData;
using static TextQuest.Game;

namespace TextQuest
{
    class WeaponsData
    {
        public class Fists : NativeWeapon
        {
            public override string Title => "Без оружия";
            public Fists()
            {
                Weight = 5;
                Damage = new Damage(Damage.Types.Physic, new Range(1, 3));
                Accuracy = null;
            }
            public override void Hit(Character target)
            {
                Print($"Бьёт персонажа {target.Title} голыми руками");
                target.GetDamage(Damage);
            }
        }

        public class Sword : Weapon
        {
            public override string Title => "Меч";
            public Sword()
            {
                Weight = 5;
                Damage = new Damage(Damage.Types.Physic, new Range(2, 10), "от меча");
                Accuracy = 80;
            }
            public override void Hit(Character target)
            {
                Print($"Бьёт персонажа {target.Title} мечом");
                target.GetDamage(Damage);
            }
        }

        public class Axe : Weapon
        {
            public override string Title => "Топор";
            public Axe()
            {
                Weight = 8;
                Damage = new Damage(Damage.Types.Physic, new Range(6, 10));
                Accuracy = 60;
            }
            public override void Hit(Character target)
            {
                Print($"Бьёт персонажа {target.Title} топором");
                target.GetDamage(Damage);
            }
        }
    }
}
