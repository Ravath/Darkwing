using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    class Player : Agent
    {
        public Player() : base(new Sprite())
        {
            sprite.SetChar(new Position(0, 0), 'X');
            sprite.SetChar(new Position(-1, 0), '<');
            sprite.SetChar(new Position(1, 0), '>');
            sprite.SetChar(new Position(0, -1), '^');
            sprite.SetChar(new Position(0, 1), '^');
        }

        public override void DoAction()
        {
            ConsoleKeyInfo cki;
            while (Console.KeyAvailable)
            {
                cki = Console.ReadKey();
                switch(cki.KeyChar){
                    case 'z':
                        if (CanMove(0, -1))
                            Shift(0, -1);
                        break;
                    case 'q':
                        if (CanMove(-1, 0))
                            Shift(-1, 0);
                        break;
                    case 's':
                        if (CanMove(0, 1))
                            Shift(0, 1);
                        break;
                    case 'd':
                        if (CanMove(1, 0))
                            Shift(1, 0);
                        break;
                }
            }
        }
    }
}
