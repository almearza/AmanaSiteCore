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
            .ForMember(des => des.Lang, opt => opt.MapFrom(src => GetLang(src.LangCode)))
            .ReverseMap();

            CreateMap<SlidersShow, SlidersShowVM>()
            .ForMember(des => des.Lang, opt => opt.MapFrom(src => GetLang(src.LangCode)))
            .ReverseMap();

            CreateMap<Baladyat, Baladyat>();
            CreateMap<Project, Project>();

        }
        private static string GetLang(LangCode lang)
        {
            switch (lang)
            {
                case LangCode.Ar:
                    return "العريبة";
                case LangCode.En:
                    return "ُEnglish";
                default:
                    return "اردو";
            }
        }
    }
}