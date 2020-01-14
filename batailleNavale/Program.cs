using System;

namespace batailleNavale
{
    class Program
    {
        public static int[,] grid;
        public int buffer;
        public int porteANumber;
        public int croiseurNumber;
        public int contreTNumber;
        public int torpilleurNumber;
        public PorteAvion pa = new PorteAvion();
        public Croiseur c = new Croiseur();
        public ContreTorpilleur ct = new ContreTorpilleur();
        public Torpilleur t = new Torpilleur();
        public Boolean PaState = false;
        public Boolean CState = false;
        public Boolean CTState = false;
        public Boolean TState = false;

        public Program() { }

        public Program(int x, int a, int b, int c, int d)
        {
            generateGrid(x);
            generateShips(a, b, c, d);
            showGrid();
        }

        public static void generateGrid(int x) {
            // définir une grille X/Y carrée
            grid = new int[x, x];
            for (int i = 0; i < x; i++){
                for (int j = 0; j < x; j++)
                {
                    grid[i, j] = 0;
                }
            }
        }

        public static void showGrid()
        {
            for (int i = 0; i < Math.Sqrt(grid.Length); i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < Math.Sqrt(grid.Length); j++)
                {
                    Console.Write(grid[i, j] + ",");
                }
            }
        }

        public void generateShips(int porteA, int croiseur, int contreT, int torpilleur) {
            this.porteANumber = porteA;
            this.croiseurNumber = croiseur;
            this.contreTNumber = contreT;
            this.torpilleurNumber = torpilleur;
            for(int i = 0; i < porteA; i++)
            {
                placeShips(this.pa);
            }
            for (int i = 0; i < croiseur; i++)
            {
                placeShips(this.c);
            }
            for (int i = 0; i < contreT; i++)
            {
                placeShips(this.ct);
            }
            for (int i = 0; i < torpilleur; i++)
            {
                placeShips(this.t);
            }
        }

        public void change0ToN(Ship s, int randomX, int randomY)
        {
            // 1 chance sur 2 de descendre ou d'aller à droite pour construire le bateau
            Random aleatoire3 = new Random();
            int southOrEast = aleatoire3.Next(2);
            // marquage des autres cases du bateau
            for (int i = 0; i < s.Length; i++)
            {
                if (southOrEast == 0)
                {
                    if (s.Length == 5)
                        grid[randomX + i, randomY] = 5;
                    if (s.Length == 4)
                        grid[randomX + i, randomY] = 4;
                    if (s.Length == 3)
                        grid[randomX + i, randomY] = 3;
                    if (s.Length == 2)
                        grid[randomX + i, randomY] = 2;
                }
                else
                {
                    if (s.Length == 5)
                       grid[randomX, randomY + i] = 5;
                    if (s.Length == 4)
                        grid[randomX, randomY + i] = 4;
                    if (s.Length == 3)
                        grid[randomX, randomY + i] = 3;
                    if (s.Length == 2)
                        grid[randomX, randomY + i] = 2;
                }
            }
        }

