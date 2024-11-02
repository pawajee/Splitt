using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Common.Helpers;
using Duc.Splitt.Data.Dapper;
using System.Security.Claims;

namespace Duc.Splitt.ConsumerApi.Helper
{
    public class UtilsService : IUtilsService
    {
        private readonly IDapperDBConnection _dapperDBConnection;


        public UtilsService(IDapperDBConnection dapperDBConnection)
        {
            _dapperDBConnection = dapperDBConnection;
        }
        public async Task<RequestHeader> ValidateRequest(HttpRequest request, ClaimsPrincipal? user)
        {
            RequestHeader requestHeader = new RequestHeader();
            if (request != null)
            {
                if (request.Headers["accept-language"].Count > 0 && !string.IsNullOrWhiteSpace(request.Headers["accept-language"]))
                {
                    requestHeader.IsArabic = request.Headers["accept-language"].ToString().ToLower().Equals(Constant.LanguageArText.ToLower()) ? true : false;
                }
                if (request.Headers.ContainsKey("DeviceId") && !string.IsNullOrWhiteSpace(request.Headers["DeviceId"]))
                {
                    requestHeader.DeviceId = Convert.ToString(request.Headers["DeviceId"]);
                }
                if (request.Headers.ContainsKey("DeviceToken") && !string.IsNullOrWhiteSpace(request.Headers["DeviceToken"]))
                {
                    requestHeader.DeviceToken = Convert.ToString(request.Headers["DeviceToken"]);
                }
                if (request.Headers.ContainsKey("DeviceTypeId") && !string.IsNullOrWhiteSpace(request.Headers["DeviceTypeId"]))
                {
                    requestHeader.DeviceTypeId = (DeviceTypes)Enum.Parse(typeof(DeviceTypes), Convert.ToString(request.Headers["DeviceTypeId"]));
                }
                //if (request.Headers.ContainsKey("TokenId") && !string.IsNullOrWhiteSpace(request.Headers["TokenId"]))
                //{
                //    requestHeader.TokenId = Convert.ToString(request.Headers["TokenId"]);
                //}
                requestHeader.LocationId = Locations.MerchantPortal;


            }
            return requestHeader;
        }
    }

}

