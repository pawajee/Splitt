using Duc.Splitt.MerchantApi.Helper;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Logger;
using Microsoft.AspNetCore.Mvc;

namespace Duc.Splitt.MerchantApi.Controllers
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
        public async Task<ResponseDto<List<LookupDto>>> GetCountries()
        {
            ResponseDto<List<LookupDto>> response = new ResponseDto<List<LookupDto>>
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

                var obj = await _lookupService.GetCountries(validateRequest);
                response.Data = obj;
                response.Code = ResponseStatusCode.Success;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                response.Code = ResponseStatusCode.ServerError;
                response.Details = ex?.InnerException?.Message;
                return response;
            }

        }


        [HttpGet()]
        public async Task<ResponseDto<List<LookupDto>>> GetNationalities()
        {
            ResponseDto<List<LookupDto>> response = new ResponseDto<List<LookupDto>>
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

                var obj = await _lookupService.GetNationalities(validateRequest);
                response.Data = obj;
                response.Code = ResponseStatusCode.Success;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                response.Code = ResponseStatusCode.ServerError;
                response.Details = ex?.InnerException?.Message;
                return response;
            }

        }

        [HttpGet()]
        public async Task<ResponseDto<List<LookupDto>>> GeMerchantBusinessTypes()
        {
            ResponseDto<List<LookupDto>> response = new ResponseDto<List<LookupDto>>
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

                var obj = await _lookupService.GeMerchantBusinessTypes(validateRequest);
                response.Data = obj;// new List<LookupDto> { new LookupDto { Id = 1, Name = "Retail Business" }, new LookupDto { Id = 1, Name = "Manufacturing" } }; ;
                response.Code = ResponseStatusCode.Success;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                response.Code = ResponseStatusCode.ServerError;
                response.Details = ex?.InnerException?.Message;
                return response;
            }

        }
        [HttpGet()]
        public async Task<ResponseDto<List<LookupDto>>> GetMerchantCategories()
        {
            ResponseDto<List<LookupDto>> response = new ResponseDto<List<LookupDto>>
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

                var obj = await _lookupService.GeMerchantCategories(validateRequest);

                response.Data = obj;// new List<LookupDto> { new LookupDto { Id = 1, Name = "Pharmacies" }, new LookupDto { Id = 1, Name = "Supermarkets" } };
                response.Code = ResponseStatusCode.Success;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                response.Code = ResponseStatusCode.ServerError;
                response.Details = ex?.InnerException?.Message;
                return response;
            }

        }
        [HttpGet()]
        public async Task<ResponseDto<List<LookupDto>>> GeMerchantAnnualSales()
        {
            ResponseDto<List<LookupDto>> response = new ResponseDto<List<LookupDto>>
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

                var obj = await _lookupService.GeMerchantAnnualSales(validateRequest);
                response.Data = obj;// new List<LookupDto> { new LookupDto { Id = 1, Name = "KD 10000- KD 20000" }, new LookupDto { Id = 1, Name = "KD 20000-KD 30000" } };
                response.Code = ResponseStatusCode.Success;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                response.Code = ResponseStatusCode.ServerError;
                response.Details = ex?.InnerException?.Message;
                return response;
            }

        }
        [HttpGet()]
        public async Task<ResponseDto<List<LookupDto>>> GeMerchantAverageOrders()
        {
            ResponseDto<List<LookupDto>> response = new ResponseDto<List<LookupDto>>
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

                var obj = await _lookupService.GeMerchantAverageOrders(validateRequest);
                response.Data = obj;// new List<LookupDto> { new LookupDto { Id = 1, Name = "100-200" }, new LookupDto { Id = 1, Name = "200-300" } };
                response.Code = ResponseStatusCode.Success;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                response.Code = ResponseStatusCode.ServerError;
                response.Details = ex?.InnerException?.Message;
                return response;
            }

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
                response.Details = ex?.InnerException?.Message;
                return response;
            }

        }

    }
}