        // placer les bateaux
        public void placeShips(Ship s) {

            // initialisation d'une valeur tampon
            buffer = (int)Math.Sqrt(grid.Length) - (s.Length - 1);

            // sélection de coordonnées aléatoires comprises entre 0 et cette valeur tampon
            Random aleatoire = new Random();
            int randomX = aleatoire.Next(buffer);
            Random aleatoire2 = new Random();
            int randomY = aleatoire2.Next(buffer);

            // utilisation de ces mêmes coordonnées dans la grille pour y placer un "1"
            // le "1" représente le point initial du bateau
            //grid[randomX, randomY] = s.Length;

            // 1 chance sur 2 de descendre ou d'aller à droite pour construire le bateau
            Random aleatoire3 = new Random();
            int southOrEast = aleatoire3.Next(2);
            // marquage des autres cases du bateau
            if (southOrEast == 0)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if(s.Length == 5)
                        // pour éviter de placer un bateau à un endroit où il en existe déjà un, on teste si les cases à côté de lui sont occupées
                        // on teste seulement à la première itération pour éviter de refaire le calcul pour rien
                        if(i == 0)
                            if (grid[randomX + 1, randomY] != 0 || grid[randomX + 2, randomY] != 0 || grid[randomX + 3, randomY] != 0 || grid[randomX + 4, randomY] != 0)
                                // on relance la fonction si une des cases proches du bateau est occupée, afin d'éviter une collision
                                placeShips(s);
                            // sinon on place normalement le point
                            // else est réécrit dans cette condition car sinon lorsque i vaut 1, le point ne serait pas changé
                            else
                                grid[randomX + i, randomY] = 5;
                        else
                            grid[randomX + i, randomY] = 5;

                    if (s.Length == 4)
                        if (i == 0)
                            if (grid[randomX + 1, randomY] != 0 || grid[randomX + 2, randomY] != 0 || grid[randomX + 3, randomY] != 0) 
                            { 
                                i = 0;
                                placeShips(s);
                            }
                            else
                                grid[randomX + i, randomY] = 4;
                        else
                            grid[randomX + i, randomY] = 4;

                    if(s.Length == 3)
                        if (i == 0)
                            if (grid[randomX + 1, randomY] != 0 || grid[randomX + 2, randomY] != 0)
                                placeShips(s);
                            else
                                grid[randomX + i, randomY] = 3;
                        else
                            grid[randomX + i, randomY] = 3;

                    if(s.Length == 2)
                        if (i == 0)
                            if (grid[randomX + 1, randomY] != 0)
                                placeShips(s);
                            else
                                grid[randomX + i, randomY] = 2;
                        else
                            grid[randomX + i, randomY] = 2;
                }
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s.Length == 5)
                        if (i == 0)
                            if (grid[randomX, randomY + 1] != 0 || grid[randomX, randomY + 2] != 0 || grid[randomX, randomY + 3] != 0 || grid[randomX, randomY + 4] != 0)
                                placeShips(s);
                            else
                                grid[randomX, randomY + i] = 5;
                        else
                            grid[randomX, randomY + i] = 5;

                    if (s.Length == 4)
                        if (i == 0)
                            if (grid[randomX, randomY + 1] != 0 || grid[randomX, randomY + 2] != 0 || grid[randomX, randomY + 3] != 0)
                            {
                                i = 0;
                                placeShips(s);
                            }
                                
                            else
                                grid[randomX, randomY + i] = 4;
                        else
                            grid[randomX, randomY + i] = 4;

                    if (s.Length == 3)
                        if (i == 0)
                            if (grid[randomX, randomY + 1] != 0 || grid[randomX, randomY + 2] != 0)
                                placeShips(s);
                            else
                                grid[randomX, randomY + i] = 3;
                        else
                            grid[randomX, randomY + i] = 3;

                    if (s.Length == 2)
                        if (i == 0)
                            if (grid[randomX, randomY + 1] != 0)
                                placeShips(s);
                            else
                                grid[randomX, randomY + i] = 2;
                        else
                            grid[randomX, randomY + i] = 2;
                }
            }
            
        }

        public void shoot(int x, int y) 
        {
            if(grid[x,y] != 0)
            {
                Console.WriteLine("Un bateau a été touché");
                grid[x, y] = 0;

                int count5 = 0;
                int count4 = 0;
                int count3 = 0;
                int count2 = 0;

                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        if (PaState == false && grid[i, j] == 5)
                            count5++;
                        if (CState == false && grid[i, j] == 4)
                            count4++;
                        if (CTState == false && grid[i, j] == 3)
                            count3++;
                        if (TState == false && grid[i, j] == 2)
                            count2++;
                    }
                }

                if (count5 == 0 && PaState == false)
                {
                    Console.WriteLine("Le porte-avion a été coulé");
                    PaState = true; // on indique que le porte avion est coulé
                }
                if (count4 == 0 && CState == false)
                {
                    Console.WriteLine("Le croiseur a été coulé");
                    CState = true;
                }
                if (count3 == 0 && CTState == false)
                {
                    Console.WriteLine("Le contre-torpilleur a été coulé");
                    CTState = true;
                }
                if (count2 == 0 && TState == false)
                {
                    Console.WriteLine("Le torpilleur a été coulé");
                    TState = true;
                }
            }
            else
            {
                Console.WriteLine("Un tir dans le vide...");
            }
        }


        public Boolean GameState() {

            while (PaState == false || CState == false || CTState == false || TState == false) 
            {
                return false;
            }

            Console.WriteLine("La partie est finie");
            return true;
        }

        public void saveGame() { }

        public void loadGame() { }

        public void shotsCounter() { }

   
    }
}
