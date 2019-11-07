using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack.Extensions;

namespace Blackjack
{
    class Deck
    {
        private List<Kaarten> kaartenList = new List<Kaarten>();

        public List<Kaarten> KaartenMaken()
        {
            string[] Naam = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Boer", "Heer", "Vrouw", "Aas" };
            int[] Waarde = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };
            string[] Type = new string[] { "Harten", "Klaver", "Ruiten", "Schoppen" };

            for (int x = 0; x < Waarde.Length; x++)
            {
                for (int y = 0; y < Type.Length; y++)
                {
                    kaartenList.Add(new Kaarten(Naam [x], Waarde[x], Type[y]));
                }
            }
            kaartenList = kaartenList.Shuffle();
            return kaartenList;
        }
    }
}
