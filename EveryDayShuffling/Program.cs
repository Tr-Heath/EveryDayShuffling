using System;

namespace EveryDayShuffling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome. The program will initialize, shuffle and display a deck of cards. More options to come.");
            CardDeck cd = new CardDeck();
            CardDealer.Shuffle(ref cd);
            Console.WriteLine(cd.ToString());
            Console.WriteLine("Please press any key to close...");
            Console.ReadKey();
        }
    }
}