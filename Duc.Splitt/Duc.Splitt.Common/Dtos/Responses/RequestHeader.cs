using Duc.Splitt.Common.Enums;

namespace Duc.Splitt.Common.Dtos.Responses
{
    public class RequestHeader
    {
        public bool IsArabic { get; set; }
        public string DeviceId { get; set; } = null!;
        public string DeviceToken { get; set; } = null!;
        public DeviceTypes DeviceTypeId { get; set; }
        public Locations LocationId { get; set; }

    }

}

