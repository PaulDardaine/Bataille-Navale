using BatailleNavaleConsole.Bateaux;
using System;
using System.Collections.Generic;
using System.Text;

namespace BatailleNavaleConsole
{
    class Grille
    {
        int _lignes;
        int _colonnes;
        int[,] _grille;

        Bateau porteavion = new Porte_avion();
        Bateau croiseur = new Croiseur();
        Bateau contre_torpilleur = new Contre_torpilleur();
        Bateau torpilleur = new Torpilleur();

        public Boolean PaState = false;
        public Boolean CState = false;
        public Boolean CTState = false;
        public Boolean TState = false;

        public Grille(int lignes, int colonnes)
        {
            _lignes = lignes;
            _colonnes = colonnes;
            _grille = new int[lignes, colonnes];

            for(int i=0; i<lignes; i++)
            {
                for(int j=0; j< colonnes; j++)
                {
                    _grille[i, j] = 0;
                }
            }

        }

        public Grille(int lignes)
        {
            _lignes = lignes;
            _colonnes = lignes;
            _grille = new int[lignes, lignes];

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < _colonnes; j++)
                {
                    _grille[i, j] = 0;
                }
            }
            AjouterBateau(porteavion);
            AjouterBateau(croiseur);
            AjouterBateau(contre_torpilleur);
            AjouterBateau(torpilleur);

        }

        public void AjouterBateau(Bateau b)
        {
            #region Point aléatoire sur la grille
            Random aleatoire = new Random();
            int ligneAleatoire = aleatoire.Next(_lignes);
            int colonneAleatoire = aleatoire.Next(_lignes);
            //Console.WriteLine(ligneAleatoire + ";" + colonneAleatoire);

            _grille[ligneAleatoire, colonneAleatoire] = b.taille; //inialise le point choisit au hasard par le num du bateau
            #endregion

            #region Verifier Direction possible en fonction du point choisit
            List<int> direction = new List<int>();
            direction.Add(1); // haut
            direction.Add(2); // bas
            direction.Add(3); // droite
            direction.Add(4); // gauche

            //supprime les direction impossible lorsque le point est proche du bord
            if (ligneAleatoire - b.taille < 0) // direction =/= haut
            {
                direction.Remove(1);
            }
            if (ligneAleatoire + b.taille > _lignes) // direction =/= bas
            {
                direction.Remove(2);
            }
            if (colonneAleatoire + b.taille > _lignes) // direction =/= droite
            {
                direction.Remove(3);
            }
            if (colonneAleatoire - b.taille < 0) // direction =/= gauche
            {
                direction.Remove(4);
            }
            #endregion

            #region Choix de la direction au hasard et remplissage de la grille
            int directionRestante = aleatoire.Next(0, direction.Count); // choisit un nombre aléatoire parmis les direction possible

            if (direction.Count == 0)
            {
                Console.WriteLine("La taille de la grille est trop petite"); // impossible de placer le bateau
            }
            else { 
                if (direction[directionRestante] == 1) //rempli vers le haut
                {
                    for(int i=0; i<b.taille; i++)
                    {
                        // si une des cases à remplir est deja occupé par un autre bateau, on relance la fonction
                        if (_grille[ligneAleatoire - i, colonneAleatoire] == 0)
                        {
                            _grille[ligneAleatoire - i, colonneAleatoire] = b.taille;
                        }
                        else
                        {
                            //ajouterBateau(b);
                        }

                    }
                }

                else if (direction[directionRestante] == 2) //rempli vers le bas
                {
                    for (int i = 0; i < b.taille; i++)
                    {
                        if (_grille[ligneAleatoire + i, colonneAleatoire] == 0)
                        {
                            _grille[ligneAleatoire + i, colonneAleatoire] = b.taille;
                        }
                        else
                        {
                            //ajouterBateau(b);
                        }
                    }
                }

                else if (direction[directionRestante] == 3) //rempli vers la droite
                {
                    for (int i = 0; i < b.taille; i++)
                    {
                        if (_grille[ligneAleatoire, colonneAleatoire + i] == 0)
                        {
                            _grille[ligneAleatoire, colonneAleatoire + i] = b.taille;
                        }
                        else
                        {
                            //ajouterBateau(b);
                        }
                    }
                }

                else if (direction[directionRestante] == 4) //rempli vers la gauche
                {
                    for (int i = 0; i < b.taille; i++)
                    {
                        if (_grille[ligneAleatoire, colonneAleatoire - i] == 0)
                        {
                            _grille[ligneAleatoire, colonneAleatoire - i] = b.taille;
                        }
                        else
                        {
                            //supprimer les cases déja ajoutés
                            //ajouterBateau(b);
                        }
                    }
                }
            }
            #endregion

        }

        public void Jouer()
        {
            while (VerifierGrilleVide())
            {
                int x, y;
                Console.WriteLine("");
                Console.WriteLine("Ecrire sous la forme : \"x y\" avec x et y entre 0 et " + (_lignes - 1));
                string input = Console.ReadLine();
                string[] split = input.Split(" ");
            
                x = Int32.Parse(split[0]);
                y = Int32.Parse(split[1]);

                Tirer(x, y);
                VerifierBateauCoule();
                Afficher();
            }
            Console.WriteLine("");
            Console.WriteLine("-----------------------");
            Console.WriteLine("LA PARTIE EST TERMINEE");
            Console.WriteLine("-----------------------");
        }

        public void Tirer(int x , int y)
        {
            if (_grille[x, y] == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("MISS !!");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("TOUCHE !!");
                Console.WriteLine("");
                _grille[x, y] = 0;
            }
        }

        public bool VerifierGrilleVide()
        {
            bool grillevide = true;
            if (CState && TState && PaState && CTState)
            {
                grillevide = false;
                return grillevide;
            }
            return grillevide;
        }

        public void VerifierBateauCoule()
        {
            int porteavion = 0;
            int croiseur = 0;
            int contre_torpilleur = 0;
            int torpilleur = 0;

            // Si une case dans le tableau contient le num du bateau alors la somme est différente de 0 et le bateau n'est pas coulé
            for (int i=0; i<_lignes; i++)
            {
                for (int j = 0; j < _lignes; j++)
                {
                    if(_grille[i,j] == 5)
                    {
                        porteavion += 1;
                    }

                    if (_grille[i, j] == 4)
                    {
                        croiseur += 1;
                    }

                    if (_grille[i, j] == 3)
                    {
                        contre_torpilleur += 1;
                    }

                    if (_grille[i, j] == 2)
                    {
                        torpilleur += 1;
                    }
                }
            }

            // si il n'y a aucune case, la somme est égale à 0 donc le bateau est coulé
            if (porteavion == 0 && PaState != true)
            {
                Console.WriteLine("Porte-Avion coulé");
                PaState = true;
            }

            if (croiseur == 0 && CState != true)
            {
                Console.WriteLine("Croiseur coulé");
                CState = true;
            }

            if (contre_torpilleur == 0 && CTState != true)
            {
                Console.WriteLine("Contre-Torpilleur coulé");
                CTState = true;
            }

            if (torpilleur == 0 && TState != true)
            {
                Console.WriteLine("Torpilleur coulé");
                TState = true;
            }
        }

        public void Afficher() // affiche la grille
        {
            for (int i = 0; i < _lignes; i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < _colonnes; j++)
                {
                    Console.Write(_grille[i, j] + ", ");
                }
            }
            Console.WriteLine("");
        }
    }
}
