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
        private IConfiguration configuration;
        private string privateKey;
        private double expireTime;

        public JWTAuthenticationService(IConfiguration config)
        {
            configuration = config;
            IConfigurationSection JWTData = configuration.GetSection("JWTTokenData");
            privateKey = JWTData.GetSection("Private_Key").Value;
            expireTime = Convert.ToDouble(JWTData.GetSection("Expires_In").Value);
        }

        public string CreateJWTToken(User user)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(privateKey);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor() {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.username)
                }),
                Expires = DateTime.UtcNow.AddHours(expireTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
