using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    class AgentType : Agent
    {
        private TypeAgent type;

        public AgentType(Sprite sprite, TypeAgent type) : this(sprite, type,0,0){}
        public AgentType(Sprite sprite, TypeAgent type, int x, int y) : base(sprite,x,y)
        {
            this.type = type;
        }

        public override void agir()
        {
            type.agir();
        }
    }
}
