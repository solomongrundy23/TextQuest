using System;
using System.Collections.Generic;
using System.Linq;
using static TextQuest.BaseData;
using static TextQuest.Game;

namespace TextQuest
{
    class CharsData
    {
        public class Hero : Character
        {
            public Hero()
            {
                Bag = new Bag(100);
                Expire = 0;
                Speed = 10;
                NativeWeapon = new WeaponsData.Fists();
            }
            public override string Title => "Герой";
            public readonly Bag Bag;
            override public void GetDamage(Damage damage) { }
            public override void Die()
            {
                throw new NotImplementedException();
            }
            public void WeaponSelector(bool Cancel = true)
            {
                List<Weapon> weapon_list = Bag.GetWeapons().ToList();
                weapon_list.Insert(0, NativeWeapon);
                Bag.View(weapon_list);
                if (weapon_list.Count() == 0)
                {
                    Print("<Пусто>");
                    SetWeapon(null);
                }
                else
                {
                    if (Cancel == true) Print($"{weapon_list.Count()} : <ОТМЕНА>");
                    int x = Input.Integer(0, weapon_list.Count() + (Cancel ? 1 : 0));
                    if (x == weapon_list.Count()) 
                        SetWeapon(null);
                    else
                        SetWeapon(weapon_list.ElementAt(x));
                }
            }
            public void SetWeapon(Weapon weapon)
            {
                Weapon = weapon;
                return;
                if (!(Weapon is NativeWeapon))
                    if (Bag.FreeWeight < Weapon.Weight)
                    {

                    }
                if (weapon == null) Weapon = NativeWeapon;
            }
        }

        public class Ork : Enemy
        {
            public override string Title => "Орк";
            public Ork()
            {
                Expire = 0;
                Speed = 10;
                Health = 40;
                NativeWeapon = new WeaponsData.Fists();
            }
        }

        public class Troll : Enemy
        {
            public override string Title => "Тролль";
            public Troll()
            {
                Expire = 0;
                Speed = 3;
                Health = 50;
                NativeWeapon = new WeaponsData.Fists();
            }
        }
    }
}
