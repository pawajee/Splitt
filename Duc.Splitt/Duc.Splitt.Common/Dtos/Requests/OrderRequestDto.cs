using Duc.Splitt.Common.Enums;

namespace Duc.Splitt.Common.Dtos.Requests
{
    public class OrderRequestDto
    {
        public class CreateOrderRequestDto
        {
            public Guid CustomerId { get; set; }
            public Guid MerchantId { get; set; }



            public List<CreaterOrderItemDto>? OrderItems { get; set; }
        }
        public class CreaterOrderItemDto
        {
        }



        public class GetOrderRequestDetailsDto
        {
            public Guid? OrderId { get; set; }
            public Guid? CustomerId { get; set; }
        }
    }
}