using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welkom bij Blackjack!");
            Console.WriteLine("Druk eerst op een toets om de kaarten van de dealer weer te geven.");

            Spel nieuwSpel = new Spel();
            nieuwSpel.spelersToevoegen();
            nieuwSpel.spelEngine();

            Console.WriteLine("Spel over!");

            Console.ReadKey();

        }
    }
}
