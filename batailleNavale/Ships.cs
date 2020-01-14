using System;
using System.Collections.Generic;
using System.Text;

namespace batailleNavale
{
    public abstract class Ship
    {
        public int Length, HP;
        public Boolean isAlive = true;

        public Ship(int Length)
        {
            this.Length = Length;
            this.HP = Length;
        }
    }

    public class PorteAvion : Ship {
        public PorteAvion()
            : base(5) { }
    }

    public class Croiseur : Ship {

        public Croiseur()
            : base(4) { }
    }

    public class ContreTorpilleur : Ship {
        public ContreTorpilleur()
                : base(3) { }
    }

    public class Torpilleur : Ship {
        public Torpilleur()
                : base(2) { }
    }

}
