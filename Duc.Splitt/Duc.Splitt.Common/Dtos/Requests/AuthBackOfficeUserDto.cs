using System.ComponentModel.DataAnnotations;

namespace Duc.Splitt.Common.Dtos.Requests
{
    public class AuthBackOfficeUserDto
    {

        public class SetPasswordDto
        {

            public string Identifier { get; set; } = null!;
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
        public class RegisterDto
        {

            [Required(ErrorMessage = "User Name is required")]
            public string UserName { get; set; } = null!;


            [Required(ErrorMessage = "Email is required")]
            public string Email { get; set; } = null!;


            public string? Comments { get; set; }
        }
        public class ChangePasswordDto
        {
            [Required(ErrorMessage = "email is required")]
            [EmailAddress]
            public string Email { get; set; } = null!;

            [Required(ErrorMessage = "Current Password is required")]
            public string CurrentPassword { get; set; } = null!;

            [Required(ErrorMessage = "New Password is required")]
            public string NewPassword { get; set; } = null!;
        }
        public class ForgetPasswordDto
        {
            [Required(ErrorMessage = "email is required")]
            [EmailAddress]
            public string Email { get; set; } = null!;


        }

        public class ResetPasswordDto
        {
            [Required]
            public string Token { get; set; } = null!;

            [Required]
            [EmailAddress]
            public string Email { get; set; } = null!;

            [Required]
            public string NewPassword { get; set; } = null!;

            [Required]
            public string ConfirmPassword { get; set; } = null!;
        }
        public class CreateAdminUserDto
        {

            [Required]
            [EmailAddress]
            public string Email { get; set; } = null!;

            [Required]
            [StringLength(100, MinimumLength = 5)]
            public string NameArabic { get; set; } = null!;
            [Required]
            [StringLength(100, MinimumLength = 5)]
            public string NameEnglish { get; set; } = null!;
            [Required]
            
            public string MobileNo { get; set; } = null!;

        }

    }

}
