using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    class BaseData
    {
        const int ExpForLevel = 1000;

        public interface IHaveInfo
        {
            string Title();
            //string Rank();
        }

        public static Random rnd = new Random();

        public abstract class Character : IHaveInfo
        {
            public virtual string Title() => "Персонаж";
            public int Health;
            public abstract void GetDamage(Damage damage);
            public abstract void Die();
            public int Power;
            public Weapon Weapon;
            public Armor Armor;
            public int Exp;
            public bool Frozen;
            public bool Poisoned;
            public int Level
            {
                get => Exp / ExpForLevel;
            }
        }

        public abstract class Armor : Item
        {
            Damage Damage;
            public override string Title() => "Броня";
        }

        public class Range
        {
            int Min;
            int Max;
            public Range(int min, int max)
            {
                Min = min;
                Max = max;
            }
            public int RandomValue
            {
                get => rnd.Next(Max - Min + 1) + Min;
            }
        }

        public class Damage
        {
            public Damage(Range physic = null, Range fire = null, Range frost = null, Range poisen = null)
            {
                Physic = physic;
                Fire = fire;
                Frost = frost;
                Poisen = poisen;
            }
            public Range Physic;
            public Range Fire;
            public Range Frost;
            public Range Poisen;
        }

        public abstract class NPC
        {
            public abstract void StartDialog(TextsBase.Dialogs.IDialog Dialog);
        }

        public class Enemy : Character
        {
            public override void Die()
            {
                throw new NotImplementedException();
            }

            public override void GetDamage(Damage damage)
            {
                throw new NotImplementedException();
            }
        }

        public class Hero : Character
        {
            public Hero()
            {
                Bag = new Bag(100);
                Exp = 0;
            }
            public override string Title() => "Герой";
            public readonly Bag Bag;
            override public void GetDamage(Damage damage) { }
            public override void Die()
            {
                throw new NotImplementedException();
            }
            int Speed = 10;
        }

        public interface ISkill
        {
            string Name { get; set; }
            void Use();
        }

        public class Scroll
        { }

        abstract public class Item : IHaveInfo
        {
            public int Weight;
            public string Name;
            public string ClassName;
            public virtual string Title() => "Предмет";
        }

        public class Bag : List<Item>
        {
            public new bool Add(Item item)
            {
                if (item.Weight <= this.FreeWeight)
                {
                    base.Add(item);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public Bag(int Max_Weight) => MaxWeight = Max_Weight;
            public int MaxWeight;
            public int ItemsWeight => this.Sum(x => x.Weight);
            public int FreeWeight => MaxWeight - ItemsWeight;
            public void RemoveDialog()
            { 
                
            }
            public IEnumerable<Weapon> GetWeapons()
            {
                return this.Where(x => x is Weapon).Select(x => x as Weapon);
            }
            public IEnumerable<Armor> GetArmors()
            {
                return this.Where(x => x is Armor).Select(x => x as Armor);
            }
        }

        public abstract class Weapon : Item
        {
            public override string Title() => "Оружие";
            public virtual void Hit() { }
            public Damage Damage;
            public int Accuracy;
        }

        public interface Event
        {
            string Title { get; }
            void ItsHappend();
        }
    }
}