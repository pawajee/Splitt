﻿using Duc.Splitt.Common.Dtos.Requests;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Core.Helper;
using Duc.Splitt.Logger;
using Duc.Splitt.MerchantApi.Helper;
using Duc.Splitt.Service;
using Microsoft.AspNetCore.Mvc;
using static Duc.Splitt.Common.Dtos.Requests.AuthMerchantUserDto;
using static Duc.Splitt.Common.Dtos.Requests.MerchantRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;

namespace Duc.Splitt.MerchantApi.Controllers
{

    public class AuthController : BaseAnonymous
    {
        private readonly IAuthMerchantService _authMerchantService;
        private readonly ILoggerService _logger;
        private IUtilsService _utilsService;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public AuthController(ILookupService lookupService, ILoggerService logger, IUtilsService utilsService, IAuthMerchantService authMerchantService)
        {

            _logger = logger;
            _utilsService = utilsService;
            _authMerchantService = authMerchantService;
        }

        [HttpPost]
        public async Task<ResponseDto<AuthTokens?>> ActivateMerchantUser(SetPasswordDto requestDto)
        {
            ResponseDto<AuthTokens?> response = new ResponseDto<AuthTokens?>
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
                var obj = await _authMerchantService.ActivateMerchantByUser(validateRequest, requestDto);
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
        public async Task<ResponseDto<AuthTokens?>> Login(LoginDto requestDto)
        {
            ResponseDto<AuthTokens?> response = new ResponseDto<AuthTokens?>
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
                var result = await _authMerchantService.Login(validateRequest, requestDto);
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
        public async Task<ResponseDto<bool?>> ResetPassword(ResetPasswordDto requestDto)
        {
            ResponseDto<bool?> response = new ResponseDto<bool?>
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
                var result = await _authMerchantService.ResetPassword(validateRequest, requestDto);
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
        public async Task<ResponseDto<bool?>> ForgetPassword(ForgetPasswordDto requestDto)
        {
            ResponseDto<bool?> response = new ResponseDto<bool?>
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
                var result = await _authMerchantService.ForgetPassword(validateRequest, requestDto);
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


    }
}


