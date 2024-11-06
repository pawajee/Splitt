using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.MIDIntegrationService;
using static Duc.Splitt.Common.Dtos.Requests.PACIMobileIdRequest;
using static Duc.Splitt.Common.Dtos.Responses.PACIMobileIdResponse;

namespace Duc.Splitt.Core.Contracts.Services
{
    public interface IMIDServiceAuthenticationService
    {

        Task<ResponseDto<MobileAuthPNResponseDto?>> InitiateAuthRequestPN(RequestHeader requestHeader, MobileAuthPNRequestDto requestDto);
        Task<ResponseDto<bool?>> CallBackPN(CallbackResponse PACIcallback);
    }
}
