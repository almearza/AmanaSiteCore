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
            CreateMap<New,NewsVM>()
            .ForMember(des=>des.TypeName,opt=>opt.MapFrom(src=>src.Type.Type))
            .ForMember(des=>des.Lang,opt=>opt.MapFrom(src=>src.LangCode==LangCode.Ar?"العربية":"English"));
            
            // CreateMap<New, CustomNewsVM>();
            // CreateMap<BaladyaDescr, BaladyaVM>().ReverseMap();
            // CreateMap<AmanaLink, AmanaLinkVM>();
            // CreateMap<MAndF, CustomMAndF>();
            // CreateMap<CustomerIdea, CustomerIdeaVM>();

            // //cpanel
            // CreateMap<New, NewsVM>();
            // CreateMap<SlidersShow, SlidersShowVM>();
            // CreateMap<MAndF, MAndFVM>();
            // CreateMap<MAndFVM, MAndF>();
            // CreateMap<AmanaService, AmanaServiceVM>();
            // CreateMap<AmanaServiceVM, AmanaService>();
            // CreateMap<InvestChance, InvestChanceVM>();
            // CreateMap<OurIdea, OurIdeaVM>();
            // CreateMap<SurveyVM, SurveyData>();
            // CreateMap<SurveyData, SurveyJson>();

            // CreateMap<Message, MessageDto>()
            // .ForMember(dest => dest.SenderPhotoUrl, opt =>
            //    opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
        }
    }
}