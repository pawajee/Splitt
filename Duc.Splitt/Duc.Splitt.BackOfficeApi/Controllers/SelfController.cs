using Duc.Splitt.BackOfficeApi.Helper;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Logger;
using Duc.Splitt.Service;
using Microsoft.AspNetCore.Mvc;
using static Duc.Splitt.Common.Dtos.Requests.AuthBackOfficeUserDto;

namespace Duc.Splitt.BackOfficeApi.Controllers
{

    public class SelfController : BaseAuth
    {
        private readonly IAuthBackOfficeService _authBackOfficeService;
        private readonly ILoggerService _logger;
        private IUtilsService _utilsService;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public SelfController(ILookupService lookupService, ILoggerService logger, IUtilsService utilsService, IAuthBackOfficeService authBackOfficeService)
        {

            _logger = logger;
            _utilsService = utilsService;
            _authBackOfficeService = authBackOfficeService;
        }

        [HttpPost]
        public async Task<ResponseDto<bool?>> ChangePassword(ChangePasswordDto requestDto)
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
                var result = await _authBackOfficeService.ChangePassword(validateRequest, requestDto);
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
        public async Task<ResponseDto<bool?>> Logout()
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
                var result = await _authBackOfficeService.Logout(validateRequest);
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



    }
}


