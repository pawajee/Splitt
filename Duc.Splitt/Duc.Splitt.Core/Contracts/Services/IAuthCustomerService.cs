using Duc.Splitt.Common.Dtos.Responses;
using System.Threading.Tasks;
using static Duc.Splitt.Common.Dtos.Requests.AuthConsumerUserRequestDto;

namespace Duc.Splitt.Core.Contracts.Services
{
    public interface IAuthCustomerService
    {

        Task<ResponseDto<bool?>> RequestOTP(RequestHeader requestHeader, RegisterDto request);
        Task<ResponseDto<VerifyOtpResponse?>> VerifyOTP(RequestHeader requestHeader, VerifyOtpDto request);
        Task<ResponseDto<CustomerRegistrationResponseDto?>> CustomerRegistrationRequest(RequestHeader requestHeader, CustomerRegistrationRequestDto request);
        Task<ResponseDto<CheckMIdRequestStatusDto?>> CheckMidStatus(RequestHeader requestHeader, CheckMIdRequestStatusDto request);
    }
}
