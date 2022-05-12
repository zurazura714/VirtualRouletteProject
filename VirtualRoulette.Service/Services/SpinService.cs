using VirtualRoulette.Common.Abstractions.Repositories;
using VirtualRoulette.Common.Abstractions.Services;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Service.Services
{
    public class SpinService : ServiceBase<Spin, ISpinRepository>, ISpinService
    {
        public SpinService(IUnitOfWork context, ISpinRepository spinRepository) : base(context, spinRepository)
        {
        }
    }
}
