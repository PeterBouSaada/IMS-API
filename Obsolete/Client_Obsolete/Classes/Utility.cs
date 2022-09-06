using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Client.Classes
{
    public class Utility
    {
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        public Utility(NavigationManager nav)
        {
            _navigationManager = nav;
        }

        public string getCurrentPage()
        {
            int baseUriLength = _navigationManager.BaseUri.Length - 1;
            return _navigationManager.Uri.Remove(0, baseUriLength);
        }

        public static bool isTokenExpired(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            try
            {
                JwtSecurityToken Token = handler.ReadJwtToken(token);
                return DateTime.Compare(Token.ValidTo, DateTime.UtcNow) < 0;
            }
            catch(Exception)
            {
                return true;
            }
            
        }

    }
}
