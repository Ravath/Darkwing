using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    abstract class Agent(Sprite sprite, int x, int y)
    {
        public int X { get; private set; } = x;
        public int Y { get; private set; } = y;
        public int Vie { get; private set; }//Current life
        protected Sprite sprite = sprite;

        //constructors
        public Agent(Sprite sprite) : this(sprite,0,0){}

        public void Display()
        {
            sprite.Display(X, Y);
        }
        public void SubirAttaque(Attaque attaque)
        {
            throw new NotImplementedException();
        }
        public void Shift(int _x, int _y)
        {
            X += _x;
            Y += _y;
        }
        public void GoTo(int nx, int ny)
        {
            X = nx;
            Y = ny;
        }
        public bool Collision(int cx, int cy)
        {
            // Replace coordinates relative to the sprite coordinates.
            return sprite.Collision(cx - X, cy - Y);
        }

        /// <summary>
        /// Check if the sprite can move in the given direction without going out of frame.
        /// </summary>
        /// <param name="cx">Horizontal shift.</param>
        /// <param name="cy">Vertical shift.</param>
        /// <returns>True if not out of bound.</returns>
        public bool CanMove(int cx, int cy)
        {
            if (X+sprite.TopLeftCorner.x+cx < 0 || X+sprite.BottomRightCorner.x+cx >= Console.WindowWidth) 
                return false;
            if (Y+sprite.TopLeftCorner.y+cy < 0 || Y+sprite.BottomRightCorner.y+cy >= Console.WindowHeight) 
                return false;
            return true;
        }
        
        public abstract void DoAction();
    }
}
