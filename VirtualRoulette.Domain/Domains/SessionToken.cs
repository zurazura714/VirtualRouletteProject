using System;
using System.Collections.Generic;

namespace VirtualRoulette.Domain.Domains
{
    public class SessionToken
    {
        public int ID { get; set; }

        public string Token { get; set; }

        public DateTime LastRequest { get; set; }

        public DateTime CreateDate { get; set; }

        public AppUser AppUser { get; set; }

        public ICollection<Spin> Spins { get; set; }
    }
}
