using Duc.Splitt.Common.Dtos.Requests;

namespace Duc.Splitt.Common.Dtos.Responses
{
    public class MerchantDto
    {


        public class CreateMerchantResponseDto
        {

            public string RequestNo { get; set; } = null!;

        }

        public class SearchMerchantResponseDto
        {
            public Guid? Id { get; set; }
            public string? RequestNo { get; set; }
            public string? BusinessName { get; set; }
            public string? MerchantCategory { get; set; }
            public string? RequestStatus { get; set; }
            public string? MerchantBusinessType { get; set; }
            public string? MerchantAnnualSales { get; set; }
            public string? MerchantAverageOrder { get; set; }
            public string? RequestedBy { get; set; }
            //public DateTime? CreatedOn { get; set; }
            public string? CreatedOnFormattedDate { get; set; }
            public int MerchantStatusId { get; set; }
            public string? MobileNo { get; set; }
            public string? BusinessEmail { get; set; }
            public int TotalRecords { get; set; }
        }
        public class DownloadAttachmentResponseDto
        {
            public string? MineType { get; set; }

            public byte[]? AttachmentByte { get; set; }
        }

        public class GetMerchantResponseDto
        {
            public Guid? Id { get; set; } = null!;
            public string? RequestNo { get; set; } = null!;
            public string? BusinessNameEnglish { get; set; }
            public string? BusinessNameArabic { get; set; }
            public string? BusinessEmail { get; set; }
            public string MobileNumber { get; set; } = null!;
            public int CountryId { get; set; }
            public int MerchantStatusId { get; set; }
            public int MerchantCategoryId { get; set; }
            public int MerchantBusinessTypeId { get; set; }
            public int MerchantAnnualSalesId { get; set; }
            public int MerchantAverageOrderId { get; set; }

            public string? MerchantCategory { get; set; }
            public string? RequestStatus { get; set; }
            public string? MerchantBusinessType { get; set; }
            public string? MerchantAnnualSales { get; set; }
            public string? MerchantAverageOrder { get; set; }
            public string? Country { get; set; }
            public List<DocumentResponseDto>? Documents { get; set; }
            public List<GetMerchantRequestHistory>? MerchantRequestHistory { get; set; }
        }
        public class GetMerchantRequestHistory
        {
            public string? RequestStatus { get; set; }
            public string? RequestStatusDesc { get; set; }
            public string? Comment { get; set; }

            public string? CreatedOn { get; set; }
            public string? CreatedBy { get; set; }

        }
    }
}
