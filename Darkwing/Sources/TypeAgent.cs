using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    abstract class TypeAgent
    {
        public int vieInitiale {
            get
            {
                return vieInitiale;
            }
            protected set
            {
                vieInitiale = value;
            }
        }

        public TypeAgent()
        {
            vieInitiale = 1;
        }
        public abstract void agir();
    }
}
