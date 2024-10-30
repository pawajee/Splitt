using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Core.Helper;
using System.Threading.Tasks;
using static Duc.Splitt.Common.Dtos.Requests.MerchantRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;

namespace Duc.Splitt.Core.Contracts.Services
{
    public interface IMerchantService
    {

        Task<ResponseDto<CreateMerchantResponseDto>> PostMerchant(RequestHeader requestHeader, CreaterMerchantRequestDto requestDto);
        Task<PagedList<SearchMerchantResponseDto>> SearchMerchantRequest(RequestHeader requestHeader,
          SearchMerchantRequestDto searchRequestDto);
        Task<ResponseDto<GetMerchantResponseDto>> GetMerchantDetailsById(RequestHeader requestHeader, GetMerchantRequestDto requestDto);
    }
}
