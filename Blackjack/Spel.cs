using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack.Extensions;

namespace Blackjack
{
    class Spel
    {
        string input = Console.ReadLine();
        bool spelOver = false;
        bool allLosses = false;
        bool allFinished = false;
        private Deck deck = new Deck();

        List<Speler> spelerList = new List<Speler>();
        Speler spelerEen = new Speler("Peter");
        Speler spelerTwee = new Speler("Maria");
        Speler spelerDrie = new Speler("Joep");

        public void spelersToevoegen()
        {
            spelerList.Add(spelerEen);
            spelerList.Add(spelerTwee);
            spelerList.Add(spelerDrie);
        }

        public void spelEngine()
        {
            int i = 0;
            int dealerpunten = 0;
            var kaartenList = deck.KaartenMaken();
            Kaarten dealerKaartEen;
            Kaarten dealerKaartTwee;

            //Eerst krijgt de dealer twee kaarten:
            Console.WriteLine("De kaarten van de dealer zijn:");
            Console.WriteLine(kaartenList[i]);
            dealerpunten += kaartenList[i].Waarde;
            dealerKaartEen = kaartenList[i];
            i++;
            dealerpunten += kaartenList[i].Waarde;
            dealerKaartTwee = kaartenList[i];
            Console.WriteLine("[Tweede kaart is niet zichtbaar.]");
            i++;


            Console.WriteLine("---------------------------");
            Console.WriteLine("Elke speler drukt vervolgens één keer op een toets om twee kaarten te ontvangen.");
            input = Console.ReadLine();

            //Dan krijgt iedere speler twee kaarten:
            foreach (Speler speler in spelerList)
            {
                Console.WriteLine(speler.Naam + " heeft de volgende kaarten gekregen:");
                Console.WriteLine(kaartenList[i]);
                speler.Punten += kaartenList[i].Waarde;
                i++;
                Console.WriteLine(kaartenList[i]);
                speler.Punten += kaartenList[i].Waarde;
                Console.WriteLine("--Je puntenaantal is " + speler.Punten + ".");
                i++;

                if (speler.Punten == 21)
                {
                    Console.WriteLine("---------------------------");
                    Console.WriteLine(speler.Naam + " heeft 21 punten oftwel Blackjack!");
                    speler.Blackjack = true;
                    speler.Finished = true;
                }
                Console.ReadLine();
            }
            Console.WriteLine("De kaarten zijn verdeeld. De spelers kunnen nu kiezen om te standen, of om nog een kaart te trekken.");
            Console.WriteLine("Voer 's' om te standen. Voer 'k' in om een kaart bij te krijgen. Voer 'q' in om te stoppen. " + spelerEen.Naam + " begint.");
            input = Console.ReadLine();



            //Dan kunnen de spelers kiezen om te 'standen' of om een kaart bij te vragen:
            while (!spelOver)
            {
                foreach (Speler speler in spelerList)
                {
                    if (input == "k" && !speler.Finished && !allLosses && !allFinished) //De laatste 2 zet ik erbij zodat je niet eerst nog bv. 3 keer moet drukken voordat hij uit de loop gaat.
                    {
                        speler.Punten += kaartenList[i].Waarde;

                        Console.WriteLine(speler.Naam + " heeft een " + kaartenList[i] + " getrokken! Je puntenaantal is nu " + speler.Punten + ".");
                        i++;   //Zorgt ervoor dat de volgende kaart getrokken wordt i.p.v. dezelfde voor iedere speler.

                        if (speler.Punten == 21)
                        {
                            Console.WriteLine(speler.Naam + " heeft 21 punten oftwel Blackjack!");
                            speler.Blackjack = true;
                            // Speler verwijderen! Zodat hij niet checkStatus blokkeert.
                            speler.Finished = true;
                            checkStatus(dealerpunten, dealerKaartEen, dealerKaartTwee);
                        }
                        if (speler.Punten > 21)
                        {
                            Console.WriteLine(speler.Naam + " is over de 21 punten. Verloren!");
                            speler.Verloren = true;
                            // Speler verwijderen! Zodat hij niet checkStatus blokkeert.
                            speler.Finished = true;
                            checkStatus(dealerpunten, dealerKaartEen, dealerKaartTwee);
                        }
                    }
                    else if (input == "s" && !speler.Finished)
                    {
                        Console.WriteLine(speler.Naam + " kiest voor 'stand'. Je definitieve score is " + speler.Punten + ".");
                        speler.Finished = true;
                        checkStatus(dealerpunten, dealerKaartEen, dealerKaartTwee);
                    }
                    else if (input == "q")
                    {
                        Console.WriteLine("Het spel stopt.");
                        return;
                    }
                    else if (input != "k" && input != "s" && input != "q")
                    {
                        Console.WriteLine("Voer 'k' in.");
                    }
                    input = Console.ReadLine();
                }
            }
        }

