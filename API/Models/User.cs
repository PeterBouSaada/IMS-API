using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonElement("username")]
        public string username { get; set; }
        [BsonElement("password")]
        public string password { get; set; }
        [BsonElement("salt")]
        public string salt { get; set; }
    }
}
