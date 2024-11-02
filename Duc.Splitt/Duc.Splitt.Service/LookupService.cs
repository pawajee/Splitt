using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Data.Dapper;
using Duc.Splitt.Data.DataAccess.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Data;

namespace Duc.Splitt.Service
{
    public class LookupService : ILookupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperDBConnection _dapperDBConnection;

        public LookupService(IUnitOfWork unitOfWork, IDapperDBConnection dapperDBConnection)
        {
            _unitOfWork = unitOfWork;
            _dapperDBConnection = dapperDBConnection;
        }
        public async Task<List<LookupDto>> GetNationalities(RequestHeader requestHeader)
        {
            var obj = await _unitOfWork.Nationalities.FindAsync(t => t.IsDeleted == null || (t.IsDeleted.HasValue && t.IsDeleted.Value != true));
            if (requestHeader.IsArabic)
            {
                return ObjectMapperAr.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleArabic));
            }
            else
            {
                return ObjectMapperEn.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleEnglish));
            }
        }
        public async Task<List<LookupDto>> GetCountries(RequestHeader requestHeader)
        {
            var obj = await _unitOfWork.Countries.FindAsync(t => t.IsDeleted == null || (t.IsDeleted.HasValue && t.IsDeleted.Value != true));
            if (requestHeader.IsArabic)
            {
                return ObjectMapperAr.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleArabic));
            }
            else
            {
                return ObjectMapperEn.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleEnglish));
            }
        }
        public async Task<List<LookupDto>> GetGenders(RequestHeader requestHeader)
        {
            var obj = await _unitOfWork.Genders.FindAsync(t => t.IsDeleted == null || (t.IsDeleted.HasValue && t.IsDeleted.Value != true));
            if (requestHeader.IsArabic)
            {
                return ObjectMapperAr.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleArabic));
            }
            else
            {
                return ObjectMapperEn.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleEnglish));
            }
        }
        public async Task<List<LookupDto>> GetLanguages(RequestHeader requestHeader)
        {
            var obj = await _unitOfWork.Languages.FindAsync(t => t.IsDeleted == null || (t.IsDeleted.HasValue && t.IsDeleted.Value != true));
            if (requestHeader.IsArabic)
            {
                return ObjectMapperAr.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleArabic));
            }
            else
            {
                return ObjectMapperEn.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleEnglish));
            }
        }
        public async Task<List<LookupDto>> GeMerchantAnnualSales(RequestHeader requestHeader)
        {
            var obj = await _unitOfWork.MerchantAnnualSales.FindAsync(t => t.IsDeleted == null || (t.IsDeleted.HasValue && t.IsDeleted.Value != true));
            if (requestHeader.IsArabic)
            {
                return ObjectMapperAr.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleArabic));
            }
            else
            {
                return ObjectMapperEn.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleEnglish));
            }
        }
        public async Task<List<LookupDto>> GeMerchantAverageOrders(RequestHeader requestHeader)
        {
            var obj = await _unitOfWork.MerchantAverageOrders.FindAsync(t => t.IsDeleted == null || (t.IsDeleted.HasValue && t.IsDeleted.Value != true));
            if (requestHeader.IsArabic)
            {
                return ObjectMapperAr.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleArabic));
            }
            else
            {
                return ObjectMapperEn.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleEnglish));
            }
        }
        public async Task<List<LookupDto>> GeMerchantBusinessTypes(RequestHeader requestHeader)
        {
            var obj = await _unitOfWork.MerchantBusinessTypes.FindAsync(t => t.IsDeleted == null || (t.IsDeleted.HasValue && t.IsDeleted.Value != true));
            if (requestHeader.IsArabic)
            {
                return ObjectMapperAr.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleArabic));
            }
            else
            {
                return ObjectMapperEn.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleEnglish));
            }
        }
        public async Task<List<LookupDto>> GeMerchantCategories(RequestHeader requestHeader)
        {
            var obj = await _unitOfWork.MerchantCategories.FindAsync(t => t.IsDeleted == null || (t.IsDeleted.HasValue && t.IsDeleted.Value != true));
            if (requestHeader.IsArabic)
            {
                return ObjectMapperAr.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleArabic));
            }
            else
            {
                return ObjectMapperEn.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleEnglish));
            }
        }
        public async Task<List<LookupDto>> GeRequestStatus(RequestHeader requestHeader)
        {
            var obj = await _unitOfWork.MerchantRequestStatuses.FindAsync(t => t.IsDeleted == null || (t.IsDeleted.HasValue && t.IsDeleted.Value != true));
            if (requestHeader.IsArabic)
            {
                return ObjectMapperAr.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleArabic));
            }
            else
            {
                return ObjectMapperEn.Mapper().Map<List<LookupDto>>(obj.OrderBy(t => t.SortOrder).OrderBy(t => t.TitleEnglish));
            }
        }
        public async Task<List<LookupDocumentDto>> GeDocumentConfigurations(RequestHeader requestHeader, DocumentCategories documentCategories)
        {
            List<LookupDocumentDto> lst = null;
            var obj = await _unitOfWork.DocumentConfigurations.FindAsync(t => t.IsDeleted == null || (t.IsDeleted.HasValue && t.IsDeleted.Value != true) && t.DocumentCategoryId == (int)documentCategories);//ToDO
            if (obj != null && obj.Count() > 0)
            {
                lst = new List<LookupDocumentDto>();
                foreach (var item in obj)
                {
                    List<string> supportedMineType = item.SupportedMineType.Split(',')
                                  .Select(item => item.Trim())
                                  .ToList();

                    lst.Add(
                        new LookupDocumentDto
                        {
                            Id = item.Id,
                            MaxFileSizeKb = item.MaxFileSizeKb,
                            IsRequired = item.IsRequired,
                            Name = requestHeader.IsArabic ? item.TitleArabic : item.TitleEnglish,
                            SupportedFileTypes = supportedMineType,
                            Description = requestHeader.IsArabic ? item.DescriptionArabic : item.DescriptionEnglish
                        });
                }
            }
            return lst;
        }

    }
}
