namespace Duc.Splitt.Common.Dtos.Responses
{
    public class AuthTokens
    {
        public string Token { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
    public class VerifyOtpResponse
    {
        public bool IsNewCustomer { get; set; }
        public Guid OtpRequestId { get; set; }
        public AuthTokens? AuthTokens { get; set; }
    }
    public class CustomerRegistrationResponseDto
    {
        public Guid CustomerRegistrationRequestId { get; set; }
    }
}
