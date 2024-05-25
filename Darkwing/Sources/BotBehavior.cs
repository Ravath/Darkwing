using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    public abstract class BotBehavior
    {
        public abstract void DoAction(Agent a);
    }

    public class GoStraight(int vspeed = 1) : BotBehavior
    {
        private readonly int vspeed = vspeed;

        public override void DoAction(Agent a)
        {
            if(a.Life <= 0)
                return;
            if(a.CanMove(0, vspeed))
            {
                int st = 1, inc = 1;
                if(vspeed < 0)
                { st = -1; inc = -1; }
                for(int i = st; Math.Abs(i) <= Math.Abs(vspeed); i+=inc)
                {
                    a.Shift(0,inc);
                    Game.Instance.Collision(a);
                    if(a.Life <= 0)
                        break;
                }
            }
            else // Has reached end of display : remove from game
                a.Life = 0;
        }
    }
}