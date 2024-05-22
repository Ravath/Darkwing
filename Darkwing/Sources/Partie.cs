using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DarkWing
{
    class Partie
    {
        private Decor decor;
        private Joueur joueur;
        public Partie()
        {
            joueur = new Joueur();
            joueur.aller(Console.WindowWidth/2, Console.WindowHeight/2);
            decor = new Decor(Console.WindowWidth, Console.WindowHeight, 1);
            bool end = false;
            int delay = 40;
            DateTime last = DateTime.Now;
            while (!end)
            {
                loop();
                TimeSpan t = DateTime.Now - last;
                if (t.Milliseconds < delay) {
                    System.Threading.Thread.Sleep(delay - t.Milliseconds);
                }
                last = DateTime.Now;
            }
        }
        public void loop()
        {
            joueur.agir();
            decor.scroll(1);
            Console.Clear();
            decor.afficher();
            joueur.afficher();
        }
    }
}
