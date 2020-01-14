using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity; // pour convertir les classes en schéma relationnel pour les bases de données
using System.ComponentModel.DataAnnotations;

namespace batailleNavale
{
    class TestProgram
    {
        
    }
    class Annuaire : DbContext
    {

        public Annuaire(string cn)
            : base(cn) { }
        // Tables

        public DbSet<Personne> Personnes { get; set; }
        public DbSet<Adresse> Adresses { get; set; }
    }

    class Personne
    {
        [Key]
        public int Id { get; set; } // clé primaire
        public string Nom { get; set; }
        public string Prenom { get; set; }

        // Collection des adresses de cette personne
        // Ce sont les adresses qui vérifient Adresse.PersonneId == Personne.Id
        public ICollection<Adresse> Adresses { get; set; } = new HashSet<Adresse>(); //HashSet sert à initialiser la liste d'adresse

    }

    class Adresse
    {
        [Key]
        public int Id { get; set; } // clé primaire
        public string Numero { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public int PersonneId { get; set; } // clé secondaire vers une entité Personne
    }
}
