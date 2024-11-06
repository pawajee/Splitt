using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Data.Dapper;
using Duc.Splitt.Data.DataAccess.Models;
using static Duc.Splitt.Common.Dtos.Requests.OrderRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.OrderResponseDto;

namespace Duc.Splitt.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperDBConnection _dapperDBConnection;
        public OrderService(IUnitOfWork unitOfWork, IDapperDBConnection dapperDBConnection)
        {
            _unitOfWork = unitOfWork;
            _dapperDBConnection = dapperDBConnection;
        }

        public async Task<ResponseDto<CreaterOrderResponseDto?>> PostOrder(RequestHeader requestHeader, CreateOrderRequestDto requestDto)
        {

            Order order = new Order { };
            _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync();
            ResponseDto<CreaterOrderResponseDto?> response = new ResponseDto<CreaterOrderResponseDto?>
            {
                Code = ResponseStatusCode.NoDataFound
            };

            response.Data = null;
            response.Code = ResponseStatusCode.Success;
            return response;
        }
        public async Task<ResponseDto<GetOrderDetailstResponseDto?>> GetOrderById(RequestHeader requestHeader, GetOrderRequestDetailsDto requestDto)
        {

            Order order = new Order { };
            _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync();
            ResponseDto<GetOrderDetailstResponseDto?> response = new ResponseDto<GetOrderDetailstResponseDto?>
            {
                Code = ResponseStatusCode.NoDataFound
            };

            response.Data = null;
            response.Code = ResponseStatusCode.Success;
            return response;
        }
    }
}
