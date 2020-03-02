using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextQuest.Game;

namespace TextQuest
{
    class BaseData
    {
        public enum ItemType
        {
            Item,
            Weapon,
            Armor,
            Food,
            Artefact,
            OtherItem,
        }

        public abstract class Existence
        {
            public bool isAlive = true;
            public readonly Effects Effects = new Effects();
        }

        public interface IStartAndEnd
        {
            void Run();
            void Stop();
        }

        public interface ITimeDependent
        {
            void NewDay();
        }

        public interface IEvent : IHaveInfo, IStartAndEnd
        {
            bool ItsHappend();
        }

        public interface Magic : IHaveInfo
        {
            void Use();
        }

        public interface IEffect : IHaveInfo, IStartAndEnd { }

        public interface ITimeEffect : IEffect, ITimeDependent
        {
            bool Condition();
        }

        public class Effects : List<IEffect>
        {

        }

        public class DayCircle
        {
            readonly List<ITimeDependent> TimeDependents = new List<ITimeDependent>();
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
            string Rank { get; }
            string About { get; }
            string ToString();
        }

        public static Random rnd = new Random();

        public abstract class Character : Existence, IHaveInfo
        {
            public virtual string Title => "Незнакомец";
            public virtual string Rank => "Неизвестно";
            public virtual string About => "Неизвестно";
            public int Health;
            public abstract void GetDamage(Damage damage);
            public abstract void Die();
            public int Power;
            public NativeWeapon NativeWeapon;
            public Weapon Weapon
            {
                get => weapon ?? NativeWeapon;
                set => weapon = value;
            }
            private Weapon weapon;
            public Armor Armor;
            public int Speed;
            public int Expire;
            public int Level
            {
                get => Expire / 5000;
            }
        }

        public abstract class Armor : Item
        {
            Damage Damage;
            public override string Title => "Броня";
        }

        public class Range
        {
            public int Min;
            public int Max;
            public Range(int min, int max)
            {
                Min = min;
                Max = max;
            }
            public int RandomValue
            {
                get => rnd.Next(Max - Min + 1) + Min;
            }
            public string ToString() => $"[{Min}-{Max}]";
        }

        public class Damage
        {
            public enum Types
            {
                Physic,
                Fire,
                Frost,
                Poisen
            }
            public string GetTypeString()
            {
                switch (Type)
                {
                    case Types.Fire: return "Огнём";
                    case Types.Frost: return "От холода";
                    case Types.Physic: return "Обычный";
                    case Types.Poisen: return "От отравления";
                    default: throw new Exception("Нет такого урона");
                }
            }
            public Damage(Types type, Range damage_points, string comment = "")
            {
                Type = type;
                Points = damage_points;
                Comment = comment;
            }
            public Types Type;
            public Range Points;
            public string Comment;
            public new string ToString() => $"Урон {GetTypeString()} {Points.ToString()}";
        }

        public interface NPC
        {
            void StartDialog(TextsBase.Dialogs.IDialog Dialog);
        }

        public abstract class Enemy : Character
        {
            public override void Die()
            {
                Print($"{Title} умирает");
                isAlive = false;
            }

            public override void GetDamage(Damage damage)
            {
                int pain = damage.Points.RandomValue;
                Health -= (Health - pain < 0) ? Health : pain;
                Print($"{this.Title} получает {pain} урона {damage.Comment} остаётся здоровья {Health}");
                if (Health == 0) Die();
            }
        }

        public interface ISkill
        {
            string Name { get; set; }
            void Use(Character character);
        }

        public class Artefact : Item
        {
            public override string Title => "Неизвестный предмет";
        }

        public class Food : Item
        {
            public override string Title => "Пища";
        }

        abstract public class Item : IHaveInfo
        {
            public int Weight;
            public virtual string Title => "Предмет";
            public virtual string Rank => "Неизвестно";
            public virtual string About => "Неизвестно";
        }

        abstract public class ItemOther : Item
        {
            public override string Title => "Предмет";
        }

        public class Bag : List<Item>
        {
            public new bool Add(Item item)
            {
                if (item.Weight <= FreeWeight)
                {
                    base.Add(item);
                    Print($"{item.Title} добавлен в сумку");
                    return true;
                }
                else
                {
                    Print($"Недостаточно места для добавления {item.Title}");
                    while (true)
                    {
                        if (Dialogs.YesNo("Освободить место?"))
                        {
                            Item remove = SelectDialog(Cancel: true);
                            if (remove != null) RemoveDialog(remove);
                            return Add(item);
                        }
                        else
                        { 
                            if (Dialogs.YesNo($"Выбросить {item.Title}?")) return false;
                        }
                    }
                }
            }
            public void View(IEnumerable<Item> items = null)
            {
                items = items ?? this.AsEnumerable(); 
                Print("Содержимое сумки:");
                for (int i = 0; i < items.Count(); i++)
                {
                    Print($"{i} : {items.ElementAt(i).Title}");
                }
                Print();
            }
            public Bag(int Max_Weight) => MaxWeight = Max_Weight;
            public int MaxWeight;
            public int ItemsWeight => this.Sum(x => x.Weight);
            public int FreeWeight => MaxWeight - ItemsWeight;
            public Item SelectDialog(ItemType itemtype = ItemType.Item, bool Cancel = true)
            {
                IEnumerable<Item> items = null;
                switch (itemtype)
                {
                    case ItemType.Item: items = this.AsEnumerable(); break;
                    case ItemType.Armor: items = GetArmors(); break;
                    case ItemType.Artefact: items = GetArtefacts(); break;
                    case ItemType.Food: items = GetFoods(); break;
                    case ItemType.OtherItem: items = GetOtherItems(); break;
                    case ItemType.Weapon: items = GetWeapons(); break;
                    default: throw new Exception($"Не найден тип");
                }
                View(items);
                if (items.Count() == 0)
                {
                    Print("<Пусто>");
                    return null;
                }
                else
                {
                    if (Cancel == true) Print($"{items.Count()} : <ОТМЕНА>");
                    int x = Input.Integer(0, items.Count() - (!Cancel ? 1 : 0));
                    if (x == items.Count()) return null;
                    return items.ElementAt(x);
                }
            }
            public void RemoveDialog(Item item, string question = "Выбросить")
            {
                Print($"{question} {item.Title}?");
                if (Dialogs.YesNo())
                {
                    Print($"{item.Title} удален из сумки");
                    this.Remove(item);
                }
                else
                {
                    Print($"{item.Title} не удален из сумки");
                }
            }
            public IEnumerable<Weapon> GetWeapons() => this.Where(x => x is Weapon).Select(x => x as Weapon);
            public IEnumerable<Armor> GetArmors() => this.Where(x => x is Armor).Select(x => x as Armor);
            public IEnumerable<Artefact> GetArtefacts() => this.Where(x => x is Artefact).Select(x => x as Artefact);
            public IEnumerable<Food> GetFoods() => this.Where(x => x is Food).Select(x => x as Food);
            public IEnumerable<ItemOther> GetOtherItems() => this.Where(x => x is ItemOther).Select(x => x as ItemOther);
        }

        public abstract class Weapon : Item
        {
            public override string Title => "Оружие";
            public virtual void Hit(Character targer) { }
            public Damage Damage;
            public int? Accuracy;
            public readonly bool Native = false;
        }

        public abstract class NativeWeapon : Weapon
        {
            public new readonly bool Native = true;
        }

        public interface IWeatherEvent : IHaveInfo, IEvent
        {

        }
    }
}