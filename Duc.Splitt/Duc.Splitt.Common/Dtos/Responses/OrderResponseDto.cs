using Duc.Splitt.Common.Dtos.Requests;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;
using static Duc.Splitt.Common.Dtos.Responses.OrderDto;

namespace Duc.Splitt.Common.Dtos.Responses
{
    public class OrderResponseDto
    {


        public class CreateOrderResponseDto
        {
            public bool? IsSucess { get; set; }
            public Guid? OrderId { get; set; }
            public Guid? CustomerId { get; set; }
        }

        public class GetOrderDetailstResponseDto
        {
            public Guid CustomerId { get; set; }
            public Guid MerchantId { get; set; }



          //  public List<GetOrderItemDto>? OrderItems { get; set; }
        }
        //PaymentInstallment
        public class GetOrderResponseDto
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
           // public List<OrderItemResponseDto>? OrderItems { get; set; }
            public List<DocumentResponseDto>? Documents { get; set; }
            public List<GetMerchantRequestHistory>? MerchantRequestHistory { get; set; }
        }

        public class GetOrderPaymentInstallmentDto
        {
            public Guid Id { get; set; }

            public Guid OrderId { get; set; }

            public decimal InstallmentAmount { get; set; }

            public decimal DueAmount { get; set; }

            public DateTime? DueDate { get; set; }

            public string? ErpreferenceNumber { get; set; }

            public int InstallmentTypeId { get; set; }

            public int? PaymentStatusId { get; set; }

            public Guid CreatedBy { get; set; }

            public DateTime CreatedOn { get; set; }

            //public byte CreatedAt { get; set; }

            //public Guid? ModifiedBy { get; set; }

            //public DateTime? ModifiedOn { get; set; }

            //public byte? ModifiedAt { get; set; }
        }

    }
}
