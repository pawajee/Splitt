using Azure.Core;
using Duc.Splitt.Common.Dtos.Requests;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Common.Helpers;
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Data.Dapper;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Duc.Splitt.Common.Dtos.Requests.AuthConsumerUserDto;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;

namespace Duc.Splitt.Service
{
    public class AuthConsumerService : IAuthConsumerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperDBConnection _dapperDBConnection;
        private readonly UserManager<SplittIdentityUser> _userManager;
        private readonly RoleManager<SplittIdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUtilitiesService _UtilitiesService;

        public AuthConsumerService(IUnitOfWork unitOfWork, IDapperDBConnection dapperDBConnection, UserManager<SplittIdentityUser> userManager, RoleManager<SplittIdentityRole> roleManager, IConfiguration configuration, IUtilitiesService utilitiesService)
        {
            _unitOfWork = unitOfWork;
            _dapperDBConnection = dapperDBConnection;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _UtilitiesService = utilitiesService;
        }

        public async Task<ResponseDto<bool?>> RequestConsumerUserOTP(RequestHeader requestHeader, RegisterDto request)
        {
            // Generate OTP and send SMS
            var otp = _UtilitiesService.GenerateOtp();
            _unitOfWork.ConsumerOtpRequests.AddAsync(new ConsumerOtpRequest
            {
                MobileNo = request.MobileNo,
                Otp = otp,
                Attempts = 1,
                CreatedAt = (byte)requestHeader.LocationId,
                CreatedOn = DateTime.Now,
                CreatedBy = Utilities.AnonymousUserID,//ToDo
            });

            await _unitOfWork.CompleteAsync();
            // Send SMS
            return new ResponseDto<bool?>
            {
                Message = otp,
                Code = ResponseStatusCode.Success,
                Data = true,

            };

        }

        public async Task<ResponseDto<AuthTokens?>> VerifyConsumerUserOTP(RequestHeader requestHeader, VerifyOtpDto request)
        {
            ResponseDto<AuthTokens?> response = new ResponseDto<AuthTokens?>
            {
                Code = ResponseStatusCode.NoDataFound
            };
            var otpRequest = await _unitOfWork.ConsumerOtpRequests.GetLatestOtpRequestByMobileNo(request.MobileNo);
            if (otpRequest == null)
            {
                return response; // No OTP exists for this mobile number
            }
            if (otpRequest.IsUsed.HasValue && otpRequest.IsUsed == true)
            {
                otpRequest.Status = "Already Used";
                await _unitOfWork.CompleteAsync();
                response.Code = ResponseStatusCode.AlreadyUsed;
                return response;
            }
            if (otpRequest.ExpiredOn < DateTime.Now)
            {
                otpRequest.Status = "Expired";
                await _unitOfWork.CompleteAsync();
                response.Code = ResponseStatusCode.OTPExpired;
                return response;
            }
            // Check if maximum attempts have been reached
            int maxAttempts = 3; // Define your max attempt limit
            if (otpRequest.Attempts >= maxAttempts)
            {
                otpRequest.Status = "Exceeded max attempts";
                await _unitOfWork.CompleteAsync();
                response.Code = ResponseStatusCode.OTPMaxAttempts;
                return response;
            }
            if (otpRequest != null && otpRequest.Otp == request.Otp)
            {
                otpRequest.Status = "Used";
                otpRequest.IsUsed = true;

                // OTP is valid, log the user in
                var user = await _userManager.FindByNameAsync(request.MobileNo);
                if (user == null)
                {
                    SplittIdentityUser splittIdentityUser = new SplittIdentityUser
                    {
                        SecurityStamp = Guid.NewGuid().ToString(),
                        PhoneNumber = request.MobileNo,
                        UserName = request.MobileNo,
                        PhoneNumberConfirmed = true,
                    };
                    var result = await _userManager.CreateAsync(splittIdentityUser);
                    if (!result.Succeeded)
                    {
                        return new ResponseDto<AuthTokens?>
                        {
                            Code = ResponseStatusCode.ServerError,
                            Message = $"User Creation failed",
                            Errors = _UtilitiesService.GetErrorMessages(result)
                        };

                    }
                }

                var consumerUser = await _unitOfWork.ConsumerUsers.GetConsumerUserByMobileNo(request.MobileNo);
                if (consumerUser == null)
                {
                    consumerUser = new ConsumerUser
                    {
                        MobileNo = request.MobileNo,
                        CreatedAt = (byte)requestHeader.LocationId,
                        CreatedOn = DateTime.Now,
                        CreatedBy = Utilities.AnonymousUserID,
                        User = new User
                        {
                            LoginId = request.MobileNo,
                            UserTypeId = (int)UserTypes.Consumer,
                            IsActive = true,
                            CreatedAt = (byte)requestHeader.LocationId,
                            CreatedOn = DateTime.Now,
                            CreatedBy = Utilities.AnonymousUserID,
                        }
                    };
                    _unitOfWork.ConsumerUsers.AddAsync(consumerUser);
                }
                await _unitOfWork.CompleteAsync();
                var token = _UtilitiesService.GenerateJwtToken(user);
                return new ResponseDto<AuthTokens?>
                {
                    Code = ResponseStatusCode.Success,
                    Data = new AuthTokens { Token = token }
                };
            }
            else
            {
                otpRequest.Attempts += 1; // Increment attempt count
                otpRequest.Status = "Failed";
                return new ResponseDto<AuthTokens?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "Invalid OTP",
                    Errors = new List<string> { $"{request.MobileNo} - OTP is not valid" }
                };
            }

        }




    }

}

