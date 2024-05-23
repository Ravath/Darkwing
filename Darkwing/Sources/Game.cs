using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DarkWing
{
    class Game
    {
        private readonly Background background;
        private readonly Player player;
        private bool end = false;
        private readonly int delay = 40;
        private DateTime last = DateTime.Now;
        public Game()
        {
            player = new Player();
            player.GoTo(Console.WindowWidth/2, Console.WindowHeight/2);
            background = new Background(Console.WindowWidth, Console.WindowHeight, 1);
            end = false;
            last = DateTime.Now;

            while (!end)
            {
                ExecuteAction();

                // Control framerate
                SynchronizeFrameRate();
            }

            // TODO MENU, score display, ending animation
        }

        public void ExecuteAction()
        {
            player.DoAction();
            background.Scroll(1);

            for ( int y=0; y<background.height; y++)
            {
                if(player.Collision(background.left[y], y)
                || player.Collision(background.right[y], y))
                {
                    end = true;
                    // TODO collision animation
                }
            }
            background.Scroll(0);   

            Console.Clear();
            background.Display();
            player.Display();
        }

        private void SynchronizeFrameRate()
        {
                TimeSpan t = DateTime.Now - last;
                if (t.Milliseconds < delay) {
                    System.Threading.Thread.Sleep(delay - t.Milliseconds);
                }
                last = DateTime.Now;
        }
    }
}
