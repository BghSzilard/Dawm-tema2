using DataLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        }
    }
}
