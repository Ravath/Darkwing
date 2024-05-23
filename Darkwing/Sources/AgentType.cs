using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    class AgentType(Sprite sprite, TypeAgent type, int x, int y) : Agent(sprite,x,y)
    {
        private readonly TypeAgent type = type;

        public AgentType(Sprite sprite, TypeAgent type) : this(sprite, type,0,0){}

        public override void DoAction()
        {
            type.DoAction();
        }
    }
}
