using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualRoulette.Commons.POCO;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Common.Abstractions.Services
{
    public interface IUserService : IServiceBase<AppUser>
    {
        UserModel GetUser(Login login);
    }
}
