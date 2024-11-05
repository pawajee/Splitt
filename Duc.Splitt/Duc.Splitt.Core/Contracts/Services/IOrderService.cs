using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Core.Helper;
using System.Threading.Tasks;
using static Duc.Splitt.Common.Dtos.Requests.MerchantRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;

namespace Duc.Splitt.Core.Contracts.Services
{
    public interface IOrderService
    {

        Task<ResponseDto<CreateMerchantResponseDto>> PostMerchant(RequestHeader requestHeader, CreaterMerchantRequestDto merchant);


    }
}
