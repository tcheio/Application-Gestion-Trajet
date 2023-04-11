using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sycilines.Modele
{
    public class Port
    {
        private int id;
        private string nom;

        public Port(int id, string nom)
        {
            this.id = id;
            this.nom = nom;
        }

        public int getId()
        {
            return this.id;
        }

        public string getNom()
        {
            return this.nom;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public void setNom(string nom)
        {
            this.nom = nom;
        }
    }
}
