using System;
using VirtualRoulette.Domain.Domains.Enums;

namespace VirtualRoulette.Commons.POCO
{
    public class SpinModel
    {
        public int ID { get; set; }

        public long BetAmount { get; set; }

        public long WonAmount { get; set; }

        public int WinningNumber { get; set; }

        public DateTime SpinDate { get; set; }

        public string IpAddress { get; set; }

        public AppUserModel AppUser { get; set; }

        public SessionTokenModel SessionToken { get; set; }

        public long AmountForJackpot { get; set; }
        public WonOrLostStatus Status { get; set; }
    }
}
