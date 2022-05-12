namespace VirtualRoulette.Web.Models
{
    public class BetResponse
    {
        public int SpinId { get; set; }
        public string Status { get; set; }
        public long Amount { get; set; }
        public int WinningNumber { get; set; }
    }
}
