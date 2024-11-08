﻿using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Core.Helper;
using Duc.Splitt.Logger;
using Duc.Splitt.BackOfficeApi.Helper;
using Microsoft.AspNetCore.Mvc;
using static Duc.Splitt.Common.Dtos.Requests.MerchantRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;
using Duc.Splitt.Service;

namespace Duc.Splitt.BackOfficeApi.Controllers
{

    public class MerchantController : BaseAuth
    {
        private readonly IMerchantService _merchantService;
        private readonly IBackOfficeMerchantService _backOfficeMerchantService;
        private readonly ILoggerService _logger;
        private IUtilsService _utilsService;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public MerchantController(ILookupService lookupService, ILoggerService logger, IUtilsService utilsService, IMerchantService merchantService, IBackOfficeMerchantService backOfficeMerchantService)
        {
            _logger = logger;
            _utilsService = utilsService;
            _merchantService = merchantService;
            _backOfficeMerchantService = backOfficeMerchantService;
        }

        [HttpPost]
        public async Task<PagedResponseDto<PagedList<SearchMerchantResponseDto>>> GetMerchantRequestList(SearchMerchantRequestDto requestDto)
        {
            PagedResponseDto<PagedList<SearchMerchantResponseDto>> response = new PagedResponseDto<PagedList<SearchMerchantResponseDto>>
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
                var result = await _merchantService.SearchMerchantRequest(validateRequest, requestDto);
                response.Data = result;
                response.PageNumber = result.PageNumber;
                response.PageSize = result.PageSize;
                response.TotalRecords = result.TotalCount;
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
        [HttpPost]
        public async Task<ResponseDto<string?>> ChangeMerchantStatus(AdminChangeUserStatus requestDto)
        {
            ResponseDto<string?> response = new ResponseDto<string?>
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
                var result = await _backOfficeMerchantService.ChangeMerchantStatus(validateRequest, requestDto);
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
        public async Task<ResponseDto<GetMerchantResponseDto>> GetMerchantDetailsById(GetMerchantRequestDto requestDto)
        {
            ResponseDto<GetMerchantResponseDto> response = new ResponseDto<GetMerchantResponseDto>
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
                var obj = await _merchantService.GetMerchantDetailsById(validateRequest, requestDto);
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
        [HttpPost]
        public async Task<ResponseDto<DownloadAttachmentResponseDto?>> DownloadAttchemnts(DownloadAttachmentRequestDto requestDto)
        {
            ResponseDto<DownloadAttachmentResponseDto?> response = new ResponseDto<DownloadAttachmentResponseDto?>
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
                var obj = await _merchantService.DownloadAttchemnts(validateRequest, requestDto);
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


    }
}


