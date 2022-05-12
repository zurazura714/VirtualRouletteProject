using VirtualRoulette.Commons.POCO;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Common.Abstractions.Services
{
    public interface ISessionService : IServiceBase<SessionToken>
    {
        SessionToken ReturnCurrentSession(string token);
    }
}
