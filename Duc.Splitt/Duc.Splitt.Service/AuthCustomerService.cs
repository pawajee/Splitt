using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Common.Helpers;
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Data.Dapper;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Identity;
using Duc.Splitt.Logger;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using static Duc.Splitt.Common.Dtos.Requests.AuthConsumerUserRequestDto;
using static Duc.Splitt.Common.Dtos.Requests.PACIMobileIdRequest;
using static Duc.Splitt.Common.Dtos.Responses.PACIMobileIdResponse;

namespace Duc.Splitt.Service
{
    public class AuthCustomerService : IAuthCustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperDBConnection _dapperDBConnection;
        private readonly UserManager<SplittIdentityUser> _userManager;
        private readonly RoleManager<SplittIdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUtilitiesService _UtilitiesService;
        private readonly ILoggerService _logger;

        private readonly IMIDServiceAuthenticationService _midServiceAuthenticationService;
        public AuthCustomerService(IUnitOfWork unitOfWork, IDapperDBConnection dapperDBConnection, UserManager<SplittIdentityUser> userManager, RoleManager<SplittIdentityRole> roleManager, IConfiguration configuration, IUtilitiesService utilitiesService, IMIDServiceAuthenticationService mIDServiceAuthenticationService, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _dapperDBConnection = dapperDBConnection;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _UtilitiesService = utilitiesService;
            _midServiceAuthenticationService = mIDServiceAuthenticationService;
            _logger = logger;
        }

