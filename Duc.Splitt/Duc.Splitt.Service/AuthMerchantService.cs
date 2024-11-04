using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Common.Helpers;
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Data.Dapper;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;
using static Duc.Splitt.Common.Dtos.Requests.AuthMerchantUserDto;

namespace Duc.Splitt.Service
{
    public class AuthMerchantService : IAuthMerchantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperDBConnection _dapperDBConnection;
        private readonly UserManager<SplittIdentityUser> _userManager;
        private readonly RoleManager<SplittIdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUtilitiesService _utilitiesService;
        private readonly SignInManager<SplittIdentityUser> _signInManager;

        public AuthMerchantService(IUnitOfWork unitOfWork, IDapperDBConnection dapperDBConnection, UserManager<SplittIdentityUser> userManager, RoleManager<SplittIdentityRole> roleManager, IConfiguration configuration, IUtilitiesService utilitiesService, SignInManager<SplittIdentityUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _dapperDBConnection = dapperDBConnection;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _utilitiesService = utilitiesService;
            _signInManager = signInManager;
        }



        public async Task<ResponseDto<bool?>> ChangePassword(RequestHeader requestHeader, ChangePasswordDto request)
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

        [HttpPost]
        public async Task<ResponseDto<bool?>> Logout(RequestHeader requestHeader)
        {
            await _signInManager.SignOutAsync();
            return new ResponseDto<bool?>
            {
                Data = true,
                Code = ResponseStatusCode.Success,
                Message = "Logout  successfully!",
            };
        }

        #region Anonymous API 
        public async Task<ResponseDto<AuthTokens?>> ActivateMerchantByUser(RequestHeader requestHeader, SetPasswordDto request)
        {

            var user = await _userManager.FindByIdAsync(request.Identifier);
            if (user == null)
            {
                return new ResponseDto<AuthTokens?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "User not exists",
                    Errors = new List<string> { $"{request.Identifier} is not available" }
                };
            }
            var merchantUser = await _unitOfWork.MerchantUsers.GetMerchantRequestByEmail(user.Email);
            if (merchantUser == null)
            {
                return new ResponseDto<AuthTokens?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "Email Not Available in Request",
                    Errors = new List<string> { $"{merchantUser.BusinessEmail} Email Not Available in Request" }
                };
            }
            var decodedToken = WebEncoders.Base64UrlDecode(request.Token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);

            // Confirm email with the provided token
            var result = await _userManager.ConfirmEmailAsync(user, normalToken);
            if (!result.Succeeded)
            {
                return new ResponseDto<AuthTokens?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "User already exists",
                    Errors = _utilitiesService.GetErrorMessages(result)
                };

            }

            // Set password for the user
            var passwordResult = await _userManager.AddPasswordAsync(user, request.Password);
            if (!passwordResult.Succeeded)
            {

                return new ResponseDto<AuthTokens?>
                {
                    Code = ResponseStatusCode.ServerError,
                    Message = "Unable to Set the Password",
                    Errors = _utilitiesService.GetErrorMessages(passwordResult)
                };
            }
            var userMerchant = new User
            {
                LoginId = user.Email,
                UserTypeId = (int)UserTypes.MerchantOwner,
                IsActive = true,
                CreatedAt = (byte)requestHeader.LocationId,
                CreatedOn = DateTime.Now,
                CreatedBy = Utilities.AnonymousUserID,
            };

            _unitOfWork.Users.AddAsync(userMerchant);
            merchantUser.MerchantRequest.MerchantStatusId = (int)MerchantRequestStatuses.Active;
            merchantUser.ModifiedAt = (byte)requestHeader.LocationId;
            merchantUser.ModifiedOn = DateTime.Now;
            merchantUser.ModifiedBy = Utilities.AnonymousUserID; //ToDoM
            merchantUser.MerchantRequest.MerchantHistory.Add(new MerchantHistory
            {
                MerchantRequestStatusId = (int)MerchantRequestStatuses.Active,
                CreatedAt = (byte)requestHeader.LocationId,
                CreatedOn = DateTime.Now,
                CreatedBy = Utilities.AnonymousUserID


            });
            merchantUser.User = userMerchant;
            await _unitOfWork.CompleteAsync();
            var token = _utilitiesService.GenerateJwtToken(user);

            return new ResponseDto<AuthTokens?>
            {
                Code = ResponseStatusCode.Success,
                Data = new AuthTokens { Token = token }
            };

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

        public async Task<ResponseDto<bool?>> ForgetPassword(RequestHeader requestHeader, ForgetPasswordDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

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

            var url = $"/ResetPassword?email={request.Email}&token={validToken}";

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
        public async Task<ResponseDto<bool?>> ResetPassword(RequestHeader requestHeader, ResetPasswordDto request)
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
        #endregion 
    }

}

