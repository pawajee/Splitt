using Duc.Splitt.Common.Helpers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Duc.Splitt.MIDCallbackAPI.ActionFilters
{
    public class CustomHeaderSwaggerAttribute : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null)
            {
                throw new ArgumentNullException("Invalid operation");
            }
            operation.Parameters.Add(new OpenApiParameter
            {
                In = ParameterLocation.Header,
                Name = "Accept-Language",
                Description = $"Input as the langaue here: examples like => {Constant.LanguageEnText}, {Constant.LanguageArText}",
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                In = ParameterLocation.Header,
                Name = "PlatformTypeId",
                Description = $"Input as the Device Type Id (MerchantPortal=1),(IOS=1),(Android=2)",
                Schema = new OpenApiSchema
                {
                    Type = "int"
                }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                In = ParameterLocation.Header,
                Name = "DeviceIdentifier",
                Description = $"Input as the  IP / Device Id",
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                In = ParameterLocation.Header,
                Name = "SessionIdentifier",
                Description = $"Input as the  SessionIdentifier /DeviceToken",
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });
        }
    }
}
