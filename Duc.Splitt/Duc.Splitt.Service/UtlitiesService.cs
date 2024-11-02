using Azure;
using Dapper;
using Duc.Splitt.Common.Dtos.Requests;
using Duc.Splitt.Common.Dtos.Responses;
using Duc.Splitt.Common.Enums;
using Duc.Splitt.Common.Helpers;
using Duc.Splitt.Common.Resources;
using Duc.Splitt.Core.Contracts.Repositories;
using Duc.Splitt.Core.Contracts.Services;
using Duc.Splitt.Core.Helper;
using Duc.Splitt.Data.Dapper;
using Duc.Splitt.Data.DataAccess.Models;
using Duc.Splitt.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using static Duc.Splitt.Common.Dtos.Requests.MerchantRequestDto;
using static Duc.Splitt.Common.Dtos.Responses.MerchantDto;

namespace Duc.Splitt.Service
{
    public class UtilitiesService : IUtilitiesService
    {

        private readonly IConfiguration _configuration;
        public UtilitiesService(IConfiguration configuration)
        {
            _configuration=configuration;
        }


        public string GenerateRequestNumber()
        {
            // Format date and time as "yyyyMMddHHmmss"
            string dateTimePart = DateTime.Now.ToString("yyyyMMddHHmm");

            // Generate a 5-digit random number
            Random random = new Random();
            string randomPart = random.Next(10000, 99999).ToString();

            // Combine date-time and random number
            return $"{dateTimePart}-{randomPart}";
        }
        public string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // 6-digit OTP
        }
        public string GenerateJwtToken(SplittIdentityUser user)
        {
            // Define claims for the token based on the user data
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          //  new Claim("refreshToken", user.RefreshToken ?? string.Empty),
          //  new Claim("refreshTokenExpiry", user.RefreshTokenExpiry?.ToString("o") ?? string.Empty) // ISO 8601 format
        };

            // Read secret and other JWT settings from configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]));

            // Create JWT token
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            // Return serialized JWT
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public List<string> GetErrorMessages(IdentityResult result)
        {
            List<IdentityError> errors = result.Errors.ToList();

            List<string> errorMessages = errors.Select(error => $"{error.Code}: {error.Description}")
                                              .ToList();

            return errorMessages;
        }

    }
}
