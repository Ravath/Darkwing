using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    class Joueur : Agent
    {
        public Joueur() : base(new Sprite())
        {
            sprite.setChar(new Position(0, 0), 'X');
            sprite.setChar(new Position(-1, 0), '<');
            sprite.setChar(new Position(1, 0), '>');
            sprite.setChar(new Position(0, -1), '^');
            sprite.setChar(new Position(0, 1), '^');
        }

        public override void agir()
        {
            ConsoleKeyInfo cki;
            while (Console.KeyAvailable)
            {
                cki = Console.ReadKey();
                switch(cki.KeyChar){
                    case 'z':
                        decaler(0, -1);
                        break;
                    case 'q':
                        decaler(-1, 0);
                        break;
                    case 's':
                        decaler(0, 1);
                        break;
                    case 'd':
                        decaler(1, 0);
                        break;
                }
            }
        }
    }
}
