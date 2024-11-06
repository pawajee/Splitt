using Duc.Splitt.Common.Dtos.Requests;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.CustomerApi.Helper;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Logger;
using Microsoft.AspNetCore.Mvc;
using static Duc.Splitt.Common.Dtos.Requests.AuthMerchantUserDto;

namespace Duc.Splitt.CustomerApi.Controllers
{

    public class AuthController : BaseAnonymous
    {
        private readonly IAuthCustomerService _authConsumerService;
        private readonly ILoggerService _logger;
        private IUtilsService _utilsService;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public AuthController(ILookupService lookupService, ILoggerService logger, IUtilsService utilsService, IAuthCustomerService authConsumerService)
        {

            _logger = logger;
            _utilsService = utilsService;
            _authConsumerService = authConsumerService;
        }

        [HttpPost]
        public async Task<ResponseDto<bool?>> RequestConsumerUserOTP(AuthConsumerUserRequestDto.RegisterDto requestDto)
        {
            ResponseDto<bool?> response = new ResponseDto<bool?>
            {
                Code = ResponseStatusCode.NoDataFound
            };

            try
            {
                var validateRequest = await _utilsService.ValidateRequest(this.Request, null);
                if (validateRequest == null)
                {
                    response.Code = ResponseStatusCode.InvalidToken;
                    return response;
                }
                var result = await _authConsumerService.RequestOTP(validateRequest, requestDto);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                response.Code = ResponseStatusCode.ServerError;
                response.Errors = _logger.ConvertExceptionToStringList(ex);
                return response;
            }

        }

        [HttpPost]
        public async Task<ResponseDto<VerifyOtpResponse?>> VerifyConsumerUserOTP(AuthConsumerUserRequestDto.VerifyOtpDto requestDto)
        {
            ResponseDto<VerifyOtpResponse?> response = new ResponseDto<VerifyOtpResponse?>
            {
                Code = ResponseStatusCode.NoDataFound
            };

            try
            {
                var validateRequest = await _utilsService.ValidateRequest(this.Request, null);
                if (validateRequest == null)
                {
                    response.Code = ResponseStatusCode.InvalidToken;
                    return response;
                }
                var obj = await _authConsumerService.VerifyOTP(validateRequest, requestDto);
                return obj;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                response.Code = ResponseStatusCode.ServerError;
                response.Errors = _logger.ConvertExceptionToStringList(ex);
                return response;
            }

        }

    }
}


