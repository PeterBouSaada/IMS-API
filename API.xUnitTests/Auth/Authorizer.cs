using API.Classes.Utility;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using MongoDB.Bson;
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

            var settingPath = "../../../../API";

            var absolutePath = System.IO.Path.GetFullPath(settingPath);

            var builder = new ConfigurationBuilder().SetBasePath(absolutePath).AddJsonFile("appsettings.Development.json", false, true);

            var config = builder.Build();

            AuthService = new JWTAuthenticationService(config);
        }

        public string? GetToken()
        {
            if (token != null) return token;

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
