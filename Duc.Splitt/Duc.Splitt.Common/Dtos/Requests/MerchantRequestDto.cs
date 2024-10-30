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
        public class SearchMerchantRequestDto : PagedRequestDto
        {
            public string? BusinessName { get; set; }
            public string? BusinessEmail { get; set; }
            public string? MobileNumber { get; set; }
            public int? CategoryId { get; set; }
            public int? BusinessTypeId { get; set; }
        }
    }
}