        public void checkStatus(int dealerpunten, Kaarten dealerKaartEen, Kaarten dealerKaartTwee)
        {
            allLosses = spelerList.All(x => x.Verloren);  // Een bool die alleen true is als alle spelers teveel punten hebben.
            if (allLosses)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("Alle spelers hebben verloren!");
                spelOver = true;
            }

            allFinished = spelerList.All(x => x.Finished);
            if (allFinished && !allLosses) //Als alle spelers verloren hebben stopt het spel gelijk en krijg je niet alles hieronder.
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("Alle spelers zijn klaar!");
                spelOver = true;
                gameEinde(dealerpunten, dealerKaartEen, dealerKaartTwee);
            }
        }

        public void gameEinde(int dealerpunten, Kaarten dealerKaartEen, Kaarten dealerKaartTwee)
        {
            toonDealerKaarten(dealerpunten, dealerKaartEen, dealerKaartTwee);
            while (dealerpunten < 17)
            {
                Console.WriteLine("De dealer trekt een kaart erbij.");
                int i = 0;
                var kaartenList = deck.KaartenMaken();
                i++;

                Console.WriteLine("De dealer heeft een " + kaartenList[i] + " getrokken!");
                dealerpunten += kaartenList[i].Waarde;
                Console.WriteLine("Het puntenaantal van de dealer is nu " + dealerpunten + ".");
                Console.WriteLine("---------------------------");
            }
            checkWinst(dealerpunten);
        }
    

    public void toonDealerKaarten(int dealerpunten, Kaarten dealerKaartEen, Kaarten dealerKaartTwee)
    {
        Console.WriteLine("De dealer had de volgende kaarten:");
        Console.WriteLine(dealerKaartEen);
        Console.WriteLine(dealerKaartTwee);
        Console.WriteLine("Het puntenaanal van de dealer is: " + dealerpunten + ".");
        Console.WriteLine("---------------------------");
    }

    public void checkWinst(int dealerpunten)
    {
        foreach (Speler speler in spelerList)
        {
            if (dealerpunten > 21 && speler.Punten < 21)
            {
                Console.WriteLine("De dealer heeft te veel en " + speler.Naam + " niet. WINST.");
            }
            else if (speler.Blackjack && dealerpunten != 21)
            {
                Console.WriteLine(speler.Naam + " heeft blackjack! De dealer heeft geen blackjack. WINST.");
            }
            else if (speler.Blackjack && dealerpunten == 21)
            {
                Console.WriteLine(speler.Naam + " en de dealer hebben beide blackjack. PUSH.");
            }
            else if (speler.Verloren) //Volgens de regels heeft de speler evengoed verloren als de dealer ook teveel heeft.
            {
                Console.WriteLine(speler.Naam + " had al verloren!");
            }
            else if (speler.Punten > dealerpunten)
            {
                Console.WriteLine(speler.Naam + " heeft een hogere score dan de dealer! WINST.");
            }
            else if (speler.Punten == dealerpunten)
            {
                Console.WriteLine(speler.Naam + " heeft dezelfde score als de dealer! PUSH.");
            }
            else
            {
                Console.WriteLine("De dealer heeft een hogere score dan " + speler.Naam + ". VERLIES.");
            }
            Console.ReadLine();
        }
    }
}
}
