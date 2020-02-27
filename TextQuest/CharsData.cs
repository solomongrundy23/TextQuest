using System;
using static TextQuest.BaseData;

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
            }
            public override string Title => "Герой";
            public readonly Bag Bag;
            override public void GetDamage(Damage damage) { }
            public override void Die()
            {
                throw new NotImplementedException();
            }
        }

        public class Ork : Enemy
        { 
            
        }
    }
}
