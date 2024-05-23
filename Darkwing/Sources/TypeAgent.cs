using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    abstract class TypeAgent
    {
        public int VieInitiale
        {
            get
            {
                return VieInitiale;
            }
            protected set => VieInitiale = value;
        }

        public TypeAgent()
        {
            VieInitiale = 1;
        }
        public abstract void DoAction();
    }
}
