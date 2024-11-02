using Duc.Splitt.Common.Dtos.Responses;
using static Duc.Splitt.Common.Dtos.Requests.AuthConsumerUserDto;

namespace Duc.Splitt.Core.Contracts.Services
{
    public interface IAuthConsumerService
    {

        Task<ResponseDto<bool?>> RequestConsumerUserOTP(RequestHeader requestHeader, RegisterDto request);
          Task<ResponseDto<AuthTokens?>> VerifyConsumerUserOTP(RequestHeader requestHeader,VerifyOtpDto request);
    }
}
