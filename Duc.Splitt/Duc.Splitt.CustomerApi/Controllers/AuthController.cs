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
        private readonly IAuthConsumerService _authConsumerService;
        private readonly ILoggerService _logger;
        private IUtilsService _utilsService;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public AuthController(ILookupService lookupService, ILoggerService logger, IUtilsService utilsService, IAuthConsumerService authConsumerService)
        {

            _logger = logger;
            _utilsService = utilsService;
            _authConsumerService = authConsumerService;
        }

        [HttpPost]
        public async Task<ResponseDto<bool?>> RequestConsumerUserOTP(AuthConsumerUserDto.RegisterDto requestDto)
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
                var result = await _authConsumerService.RequestConsumerUserOTP(validateRequest, requestDto);
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
        public async Task<ResponseDto<AuthTokens?>> VerifyConsumerUserOTP(AuthConsumerUserDto.VerifyOtpDto requestDto)
        {
            ResponseDto<AuthTokens?> response = new ResponseDto<AuthTokens?>
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
                var obj = await _authConsumerService.VerifyConsumerUserOTP(validateRequest, requestDto);
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


