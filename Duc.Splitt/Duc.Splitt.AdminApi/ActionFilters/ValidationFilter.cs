using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Duc.Splitt.MerchantApi.ActionFilters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context != null && context.ModelState != null && !context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState.Where(x => x.Value != null &&
                x.Value.Errors != null && x.Value.Errors.Count > 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();
                var errorResponse = new ErrorDtos();
                if (errorsInModelState != null && errorsInModelState.Count() > 0)
                {
                    foreach (var error in errorsInModelState)
                    {
                        if (error.Value != null && error.Value.Count() > 0)
                        {
                            foreach (var subError in error.Value)
                            {
                                var errorModel = new ErrorModel
                                {
                                    PropertyName = error.Key,
                                    Message = subError,
                                    Code = ResponseStatusCode.BadRequest
                                };

                                if (subError.Contains("409"))
                                {
                                    errorModel.Message = subError.Replace("409", "");
                                    errorModel.Code = ResponseStatusCode.Conflict;
                                }
                                errorResponse.Errors.Add(errorModel);
                            }
                        }
                    }
                }
                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();

        }
    }
}
