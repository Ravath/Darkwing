using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    class Player : Agent
    {
        private readonly InputMap input;
        public Player(InputMap input) : base(new Sprite())
        {
            this.input = input;
            sprite.SetChar(new Position(0, 0), 'X');
            sprite.SetChar(new Position(-1, 0), '<');
            sprite.SetChar(new Position(1, 0), '>');
            sprite.SetChar(new Position(0, -1), '^');
            sprite.SetChar(new Position(0, 1), '^');
        }

        public override void DoAction()
        {
            if(input.RisedAction("up"))
            {
                if (CanMove(0, -1))
                    Shift(0, -1);
            }
            if(input.RisedAction("down"))
            {
                if (CanMove(0, 1))
                    Shift(0, 1);
            }
            if(input.RisedAction("right"))
            {
                if (CanMove(1, 0))
                    Shift(1, 0);
            }
            if(input.RisedAction("left"))
            {
                if (CanMove(-1, 0))
                    Shift(-1, 0);
            }
        }
    }
}
