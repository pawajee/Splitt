using Duc.Splitt.Common.Dtos.Responses;
using static Duc.Splitt.Common.Dtos.Requests.AuthMerchantUserDto;

namespace Duc.Splitt.Core.Contracts.Services
{
    public interface IAuthMerchantService
    {

       
        Task<ResponseDto<AuthTokens?>> ActivateMerchantByUser(RequestHeader requestHeader, SetPasswordDto model);
        Task<ResponseDto<AuthTokens?>> Login(RequestHeader requestHeader, LoginDto model);
        Task<ResponseDto<bool?>> ChangePassword(RequestHeader requestHeader, ChangePasswordDto request);
        Task<ResponseDto<bool?>> ForgetPassword(RequestHeader requestHeader, ForgetPasswordDto request);
        Task<ResponseDto<bool?>> ResetPassword(RequestHeader requestHeader, ResetPasswordDto request);
        Task<ResponseDto<bool?>> Logout(RequestHeader requestHeader);
    }
}
