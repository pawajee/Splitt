using AutoMapper;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Data.DataAccess.Models;

namespace Duc.Splitt.Service
{
    public class MappingProfileEn : Profile
    {
        public MappingProfileEn()
        {
            #region LookUp
           
            CreateMap<Nationality, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<Country, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<Gender, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<Language, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<MerchantAnnualSale, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<MerchantAverageOrder, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<MerchantBusinessType, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<MerchantCategory, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<RequestStatus, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<UserType, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<DocumentConfiguration, LookupDocumentDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            #endregion
        }
    }
    public static class ObjectMapperEn
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod != null && (p.GetMethod.IsPublic || p.GetMethod.IsAssembly);
                cfg.AddProfile(new MappingProfileEn());
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper() => Lazy.Value;
    }

}
