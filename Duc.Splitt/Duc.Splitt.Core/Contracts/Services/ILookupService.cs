using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using System.Threading.Tasks;

namespace Duc.Splitt.Core.Contracts.Services
{
    public interface ILookupService
    {
        Task<List<LookupDto>> GetNationalities(RequestHeader requestHeader);
        Task<List<LookupDto>> GetCountries(RequestHeader requestHeader);
        Task<List<LookupDto>> GetGenders(RequestHeader requestHeader);
        Task<List<LookupDto>> GetLanguages(RequestHeader requestHeader);
        Task<List<LookupDto>> GeMerchantAnnualSales(RequestHeader requestHeader);
        Task<List<LookupDto>> GeMerchantAverageOrders(RequestHeader requestHeader);
        Task<List<LookupDto>> GeMerchantBusinessTypes(RequestHeader requestHeader);
        Task<List<LookupDto>> GeMerchantCategories(RequestHeader requestHeader);
        Task<List<LookupDto>> GeRequestStatus(RequestHeader requestHeader);
        Task<List<LookupDocumentDto>> GeDocumentConfigurations(RequestHeader requestHeader, DocumentCategories documentCategories);
    }
}
