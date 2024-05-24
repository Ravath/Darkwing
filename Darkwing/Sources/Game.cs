using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        }

        public void StartGame()
        {
            // Init
            player.GoTo(Console.WindowWidth/2, Console.WindowHeight/2);
            end = false;
            last = DateTime.Now;

            // Start
            while (!end)
            {
                ExecuteAction();

                // Control framerate
                SynchronizeFrameRate();
            }
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

        public void Menu()
        {
            bool endMenu = false;

            while(!endMenu)
            {
                Console.Clear();
                Position menuPos = new(
                    Console.WindowWidth/2 - 10,
                    Console.WindowHeight/2 - 3);
                string[] menuChoices = {
                    "1 - Play Game",
                    "2 - Reset Score",
                    "3 - QWERTY",
                    "4 - Credits",
                    "5 - QUIT",
                };
                for(int i =0; i<menuChoices.Length; i++)
                {
                    Console.SetCursorPosition(menuPos.x, menuPos.y+i);
                    Console.WriteLine(menuChoices[i]);
                }
                
                ConsoleKeyInfo cki;
                while (!Console.KeyAvailable) { Thread.Sleep(100); }
            
                cki = Console.ReadKey();
                switch(cki.Key)
                {
                    case ConsoleKey.D1 :
                        StartGame();
                        break;
                    case ConsoleKey.D2 :
                        break;
                    case ConsoleKey.D3 :
                        break;
                    case ConsoleKey.D4 :
                        break;
                    case ConsoleKey.D5 :
                        endMenu = true;
                        break;
                }
            }
        }
    }
}
