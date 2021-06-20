using AmanaSite.Models;
using AmanaSite.Models.VM;
using AutoMapper;
namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserVM, AppUser>().ReverseMap();
            CreateMap<RegisterVM, AppUser>().ReverseMap();
            CreateMap<AppUser, UserDto>();

            CreateMap<New, NewsVM>()
            .ForMember(des => des.TypeName, opt => opt.MapFrom(src => src.Type.Type))
            .ForMember(des => des.Lang, opt => opt.MapFrom(src => src.LangCode == LangCode.Ar ? "العربية" : "English"))
            .ReverseMap();

            CreateMap<SlidersShow, SlidersShowVM>()
            .ForMember(des => des.Lang, opt => opt.MapFrom(src => src.LangCode == LangCode.Ar ? "العربية" : "English"))
            .ReverseMap();

            CreateMap<AmanaService, AmanaServiceVM>()
            .ForMember(des => des.TypeName, opt => opt.MapFrom(src => src.Type.Name))
            .ForMember(des => des.Lang, opt => opt.MapFrom(src => src.LangCode == LangCode.Ar ? "العربية" : "English"))
            .ReverseMap();

            CreateMap<MAndF, MAndFVM>()
            .ForMember(des => des.TypeName, opt => opt.MapFrom(src => src.Type.Type))
            .ForMember(des => des.Lang, opt => opt.MapFrom(src => src.LangCode == LangCode.Ar ? "العربية" : "English"))
            .ReverseMap();

        }
    }
}