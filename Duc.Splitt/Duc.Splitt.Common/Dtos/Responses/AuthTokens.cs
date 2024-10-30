namespace Duc.Splitt.Common.Dtos.Responses
{
    public class AuthTokens
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
