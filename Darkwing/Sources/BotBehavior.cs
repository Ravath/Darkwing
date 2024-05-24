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
            if(a.CanMove(0, vspeed))
                a.Shift(0,vspeed);
            else // Has reached end of display : remove from game
                a.Life = 0;
        }
    }
}