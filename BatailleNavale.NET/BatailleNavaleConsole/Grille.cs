using BatailleNavaleConsole.Bateaux;
using System;
using System.Collections.Generic;
using System.Text;

namespace BatailleNavaleConsole
{
    class Grille
    {
        private int _lignes;
        private int _colonnes;
        private int[,] _grille;
        private string[,] _GrilleJoueur;
        private int _Compteur = 0;

        Bateau porteavion = new Porte_avion();
        Bateau croiseur = new Croiseur();
        Bateau contre_torpilleur = new Contre_torpilleur();
        Bateau torpilleur = new Torpilleur();

        private bool PaState = false;
        private bool CState = false;
        private bool CTState = false;
        private bool TState = false;
        private bool Explications = true;
        static Random aleatoire = new Random();

        public Grille(int lignes, int colonnes)
        {
            _lignes = lignes;
            _colonnes = colonnes;
            _grille = new int[lignes, colonnes];

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
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
            _GrilleJoueur = new string[lignes, lignes];

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < _colonnes; j++)
                {
                    _grille[i, j] = 0;
                }
            }

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < _colonnes; j++)
                {
                    _GrilleJoueur[i, j] = " ";
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

            // On prend un point aléatoire dans la grille
            int ligneAleatoire = aleatoire.Next(_lignes);
            int colonneAleatoire = aleatoire.Next(_lignes);

            if (_grille[ligneAleatoire, colonneAleatoire] == 0)
            {
                //inialise le point choisit au hasard par le num du bateau si le point est déja à 0
                _grille[ligneAleatoire, colonneAleatoire] = b.taille;
            }
            else
            // Sinon on choisit un autre point tant qu'il est différent de 0
            {
                do
                {
                    ligneAleatoire = aleatoire.Next(_lignes);
                    colonneAleatoire = aleatoire.Next(_lignes);
                } while (_grille[ligneAleatoire, colonneAleatoire] != 0);
            }

            #endregion

            #region Verifier Direction possible en fonction du point choisit

            // On créé une liste d'entier correspondant au 4 directions possibles sur la grille
            List<int> direction = new List<int>();
            direction.Add(1); // haut
            direction.Add(2); // bas
            direction.Add(3); // droite
            direction.Add(4); // gauche

            //On supprime les direction impossible lorsque le point est trop proche du bord en fonction de la taille du bateau
            if (ligneAleatoire + 1 - b.taille < 0) // direction != haut
            {
                direction.Remove(1);
            }
            if (ligneAleatoire + b.taille > _lignes) // direction != bas
            {
                direction.Remove(2);
            }
            if (colonneAleatoire + b.taille > _lignes) // direction != droite
            {
                direction.Remove(3);
            }
            if (colonneAleatoire + 1 - b.taille < 0) // direction != gauche
            {
                direction.Remove(4);
            }
            #endregion

            #region Choix de la direction au hasard et remplissage de la grille

            //On choisit un nombre aléatoire parmis le nombre de direction possible restante
            int directionRestante = aleatoire.Next(0, direction.Count);

            //Si il n'y a pas de direction possible alors c'est que la grille est trop petite
            if (direction.Count == 0)
            {
                Console.WriteLine("La taille de la grille est trop petite");
            }
            else
            {

                int compteur1 = 0;
                int compteur2 = 0;
                int compteur3 = 0;
                int compteur4 = 0;

                //On vérifie quelle direction a été choisit par le nombre aléatoire directionRestante 
                //et on remplit la grille dans la direction choisit en fonction de la taille du bateau
                if (direction[directionRestante] == 1) //rempli vers le haut
                {
                    for (int i = 1; i < b.taille; i++)
                    {
                        compteur1++;
                        // Si la grille le point suivant sur la grille == 0 alors on remplit avec le numéro du bateau
                        if (_grille[ligneAleatoire - i, colonneAleatoire] == 0)
                        {
                            _grille[ligneAleatoire - i, colonneAleatoire] = b.taille;

                        }
                        //Sinon => gestion de collision
                        else
                        {
                            //_grille[ligneAleatoire, colonneAleatoire] = 0;
                            //for (int j = 1; j < compteur1; j++)
                            //{
                            //    _grille[ligneAleatoire + j, colonneAleatoire] = 0;
                            //}
                            //AjouterBateau(b);
                        }

                    }
                }

                else if (direction[directionRestante] == 2) //rempli vers le bas
                {
                    for (int i = 1; i < b.taille; i++)
                    {
                        compteur2++;
                        if (_grille[ligneAleatoire + i, colonneAleatoire] == 0)
                        {
                            _grille[ligneAleatoire + i, colonneAleatoire] = b.taille;
                        }
                        else
                        {
                            //_grille[ligneAleatoire, colonneAleatoire] = 0;
                            //for (int j = 1; j < compteur2; j++)
                            //{
                            //    _grille[ligneAleatoire - j, colonneAleatoire] = 0;
                            //}
                            //AjouterBateau(b);
                        }
                    }
                }

                else if (direction[directionRestante] == 3) //rempli vers la droite
                {
                    for (int i = 1; i < b.taille; i++)
                    {
                        compteur3++;
                        if (_grille[ligneAleatoire, colonneAleatoire + i] == 0)
                        {
                            _grille[ligneAleatoire, colonneAleatoire + i] = b.taille;

                        }
                        else
                        {
                            //_grille[ligneAleatoire, colonneAleatoire] = 0;
                            //for (int j = 1; j < compteur3; j++)
                            //{
                            //    _grille[ligneAleatoire, colonneAleatoire - j] = 0;
                            //}
                            //AjouterBateau(b);
                        }
                    }
                }

                else if (direction[directionRestante] == 4) //rempli vers la gauche
                {
                    for (int i = 1; i < b.taille; i++)
                    {
                        compteur4++;
                        if (_grille[ligneAleatoire, colonneAleatoire - i] == 0)
                        {
                            _grille[ligneAleatoire, colonneAleatoire - i] = b.taille;

                        }
                        else
                        {
                            //_grille[ligneAleatoire, colonneAleatoire] = 0;
                            //for (int j = 1; j < compteur4; j++)
                            //{
                            //    _grille[ligneAleatoire, colonneAleatoire + j] = 0;
                            //}
                            //AjouterBateau(b);
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
                // On split pour récuperer les deux nombres taper par le joueur
                string[] split = input.Split(' ');

                // On parse en int
                x = Int32.Parse(split[0]);
                y = Int32.Parse(split[1]);

                Tirer(x, y);
                VerifierBateauCoule();
                AfficherGrilleJoueur();
            }
            //Lorsque la grille est vide (tout à 0), la partie est terminée
            Console.WriteLine("");
            Console.WriteLine("-----------------------");
            Console.WriteLine("LA PARTIE EST TERMINEE");
            Console.WriteLine("-----------------------");
        }

        public void AfficherGrilleJoueur() //La GrilleJoueur est la grille que le joueur voit :
                                           // elle est différente de la grille logique où sont les bateaux
        {
            for (int i = 0; i < _lignes; i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < _colonnes; j++)
                {
                    Console.Write(_GrilleJoueur[i, j] + ", ");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("");
            //On affiche qu'une seule fois les Explications au début de partie
            if (Explications)
            {
                Console.WriteLine(" les \"o\" correspondent à un tir touché");
                Console.WriteLine(" les \"x\" correspondent à un tir raté");
                Explications = false;
            }
            Console.WriteLine(" Nombre de coups déjà joués : " + _Compteur);
        }

        public void Tirer(int x, int y)
        {
            _Compteur++; // Compteur du nombre de coup joué           
            if (_grille[x, y] == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("RATÉ !!");
                Console.WriteLine("");
                _GrilleJoueur[x, y] = "x";
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("TOUCHE !!");
                Console.WriteLine("");
                _grille[x, y] = 0;
                _GrilleJoueur[x, y] = "o";
            }
        }

        public void VerifierBateauCoule()
        {
            int porteavion = 0;
            int croiseur = 0;
            int contre_torpilleur = 0;
            int torpilleur = 0;

            // Si une case dans le tableau contient le num du bateau alors la somme est différente de 0 et le bateau n'est pas coulé
            for (int i = 0; i < _lignes; i++)
            {
                for (int j = 0; j < _lignes; j++)
                {
                    if (_grille[i, j] == 5)
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
            // On met une condition sur State!=true pour que le message ne s'affiche qu'une fois
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
