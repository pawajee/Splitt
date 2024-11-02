﻿using Duc.Splitt.Common.Dtos.Requests;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Core.Helper;
using System.Threading.Tasks;
using static Duc.Splitt.Common.Dtos.Requests.AuthMerchantUserDto;
using static Duc.Splitt.Common.Dtos.Requests.MerchantRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;

namespace Duc.Splitt.Core.Contracts.Services
{
    public interface IAuthMerchantService
    {

        Task<ResponseDto<string?>> ApproveMerchantUser(RequestHeader requestHeader, RegisterDto request);

        Task<ResponseDto<AuthTokens?>> ActivateMerchantUser(RequestHeader requestHeader, SetPasswordDto model);
        Task<ResponseDto<AuthTokens?>> Login(RequestHeader requestHeader, LoginDto model);
    }
}