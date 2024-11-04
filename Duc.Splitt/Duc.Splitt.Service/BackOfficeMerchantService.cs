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
using static Duc.Splitt.Common.Dtos.Requests.AuthBackOfficeUserDto;
using static Duc.Splitt.Common.Dtos.Requests.AuthMerchantUserDto;
using static Duc.Splitt.Common.Dtos.Requests.MerchantRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;

namespace Duc.Splitt.Service
{
    public class BackOfficeMerchantService : IBackOfficeMerchantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperDBConnection _dapperDBConnection;
        private readonly IAuthBackOfficeService _authBackOfficeService;
        private readonly UserManager<SplittIdentityUser> _userManager;
        public BackOfficeMerchantService(IUnitOfWork unitOfWork, IDapperDBConnection dapperDBConnection, IAuthBackOfficeService authBackOfficeService, UserManager<SplittIdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _dapperDBConnection = dapperDBConnection;
            _authBackOfficeService = authBackOfficeService;
        }



        public async Task<ResponseDto<string?>> ChangeMerchantStatus(RequestHeader requestHeader, AdminChangeUserStatus requestDto)
        {

            ResponseDto<string?> response = new ResponseDto<string?>
            {
                Code = ResponseStatusCode.NoDataFound
            };
            var merchant = await _unitOfWork.Merchants.GetMerchantRequest(requestDto.RequestId);
            if (merchant == null)
            {
                return response;
            }

            if (requestDto.RequestStatusId == (int)MerchantRequestStatuses.Approved)
            {
                var reqisterDto = new RegisterDto
                {
                    Comments = requestDto.Comments,
                    Email = merchant.BusinessEmail,
                    UserName = merchant.BusinessEmail,
                };
                return await _authBackOfficeService.ApproveMerchantUserByAdmin(requestHeader, reqisterDto);

            }
            else if (requestDto.RequestStatusId == (int)MerchantRequestStatuses.Rejected)
            {
                merchant.MerchantStatusId = (int)MerchantRequestStatuses.Rejected;
                merchant.ModifiedAt = (byte)requestHeader.LocationId;
                merchant.ModifiedOn = DateTime.Now;
                merchant.ModifiedBy = Utilities.AnonymousUserID; //ToDoM
                merchant.MerchantHistory.Add(new MerchantHistory
                {
                    MerchantRequestStatusId = (int)MerchantRequestStatuses.Rejected,
                    CreatedAt = (byte)requestHeader.LocationId,
                    CreatedOn = DateTime.Now,
                    CreatedBy = Utilities.AnonymousUserID,
                    Comment = requestDto.Comments

                });
                await _unitOfWork.CompleteAsync();
                response.Data = merchant.RequestNo;
                response.Code = ResponseStatusCode.Success;
                return response;
            }
            return response;
        }
    }
}
