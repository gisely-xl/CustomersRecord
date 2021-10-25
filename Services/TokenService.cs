using CustomersRec.APIrest.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CustomersRec.APIrest.Services
{
    public class TokenService
    {
        private readonly string _privateKey;
        public TokenService(IConfiguration configuration)
        {
            _privateKey = configuration.GetSection("Secret").GetSection("PrivateKey").Value;
        }

        public string GenerateToken(Admin admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var keyEncripted = Encoding.ASCII.GetBytes(_privateKey);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, admin.Name),
                    new Claim(ClaimTypes.Role, admin.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyEncripted), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
