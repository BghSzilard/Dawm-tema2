using DataLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthorizationService
    {
        private readonly string _securityKey;
        private int PBKDF2IterCount = 1000;
        private int PBKDF2SubkeyLength = 256 / 8;
        private int SaltSize = 128 / 8;
        public AuthorizationService(IConfiguration config)
        {
            _securityKey = config["JWT:SecurityKey"];
        }
        public string GetToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var roleClaim = new Claim("role", user.RoleType.ToString());
            var idClaim = new Claim("userId", user.Id.ToString());
            var infoClaim = new Claim("username", user.Email);
            var tokenDescriptior= new SecurityTokenDescriptor
            {
                Issuer = "Backend",
                Audience = "Frontend",
                Subject = new ClaimsIdentity(new[] { roleClaim, idClaim, infoClaim }),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptior);
            var tokenString = jwtTokenHandler.WriteToken(token);

            return tokenString;

        }
    }
}
