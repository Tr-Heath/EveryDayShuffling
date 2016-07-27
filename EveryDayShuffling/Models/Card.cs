namespace EveryDayShuffling.Models
{
    public struct Card
    {
        public CardValue Rank { get; set; }

        public Suit Suit { get; set; }

        public override string ToString()
        {
            return $"{Rank} of {Suit}s.";
        }

        public string ToSmallString()
        {
            return $"{(int)Rank}{(int)Suit}";
        }
    }
}
