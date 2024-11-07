
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Data.Dapper;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Logger;
using Duc.Splitt.MIDIntegrationService;
using Microsoft.Extensions.Configuration;
using MIDWrapper;
using System.ServiceModel;
using System.ServiceModel.Channels;
using static Duc.Splitt.Common.Dtos.Requests.PACIMobileIdRequest;
using static Duc.Splitt.Common.Dtos.Responses.PACIMobileIdResponse;

namespace Duc.Splitt.Service
{
    public class MIDServiceAuthenticationService : IMIDServiceAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerService _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperDBConnection _dapperDBConnection;
        private readonly string username = "";
        private readonly string password = "";
        private readonly string mIDAssuranceLevel_AUTH = "";
        private readonly string signatureProfileName = "";
        private readonly string authenticationReasonAr = "";
        private readonly string authenticationReasonEn = "";
        private readonly string authServiceDescriptionAR = "";
        private readonly string authServiceDescriptionEN = "";
        private readonly string sPCallbackURLPN = "";
        private readonly string authServiceURL = "";
        public MIDServiceAuthenticationService(IConfiguration configuration, IUnitOfWork unitOfWork, IDapperDBConnection dapperDBConnection, ILoggerService logger)
        {
            _configuration = configuration;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _dapperDBConnection = dapperDBConnection;
            username = _configuration["MIDServiceAppSettings:Username"] ?? "";
            password = _configuration["MIDServiceAppSettings:Password"] ?? "";
            mIDAssuranceLevel_AUTH = _configuration["MIDServiceAppSettings:MIDAssuranceLevel_AUTH"] ?? "";
            signatureProfileName = _configuration["MIDServiceAppSettings:SignatureProfileName"] ?? "";
            authenticationReasonAr = _configuration["MIDServiceAppSettings:AuthenticationReasonAr"] ?? "";
            authenticationReasonEn = _configuration["MIDServiceAppSettings:AuthenticationReasonEn"] ?? "";
            authServiceDescriptionAR = _configuration["MIDServiceAppSettings:AuthServiceDescriptionAR"] ?? "";
            authServiceDescriptionEN = _configuration["MIDServiceAppSettings:AuthServiceDescriptionEN"] ?? "";
            sPCallbackURLPN = _configuration["MIDServiceAppSettings:SPCallbackURLPN"] ?? "";
            authServiceURL = _configuration["MIDServiceAppSettings:AuthServiceURL"] ?? "";
        }
        public async Task<ResponseDto<MobileAuthPNResponseDto?>> InitiateAuthRequestPN(RequestHeader requestHeader, MobileAuthPNRequestDto requestDto)
        {
            ResponseDto<MobileAuthPNResponseDto?> response = new ResponseDto<MobileAuthPNResponseDto?>
            {
                Code = ResponseStatusCode.NoDataFound
            };
            _logger.LogInfo($"requestDto:{_logger.ToJson(requestDto)}");
            _logger.LogInfo($"authServiceURL:{authServiceURL}");
            var binding = new BasicHttpsBinding
            {
                CloseTimeout = TimeSpan.FromSeconds(40),
                OpenTimeout = TimeSpan.FromSeconds(40),
                ReceiveTimeout = TimeSpan.FromSeconds(40),
                SendTimeout = TimeSpan.FromSeconds(40),
                MaxBufferPoolSize = int.MaxValue,
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue,
                MessageEncoding = WSMessageEncoding.Mtom,
                Security = new BasicHttpsSecurity
                {
                    Mode = BasicHttpsSecurityMode.Transport,
                    Transport = new HttpTransportSecurity
                    {
                        ClientCredentialType = HttpClientCredentialType.Basic
                    }
                }
            };
            var midWrapperServiceEndpoint = new EndpointAddress(authServiceURL);
            var midWrapperClient = new MIDWrapperClient(binding, midWrapperServiceEndpoint);
            midWrapperClient.ClientCredentials.UserName.UserName = username;
            midWrapperClient.ClientCredentials.UserName.Password = password;
            try
            {
                string refID = Guid.NewGuid().ToString();
                MIDWrapper.AuthenticateRequest auth = new MIDWrapper.AuthenticateRequest();
                auth.PersonCivilNo = requestDto.CivilId;
                auth.AuthenticationReasonAr = authenticationReasonAr;
                auth.AuthenticationReasonEn = authenticationReasonEn;
                auth.ServiceDescriptionAR = authServiceDescriptionAR;
                auth.ServiceDescriptionEN = authServiceDescriptionEN;
                auth.SPCallbackURL = sPCallbackURLPN;
                auth.RefId = requestDto.RefId.ToString();
                auth.Challenge = "";
                auth.AdditionalData = requestDto.CustomerRegistrationRequestId.ToString();
                auth.RequestUserDetails = true;
                _logger.LogInfo($"RequestDto:{_logger.ToJson(auth)}");
                MIDWrapper.MIDAssuranceLevel mIDAssuranceLevel = MIDWrapper.MIDAssuranceLevel.High;
                if (mIDAssuranceLevel_AUTH == "20")
                {
                    mIDAssuranceLevel = MIDWrapper.MIDAssuranceLevel.Medium;
                }
                _logger.LogInfo($"mIDAssuranceLevel:{_logger.ToJson(mIDAssuranceLevel)}");

                var resposnePACI = await midWrapperClient.InitiateAuthRequestPNWithAssuranceLevelAsync(auth, mIDAssuranceLevel);

                _logger.LogInfo($"resposne:{_logger.ToJson(resposnePACI)}");
                if (resposnePACI != null && !string.IsNullOrEmpty(resposnePACI.Data))
                {
                    response.Data = new MobileAuthPNResponseDto { DSPRefNo = resposnePACI?.Data };
                    response.Code = ResponseStatusCode.Success;
                    return response;
                }
                else
                {
                    string errorCode = resposnePACI != null && resposnePACI.Error != null && resposnePACI.Error?.Code > 0 ? resposnePACI.Error.Code.ToString() : "";
                    string errorMessage = resposnePACI != null && resposnePACI.Error != null && !string.IsNullOrEmpty(resposnePACI.Error.Message) ? Convert.ToString(resposnePACI.Error.Message) : "";
                    response.Code = ResponseStatusCode.MIDAPIIssue;
                    response.Errors = new List<string> { $"errorCode:{errorCode}, errorMessage:{errorMessage}" };
                    _logger.LogInfo($"resposne:{resposnePACI}");
                    return response;
                }
            }
            catch
            {
                midWrapperClient.Abort();
                throw;
            }
            finally
            {
                midWrapperClient.Close();
                await midWrapperClient.CloseAsync();
            }
        }

