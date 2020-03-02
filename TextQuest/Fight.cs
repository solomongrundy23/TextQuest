using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static TextQuest.BaseData;
using static TextQuest.Game;

namespace TextQuest
{
    class Fight
    {
        public bool Ended = false;
        private Character A;
        private Character B;

        public Fight(Character a, Character b)
        {
            A = a; B = b;
        }

        public async Task StartAsync() => await Task.Run(() => Start());

        public void Start()
        {
            int Circle = 0;
            while (A.isAlive && B.isAlive)
            {
                Print($"Ход {++Circle}");
                if (A.Speed < B.Speed)
                {
                    Step(B, A);
                    if (A.isAlive) 
                        Step(A, B);
                    else
                        break;
                }
                else
                {
                    Step(A, B);
                    if (B.isAlive)
                        Step(B, A);
                    else
                        break;
                }
                Print();
                Thread.Sleep(rnd.Next(1000, 3000));
            }
            if (A.isAlive && B.isAlive) throw new Exception("Так не может быть, один должен умиреть");
            else
            if (!(A.isAlive && B.isAlive)) Print($"Нет победителей");
        }

        public void HeroStep(Character ch, Character target)
        {

        }

        public void Step(Character ch, Character target)
        {
            Print($"Ход персонажа {ch.Title}");
            ch.Weapon.Hit(target);
        }
    }
}
