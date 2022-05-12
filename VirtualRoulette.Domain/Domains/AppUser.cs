using System;
using System.Collections.Generic;

namespace VirtualRoulette.Domain.Domains
{
    public class AppUser : IHasCreationTime
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long Balance { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public ICollection<Spin> spins { get; set; }
        public ICollection<SessionToken> Tokens { get; set; }
    }
}
