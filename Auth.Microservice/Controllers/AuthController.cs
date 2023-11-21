using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Microservice.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Get(string name, string pwd)
        {
            //just hard code here.  
            if (name == "catcher" && pwd == "123")
            {
                var now = DateTime.UtcNow;

                var claims = new Claim[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                };

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("abcdefgh12345678abcdefgh12345678"));
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                var jwt = new JwtSecurityToken(
                    issuer: "http",
                    audience: "Catcher Wong",
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var responseJson = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)TimeSpan.FromMinutes(2).TotalSeconds
                };

                return Json(responseJson);
            }
            else
            {
                return Json("");
            }
        }
    }
}
