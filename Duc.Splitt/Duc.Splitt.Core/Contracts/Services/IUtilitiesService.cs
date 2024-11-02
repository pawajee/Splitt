using Duc.Splitt.Identity;
using Microsoft.AspNetCore.Identity;

namespace Duc.Splitt.Core.Contracts.Services
{
    public interface IUtilitiesService
    {
        string GenerateRequestNumber();
        string GenerateOtp();
        string GenerateJwtToken(SplittIdentityUser user);
        List<string> GetErrorMessages(IdentityResult result);
    }
}
