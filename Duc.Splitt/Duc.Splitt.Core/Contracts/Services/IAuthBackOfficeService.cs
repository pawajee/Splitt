using Duc.Splitt.Common.Dtos.Responses;
using static Duc.Splitt.Common.Dtos.Requests.AuthBackOfficeUserDto;

namespace Duc.Splitt.Core.Contracts.Services
{
    public interface IAuthBackOfficeService
    {
        Task<ResponseDto<string?>> ApproveMerchantUserByAdmin(RequestHeader requestHeader, RegisterDto request);
        Task<ResponseDto<bool?>> ChangePassword(RequestHeader requestHeader, ChangePasswordDto request);
        Task<ResponseDto<AuthTokens?>> Login(RequestHeader requestHeader, LoginDto request);
        Task<ResponseDto<bool?>> ForgetPassword(RequestHeader requestHeader, ForgetPasswordDto request);
        Task<ResponseDto<bool?>> ResetPassword(RequestHeader requestHeader, ResetPasswordDto request);
        Task<ResponseDto<bool?>> CreateUser(RequestHeader requestHeader, CreateAdminUserDto request);
        Task<ResponseDto<bool?>> Logout(RequestHeader requestHeader);
    }
}
