using Azure;
using Dapper;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Common.Helpers;
using Duc.Splitt.Common.Resources;
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Core.Helper;
using Duc.Splitt.Data.Dapper;
using Duc.Splitt.Data.DataAccess.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using System.IO;
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
                var documentCategories = await _unitOfWork.DocumentCategories.GetAsync(1);//ToDO
                foreach (var x in requestDto.Documents)
                {
                    var fileGUid = Guid.NewGuid();

                    var directoryPath = documentCategories.BaseUrl;
                    if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    File.WriteAllBytes(Path.Combine(directoryPath, FileNameGenerator.GenerateFileName(x.MineType, fileGUid)), x.AttachmentByte);

                    merchantRequest.MerchantRequestAttachment.Add(
                        new MerchantRequestAttachment
                        {

                            DocumentLibrary = new DocumentLibrary
                            {
                                Id = fileGUid,
                                DocumentCategoryId = (int)DocumentCategories.MerchantRequest,
                                CreatedAt = (byte)requestHeader.LocationId,
                                CreatedOn = DateTime.Now,
                                CreatedBy = Utilities.AnonymousUserID,
                                MineType = x.MineType,
                                FileName = x.FileName,
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

        public async Task<PagedList<SearchMerchantResponseDto>> SearchMerchantRequest(RequestHeader requestHeader,
          SearchMerchantRequestDto searchRequestDto)
        {

            var procName = "uspSearchMerchantRequests";
            var param = new DynamicParameters();
            param.Add("@isArabic", requestHeader.IsArabic);

            param.Add("@@userId", null);
            param.Add("@dateFrom", searchRequestDto?.DateFrom);
            param.Add("@dateTo", searchRequestDto?.DateTo);

            param.Add("@businessName", searchRequestDto?.BusinessName);

            param.Add("@mobile", searchRequestDto?.MobileNo);

            param.Add("@businessEmail", searchRequestDto?.BusinessName);
            param.Add("@requestNo", searchRequestDto?.RequestNo);

            param.Add("@requestStatusId", searchRequestDto?.RequestStatusId);
            param.Add("@merchantCategoryId", searchRequestDto?.MerchantCategoryId);
            param.Add("@merchantBusinessTypeId", searchRequestDto?.MerchantBusinessTypeId);
            param.Add("@merchantAnnualSalesId", searchRequestDto?.MerchantAnnualSalesId);
            param.Add("@merchantAverageOrderId", searchRequestDto?.MerchantAverageOrderId);


            param.Add("@pageNumber", searchRequestDto?.PageNumber);
            param.Add("@pageSize", searchRequestDto?.PageSize);
            param.Add("@SortBy", searchRequestDto?.OrderBy.ToString());
            param.Add("@SortDirection", searchRequestDto?.OrderDirections.ToString());
            try
            {
                using (var connection = _dapperDBConnection.GetConnection())
                {
                    var multiResult = await connection.QueryMultipleAsync(procName, param, commandType: CommandType.StoredProcedure);
                    {
                        var obj = await multiResult.ReadAsync<SearchMerchantResponseDto>();
                        var totalRowCount = 0;
                        var result = obj.ToList();
                        if (result.Count > 0)
                        {
                            totalRowCount = result[0].TotalRecords;
                        }
                        return new PagedList<SearchMerchantResponseDto>(result, totalRowCount, searchRequestDto.PageNumber, searchRequestDto.PageSize);
                    }
                }

            }
            finally
            {
                _dapperDBConnection.CloseConnection();
            }
        }

        public async Task<ResponseDto<GetMerchantResponseDto>> GetMerchantDetailsById(RequestHeader requestHeader, GetMerchantRequestDto requestDto)
        {

            ResponseDto<GetMerchantResponseDto> response = new ResponseDto<GetMerchantResponseDto>
            {
                Code = ResponseStatusCode.NoDataFound
            };
            var merchantRequest = await _unitOfWork.MerchantRequest.GetMerchantRequest(requestDto.RequestId);
            if (merchantRequest == null)
            {
                return response;
            }

            GetMerchantResponseDto temp = new GetMerchantResponseDto
            {
                Id = merchantRequest.Id,
                BusinessNameArabic = merchantRequest.BusinessNameArabic,
                BusinessNameEnglish = merchantRequest.BusinessNameEnglish,
                CountryId = merchantRequest.CountryId,
                MerchantAnnualSalesId = merchantRequest.MerchantAnnualSalesId,
                MerchantAverageOrderId = merchantRequest.MerchantAverageOrderId,
                MerchantBusinessTypeId = merchantRequest.MerchantBusinessTypeId,
                MerchantCategoryId = merchantRequest.MerchantCategoryId,
                MobileNumber = merchantRequest.MobileNumber,
                RequestNo = merchantRequest.RequestNo,
                BusinessEmail = merchantRequest.BusinessEmail

            };
            if (merchantRequest != null && merchantRequest.MerchantRequestAttachment?.Count > 0)
            {
                temp.Documents = new List<DocumentResponseDto>();
                foreach (var attch in merchantRequest.MerchantRequestAttachment)
                {
                    var docConfig = await _unitOfWork.DocumentConfigurations.GetAsync(attch.DocumentConfigurationId);
                    temp.Documents.Add(new DocumentResponseDto
                    {
                        DocumentConfigurationName = requestHeader.IsArabic ? docConfig.TitleArabic : docConfig.TitleEnglish,
                        URL = "",
                        DocumentLibraryId = attch.DocumentLibraryId
                    });
                }
            }
            if (merchantRequest != null && merchantRequest.MerchantRequestHistory?.Count > 0)
            {
                temp.MerchantRequestHistory = new List<GetMerchantRequestHistory>();
                foreach (var history in merchantRequest.MerchantRequestHistory)
                {
                    var user = await _unitOfWork.Users.GetAsync(history.CreatedBy);
                    var status = await _unitOfWork.RequestStatuses.GetAsync(history.RequestStatusId);
                    temp.MerchantRequestHistory.Add(new GetMerchantRequestHistory
                    {
                        Comment = history.Comment,
                        CreatedBy = user.Name,
                        CreatedOn = history.CreatedOn.ToString("dd/MM/YYYY"),
                        RequestStatus = requestHeader.IsArabic ? status.TitleArabic : status.TitleEnglish
                    });
                }
            }
            response.Data = temp;
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
