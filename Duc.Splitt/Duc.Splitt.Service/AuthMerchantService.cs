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
    public class AuthMerchantService : IAuthMerchantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperDBConnection _dapperDBConnection;
        private readonly UserManager<SplittIdentityUser> _userManager;
        private readonly RoleManager<SplittIdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUtilitiesService _utilitiesService;
        public AuthMerchantService(IUnitOfWork unitOfWork, IDapperDBConnection dapperDBConnection, UserManager<SplittIdentityUser> userManager, RoleManager<SplittIdentityRole> roleManager, IConfiguration configuration, IUtilitiesService utilitiesService)
        {
            _unitOfWork = unitOfWork;
            _dapperDBConnection = dapperDBConnection;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _utilitiesService = utilitiesService;
        }

        public async Task<ResponseDto<string?>> ApproveMerchantUser(RequestHeader requestHeader, RegisterDto request)
        {
            SplittIdentityUser splittIdentityUser = new SplittIdentityUser
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName
            };

            var userExistsbyName = await _userManager.FindByNameAsync(request.UserName);
            if (userExistsbyName != null)
            {
                return new ResponseDto<string?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "User already exists",
                    Errors = new List<string> { $"{request.UserName} User already exists  in User" }
                };
            }
            var userExistsByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userExistsByEmail != null)
            {
                return new ResponseDto<string?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "User already exists",
                    Errors = new List<string> { $"{request.Email} User already exists in User" }
                };
            }
            var merchantUser = await _unitOfWork.MerchantUsers.GetMerchantRequestByEmail(request.Email);
            if (merchantUser == null)
            {
                return new ResponseDto<string?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "Email Not Available in Request",
                    Errors = new List<string> { $"{request.Email} Email Not Available in Request" }
                };
            }
            if (merchantUser != null && merchantUser.MerchantRequest.MerchantRequestStatusId != (int)MerchantRequestStatuses.InProgress)
            {
                return new ResponseDto<string?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = $" {merchantUser.MerchantRequest.MerchantRequestStatusId} Request Status is not valid for Approve",
                    Errors = new List<string> { $"{merchantUser.MerchantRequest.MerchantRequestStatusId}.RequestStatusId Request Status is not valid for Approve" }
                };
            }
            if (merchantUser != null)
            {
                var result = await _userManager.CreateAsync(splittIdentityUser);
                if (!result.Succeeded)
                {
                    return new ResponseDto<string?>
                    {
                        Code = ResponseStatusCode.ServerError,
                        Message = $"User Creation failed",
                        Errors = _utilitiesService.GetErrorMessages(result)
                    };

                }
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(splittIdentityUser);

                string languageText = requestHeader.IsArabic ? "ar" : "en";
                var activationLink = $"{_configuration["Jwt:MerchantVerify"]}?identifier={splittIdentityUser.Id}&token={token}?lang={languageText}";//toDO

                merchantUser.MerchantRequest.MerchantRequestStatusId = (int)MerchantRequestStatuses.Approved;
                merchantUser.MerchantRequest.ModifiedAt = (byte)requestHeader.LocationId;
                merchantUser.MerchantRequest.ModifiedOn = DateTime.Now;
                merchantUser.MerchantRequest.ModifiedBy = Utilities.AnonymousUserID; //ToDoM
               
                merchantUser.MerchantRequest.MerchantHistory.Add(new MerchantHistory
                {
                    MerchantRequestStatusId = (int)MerchantRequestStatuses.Approved,
                    CreatedAt = (byte)requestHeader.LocationId,
                    CreatedOn = DateTime.Now,
                    CreatedBy = Utilities.AnonymousUserID,//ToDo
                    Comment = request.Comments
                });
                await _unitOfWork.CompleteAsync();

                // Send Welcome EMail//TODoM
                return new ResponseDto<string?>
                {
                    Message = activationLink,
                    Code = ResponseStatusCode.Success,
                    Data = merchantUser.MerchantRequest.RequestNo,

                };

            }
            else
            {
                return new ResponseDto<string?>
                {
                    Code = ResponseStatusCode.ServerError,
                    Message = "Could not register user!",
                    Errors = new List<string> { "Could not register user!" }
                };
            }

        }

        public async Task<ResponseDto<AuthTokens?>> ActivateMerchantUser(RequestHeader requestHeader, SetPasswordDto request)
        {

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new ResponseDto<AuthTokens?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "User not exists",
                    Errors = new List<string> { $"{request.UserId} is not available" }
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

            // Confirm email with the provided token
            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
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
            merchantUser.MerchantRequest.MerchantRequestStatusId = (int)MerchantRequestStatuses.Active;
            merchantUser.ModifiedAt = (byte)requestHeader.LocationId;
            merchantUser.ModifiedOn = DateTime.Now;
            merchantUser.ModifiedBy = Utilities.AnonymousUserID; //ToDoM
            merchantUser.MerchantRequest.MerchantHistory.Add(new MerchantHistory
            {
                MerchantRequestStatusId = (int)MerchantRequestStatuses.Active,
                CreatedAt = (byte)requestHeader.LocationId,
                CreatedOn = DateTime.Now,
                CreatedBy = Utilities.AnonymousUserID,
                Comment = request.Comments,


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

