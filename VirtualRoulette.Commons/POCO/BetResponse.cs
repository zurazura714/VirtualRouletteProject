using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualRoulette.Domain.Domains.Enums;

namespace VirtualRoulette.Commons.POCO
{
    public class BetResponse
    {
        public WonOrLostStatus Status { get; set; }
        public long BetAmount { get; set; }
        public long WonAmount { get; set; }
        public int WinningNumber { get; set; }
    }
}
