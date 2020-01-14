using System;
using System.Collections.Generic;
using System.Text;

namespace batailleNavale
{
    class MainTest
    {
        static void Main(string[] args)
        {
            /* Program.generateGrid(10);
            Program p = new Program();
            p.genShips(1, 1, 1, 1);
            Program.showGrid(); */

            Program p = new Program(7, 0, 4, 0, 0);

            
            /*
            Annuaire annuaire = new Annuaire(@"Data Source=(LocalDb)\MSSQLLocalDB;
                                                Initial Catalog=Annuaire;
                                                Integrated Security=true");
            
            annuaire.Database.CreateIfNotExists();

            Personne p = new Personne();
            p.Nom = "Dardaine";
            p.Prenom = "Paul";

            Adresse a = new Adresse();
            a.Ville = "Nancy";
            p.Adresses.Add(a); // ajout de l'adresse à l'identité de la personne grâce à la clé commune entre les 2

            annuaire.Personnes.Add(p);

            // envoie le/les requêtes au SGBDR (SQL Server)
            annuaire.SaveChanges();
            */

        }
    }
}
