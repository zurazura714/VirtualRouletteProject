using VirtualRoulette.Common.Abstractions.Repositories;
using VirtualRoulette.Common.Abstractions.Services;
using VirtualRoulette.Domain.Domains;
using System.Linq;

namespace VirtualRoulette.Service.Services
{
    public class SessionService : ServiceBase<SessionToken, ISessionRepository>, ISessionService
    {
        public SessionService(IUnitOfWork context, ISessionRepository sessionRepository) : base(context, sessionRepository)
        {
        }
        public SessionToken ReturnCurrentSession(string token)
        {
            return Set().Where(a => a.Token == token).FirstOrDefault();
        }
    }
}
