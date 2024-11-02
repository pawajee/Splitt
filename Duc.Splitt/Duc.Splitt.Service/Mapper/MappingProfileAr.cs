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

            CreateMap<Nationality, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<Country, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<Gender, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<Language, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<MerchantAnnualSale, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic)); 
            CreateMap<MerchantAverageOrder, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<MerchantBusinessType, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<MerchantCategory, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<MerchantRequestStatus, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<UserType, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));
            CreateMap<DocumentConfiguration, LookupDocumentDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleArabic));

            CreateMap<MerchantRequest, GetMerchantResponseDto>();
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
