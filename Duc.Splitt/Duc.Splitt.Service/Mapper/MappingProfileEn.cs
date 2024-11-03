using AutoMapper;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Data.DataAccess.Models;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;

namespace Duc.Splitt.Service
{
    public class MappingProfileEn : Profile
    {
        public MappingProfileEn()
        {
            #region LookUp
           
            CreateMap<LkNationality, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<LkCountry, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<LkGender, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<LkLanguage, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<LkMerchantAnnualSale, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<LkMerchantAverageOrder, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<LkMerchantBusinessType, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<LkMerchantCategory, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<LkMerchantStatus, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<LkRole, LookupDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            CreateMap<LkDocumentConfiguration, LookupDocumentDto>().ForMember(dest => dest.Name, source => source.MapFrom(src => src.TitleEnglish));
            #endregion
            CreateMap<Merchant, GetMerchantResponseDto>();
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
