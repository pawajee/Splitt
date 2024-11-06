using Dapper;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Common.Helpers;
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Core.Helper;
using Duc.Splitt.Data.Dapper;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Identity;
using Microsoft.AspNetCore.Identity;
using System.Data;
using static Duc.Splitt.Common.Dtos.Requests.AuthMerchantUserDto;
using static Duc.Splitt.Common.Dtos.Requests.MerchantRequestDto;
using static Duc.Splitt.Common.Dtos.Requests.OrderRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.OrderResponseDto;
using Duc.Splitt.Data.DataAccess.Models;

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

        public async Task<ResponseDto<CreateOrderResponseDto?>> PostOrder(RequestHeader requestHeader, CreateOrderRequestDto requestDto)
        {
            var webSite = "testMerchant.com";//TODO:
            var NoOfInstallments = 4;
            ResponseDto<CreateOrderResponseDto?> response = new ResponseDto<CreateOrderResponseDto?>
            {
                Code = ResponseStatusCode.NoDataFound
            };
            if (requestDto.OrderItems == null)
            {
                return new ResponseDto<CreateOrderResponseDto?>
                {
                    Code = ResponseStatusCode.BadRequest,
                    Message = "Order details are missing, please provide order items!",
                    Errors = new List<string> { $"Order details are missing, please provide order items!" }
                };
            }

            Order ordReq = new Order();
            ordReq.CustomerId = requestDto.CustomerId;
            ordReq.MerchantId = requestDto.MerchantId;
            ordReq.TotalAmount = requestDto.TotalAmount;
            ordReq.ErpreferenceNumber = "9999";
            ordReq.CurrencyId = 1;
            ordReq.CheckoutId = requestDto.CheckoutId;
            ordReq.ExternalRefId = requestDto.ExternalRefId;
            ordReq.OrderNumber = requestDto.OrderNumber;
            ordReq.OrderStatusId = "1";
            ordReq.ExpiredAt = DateTime.Now.AddMinutes(30);
            ordReq.CheckoutUrl = $@"https://{webSite}/?OrderId=::OrderId::&CustomerId={requestDto.CustomerId}";
            ordReq.PaymentOptionId = 1;
            ordReq.MerchantUrlSuccess = requestDto.MerchantUrlSuccess;
            ordReq.MerchantUrlFailure = requestDto.MerchantUrlFailure;
            ordReq.MerchantUrlCancel = requestDto.MerchantUrlCancel;

            ordReq.CreatedAt = (byte)requestHeader.LocationId;
            ordReq.CreatedOn = DateTime.Now;
            ordReq.CreatedBy = Utilities.AnonymousUserID;

            foreach (var item in requestDto.OrderItems)
            {
                // var ordItem = new OrderItem;
                //ordItem.ItemName = item.ItemName;
                //ordItem.ItemDescription = item.ItemDescription;
                //ordItem.Quantity= item.Quantity;
                var ordItem = new OrderItem
                {
                    ItemName = item.ItemName,
                    ItemDescription = item.ItemDescription,
                    Quantity = item.Quantity,
                    Amount = item.Amount,
                    ExternalRefId = item.ExternalRefId,
                    ProductUrl = item.ProductUrl,
                    BrandName = item.BrandName,
                    Sku = item.Sku,
                    CreatedBy = Utilities.AnonymousUserID,
                    CreatedOn = DateTime.Now,
                };
                ordReq.OrderItem.Add(ordItem);

                //ordReq.MerchantStatusId = (int)MerchantRequestStatuses.InProgress;
                // ordReq.RequestNo = GenerateRequestNumber();
                //response.Data = new CreateOrderResponseDto {  };
                //response.Code = ResponseStatusCode.Success;
                //return response;
            }
            var totalAmount = ordReq.TotalAmount;
            var extraAmount = totalAmount % NoOfInstallments;
            var devAmount = totalAmount - extraAmount;
            var InstallmentAmount = devAmount / NoOfInstallments;

            for (var iLoop = 1; iLoop < (NoOfInstallments + 1); iLoop++)
            {
                var instAmount = InstallmentAmount;
                var dueDate = DateTime.Now.AddMonths(1).AddDays(-1);
                int instType = 2;
                if (iLoop == 1)
                {
                    instAmount += extraAmount;
                    instType = 1;
                }
                //PaymentInstallment
                var installment = new PaymentInstallment
                {
                    InstallmentAmount = instAmount,
                    DueDate = dueDate,
                    InstallmentTypeId = instType,

                    CreatedBy = Utilities.AnonymousUserID,
                    CreatedOn = DateTime.Now,
                };
                ordReq.PaymentInstallment.Add(installment);
            }

            _unitOfWork.Orders.AddAsync(ordReq);
            await _unitOfWork.CompleteAsync();
            response.Data = new CreateOrderResponseDto
            {
                OrderId = ordReq.Id,
                CustomerId = ordReq.CustomerId,
            };
            response.Code = ResponseStatusCode.Success;
            return response;
        }

        public async Task<ResponseDto<GetOrderResponseDto?>> GetOrderByOrderId(RequestHeader requestHeader, GetOrderRequestDto requestDto)
        {
            return null;
        }

    }
}