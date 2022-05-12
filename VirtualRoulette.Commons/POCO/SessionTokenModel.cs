using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualRoulette.Commons.POCO
{
    public class SessionTokenModel
    {
        public int ID { get; set; }

        public string Token { get; set; }

        public DateTime LastRequest { get; set; }

        public DateTime CreateDate { get; set; }

        public AppUserModel AppUser { get; set; }
        public int AppUserID { get; set; }

        public ICollection<SpinModel> Spins { get; set; }
    }
}