        public async Task<ResponseDto<bool?>> RequestOTP(RequestHeader requestHeader, RegisterDto request)
        {
            var resposne = new ResponseDto<bool?>
            {
                Code = ResponseStatusCode.NoDataFound,
                Data = true,
            };
            var fiveMinutesAgo = DateTime.UtcNow.AddMinutes(-5);
            var otpCount = await _unitOfWork.OtpRequests
                .FindAsync(t => t.MobileNo == request.MobileNo && t.CreatedOn >= fiveMinutesAgo);
            if (otpCount != null && otpCount.Count() > 5)
            {
                resposne.Code = ResponseStatusCode.OTPRequestLimitExceeded;
                resposne.Data = null;
                resposne.Errors = new List<string> { $"otpCount={otpCount} Requst reached Limit" };
            }

            var customer = await _unitOfWork.Customers.GetCustomerByMobileNo(request.MobileNo);
            var user = await _userManager.FindByNameAsync(request.MobileNo);
            bool IsRegistredUser = customer != null && user != null ? true : false;
            var otp = _UtilitiesService.GenerateOtp();
            _unitOfWork.OtpRequests.AddAsync(new OtpRequest
            {
                MobileNo = request.MobileNo,
                Otp = otp,
                OtpPurposeId = IsRegistredUser ? (int)OtpPurposes.Login : (int)OtpPurposes.Registration,
                CreatedAt = (byte)requestHeader.LocationId,
                CreatedOn = DateTime.Now,
                CreatedBy = Utilities.AnonymousUserID,//ToDo
                ExpiredOn = DateTime.Now.AddMinutes(5),// toDO

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

        public async Task<ResponseDto<VerifyOtpResponse?>> VerifyOTP(RequestHeader requestHeader, VerifyOtpDto request)
        {
            ResponseDto<VerifyOtpResponse?> response = new ResponseDto<VerifyOtpResponse?>
            {
                Code = ResponseStatusCode.NoDataFound
            };
            var otpRequest = await _unitOfWork.OtpRequests.GetLatestOtpRequestByMobileNo(request.MobileNo);
            if (otpRequest == null)
            {
                return response; // No OTP exists for this mobile number
            }
            var customer = await _unitOfWork.Customers.GetCustomerByMobileNo(request.MobileNo);
            var user = await _userManager.FindByNameAsync(request.MobileNo);
            bool IsRegistredUser = customer != null && user != null ? true : false;
            if (otpRequest.IsUsed.HasValue && otpRequest.IsUsed == true)
            {
                otpRequest.Status = "Already Used";
                await _unitOfWork.CompleteAsync();
                response.Code = ResponseStatusCode.OTPAlreadyUsed;
                return response;
            }
            if (otpRequest.ExpiredOn < DateTime.Now)
            {
                otpRequest.Status = "Expired";
                await _unitOfWork.CompleteAsync();
                response.Code = ResponseStatusCode.OTPExpired;
                return response;
            }
            int maxAttempts = 3; // toDo
            if (otpRequest.NumberofAttempts >= maxAttempts)
            {
                otpRequest.Status = "Exceeded max attempts";
                await _unitOfWork.CompleteAsync();
                response.Code = ResponseStatusCode.OTPVerifyLimitExceeded;
                return response;
            }
            if (otpRequest != null && otpRequest.Otp == request.Otp)
            {
                otpRequest.NumberofAttempts = otpRequest.NumberofAttempts.HasValue ? otpRequest.NumberofAttempts + 1 : 1;
                otpRequest.Status = "Used";
                otpRequest.IsUsed = true;
                otpRequest.VerifiedOn = DateTime.Now;
                if (IsRegistredUser)
                {
                    var token = _UtilitiesService.GenerateJwtToken(user);
                    return new ResponseDto<VerifyOtpResponse?>
                    {
                        Code = ResponseStatusCode.Success,
                        Data = new VerifyOtpResponse { OtpRequestId = otpRequest.Id, IsNewCustomer = false, AuthTokens = new AuthTokens { Token = token } },
                    };
                }
                else
                {
                    return new ResponseDto<VerifyOtpResponse?>
                    {
                        Data = new VerifyOtpResponse { OtpRequestId = otpRequest.Id, IsNewCustomer = true },
                        Code = ResponseStatusCode.Success,
                    };
                }


            }
            else
            {
                otpRequest.NumberofAttempts = otpRequest.NumberofAttempts.HasValue ? otpRequest.NumberofAttempts + 1 : 1;// Increment attempt count
                otpRequest.Status = "Failed";
                return new ResponseDto<VerifyOtpResponse?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "Invalid OTP",
                    Errors = new List<string> { $"{request.MobileNo} - OTP is not valid" }
                };
            }

        }

        public async Task<ResponseDto<CustomerRegistrationResponseDto?>> CustomerRegistrationRequest(RequestHeader requestHeader, CustomerRegistrationRequestDto request)
        {
            ResponseDto<CustomerRegistrationResponseDto?> response = new ResponseDto<CustomerRegistrationResponseDto?>
            {
                Code = ResponseStatusCode.NoDataFound
            };
            var otpRequest = await _unitOfWork.OtpRequests.GetOtpRequestById(request.OtpRequestId);
            if (otpRequest == null)
            {
                response.Code = ResponseStatusCode.OTPRequestNotFound;
                return response;
            }
            if (otpRequest.VerifiedOn == null)
            {
                response.Code = ResponseStatusCode.OTPVerificationNotCompleted;
                return response;
            }
            var savedCustomerRegistrationRequestsCount = await _unitOfWork.CustomerRegistrationRequests.CustomerRegistrationRequestWithin15Min(request.CivilId);
            if (savedCustomerRegistrationRequestsCount != null && savedCustomerRegistrationRequestsCount > 3)
            {
                response.Code = ResponseStatusCode.OTPRequestLimitExceeded;
                response.Data = null;
                response.Errors = new List<string> { $"Count={savedCustomerRegistrationRequestsCount} Requst reached Limit" };
            }

            var customerRegistrationRequestId = Guid.NewGuid();
            CustomerRegistrationRequest customerRegistrationRequest = new CustomerRegistrationRequest
            {
                CivilId = request.CivilId,
                CustomerRegistrationStatusId = (int)CustomerRegistrationStatuses.PACIPending,
                MobileNo = otpRequest.MobileNo,
                OtpRequestId = otpRequest.Id,
                CreatedOn = DateTime.Now,
                CreatedBy = Utilities.AnonymousUserID,//ToDo
                CreatedAt = (byte)requestHeader.LocationId,
                Id = customerRegistrationRequestId,
            };
            _unitOfWork.CustomerRegistrationRequests.AddAsync(customerRegistrationRequest);
            await _unitOfWork.CompleteAsync();
            var savedCustomerRegistrationRequests = await _unitOfWork.CustomerRegistrationRequests.GetAsync(customerRegistrationRequestId);
            MobileAuthPNRequestDto mobileAuthPNRequestDto = new MobileAuthPNRequestDto
            {
                CivilId = request.CivilId,
                CustomerRegistrationRequestId = customerRegistrationRequestId
            };
            ResponseDto<MobileAuthPNResponseDto?> paciresponse = new ResponseDto<MobileAuthPNResponseDto?>();
            try
            {
                paciresponse = await _midServiceAuthenticationService.InitiateAuthRequestPN(requestHeader, mobileAuthPNRequestDto);
                if (paciresponse != null && paciresponse.Data != null && !string.IsNullOrEmpty(paciresponse.Data.DSPRefNo))
                {
                    MidRequestLog midRequestLog = new MidRequestLog
                    {
                        CustomerRegistrationRequestId = customerRegistrationRequestId,
                        DsprefId = paciresponse.Data.DSPRefNo,
                        MidpayloadRequest = _logger.ToJson(mobileAuthPNRequestDto),
                        MidpayloadResponse = _logger.ToJson(paciresponse),
                        MidRequestStatusId = (int)MidRequestStatuses.InitiateSucess,
                        MidRequestTypeId = (int)MidRequestTypes.AuthenticationPN,
                    };
                    _unitOfWork.MidRequestLogs.AddAsync(midRequestLog);
                    savedCustomerRegistrationRequests.PacinumberofAttempts = savedCustomerRegistrationRequests.PacinumberofAttempts.HasValue ? savedCustomerRegistrationRequests.PacinumberofAttempts + 1 : 1;
                    await _unitOfWork.CompleteAsync();
                    return new ResponseDto<CustomerRegistrationResponseDto?>
                    {
                        Code = ResponseStatusCode.Success,
                        Data = new CustomerRegistrationResponseDto { CustomerRegistrationRequestId = savedCustomerRegistrationRequests.Id }
                    };
                }
                else
                {
                    MidRequestLog midRequestLog = new MidRequestLog
                    {
                        CustomerRegistrationRequestId = customerRegistrationRequestId,
                        MidpayloadRequest = _logger.ToJson(mobileAuthPNRequestDto),
                        MidpayloadResponse = _logger.ToJson(paciresponse),
                        MidRequestStatusId = (int)MidRequestStatuses.MIDIssue,
                        MidRequestTypeId = (int)MidRequestTypes.AuthenticationPN,
                    };
                    _unitOfWork.MidRequestLogs.AddAsync(midRequestLog);
                    savedCustomerRegistrationRequests.PacinumberofAttempts = savedCustomerRegistrationRequests.PacinumberofAttempts.HasValue ? savedCustomerRegistrationRequests.PacinumberofAttempts + 1 : 1;
                    await _unitOfWork.CompleteAsync();
                    return new ResponseDto<CustomerRegistrationResponseDto?>
                    {
                        Code = ResponseStatusCode.Success,
                        Data = new CustomerRegistrationResponseDto { CustomerRegistrationRequestId = savedCustomerRegistrationRequests.Id }
                    };
                }
            }
            catch (Exception ex)
            {
                MidRequestLog midRequestLog = new MidRequestLog
                {
                    CustomerRegistrationRequestId = customerRegistrationRequestId,
                    DsprefId = "",
                    MidpayloadRequest = _logger.ToJson(mobileAuthPNRequestDto),
                    MidpayloadResponse = _logger.ToJson(_logger.ConvertExceptionToStringList(ex)),
                    MidRequestStatusId = (int)MidRequestStatuses.MIDIssue,
                    MidRequestTypeId = (int)MidRequestTypes.AuthenticationPN,
                };
                _unitOfWork.MidRequestLogs.AddAsync(midRequestLog);
                savedCustomerRegistrationRequests.PacinumberofAttempts = savedCustomerRegistrationRequests.PacinumberofAttempts.HasValue ? savedCustomerRegistrationRequests.PacinumberofAttempts + 1 : 1;
                await _unitOfWork.CompleteAsync();
                return new ResponseDto<CustomerRegistrationResponseDto?>
                {
                    Code = ResponseStatusCode.Success,
                    Data = new CustomerRegistrationResponseDto { CustomerRegistrationRequestId = savedCustomerRegistrationRequests.Id }
                };
            }

        }

        public async Task<ResponseDto<CheckMIdRequestStatusDto?>> CheckMidStatus(RequestHeader requestHeader, CheckMIdRequestStatusDto request)
        {
            ResponseDto<CheckMIdRequestStatusDto?> response = new ResponseDto<CheckMIdRequestStatusDto?>
            {
                Code = ResponseStatusCode.NoDataFound
            };
            var customerRegistrationRequestStatusId = await _unitOfWork.CustomerRegistrationRequests.CustomerRegistrationRequestStatusId(request.CustomerRegistrationRequestId);
            if (customerRegistrationRequestStatusId == null)
            {
                response.Code = ResponseStatusCode.NoDataFound;
                return response;
            }
            else
            {
                response.Code = ResponseStatusCode.NoDataFound;
                response.Data = new CheckMIdRequestStatusDto { CustomerRegistrationRequestId = request.CustomerRegistrationRequestId, CustomerRegistrationRequestStatusId = customerRegistrationRequestStatusId };
                return response;
            }
        }

    }
}


