using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiereUtilisationDUnDatacontext
{
    public class Client : INotifyPropertyChanged
    {
        private string nom;

        private string prenom;

        private string sexe;

        private int age;

        public event PropertyChangedEventHandler PropertyChanged;


        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                if (value != nom)
                {
                    nom = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Nom"));
                    }
                }
            }
        }

        public string Prenom
        {
            get
            {
                return prenom;
            }

            set
            {
                if (value != prenom)
                {
                    prenom = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Prenom"));
                    }
                }
            }
        }

        public string Sexe
        {
            get
            {
                return sexe;
            }

            set
            {
                if (value != sexe)
                {
                    sexe = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Sexe"));
                    }
                }
            }
        }

        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                if (value != age)
                {
                    age = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Age"));
                    }
                }
            }
        }
    }
}
