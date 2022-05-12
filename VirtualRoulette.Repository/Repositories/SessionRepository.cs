using VirtualRoulette.Common.Abstractions.Repositories;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Repository.Repositories
{
    public class SessionRepository : RepositoryBase<SessionToken>, ISessionRepository
    {
        public SessionRepository(IUnitOfWork context) : base(context)
        {

        }
    }
}
