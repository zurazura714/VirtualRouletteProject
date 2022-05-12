using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualRoulette.Domain.Domains
{
    public class Spin
    {
        [Key]
        public int ID { get; set; }

        public long BetAmount { get; set; }

        public long WonAmount { get; set; }

        public int WinningNumber { get; set; }

        public DateTime SpinDate { get; set; }

        public string IpAddress { get; set; }

        public AppUser AppUser { get; set; }

        public SessionToken SessionToken { get; set; }

        public long AmountForJackpot { get; set; }


    }
}
