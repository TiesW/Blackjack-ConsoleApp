using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Kaarten
    {
        public string Naam { get; set; }
        public int Waarde { get; set; }
        public string Type { get; set; }

        public Kaarten(string naam, int waarde, string type)
        {
            this.Naam = naam;
            this.Waarde = waarde;
            this.Type = type;
        }
        public override string ToString()  //Inheritence.
        {
        return $"{Type} {Naam}";
        }

    }
}
