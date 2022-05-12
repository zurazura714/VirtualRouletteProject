using VirtualRoulette.Commons.POCO;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Common.Abstractions.Services
{
    public interface ISessionService : IServiceBase<SessionToken>
    {
        UserNameAndBalanceModel ReturnUserAndBalance(string token);
    }
}
