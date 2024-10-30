using Duc.Splitt.Common.Enums;

namespace Duc.Splitt.Common.Dtos.Responses

{
    public class ErrorDtos
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
    public class ErrorModel
    {
        public string? PropertyName { get; set; }

        public string? Message { get; set; }
        public ResponseStatusCode Code { get; set; }
    }

}
