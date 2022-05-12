using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualRoulette.Common.Abstractions.Repositories;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Repository.Repositories
{
    public class UserRepository : RepositoryBase<AppUser>, IUserRepository
    {
        public UserRepository(IUnitOfWork context) : base(context)
        {

        }
    }
}
