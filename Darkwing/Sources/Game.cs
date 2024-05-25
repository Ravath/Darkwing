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
        public static Game Instance { get; private set;}
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
                    "",
                    "Last Score",
                    string.Format("  {0:D6}", current_score),
                    "Max Score",
                    string.Format("  {0:D6}", max_score),
                };

                if(!inputmap.IsQwerty())
                    menuChoices[2] = "3 - AZERTY";
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
                        break;
                    case ConsoleKey.D5 :
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
