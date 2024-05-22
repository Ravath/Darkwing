using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    class Decor
    {
        private List<int> gauche;//position mur de gauche
        private List<int> droite;//position mur de droite
        private int largeur, hauteur, noise;//taille de la fenêtre et régularité inverse des murs
        private Random rand;
        public Decor(int largeur, int hauteur, int noise=0) {
            this.hauteur = hauteur;
            this.largeur = largeur;
            this.noise = noise;
            gauche = new List<int>();
            droite = new List<int>();
            rand = new Random();
            gauche.Add(0);
            droite.Add(largeur - 1);
            int _x = 0;//décalage du mur
            for(int i=1; i< hauteur; i++)
            {
                _x = rand.Next(-noise, noise+1);
                gauche.Add( Math.Max(0,gauche[i - 1] + _x) );
                _x = rand.Next(-noise, noise+1);
                droite.Add( Math.Min(largeur-1, droite[i - 1] + _x) );
            }
        }
        public void afficher()
        {
            for(int i =0; i<hauteur; i++)
            {
                Console.SetCursorPosition(gauche[i], i);
                Console.Write('H');
                Console.SetCursorPosition(droite[i], i);
                Console.Write('H');
            }
           
        }
        /// <summary>
        /// déplace verticalement le décor de _y
        /// </summary>
        /// <param name="_y"> _y>0 : vers le haut. _y<0: vers le bas</param>
        public void scroll(int _y)
        {
            if (_y == 0)
                return;
            bool haut = (_y > 0);
            int y = Math.Abs(_y);
            for(int i=0; i< y; i++)
            {
                enleverLigne(haut);
                if (haut)
                {
                    ajouterLigneHaut();
                }
                else
                {
                    ajouterLigneBas();
                }
            }
        }
        /// <summary>
        /// ajoute une ligne en haut de la liste des murs
        /// </summary>
        private void ajouterLigneHaut()
        {
            int _x;
            _x = rand.Next(-noise, noise + 1);
            gauche.Insert(0, Math.Max( gauche[0] + _x , 0 ));
            _x = rand.Next(-noise, noise + 1);
            droite.Insert(0, Math.Min( droite[0] + _x , largeur - 1 ));
        }
        /// <summary>
        /// enlève une ligne en bas de la liste des murs
        /// </summary>
        private void ajouterLigneBas()
        {
            int _x;
            _x = rand.Next(-noise, noise + 1);
            gauche.Add(Math.Max(gauche[hauteur-1] + _x, 0));
            _x = rand.Next(-noise, noise + 1);
            droite.Add(Math.Min(droite[hauteur - 1] + _x, largeur - 1));
        }
        /// <summary>
        /// enlève une ligne, en haut ou en bas
        /// </summary>
        /// <param name="haut"></param>
        private void enleverLigne(bool haut = true)
        {
            if (haut)//enlever ligne en haut
            {
                droite.Remove(0);
                gauche.Remove(0);
                return;
            }
            else//elever ligne en bas
            {
                droite.Remove(hauteur - 1);
                gauche.Remove(hauteur - 1);
                return;
            }

        }
    }
}
