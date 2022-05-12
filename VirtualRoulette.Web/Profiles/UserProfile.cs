using AutoMapper;
using VirtualRoulette.Commons.POCO;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Web.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserNameAndBalanceModel, AppUser>().ReverseMap();
            CreateMap<AppUserModel, AppUser>().ReverseMap();
        }
    }
}
