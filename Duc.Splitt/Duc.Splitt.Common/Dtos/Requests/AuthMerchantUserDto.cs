using System.ComponentModel.DataAnnotations;

namespace Duc.Splitt.Common.Dtos.Requests
{
    public class AuthMerchantUserDto
    {
        public class RegisterDto
        {
           
            [Required(ErrorMessage = "User Name is required")]
            public string UserName { get; set; } = null!;

            
            [Required(ErrorMessage = "Email is required")]
            public string Email { get; set; } = null!;


            public string? Comments { get; set; }
        }
        public class SetPasswordDto
        {

            public string UserId { get; set; } = null!;
            public string Token { get; set; } = null!;
            public string Password { get; set; } = null!;
            public string? Comments { get; set; }
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
