namespace Duc.Splitt.Common.Dtos.Responses
{
    public class AuthTokens
    {
        public string Token { get; set; } = null!;
        public string? RefreshToken { get; set; } 
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
