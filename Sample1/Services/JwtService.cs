using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample1.Setting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sample1.Controllers {
    
    public class JWTAuthenticate : IJwtService {
        private readonly JWTtoken jwtToken;

        public JWTAuthenticate(IOptions<JWTtoken> token) {
            jwtToken = token.Value;
        }

        public string GenerateToken(IEnumerable<Claim> claims) {            
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtToken.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Issuer = jwtToken.Issuer,
                Audience = jwtToken.Audience,   
                Expires = DateTime.UtcNow.AddMinutes(jwtToken.AccessExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
            };
            var stoken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(stoken);
        }
        public string GenerateRefreshToken() {
            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create()) {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}