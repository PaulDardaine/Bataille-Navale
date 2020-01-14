using BatailleNavaleConsole.Bateaux;
using System;

namespace BatailleNavaleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Grille g = new Grille(8);

            g.Afficher();
            g.Jouer();

        }
    }
}
