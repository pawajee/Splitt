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
using static Duc.Splitt.Common.Dtos.Requests.AuthBackOfficeUserDto;

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
        private readonly SignInManager<SplittIdentityUser> _signInManager;
        public AuthBackOfficeService(IUnitOfWork unitOfWork, IDapperDBConnection dapperDBConnection, UserManager<SplittIdentityUser> userManager, RoleManager<SplittIdentityRole> roleManager, IConfiguration configuration, IUtilitiesService utilitiesService, SignInManager<SplittIdentityUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _dapperDBConnection = dapperDBConnection;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _utilitiesService = utilitiesService;
            _signInManager = signInManager;
        }

        public async Task<ResponseDto<string?>> ApproveMerchantUserByAdmin(RequestHeader requestHeader, RegisterDto request)
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
            var merchant = await _unitOfWork.Merchants.GetMerchantRequestByEmail(request.Email);
            if (merchant == null)
            {
                return new ResponseDto<string?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "Email Not Available in Request",
                    Errors = new List<string> { $"{request.Email} Email Not Available in Request" }
                };
            }

            var merchantUserCheck = await _unitOfWork.MerchantContacts.GetMerchantRequestByEmail(request.Email);
            if (merchantUserCheck != null)
            {
                return new ResponseDto<string?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "Email is Already Registered in MerchantUsers",
                    Errors = new List<string> { $"{request.Email} Email Not Available in Request" }
                };
            }
            if (merchant != null && merchant.MerchantStatusId != (int)MerchantRequestStatuses.InProgress)
            {
                return new ResponseDto<string?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = $" {merchant.MerchantStatusId} Request Status is not valid for Approve",
                    Errors = new List<string> { $"{merchant.MerchantStatusId}.RequestStatusId Request Status is not valid for Approve" }
                };
            }
            if (merchant != null)
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
                var encodedToken = Encoding.UTF8.GetBytes(token);
                var validToken = WebEncoders.Base64UrlEncode(encodedToken);
                var activationLink = $"{_configuration["Jwt:MerchantVerify"]}?identifier={splittIdentityUser.Id}&token={validToken}?lang={languageText}";//toDO

                merchant.MerchantStatusId = (int)MerchantRequestStatuses.Approved;
                merchant.ModifiedAt = (byte)requestHeader.LocationId;
                merchant.ModifiedOn = DateTime.Now;
                merchant.ModifiedBy = Utilities.AnonymousUserID; //ToDoM

                merchant.MerchantHistory.Add(new MerchantHistory
                {
                    MerchantRequestStatusId = (int)MerchantRequestStatuses.Approved,
                    CreatedAt = (byte)requestHeader.LocationId,
                    CreatedOn = DateTime.Now,
                    CreatedBy = Utilities.AnonymousUserID,//ToDo
                    Comment = request.Comments
                });
                var merchantUser = new MerchantContact
                {
                    NameArabic = merchant.BusinessNameArabic,
                    NameEnglish = merchant.BusinessNameEnglish,
                    MobileNo = merchant.MobileNo,
                    BusinessEmail = merchant.BusinessEmail,
                    CreatedAt = (byte)requestHeader.LocationId,
                    CreatedOn = DateTime.Now,
                    IsPrimary = true,
                    CreatedBy = Utilities.AnonymousUserID,
                    User = new User
                    {
                        LoginId = merchant.BusinessEmail,
                        UserTypeId = (int)UserTypes.Consumer,
                        IsActive = true,
                        CreatedAt = (byte)requestHeader.LocationId,
                        CreatedOn = DateTime.Now,
                        CreatedBy = Utilities.AnonymousUserID,
                    }
                };
                merchant.MerchantContact.Add(merchantUser);
                await _unitOfWork.CompleteAsync();

                // Send Welcome EMail//TODoM
                return new ResponseDto<string?>
                {
                    Message = activationLink,
                    Code = ResponseStatusCode.Success,
                    Data = merchant.RequestNo,

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


        public async Task<ResponseDto<bool?>> CreateUser(RequestHeader requestHeader, CreateAdminUserDto request)
        {
            if (request == null)
                throw new NullReferenceException("ChangePassword Model is null");

            SplittIdentityUser splittIdentityUser = new SplittIdentityUser
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Email,
            };
            var userExistsbyName = await _userManager.FindByNameAsync(request.Email);
            if (userExistsbyName != null)
            {
                return new ResponseDto<bool?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "User already exists",
                    Errors = new List<string> { $"{request.Email} User already exists  in User" }
                };
            }
            var userExistsByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userExistsByEmail != null)
            {
                return new ResponseDto<bool?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "User already exists",
                    Errors = new List<string> { $"{request.Email} User already exists in User" }
                };
            }
            var users = await _unitOfWork.Users.FindAsync(t => t.LoginId == request.Email);
            if (users != null && users.Count() > 0)
            {
                return new ResponseDto<bool?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "User already exists",
                    Errors = new List<string> { $"{request.Email} User already exists" }
                };
            }
            var result = await _userManager.CreateAsync(splittIdentityUser, "P@ssw0rd@1379");
            if (!result.Succeeded)
            {
                return new ResponseDto<bool?>
                {
                    Code = ResponseStatusCode.ServerError,
                    Message = $"User Creation failed",
                    Errors = _utilitiesService.GetErrorMessages(result)
                };

            }
            _unitOfWork.BackOfficeUsers.AddAsync(new BackOfficeUser
            {
                NameArabic = request.NameArabic,
                NameEnglish = request.NameEnglish,
                MobileNo = request.MobileNo,
                Email = request.Email,
                CreatedAt = (byte)requestHeader.LocationId,
                CreatedOn = DateTime.Now,
                CreatedBy = Utilities.AnonymousUserID,//ToDo
                User = new User
                {
                    CreatedAt = (byte)requestHeader.LocationId,
                    CreatedOn = DateTime.Now,
                    CreatedBy = Utilities.AnonymousUserID,
                    LoginId = request.Email,
                }
            });
            await _unitOfWork.CompleteAsync();

            // Send Welcome EMail//TODoM
            return new ResponseDto<bool?>
            {
                Message = "",
                Code = ResponseStatusCode.Success,
                Data = true,

            };



        }

        #region Anonymous API 
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
    }

}

