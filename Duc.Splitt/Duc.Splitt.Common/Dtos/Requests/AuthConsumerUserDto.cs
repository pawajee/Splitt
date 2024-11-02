using System.ComponentModel.DataAnnotations;

namespace Duc.Splitt.Common.Dtos.Requests
{
    public class AuthConsumerUserDto
    {
        public class RegisterDto
        {

            [Required(ErrorMessage = "MobileNo is required")]
            public string MobileNo { get; set; } = null!;

        }
        public class VerifyOtpDto
        {
            public string MobileNo { get; set; } = null!;
            public string Otp { get; set; } = null!;
        }
        public class SetPasswordDto
        {

            public string UserId { get; set; } = null!;
            public string Token { get; set; } = null!;
            public string Password { get; set; } = null!;
        }
        public class LoginDto
        {

            [Required(ErrorMessage = "UserName is required")]
            [StringLength(50, MinimumLength = 8)]
            public string UserName { get; set; } = null!;



            [Required(ErrorMessage = "Password is required")]
            [StringLength(50, MinimumLength = 8)]
            public string Password { get; set; } = null!;

        }
    }

}
