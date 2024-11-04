using Duc.Splitt.CustomerApi.Helper;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Logger;
using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Duc.Splitt.CustomerApi.Controllers
{

    public class LookupController : BaseAnonymous
    {
        private readonly ILookupService _lookupService;
        private readonly ILoggerService _logger;
        private IUtilsService _utilsService;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public LookupController(ILookupService lookupService, ILoggerService logger, IUtilsService utilsService)
        {
            _lookupService = lookupService;
            _logger = logger;
            _utilsService = utilsService;
        }


        [HttpGet()]
        public async Task<ResponseDto<List<LookupDocumentDto>>> GeDocumentConfigurations(DocumentCategories documentCategories)
        {
            ResponseDto<List<LookupDocumentDto>> response = new ResponseDto<List<LookupDocumentDto>>
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
                var obj = await _lookupService.GeDocumentConfigurations(validateRequest, documentCategories);

                response.Data = obj;//

                response.Code = ResponseStatusCode.Success;
                return response;
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


