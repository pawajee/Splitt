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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Duc.Splitt.Common.Dtos.Requests.AuthConsumerUserDto;

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

        public async Task<ResponseDto<string?>> RequestConsumerUserOTP(RequestHeader requestHeader, RegisterDto request)
        {


            // Generate OTP and send SMS
            var otp = "123456"; _UtilitiesService.GenerateOtp();

            // await _smsService.SendSmsAsync(user.PhoneNumber, $"Your OTP is: {otp}");

            // Save OTP to database or in-memory store for verification
            //await SaveOtpAsync(user.PhoneNumber, otp);
            return new ResponseDto<string?>
            {
                Message = otp,
                Code = ResponseStatusCode.Success,
                Data = "",

            };

        }

        public async Task<ResponseDto<AuthTokens?>> VerifyConsumerUserOTP(VerifyOtpDto request)
        {
            var savedOtp = "123456"; //await GetOtpAsync(verifyDto.PhoneNumber);
            if (savedOtp == request.Otp)
            {
                // OTP is valid, log the user in
                var user = await _userManager.FindByNameAsync(request.MobileNo);
                if (user == null)
                {
                    SplittIdentityUser splittIdentityUser = new SplittIdentityUser
                    {

                        SecurityStamp = Guid.NewGuid().ToString(),
                        PhoneNumber = request.MobileNo,
                        UserName = request.MobileNo,
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

                // Generate authentication token (optional, based on your authentication logic)
                // var token = await _userManager.GenerateUserTokenAsync(user, "Default", "Login");

                var token = _UtilitiesService.GenerateJwtToken(user);
                return new ResponseDto<AuthTokens?>
                {
                    Code = ResponseStatusCode.Success,
                    Data = new AuthTokens { Token = token }
                };
            }
            else
            {
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

