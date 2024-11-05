using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Core.Helper;
using System.Threading.Tasks;
using static Duc.Splitt.Common.Dtos.Requests.MerchantRequestDto;
using static Duc.Splitt.Common.Dtos.Requests.OrderRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;
using static Duc.Splitt.Common.Dtos.Responses.OrderResponseDto;

namespace Duc.Splitt.Core.Contracts.Services
{
    public interface IOrderService
    {

        Task<ResponseDto<CreaterOrderResponseDto?>> PostOrder(RequestHeader requestHeader, CreateOrderRequestDto requestDto);
        Task<ResponseDto<GetOrderDetailstResponseDto?>> GetOrderById(RequestHeader requestHeader, GetOrderRequestDetailsDto requestDto);

    }
}
