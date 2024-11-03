using AutoMapper;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Data.DataAccess.Models;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;

namespace Duc.Splitt.Service
{
    public class MappingProfileAr : Profile
    {
        public MappingProfileAr()
        {

            CreateMap<LkNationality, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<LkCountry, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<LkGender, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<LkLanguage, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<LkMerchantAnnualSale, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic)); 
            CreateMap<LkMerchantAverageOrder, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<LkMerchantBusinessType, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<LkMerchantCategory, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<LkMerchantStatus, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<LkRole, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<LkDocumentConfiguration, LookupDocumentDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            
            CreateMap<Merchant, GetMerchantResponseDto>();
        }

    }
    public static class ObjectMapperAr
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod != null && (p.GetMethod.IsPublic || p.GetMethod.IsAssembly);
                cfg.AddProfile(new MappingProfileAr());
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper() => Lazy.Value;
    }
}
