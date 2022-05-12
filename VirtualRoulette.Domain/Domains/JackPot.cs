using VirtualRoulette.Domain.Domains.Enums;

namespace VirtualRoulette.Domain.Domains
{
    public class JackPot
    {
        public int ID { get; set; }
        public decimal JackPotAmount { get; set; }
        public JackpotStatus Status { get; set; }
    }
}
