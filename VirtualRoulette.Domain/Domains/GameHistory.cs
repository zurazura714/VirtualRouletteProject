namespace VirtualRoulette.Domain.Domains
{
    public class GameHistory
    {
        public int SpinId { get; set; }

        public long BetAmount { get; set; }

        public long WonAmount { get; set; }

        public string Date { get; set; }
    }
}
