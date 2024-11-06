using Duc.Splitt.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Duc.Splitt.Common.Dtos.Requests
{
    public class OrderRequestDto
    {
        public class CreateOrderRequestDto
        {
            public Guid CustomerId { get; set; }
            public Guid MerchantId { get; set; }
            public decimal TotalAmount { get; set; }
           // public int CurrencyId { get; set; } = 1;
            public string CheckoutId { get; set; } = null!;
            public string ExternalRefId { get; set; } = null!;
            public string OrderNumber { get; set; } = null!;
            public string MerchantUrlSuccess { get; set; } = null!;
            public string MerchantUrlFailure { get; set; } = null!;
            public string MerchantUrlCancel { get; set; } = null!;
            public List<CreateOrderItemsRequestDto>? OrderItems { get; set; }
        }
        public partial class CreateOrderItemsRequestDto
        {
            public string? ItemName { get; set; }
            public string? ItemDescription { get; set; }
            public int? Quantity { get; set; }
            public decimal? Amount { get; set; }
            public string? ExternalRefId { get; set; }
            public string? ItemImageUrl { get; set; }
            public string? ProductUrl { get; set; }
            public string? BrandName { get; set; }
            public string? Sku { get; set; }

        }

        public class DownloadAttachmentRequestDto
        {
            public required Guid AttcahmentId { get; set; }
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
