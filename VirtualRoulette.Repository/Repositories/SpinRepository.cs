using VirtualRoulette.Common.Abstractions.Repositories;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Repository.Repositories
{
    public class SpinRepository : RepositoryBase<Spin>, ISpinRepository
    {
        public SpinRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
