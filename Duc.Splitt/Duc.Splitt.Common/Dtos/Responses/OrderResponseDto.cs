using Duc.Splitt.Common.Dtos.Requests;

namespace Duc.Splitt.Common.Dtos.Responses
{
    public class OrderResponseDto
    {


        public class CreaterOrderResponseDto
        {
            public bool? IsSucess { get; set; }
            public Guid? OrderId { get; set; }
            public Guid? CustomerId { get; set; }
        }

        public class GetOrderDetailstResponseDto
        {
            public Guid CustomerId { get; set; }
            public Guid MerchantId { get; set; }



            public List<GetOrderItemDto>? OrderItems { get; set; }
        }
        public class GetOrderItemDto
        {
        }

      
    }
}
