using System;
using System.Collections.Generic;

namespace VirtualRoulette.Commons.POCO
{
    public class AppUserModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long Balance { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public IList<SpinModel> spins { get; set; }
        public IList<SessionTokenModel> Tokens { get; set; }
    }
}
