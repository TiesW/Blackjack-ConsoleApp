using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Extensions
{
    public static class ShuffleCards
    {
        public static List<Kaarten> Shuffle (this List<Kaarten> inputList)
        {
            List<Kaarten> randomList = new List<Kaarten>();

            Random rng = new Random();
            int n = inputList.Count;
            while (n > 0)
            {
                n--;
                var randomIndex = rng.Next(0, n);
                randomList.Add(inputList[randomIndex]);
                inputList.RemoveAt(randomIndex);
            }
            return randomList;
        }


    }
}
