
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Data.Dapper;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Logger;
using Duc.Splitt.MIDIntegrationService;
using Microsoft.Extensions.Configuration;
using System.ServiceModel;
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
            _logger.LogInfo($"requestDto:{requestDto}");
            string dynamicUrl = authServiceURL;
            var endpointAddress = new EndpointAddress(dynamicUrl);
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            var client = new MIDWrapper.MIDWrapperClient(binding, endpointAddress);
            client.ChannelFactory.Credentials.UserName.UserName = username;
            client.ChannelFactory.Credentials.UserName.Password = password;
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
                auth.RefId = requestDto.CustomerRegistrationRequestId.ToString();
                auth.Challenge = "";
                auth.RequestUserDetails = true;
                _logger.LogInfo($"RequestDto:{auth}");
                MIDWrapper.MIDAssuranceLevel mIDAssuranceLevel = MIDWrapper.MIDAssuranceLevel.High;
                if (mIDAssuranceLevel_AUTH == "20")
                {
                    mIDAssuranceLevel = MIDWrapper.MIDAssuranceLevel.Medium;
                }
                _logger.LogInfo($"mIDAssuranceLevel:{mIDAssuranceLevel}");

                var resposnePACI = await client.InitiateAuthRequestPNAsync(auth);
                client.Close();
                _logger.LogInfo($"resposne:{resposnePACI}");
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
                client.Abort();
                throw;
            }
        }

        public async Task<ResponseDto<bool?>> CallBackPN(CallbackResponse PACIcallback)
        {
            ResponseDto<bool?> response = new ResponseDto<bool?>
            {
                Code = ResponseStatusCode.NoDataFound
            };
            if (PACIcallback != null && PACIcallback.MIDAuthSignResponse != null && PACIcallback.MIDAuthSignResponse.RequestDetails != null)

            {

                if (PACIcallback.MIDAuthSignResponse.PersonalData != null
                                 && Convert.ToInt32(PACIcallback.MIDAuthSignResponse.ResultDetails.ResultCode) == (int)ResultCode.Authenticated
                                       && Convert.ToInt32(PACIcallback.MIDAuthSignResponse.ResultDetails.UserAction) == (int)UserAction.AuthenticateAccept)
                {
                    response.Code = ResponseStatusCode.Success;
                    response.Errors = new List<string> { };
                    return response;
                }
                else if (Convert.ToInt32(PACIcallback.MIDAuthSignResponse.ResultDetails.UserAction) == (int)UserAction.Decline)
                {
                    MidRequestLog log = new MidRequestLog { };
                    _unitOfWork.MidRequestLogs.AddAsync(log);
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
        public interface IMIDServiceAuthenticationService
        {
            Task<ResponseDto<MobileAuthPNResponseDto?>> InitiateAuthRequestPN(RequestHeader requestHeader, MobileAuthPNRequestDto requestDto);
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
