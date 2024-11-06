using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Core.Helper;
using System.Threading.Tasks;
using static Duc.Splitt.Common.Dtos.Requests.OrderRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.OrderDto;

namespace Duc.Splitt.Core.Contracts.Services
{
    public interface IOrderService
    {

        Task<ResponseDto<CreateOrderResponseDto>> PostOrder(RequestHeader requestHeader, CreateOrderRequestDto RequestOrder);


    }
}
