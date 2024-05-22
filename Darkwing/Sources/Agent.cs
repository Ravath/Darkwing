using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    abstract class Agent
    {
        public int x { get; private set; }  //position courante
        public int y { get; private set; }
        public int vie { get; private set; }//vie courante
        protected Sprite sprite;

        //constructeurs
        public Agent(Sprite sprite) : this(sprite,0,0){}
        public Agent(Sprite sprite, int x, int y) 
        {
            this.sprite = sprite;
            this.x = x;
            this.y = y;
        }

        public void afficher()
        {
            sprite.afficher(x, y);
        }
        public void subirAttaque(Attaque attaque)
        {
            throw new NotImplementedException();
        }
        public void decaler(int _x, int _y)
        {
            x += _x;
            y += _y;
        }
        public void aller(int nx, int ny)
        {
            this.x = nx;
            this.y = ny;
        }
        public bool collision(int cx, int cy)
        {
            return sprite.collision(cx - x, cy - y);
        }
        //redéfinie par filles ou délégée par un type ( vs exemplaire )
        public abstract void agir();
    }
}
