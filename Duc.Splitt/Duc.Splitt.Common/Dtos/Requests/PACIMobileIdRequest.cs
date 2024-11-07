using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Duc.Splitt.Common.Dtos.Requests
{
    public class PACIMobileIdRequest
    {
        public class MobileAuthPNRequestDto
        {
            [Description("User Identifier to be sent to  MID (For MID, this should be Civil ID) ")]
            [Required]
            public string CivilId { get; set; } = null!;

            public Guid RefId { get; set; }

            [Required]
            public Guid CustomerRegistrationRequestId { get; set; }
        }

    }
}
