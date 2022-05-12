using System;
using System.Linq;
using VirtualRoulette.Common.Abstractions.Repositories;
using VirtualRoulette.Common.Abstractions.Services;
using VirtualRoulette.Commons.POCO;
using VirtualRoulette.Domain.Domains;
using VirtualRoulette.Service.Helper;
using VirtualRoulette.Service.Services;

namespace VirtualRoulette.Service.Services
{
    public class UserService : ServiceBase<AppUser, IUserRepository>, IUserService
    {
        public UserService(IUnitOfWork context, IUserRepository userRepository) : base(context, userRepository)
        {
        }
        public UserModel GetUser(Login login)
        {
            var user = Set().Where(x => x.UserName == login.UserName &&
                                        x.Password == Cryptography.HmacSHA256(login.UserName, login.Password))
                                        .SingleOrDefault();

            if (user != null)
            {
                string key = "Signkey";
                //concat sign string Username:Password:Time, time because every token will be unique
                string data = String.Format("{0}:{1}:{2}", user.UserName, user.Password, DateTime.Now.ToLongTimeString());
                //Generate token
                string tokenValue = Cryptography.HmacSHA256(key, data);

                return new UserModel { Token = tokenValue, User = user };
            }
            return null;
        }
        public UserNameAndBalanceModel GetUserNameAndBalance(string userName)
        {
            return Set().Where(x => x.UserName == userName)
                        .Select(a => new UserNameAndBalanceModel
                        { Balance = a.Balance, UserName = a.UserName }).SingleOrDefault();
        }
    }
}
