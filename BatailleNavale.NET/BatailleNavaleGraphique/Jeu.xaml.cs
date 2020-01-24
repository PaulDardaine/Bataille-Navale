﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BatailleNavaleConsole;

namespace BatailleNavaleGraphique
{
    /// <summary>
    /// Logique d'interaction pour Jeu.xaml
    /// </summary>
    public partial class Jeu : Window
    {

        private Grille _grilleJeu;
        private Grille _grilleJoueur;
        public Jeu()
        {
            InitializeComponent();

            _grilleJeu = new Grille(8);

            for(int i = 0; i < _grilleJeu._lignes; i++)
            {
                for(int j = 0; j < _grilleJeu._lignes; j++)
                {
                    Button b = new Button();
                    b.Margin = new Thickness(1);
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    b.Content = _grilleJeu._grille[i, j];
                    int i2 = i; int j2 = j;

                    b.Click += (o, e) =>
                    {
                        VerifierPartie();
                        if (b.Background != Brushes.Red && b.Background != Brushes.Green)
                        {
                            Button B = e.Source as Button;
                            if (_grilleJeu._grille[i2,j2].Equals(0))
                            {
                                B.Background = Brushes.Red;
                            }
                            else
                            {
                                B.Background = Brushes.Green;
                                //B.Content = 0;
                                _grilleJeu._grille[i2, j2] = 0;
                                VerifierBateau();
                                VerifierPartie();
                            }
                        }
                        
                    };
                    gr.Children.Add(b);
                }
            }
        }

        private void VerifierPartie()
        {
            if (_grilleJeu.CState && _grilleJeu.TState && _grilleJeu.PaState && _grilleJeu.CTState)
            {
                MessageBox.Show("Partie terminée"); ;
            }
        }
        private void VerifierBateau()
        {
            int porteavion = 0;
            int croiseur = 0;
            int contre_torpilleur = 0;
            int torpilleur = 0;

            // Si une case dans le tableau contient le num du bateau alors la somme est différente de 0 et le bateau n'est pas coulé
            for (int i = 0; i < _grilleJeu._lignes; i++)
            {
                for (int j = 0; j < _grilleJeu._lignes; j++)
                {
                    if (_grilleJeu._grille[i, j] == 5)
                    {
                        porteavion += 1;
                    }

                    if (_grilleJeu._grille[i, j] == 4)
                    {
                        croiseur += 1;
                    }

                    if (_grilleJeu._grille[i, j] == 3)
                    {
                        contre_torpilleur += 1;
                    }

                    if (_grilleJeu._grille[i, j] == 2)
                    {
                        torpilleur += 1;
                    }
                }
            }

            // si il n'y a aucune case, la somme est égale à 0 donc le bateau est coulé
            // On met une condition sur State!=true pour que le message ne s'affiche qu'une fois
            if (porteavion == 0 && _grilleJeu.PaState != true)
            {
                MessageBox.Show("Porte-Avion coulé");
                _grilleJeu.PaState = true;
            }

            if (croiseur == 0 && _grilleJeu.CState != true)
            {
                MessageBox.Show("Croiseur coulé");
                _grilleJeu.CState = true;
            }

            if (contre_torpilleur == 0 && _grilleJeu.CTState != true)
            {
                MessageBox.Show("Contre-Torpilleur coulé");
                _grilleJeu.CTState = true;
            }

            if (torpilleur == 0 && _grilleJeu.TState != true)
            {
                MessageBox.Show("Torpilleur coulé");
                _grilleJeu.TState = true;
            }

        }
    }
}
