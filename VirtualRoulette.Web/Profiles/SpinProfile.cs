using AutoMapper;
using VirtualRoulette.Commons.POCO;
using VirtualRoulette.Domain.Domains;

namespace VirtualRoulette.Web.Profiles
{
    public class SpinProfile : Profile
    {
        public SpinProfile()
        {
            CreateMap<SpinModel, Spin>().ReverseMap();
        }
    }
}
