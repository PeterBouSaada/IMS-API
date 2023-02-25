using API.Interfaces;
using API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Classes.Utility
{
    public class JWTAuthenticationService : IJWTAuthenticationService
    {
        private string privateKey;
        private double expireTime;
        private JwtSecurityTokenHandler _handler;

        public JWTAuthenticationService()
        {
            privateKey = Environment.GetEnvironmentVariable("JWT_PRIVATE_KEY", EnvironmentVariableTarget.Machine);
            expireTime = Convert.ToDouble(Environment.GetEnvironmentVariable("JWT_EXPIRES_IN", EnvironmentVariableTarget.Machine));
            _handler = new JwtSecurityTokenHandler();
        }

        public string CreateJWTToken(User user)
        {
            
            byte[] key = Encoding.ASCII.GetBytes(privateKey);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.username)
                }),
                Expires = DateTime.UtcNow.AddHours(expireTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            SecurityToken token = _handler.CreateToken(descriptor);
            return _handler.WriteToken(token);
        }

        public bool isJWTTokenValid(string token)
        {
        
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(privateKey));

            try
            {
                _handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
