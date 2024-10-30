using Duc.Splitt.Common.Dtos.Requests;

namespace Duc.Splitt.Common.Dtos.Responses
{
    public class MerchantDto
    {


        public class CreateMerchantResponseDto
        {

            public string RequestNo { get; set; } = null!;

        }

        public class MerchantResponseDto
        {
            public string RequestId { get; set; } = null!;
            public string BusinessNameEnglish { get; set; } = null!;
            public string BusinessNameArabic { get; set; } = null!;
            public int CountryId { get; set; }
            public int CategoryId { get; set; }
            public int BusinessTypeId { get; set; }
            public string BusinessEmail { get; set; } = null!;
            public string MobileNumber { get; set; } = null!;
            public int AnnualSalesId { get; set; }
            public int AverageOrderId { get; set; }

            public List<DocumentRequestDto>? Documents { get; set; }
        }

        public class SearchMerchantResponseDto
        {
        }
    }
}
