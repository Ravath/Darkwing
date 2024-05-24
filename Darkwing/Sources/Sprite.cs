﻿using System;
using System.Collections.Generic;

namespace DarkWing
{
    class Sprite
    {
        readonly Dictionary<Position, char> parts;
        public readonly Position TopLeftCorner;
        public readonly Position BottomRightCorner;
        public Sprite()
        {
            parts = [];
            TopLeftCorner = new Position(-1, -1);
            BottomRightCorner = new Position(-1, -1);
        }
        public void Display(int x, int y)
        {
            foreach(KeyValuePair<Position, char> kvp in parts)
            {
                Console.SetCursorPosition(x+kvp.Key.x, y + kvp.Key.y);
                Console.Write(kvp.Value);
            }
        }
        public bool Collision(int x, int y)
        {
            return parts.ContainsKey(new Position(x, y));
        }
        public bool Collision(Position p)
        {
            return parts.ContainsKey(p);
        }

        public void SetChar(Position p, char c)
        {
            parts.Add(p, c);
            if(p.x < TopLeftCorner.x)
                TopLeftCorner.x = p.x;
            if(p.y < TopLeftCorner.y)
                TopLeftCorner.y = p.y;
            if(p.x > BottomRightCorner.x)
                BottomRightCorner.x = p.x;
            if(p.y > BottomRightCorner.y)
                BottomRightCorner.y = p.y;
        }
    }
}