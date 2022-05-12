using VirtualRoulette.Common.Abstractions.Repositories;
using VirtualRoulette.Common.Abstractions.Services;
using VirtualRoulette.Domain.Domains;
//using System.Data.Entity;
using VirtualRoulette.Commons.POCO;

namespace VirtualRoulette.Service.Services
{
    public class SessionService : ServiceBase<SessionToken, ISessionRepository>, ISessionService
    {
        public SessionService(IUnitOfWork context, ISessionRepository sessionRepository) : base(context, sessionRepository)
        {
        }
        public UserNameAndBalanceModel ReturnUserAndBalance(string token)
        {
            //TODO:
            //var a = Set().Include
            //AppUser user = (from t in context.SessionTokens
            //                join usr in context.AppUsers on t.AppUser.ID equals usr.ID
            //                where t.Token == token
            //                select new AppUser()
            //                {
            //                    UserName = usr.UserName,
            //                    Balance = usr.Balance

            //                }).ToList().FirstOrDefault();
            return null;
        }
    }
}
