using API.Classes.Utility;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace API.xUnitTests.Auth
{
    public class Authorizer
    {

        private IJWTAuthenticationService AuthService;
        public string token { get; private set; }


        private static Authorizer? Instance = null;

        public static Authorizer instance { get
            {
                if (Instance == null)
                    Instance = new Authorizer();
                return Instance;
            }
        }

        private Authorizer()
        {
            token = "";
            IConfiguration config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            AuthService = new JWTAuthenticationService(config);
        }

        public string? GetToken()
        {
            
            if (token != null && AuthService.isJWTTokenValid(token)) return token;

            User user = new User();
            user.username = "testUser";
            user.password = "testPassword";

            string retval = AuthService.CreateJWTToken(user);

            if (retval != null && retval.Length > 0)
                return retval;
            return null;
        }

    }
}
