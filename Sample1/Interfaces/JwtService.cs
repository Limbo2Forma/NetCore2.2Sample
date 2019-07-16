using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample1.Models;
using Sample1.Setting;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sample1.Controllers {
    public interface IJwtService {       
        string GenerateToken(string username);
    }

    public class JWTAuthenticate : IJwtService {
        // users hardcoded for simplicity
        private readonly JWTtoken jwtToken;

        public JWTAuthenticate(IOptions<JWTtoken> token) {
            jwtToken = token.Value;
        }

        public string GenerateToken(string username) {            
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtToken.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, username) }),
                Issuer = jwtToken.Issuer,
                Audience = jwtToken.Audience,   
                Expires = DateTime.UtcNow.AddMinutes(jwtToken.AccessExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
            };
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }
}