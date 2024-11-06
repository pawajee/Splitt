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
        public  class CreateOrderItemsRequestDto
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

        public class GetOrderRequestDto
        {
            public required Guid OrderId { get; set; }
        }

        //public class GetOrderPaymentInstallmentDto
        //{
        //    public Guid Id { get; set; }

        //    public Guid OrderId { get; set; }

        //    public decimal InstallmentAmount { get; set; }

        //    public decimal DueAmount { get; set; }

        //    public DateTime? DueDate { get; set; }

        //    public string? ErpreferenceNumber { get; set; }

        //    public int InstallmentTypeId { get; set; }

        //    public int? PaymentStatusId { get; set; }

        //    public Guid CreatedBy { get; set; }

        //    public DateTime CreatedOn { get; set; }

        //}

    }
}
