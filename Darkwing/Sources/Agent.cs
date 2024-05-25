using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    public class Agent(Sprite sprite, BotBehavior behavior, int x, int y)
    {
        public int X { get; private set; } = x;
        public int Y { get; private set; } = y;
        public int Life { get; set; } = 1; // Current life
        public int Score { get; set; } = 0; // Score bonus when killed
        public readonly Sprite sprite = sprite;
        protected readonly BotBehavior behavior = behavior;

        // Constructors
        public Agent(Sprite sprite) : this(sprite, null, 0, 0) {}

        public void Display()
        {
            sprite.Display(X, Y);
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

        public bool Collision(Agent agent)
        {
            foreach(var i in sprite.parts)
            {
                if(agent.Collision(i.Key.x + X, i.Key.y + Y))
                    return true;
            }

            return false;
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
        
        public virtual void DoAction()
        {
            behavior.DoAction(this);
        }

        public Agent Duplicate()
        {
            Agent ret = new(sprite, behavior, x, y)
            {
                Life = Life,
                Score = Score
            };
            return ret;
        }

        public void Collided(Agent agent)
        {
            Life --;
            if(Life <= 0)
            {
                Game.Instance.AddScore(Score);
            }
        }
    }
}
