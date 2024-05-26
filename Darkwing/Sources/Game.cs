using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Timers;
using Darkwing;

namespace DarkWing
{
    public class Game
    {
        public static Game Instance { get; private set; }
        public readonly Background background;
        public readonly Player player;
        public readonly AgentManager agents;
        public readonly InputMap inputmap;
        private bool end = false;
        private readonly int delay = 40;
        private DateTime last = DateTime.Now;

        private int max_score, current_score;

        private List<Animation> animations = [];

        public Game()
        {
            Game.Instance = this;
            inputmap = new();
            player = new Player(this);
            agents = new AgentManager(this);
            player.GoTo(Console.WindowWidth/2, Console.WindowHeight/2);
            background = new Background(Console.WindowWidth, Console.WindowHeight, 1);
            end = false;
            last = DateTime.Now;
        }

        public void StartGame()
        {
            // Init
            player.GoTo(Console.WindowWidth/2, Console.WindowHeight/2);
            agents.Init();
            end = false;
            last = DateTime.Now;
            current_score = 0;
            player.Init();
            animations.Clear();
            background.Init();

            // Start
            while (!end)
            {
                ExecuteAction();

                // Control framerate
                SynchronizeFrameRate();
            }

            if(current_score > max_score)
                max_score = current_score;
        }

        public void ExecuteAction()
        {
            inputmap.GetActions();
            player.DoAction();
            agents.DoAction();
            background.Scroll(1);

            if(background.Collision(player))
            {
                end = true;
                AddAnimation(Animation.Explosion.Duplicate(player.X, player.Y));
            }
            if(player.Life <= 0) { end = true; }
            if(inputmap.RisedAction("escape")) { end = true; }
            if(!end) { current_score++; }

            Console.Clear();
            background.Display();
            PlayAnimations();
            agents.Display();
            player.Display();

            // Display scores
            Sprite.RectangleFrame(
                Console.WindowWidth - 18,
                0,
                Console.WindowWidth - 1,
                3);
            Console.SetCursorPosition(Console.WindowWidth - 16,1);
            Console.WriteLine("score {0}", current_score);
            Console.SetCursorPosition(Console.WindowWidth - 16,2);
            Console.WriteLine("life  {0}", player.Life);
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
            Position menuPos = new(
                Console.WindowWidth/2 - 10,
                Console.WindowHeight/2 - 3);
            while(!endMenu)
            {
                Console.Clear();
                // Title
                Sprite.RectangleFrame(
                    Console.WindowWidth/2 - 30,
                    2,
                    Console.WindowWidth/2 + 30,
                    8);
                Sprite.RectangleFrame(
                    Console.WindowWidth/2 - 29,
                    3,
                    Console.WindowWidth/2 + 29,
                    7);
                Console.SetCursorPosition(Console.WindowWidth/2 - 8, 5);
                Console.WriteLine("DARK        WING");
                // Menu
                Sprite.RectangleFrame(
                    Console.WindowWidth/2 - 13,
                    Console.WindowHeight/2 - 5,
                    Console.WindowWidth/2 + 12,
                    Console.WindowHeight/2 + 8);
                string[] menuChoices = {
                    "1 - Play Game",
                    "2 - Reset Score",
                    "3 - QWERTY - K",
                    "4 - Credits",
                    "5 - QUIT",
                    "",
                    "Last Score",
                    string.Format("  {0:D6}", current_score),
                    "Max Score",
                    string.Format("  {0:D6}", max_score),
                };

                if(!inputmap.IsQwerty())
                    menuChoices[2] = "3 - AZERTY - K";
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
                        max_score = 0;
                        current_score = 0;
                        break;
                    case ConsoleKey.D3 :
                        inputmap.SwapKeyMaps();
                        break;
                    case ConsoleKey.D4 :
                        CreditMenu();
                        break;
                    case ConsoleKey.D5 :
                        endMenu = true;
                        break;
                }
            }
        }

        private void CreditMenu()
        {
            bool endMenu = false;

            Position menuPos = new(
                Console.WindowWidth/2 - 23,
                Console.WindowHeight/2 - 3);
            while(!endMenu)
            {
                Console.Clear();
                string[] menuChoices = {
                    "____________________ Development ____________________",
                    "                Ravath Studios 2024",
                    "",
                    "         Github : https://github.com/Ravath",
                    "",
                    "",
                    ">1- Return to Main Menu"
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
                        endMenu = true;
                        break;
                }
            }
        }

        public bool Collision(Agent a)
        {
            // - Collided with background
            if(background.Collision(a))
            {
                // Destruction
                a.Life = 0;
                return true;
            }
            else
            {
                Agent? ag = agents.Collision(a);
                if(ag!=null)
                {
                    ag.Collided(a);
                    a.Collided(ag);
                    return true;
                }
            }
            return false;
        }

        public void AddAnimation(Animation na)
        {
            na.Start();
            animations.Add(na);
        }

        private void PlayAnimations()
        {
            List<Animation> toremove = [];
            foreach(var animation in animations)
            {
                animation.Display();
                if(!animation.Running)
                {
                    toremove.Add(animation);
                }
            }
            // remove finished animation
            foreach (var animation in toremove)
            {
                animations.Remove(animation);
            }
        }

        public void AddScore(int bonus)
        {
            current_score += bonus;
        }
    }
}
