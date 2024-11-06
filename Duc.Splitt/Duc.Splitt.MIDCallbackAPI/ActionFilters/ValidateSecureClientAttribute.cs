using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Duc.Splitt.MIDCallbackAPI.ActionFilters
{
    public sealed class ValidateSecureClientAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!IsValidRequest(context.HttpContext.Request))
            {
                context.Result = new ContentResult
                {
                    Content = "Invalid Client Request"
                };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
            }
        }
        private static bool IsValidRequest(HttpRequest request)
        {
            if (request.Headers == null)
            {
                return false;
            }
            else if (!request.Headers.ContainsKey("Authorization"))
            {
                return false;
            }

            else if (!request.Headers.ContainsKey("Accept-Language") && string.IsNullOrWhiteSpace(request.Headers["Accept-Language"]))
            {
                return false;
            }
            if (!request.Headers.ContainsKey("DeviceIdentifier") && string.IsNullOrWhiteSpace(request.Headers["DeviceIdentifier"]))
            {
                return false;
            }
            else if (!request.Headers.ContainsKey("SessionIdentifier") && string.IsNullOrWhiteSpace(request.Headers["SessionIdentifier"]))
            {
                return false;
            }
            else if (!request.Headers.ContainsKey("PlatformTypeId") && string.IsNullOrWhiteSpace(request.Headers["PlatformTypeId"]))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
