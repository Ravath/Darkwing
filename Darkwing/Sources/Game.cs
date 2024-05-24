﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DarkWing
{
    public class Game
    {
        public readonly Background background;
        public readonly Player player;
        public readonly AgentManager agents;
        public readonly InputMap inputmap;
        private bool end = false;
        private readonly int delay = 40;
        private DateTime last = DateTime.Now;
        public Game()
        {
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
            inputmap.GetActions();
            player.DoAction();
            agents.DoAction();
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
            if(inputmap.RisedAction("escape")) { end = true; }
            background.Scroll(0);   

            Console.Clear();
            background.Display();
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
    }
}
