using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Commons.POCO
{
    public class UserModel
    {
        public string Token { get; set; }
        public AppUser User { get; set; }
    }
}
