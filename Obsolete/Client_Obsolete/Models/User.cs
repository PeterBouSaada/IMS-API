using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class User
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }
}
