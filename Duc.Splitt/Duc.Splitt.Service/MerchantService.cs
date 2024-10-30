using Azure;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Common.Helpers;
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Data.Dapper;
using Duc.Splitt.Data.DataAccess.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using static Duc.Splitt.Common.Dtos.Requests.MerchantRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;

namespace Duc.Splitt.Service
{
    public class MerchantService : IMerchantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperDBConnection _dapperDBConnection;

        public MerchantService(IUnitOfWork unitOfWork, IDapperDBConnection dapperDBConnection)
        {
            _unitOfWork = unitOfWork;
            _dapperDBConnection = dapperDBConnection;
        }

        public async Task<ResponseDto<CreateMerchantResponseDto>> PostMerchant(RequestHeader requestHeader, CreaterMerchantRequestDto requestDto)
        {

            ResponseDto<CreateMerchantResponseDto> response = new ResponseDto<CreateMerchantResponseDto>
            {
                Code = ResponseStatusCode.NoDataFound
            };

            MerchantRequest merchantRequest = new MerchantRequest();
            merchantRequest.BusinessEmail = requestDto.BusinessEmail;
            merchantRequest.BusinessNameArabic = requestDto.BusinessNameArabic;
            merchantRequest.BusinessNameEnglish = requestDto.BusinessNameEnglish;
            merchantRequest.CountryId = requestDto.CountryId;
            merchantRequest.MerchantAnnualSalesId = requestDto.AnnualSalesId;
            merchantRequest.MerchantAverageOrderId = requestDto.AverageOrderId;
            merchantRequest.MerchantBusinessTypeId = requestDto.BusinessTypeId;
            merchantRequest.MerchantCategoryId = requestDto.CategoryId;
            merchantRequest.MobileNumber = requestDto.MobileNumber;
            merchantRequest.CreatedAt = (byte)requestHeader.LocationId;
            merchantRequest.CreatedOn = DateTime.Now;
            merchantRequest.CreatedBy = Utilities.AnonymousUserID;
            merchantRequest.RequestStatusId = (int)RequestStatuses.InProgress;
            merchantRequest.RequestNo = GenerateRequestNumber();
            if (requestDto.Documents != null)
            {
                foreach (var x in requestDto.Documents)
                {
                    merchantRequest.MerchantRequestAttachment.Add(
                        new MerchantRequestAttachment
                        {

                            DocumentLibrary = new DocumentLibrary
                            {
                                Attachment = x.Attachment,
                                DocumentCategoryId = (int)DocumentCategories.MerchantRequest,
                                CreatedAt = (byte)requestHeader.LocationId,
                                CreatedOn = DateTime.Now,
                                CreatedBy = Utilities.AnonymousUserID
                            },
                            DocumentConfigurationId = x.DocumentConfigurationId,
                            CreatedAt = (byte)requestHeader.LocationId,
                            CreatedOn = DateTime.Now,
                            CreatedBy = Utilities.AnonymousUserID,

                        });

                }
            }
            merchantRequest.MerchantRequestHistory.Add(new MerchantRequestHistory
            {
                RequestStatusId = (int)RequestStatuses.InProgress,
                CreatedAt = (byte)requestHeader.LocationId,
                CreatedOn = DateTime.Now,
                CreatedBy = Utilities.AnonymousUserID,

            });
            _unitOfWork.MerchantRequest.AddAsync(merchantRequest);
            await _unitOfWork.CompleteAsync();
            response.Data = new CreateMerchantResponseDto { RequestNo = merchantRequest.RequestNo };
            response.Code = ResponseStatusCode.Success;
            return response;
        }
        private string GenerateRequestNumber()
        {
            // Format date and time as "yyyyMMddHHmmss"
            string dateTimePart = DateTime.Now.ToString("yyyyMMddHHmm");

            // Generate a 5-digit random number
            Random random = new Random();
            string randomPart = random.Next(10000, 99999).ToString();

            // Combine date-time and random number
            return $"{dateTimePart}-{randomPart}";
        }
    }
}
