using Duc.Splitt.Common.Enums;

namespace Duc.Splitt.Common.Dtos.Requests
{
    public class MerchantRequestDto
    {
        public class CreaterMerchantRequestDto
        {

            public required string BusinessNameEnglish { get; set; }
            public required string BusinessNameArabic { get; set; }
            public required int CountryId { get; set; }
            public required int CategoryId { get; set; }
            public required int BusinessTypeId { get; set; }
            public required string BusinessEmail { get; set; }
            public required string MobileNumber { get; set; }
            public required int AnnualSalesId { get; set; }
            public required int AverageOrderId { get; set; }

            public List<DocumentRequestDto>? Documents { get; set; }
        }
        public class GetMerchantRequestDto
        {
            public required Guid RequestId { get; set; }
        }
        public class AdminChangeUserStatus
        {
            public required Guid RequestId { get; set; }
            public required int RequestStatusId { get; set; }
            public string? Comments { get; set; }
        }
        public class SearchMerchantRequestDto : PagedRequestDto
        {
            public string? BusinessName { get; set; }
            public string? MobileNo { get; set; }
            public string? BusinessEmail { get; set; }
            public string? RequestNo { get; set; }
            public int? RequestStatusId { get; set; }
            public int? MerchantCategoryId { get; set; }
            public int? MerchantBusinessTypeId { get; set; }
            public int? MerchantAnnualSalesId { get; set; }
            public int? MerchantAverageOrderId { get; set; }
            public DateTime? DateFrom { get; set; }
            public DateTime? DateTo { get; set; }
            public MerchantRequestSearchOrderBy OrderBy { get; set; }
            public SortDirection OrderDirections { get; set; }

        }

        public enum MerchantRequestSearchOrderBy : byte
        {
            CreatedOn,
            RequestNo,
            ChargePointCount,
            ConnectorCount,
            AvgRating,
            ConnectorStatuses
        }
    }
}
