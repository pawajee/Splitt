using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.CustomerApi.Helper;
using Duc.Splitt.Logger;
using Microsoft.AspNetCore.Mvc;
using static Duc.Splitt.Common.Dtos.Requests.PACIMobileIdRequest;
using static Duc.Splitt.Common.Dtos.Responses.PACIMobileIdResponse;

namespace Duc.Splitt.CustomerApi.Controllers
{

    public class PACIController : BaseAnonymous
    {
        private readonly IMIDServiceAuthenticationService _mIDServiceAuthentication;
        private readonly ILoggerService _logger;
        private IUtilsService _utilsService;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public PACIController(IMIDServiceAuthenticationService mIDServiceAuthentication, ILoggerService logger, IUtilsService utilsService)
        {
            _mIDServiceAuthentication = mIDServiceAuthentication;
            _logger = logger;
            _utilsService = utilsService;
        }


        [HttpPost()]
        public async Task<ResponseDto<MobileAuthPNResponseDto?>> InitiateAuthRequestPN(MobileAuthPNRequestDto requestDto)
        {
            ResponseDto<MobileAuthPNResponseDto?> response = new ResponseDto<MobileAuthPNResponseDto?>
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
                var obj = await _mIDServiceAuthentication.InitiateAuthRequestPN(validateRequest, requestDto);
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


