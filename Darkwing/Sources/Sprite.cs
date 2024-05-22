using System;
using System.Collections.Generic;

namespace DarkWing
{
    class Sprite
    {
        Dictionary<Position, char> parties;

        public Sprite()
        {
            parties = new Dictionary<Position, char>();
        }
        public void afficher(int x, int y)
        {
            foreach(KeyValuePair<Position, char> kvp in parties)
            {
                Console.SetCursorPosition(x+kvp.Key.x, y + kvp.Key.y);
                Console.Write(kvp.Value);
            }
        }
        public bool collision(int x, int y)
        {
            return parties.ContainsKey(new Position(x, y));
        }
        public bool collision(Position p)
        {
            return parties.ContainsKey(p);
        }
        public void setChar(Position p, char c)
        {
            parties.Add(p, c);
        }
    }
}