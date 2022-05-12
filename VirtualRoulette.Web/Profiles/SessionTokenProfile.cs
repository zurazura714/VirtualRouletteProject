using AutoMapper;
using VirtualRoulette.Commons.POCO;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Web.Profiles
{
    public class SessionTokenProfile : Profile
    {
        public SessionTokenProfile()
        {
            CreateMap<SessionTokenModel, SessionToken>().ReverseMap();
        }
    }
}
