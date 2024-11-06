using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duc.Splitt.Common.Dtos.Responses
{
    public class OrderDto
    {
        public class CreateOrderResponseDto
        {
            public Guid Id { get; set; }
            public Guid CustomerId { get; set; }
            public Guid MerchantId { get; set; }
            public decimal TotalAmount { get; set; }
            //  public string? ErpreferenceNumber { get; set; }
            public int CurrencyId { get; set; }
            public int CurrencyName { get; set; }

            public string CheckoutId { get; set; }
            public string ExternalRefId { get; set; }
            public string OrderNumber { get; set; }
            public string OrderStatusId { get; set; }
            public string OrderStatusName { get; set; }
            public DateTime ExpiredAt { get; set; }
            public string CheckoutUrl { get; set; }
            public int PaymentOptionId { get; set; }
            public string MerchantUrlSuccess { get; set; }
            public string MerchantUrlFailure { get; set; }
            public string MerchantUrlCancel { get; set; }
            public List<OrderItemResponseDto>? OrderItems { get; set; }
        }
        public partial class OrderItemResponseDto
        {
            public Guid Id { get; set; }
            //public string? ErpreferenceNumber { get; set; }
            public Guid? OrderId { get; set; }
            public string? ItemName { get; set; }
            public string? ItemDescription { get; set; }
            public int? Quantity { get; set; }
            public decimal? Amount { get; set; }
            public string? ExternalRefId { get; set; }
            public string? ItemImageUrl { get; set; }
            public string? ProductUrl { get; set; }
            public string? BrandName { get; set; }
            public string? Sku { get; set; }
            public Guid CreatedBy { get; set; }
            public DateTime CreatedOn { get; set; }
            //public byte CreatedAt { get; set; }
            //public Guid? ModifiedBy { get; set; }
            //public DateTime? ModifiedOn { get; set; }
            //public byte? ModifiedAt { get; set; }
            //public virtual Order? Order { get; set; }
        }
    }
}
