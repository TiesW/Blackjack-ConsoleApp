using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Speler
    {
        public int Punten;
        public string Naam;
        public bool Verloren;
        public bool Blackjack;
        public bool Finished;

        public Speler(string naam)
        {
            Punten = 0;
            Naam = naam;
            Verloren = false;
            Blackjack = false;
            Finished = false;
        }
    }
}
