using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Common.Helpers;
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Data.Dapper;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;
using static Duc.Splitt.Common.Dtos.Requests.AuthMerchantUserDto;

namespace Duc.Splitt.Service
{
    public class AuthBackOfficeService : IAuthBackOfficeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperDBConnection _dapperDBConnection;
        private readonly UserManager<SplittIdentityUser> _userManager;
        private readonly RoleManager<SplittIdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUtilitiesService _utilitiesService;
        public AuthBackOfficeService(IUnitOfWork unitOfWork, IDapperDBConnection dapperDBConnection, UserManager<SplittIdentityUser> userManager, RoleManager<SplittIdentityRole> roleManager, IConfiguration configuration, IUtilitiesService utilitiesService)
        {
            _unitOfWork = unitOfWork;
            _dapperDBConnection = dapperDBConnection;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _utilitiesService = utilitiesService;
        }


        public async Task<ResponseDto<AuthTokens?>> Login(RequestHeader requestHeader, LoginDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return new ResponseDto<AuthTokens?>
                {
                    Code = ResponseStatusCode.Unauthorized,
                    Message = "Invalid login credentials",
                    Errors = new List<string> { $"{request.UserName} Invalid login credentials" }
                };
            }
            // Generate JWT token
            var token = _utilitiesService.GenerateJwtToken(user);
            return new ResponseDto<AuthTokens?>
            {
                Code = ResponseStatusCode.Success,
                Data = new AuthTokens { Token = token }
            };

        }

        public async Task<ResponseDto<bool?>> ChangePassword(ChangePasswordDto request)
        {
            if (request == null)
                throw new NullReferenceException("ChangePassword Model is null");

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return new ResponseDto<bool?>
                {
                    Code = ResponseStatusCode.NoDataFound,
                    Message = "Cannot find a user associated with the email",
                    Errors = new List<string> { "Cannot find a user associated with the email" }
                };

            var login = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);

            if (!login)
                return new ResponseDto<bool?>
                {
                    Code = ResponseStatusCode.BadRequest,
                    Message = "Invalid current password",
                    Errors = new List<string> { "Invalid current password" }
                };

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword,
                request.NewPassword);

            if (result.Succeeded)
            {
                return new ResponseDto<bool?>
                {
                    Data = true,
                    Code = ResponseStatusCode.Success,
                    Message = "Password has been changed successfully!",
                };
            }

            return new ResponseDto<bool?>
            {
                Code = ResponseStatusCode.ServerError,
                Message = "Something went wrong!",
                Errors = result.Errors.Select(e => e.Description),
            };
        }

        public async Task<ResponseDto<bool?>> ForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return new ResponseDto<bool?>
                {
                    Code = ResponseStatusCode.NoDataFound,
                    Message = "Invalid Request",
                };

            await _userManager.UpdateSecurityStampAsync(user);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            var url = $"{_configuration["ClientAppUrl"]}/ResetPassword?email={email}&token={validToken}";

            //Send Email for Forget password
            bool mailSent = true;// _mailService.SendResetPasswordEmail(email, url);
            if (!mailSent)
            {
                return new ResponseDto<bool?>
                {
                    Code = ResponseStatusCode.EmailNotSent,
                    Message = "System failed to send the email!",
                    //Data = url 
                };
            }

            return new ResponseDto<bool?>
            {
                Code = ResponseStatusCode.Success,
                Message = url,
                Data = true
            };
        }
        public async Task<ResponseDto<bool?>> ResetPasswordAsync(ResetPasswordDto request)
        {
            if (request == null)
                throw new NullReferenceException("ResetPassword Model is null");

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new ResponseDto<bool?>
                {
                    Code = ResponseStatusCode.NoDataFound,
                    Message = "Invalid Request",
                };

            if (request.NewPassword != request.ConfirmPassword)
                return new ResponseDto<bool?>
                {
                    Code = ResponseStatusCode.BadRequest,
                    Message = "Password doesn't match its confirmation"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(request.Token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, request.NewPassword);

            if (result.Succeeded)
                return new ResponseDto<bool?>
                {
                    Code = ResponseStatusCode.Success,
                    Message = "Password has been reset successfully!",
                    Data = true
                };

            return new ResponseDto<bool?>
            {
                Code = ResponseStatusCode.ServerError,
                Message = "Something went wrong!",
                Errors = result.Errors.Select(e => e.Description),
            };
        }

    }

}

