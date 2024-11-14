using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;

namespace TodoServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtHelperController : ControllerBase
    {
        private const string TokenSecret = "notthatbigofakeybutstillfineforyourapplication";
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(1);
        [HttpPost("token")]
        public IActionResult GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenSecret);
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Sub, "userId"), // Example user ID claim
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, "https://localhost:7225/"), // Set issuer
                new Claim(JwtRegisteredClaimNames.Aud, "https://localhost:7225/"),  // Set audience
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                NotBefore = DateTime.UtcNow,//
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            return Ok(jwt);
        }
    }
}
