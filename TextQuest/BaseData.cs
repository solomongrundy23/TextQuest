using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    class BaseData
    {
        public abstract class HaveLevel
        {
            public int Expire;
            public int Level
            {
                get => Expire / 5000;
            }
        }

        public interface ITimeDependent
        {
            void NewDay();
        }

        public interface IEvent
        {
            bool ItsHappend();
            void Execute();
        }

        public interface Magic : IHaveInfo
        {
            void Use();
        }

        public interface IEffect : IHaveInfo
        {
            void Run();
            void Stop();
        }

        public interface TimeEffect : IEffect, ITimeDependent
        {
            bool Condition();
        }

        public class Effects : List<IEffect>
        {

        }

        public class DayCircle
        {
            List<ITimeDependent> TimeDependents = new List<ITimeDependent>();
            void Update()
            {
                for (int i = 0; i < TimeDependents.Count; i++)
                {
                    TimeDependents[i].NewDay();
                }
            }
        }

        public interface IHaveInfo
        {
            string Title { get; }
            //string Rank() { get; set; }
            //string About() { get; }
        }

        public static Random rnd = new Random();

        public abstract class Character : HaveLevel, IHaveInfo
        {
            public string Name;
            public virtual string Title => "Персонаж";
            public int Health;
            public abstract void GetDamage(Damage damage);
            public abstract void Die();
            public int Power;
            public Weapon Weapon;
            public Armor Armor;
            public readonly Effects Effects = new Effects();
            public int Speed;
        }

        public abstract class Armor : Item
        {
            Damage Damage;
            public override string Title => "Броня";
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
                Expire = 0;
                Speed = 10;
            }
            public override string Title => "Герой";
            public readonly Bag Bag;
            override public void GetDamage(Damage damage) { }
            public override void Die()
            {
                throw new NotImplementedException();
            }
        }

        public interface ISkill
        {
            string Name { get; set; }
            void Use(Character character);
        }

        public class Scroll : Item
        {
        
        }

        public class Food : Item
        { 
        
        }

        abstract public class Item : IHaveInfo
        {
            public int Weight;
            public string Name;
            public string ClassName;
            public virtual string Title => "Предмет";
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
            public override string Title => "Оружие";
            public virtual void Hit() { }
            public Damage Damage;
            public int Accuracy;
        }

        public interface WeatherEvent : IHaveInfo, IEvent
        {

        }
    }
}