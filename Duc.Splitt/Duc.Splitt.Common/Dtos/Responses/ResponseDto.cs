using Duc.Splitt.Common.Enums;
using System.Text.Json.Serialization;

namespace Duc.Splitt.Common.Dtos.Responses
{

    public class ResponseDto<T>
    {
        public ResponseStatusCode Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        [JsonIgnore]
        public IEnumerable<string> Errors { get; set; }

    }
}

