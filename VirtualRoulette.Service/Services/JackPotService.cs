using VirtualRoulette.Common.Abstractions.Repositories;
using VirtualRoulette.Common.Abstractions.Services;
using VirtualRoulette.Domain.Domains;
//using System.Data.Entity;

namespace VirtualRoulette.Service.Services
{
    public class JackPotService : ServiceBase<JackPot, IJackPotRepository>, IJackPotService
    {
        public JackPotService(IUnitOfWork context, IJackPotRepository jackPotRepository) : base(context, jackPotRepository)
        {
        }
    }
}