        public async Task<ResponseDto<bool?>> CallBackPN(CallbackResponse PACIcallback)
        {
            ResponseDto<bool?> response = new ResponseDto<bool?>
            {
                Code = ResponseStatusCode.NoDataFound
            };
            _logger.LogInfo($"PACIcallback:{_logger.ToJson(PACIcallback)}");
            if (PACIcallback != null && PACIcallback.MIDAuthSignResponse != null && PACIcallback.MIDAuthSignResponse.RequestDetails != null)

            {

                if (PACIcallback.MIDAuthSignResponse.PersonalData != null
                                 && Convert.ToInt32(PACIcallback.MIDAuthSignResponse.ResultDetails.ResultCode) == (int)ResultCode.Authenticated
                                       && Convert.ToInt32(PACIcallback.MIDAuthSignResponse.ResultDetails.UserAction) == (int)UserAction.AuthenticateAccept)
                {
                    MidRequestLog midRequestLog = new MidRequestLog
                    {
                        CustomerRegistrationRequestId = Guid.Parse(PACIcallback.MIDAuthSignResponse.RequestDetails.AdditionalData),
                        DsprefId = PACIcallback.MIDAuthSignResponse.RequestDetails.RequestID,
                        MidpayloadRequest = "",
                        MidpayloadResponse = _logger.ToJson(PACIcallback),
                        MidRequestStatusId = (int)MidRequestStatuses.CallBackProcessSucess,
                        MidRequestTypeId = (int)MidRequestTypes.AuthenticationPN,
                    };
                    _unitOfWork.MidRequestLogs.AddAsync(midRequestLog);
                    await _unitOfWork.CompleteAsync();

                    response.Code = ResponseStatusCode.Success;
                    response.Errors = new List<string> { };
                    return response;
                }
                else if (Convert.ToInt32(PACIcallback.MIDAuthSignResponse.ResultDetails.UserAction) == (int)UserAction.Decline)
                {
                    MidRequestLog midRequestLog = new MidRequestLog
                    {
                        CustomerRegistrationRequestId = Guid.Parse(PACIcallback.MIDAuthSignResponse.RequestDetails.AdditionalData),
                        DsprefId = PACIcallback.MIDAuthSignResponse.RequestDetails.RequestID,
                        MidpayloadRequest = "",
                        MidpayloadResponse = _logger.ToJson(PACIcallback),
                        MidRequestStatusId = (int)MidRequestStatuses.Reject,
                        MidRequestTypeId = (int)MidRequestTypes.AuthenticationPN,
                    };
                    _unitOfWork.MidRequestLogs.AddAsync(midRequestLog);
                    await _unitOfWork.CompleteAsync();
                    response.Code = ResponseStatusCode.Success;
                    response.Errors = new List<string> { };
                    return response;
                }
                else
                {
                    response.Code = ResponseStatusCode.InvalidPACIData;
                    response.Errors = new List<string> { };
                    return response;
                }
            }
            else
            {
                response.Code = ResponseStatusCode.InvalidPACIData;
                response.Errors = new List<string> { };
                return response;
            }

        }


    }
    public enum ResultCode
    {
        Authenticated = 1,
        UserCertificateRevoked = 2,
        FailedToVerify = 3,
        Declined = 4,
        CertificateExpired = 5
    }
    public enum UserAction
    {
        AuthenticateAccept = 1,
        Decline = 2,

    }

}
