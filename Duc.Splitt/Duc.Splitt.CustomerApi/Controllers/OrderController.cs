using Duc.Splitt.CustomerApi.Helper;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Logger;
using Microsoft.AspNetCore.Mvc;
using Azure.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Duc.Splitt.Common.Dtos.Requests.OrderRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.OrderResponseDto;

namespace Duc.Splitt.CustomerApi.Controllers
{

    public class OrderController : BaseAnonymous
    {
        private readonly IOrderService _orderService;
        private readonly ILoggerService _logger;
        private IUtilsService _utilsService;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public OrderController(IOrderService orderService, ILoggerService logger, IUtilsService utilsService)
        {
            _orderService = orderService;
            _logger = logger;
            _utilsService = utilsService;
        }


        [HttpPost()]
        public async Task<ResponseDto<CreateOrderResponseDto?>> PostOrder(CreateOrderRequestDto requestDto)
        {
            ResponseDto<CreateOrderResponseDto?> response = new ResponseDto<CreateOrderResponseDto?>
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
                var obj = await _orderService.PostOrder(validateRequest, requestDto);
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

        //[HttpPost()]
        //public async Task<ResponseDto<GetOrderDetailstResponseDto?>> GetOrderById(GetOrderRequestDetailsDto requestDto)
        //{
        //    ResponseDto<GetOrderDetailstResponseDto?> response = new ResponseDto<GetOrderDetailstResponseDto?>
        //    {
        //        Code = ResponseStatusCode.NoDataFound
        //    };

        //    try
        //    {
        //        var validateRequest = await _utilsService.ValidateRequest(this.Request, null);
        //        if (validateRequest == null)
        //        {
        //            response.Code = ResponseStatusCode.InvalidToken;
        //            return response;
        //        }
        //        var obj = await _orderService.GetOrderById(validateRequest, requestDto);
        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex);
        //        response.Code = ResponseStatusCode.ServerError;
        //        response.Errors = _logger.ConvertExceptionToStringList(ex);
        //        return response;
        //    }
        //}

    }
}


