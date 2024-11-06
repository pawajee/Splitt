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
using static Duc.Splitt.Common.Dtos.Requests.MerchantRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;

namespace Duc.Splitt.Service
{
    public class MerchantService : IMerchantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperDBConnection _dapperDBConnection;
        private readonly UserManager<SplittIdentityUser> _userManager;
        public MerchantService(IUnitOfWork unitOfWork, IDapperDBConnection dapperDBConnection, UserManager<SplittIdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _dapperDBConnection = dapperDBConnection;
        }

        public async Task<ResponseDto<CreateMerchantResponseDto?>> PostMerchant(RequestHeader requestHeader, CreaterMerchantRequestDto requestDto)
        {

            ResponseDto<CreateMerchantResponseDto?> response = new ResponseDto<CreateMerchantResponseDto?>
            {
                Code = ResponseStatusCode.NoDataFound
            };
            var userExistsbyName = await _userManager.FindByNameAsync(requestDto.BusinessEmail);
            if (userExistsbyName != null)
            {
                return new ResponseDto<CreateMerchantResponseDto?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "User already exists",
                    Errors = new List<string> { $"{requestDto.BusinessEmail} User already exists  in User" }
                };
            }
            var userExistsByEmail = await _userManager.FindByEmailAsync(requestDto.BusinessEmail);
            if (userExistsByEmail != null)
            {
                return new ResponseDto<CreateMerchantResponseDto?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "User already exists",
                    Errors = new List<string> { $"{requestDto.BusinessEmail} User already exists in User" }
                };
            }
            var merchantUser = await _unitOfWork.MerchantContacts.GetMerchantRequestByEmail(requestDto.BusinessEmail);
            if (merchantUser != null)
            {
                return new ResponseDto<CreateMerchantResponseDto?>
                {
                    Code = ResponseStatusCode.Conflict,
                    Message = "User already exists",
                    Errors = new List<string> { $"{requestDto.BusinessEmail} User already exists" }
                };
            }
            Merchant merchantRequest = new Merchant();
            merchantRequest.BusinessNameArabic = requestDto.BusinessNameArabic;
            merchantRequest.BusinessNameEnglish = requestDto.BusinessNameEnglish;
            merchantRequest.CountryId = requestDto.CountryId;
            merchantRequest.MerchantAnnualSalesId = requestDto.AnnualSalesId;
            merchantRequest.MerchantAverageOrderId = requestDto.AverageOrderId;
            merchantRequest.MerchantBusinessTypeId = requestDto.BusinessTypeId;
            merchantRequest.MerchantCategoryId = requestDto.CategoryId;
            merchantRequest.MobileNo = requestDto.MobileNumber;
            merchantRequest.BusinessEmail = requestDto.BusinessEmail;
            merchantRequest.CreatedAt = (byte)requestHeader.LocationId;
            merchantRequest.CreatedOn = DateTime.Now;
            merchantRequest.CreatedBy = Utilities.AnonymousUserID;
            merchantRequest.MerchantStatusId = (int)MerchantRequestStatuses.InProgress;
            merchantRequest.RequestNo = GenerateRequestNumber();
            if (requestDto.Documents != null)
            {
                var documentCategories = await _unitOfWork.LkDocumentCategories.GetAsync(1);//ToDO
                foreach (var x in requestDto.Documents)
                {
                    var fileGUid = Guid.NewGuid();
                    var directoryPath = documentCategories.BaseUrl;
                    if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    File.WriteAllBytes(Path.Combine(directoryPath, FileNameGenerator.GenerateFileName(x.MineType, fileGUid)), x.AttachmentByte);

                    merchantRequest.MerchantAttachment.Add(
                        new MerchantAttachment
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
            merchantRequest.MerchantHistory.Add(new MerchantHistory
            {
                MerchantRequestStatusId = (int)MerchantRequestStatuses.InProgress,
                CreatedAt = (byte)requestHeader.LocationId,
                CreatedOn = DateTime.Now,
                CreatedBy = Utilities.AnonymousUserID,

            });
            _unitOfWork.Merchants.AddAsync(merchantRequest);
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
            var merchantRequest = await _unitOfWork.Merchants.GetMerchantRequest(requestDto.RequestId);
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
                MerchantStatusId = merchantRequest.MerchantStatusId,
                RequestNo = merchantRequest.RequestNo,
                MobileNumber = merchantRequest.MobileNo,
                BusinessEmail = merchantRequest.BusinessEmail,
                MerchantAnnualSales = requestHeader.IsArabic ? merchantRequest.MerchantAnnualSales.TitleArabic : merchantRequest.MerchantAnnualSales.TitleEnglish,
                MerchantAverageOrder = requestHeader.IsArabic ? merchantRequest.MerchantAverageOrder.TitleArabic : merchantRequest.MerchantAverageOrder.TitleEnglish,
                MerchantCategory = requestHeader.IsArabic ? merchantRequest.MerchantCategory.TitleArabic : merchantRequest.MerchantCategory.TitleEnglish,
                MerchantBusinessType = requestHeader.IsArabic ? merchantRequest.MerchantBusinessType.TitleArabic : merchantRequest.MerchantBusinessType.TitleEnglish,
                RequestStatus = requestHeader.IsArabic ? merchantRequest.MerchantStatus.TitleArabic : merchantRequest.MerchantStatus.TitleEnglish,
                Country = requestHeader.IsArabic ? merchantRequest.Country.TitleArabic : merchantRequest.Country.TitleEnglish,
            };
            if (merchantRequest != null && merchantRequest.MerchantAttachment?.Count > 0)
            {
                temp.Documents = new List<DocumentResponseDto>();
                foreach (var attch in merchantRequest.MerchantAttachment)
                {
                    var docConfig = await _unitOfWork.LkDocumentConfigurations.GetAsync(attch.DocumentConfigurationId);
                    temp.Documents.Add(new DocumentResponseDto
                    {
                        DocumentConfigurationName = requestHeader.IsArabic ? docConfig.TitleArabic : docConfig.TitleEnglish,
                        DocumentLibraryId = attch.DocumentLibraryId
                    });
                }
            }
            if (merchantRequest != null && merchantRequest.MerchantHistory?.Count > 0)
            {
                temp.MerchantRequestHistory = new List<GetMerchantRequestHistory>();
                foreach (var history in merchantRequest.MerchantHistory)
                {
                    var user = await _unitOfWork.Users.GetUserById(history.CreatedBy);
                    var status = await _unitOfWork.LkMerchantStatuses.GetAsync(history.MerchantRequestStatusId);
                    temp.MerchantRequestHistory.Add(new GetMerchantRequestHistory
                    {
                        Comment = history.Comment,
                        CreatedBy = requestHeader.IsArabic ? user.UserType.TitleArabic : user.UserType.TitleEnglish,
                        CreatedOn = history.CreatedOn.ToString("dd/MM/YYYY"),
                        RequestStatus = requestHeader.IsArabic ? status.TitleArabic : status.TitleEnglish,
                        RequestStatusDesc = requestHeader.IsArabic ? status.AdminStatusArabic : status.AdminStatusEnglish,
                    });
                }
            }
            response.Data = temp;
            response.Code = ResponseStatusCode.Success;
            return response;
        }

        public async Task<ResponseDto<DownloadAttachmentResponseDto?>> DownloadAttchemnts(RequestHeader requestHeader, DownloadAttachmentRequestDto requestDto)
        {

            ResponseDto<DownloadAttachmentResponseDto?> response = new ResponseDto<DownloadAttachmentResponseDto?>
            {
                Code = ResponseStatusCode.NoDataFound
            };
            var attch = await _unitOfWork.DocumentLibrarys.GetAsync(requestDto.AttcahmentId);
            if (attch == null)
            {
                return response;
            }

            response.Data = new DownloadAttachmentResponseDto { AttachmentByte = attch.Attachment, MineType = attch.MineType };
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
