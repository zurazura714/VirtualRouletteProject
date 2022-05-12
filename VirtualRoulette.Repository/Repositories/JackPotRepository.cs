using VirtualRoulette.Common.Abstractions.Repositories;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Repository.Repositories
{
    public class JackPotRepository : RepositoryBase<JackPot>, IJackPotRepository
    {
        public JackPotRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
