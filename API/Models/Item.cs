using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [JsonProperty(PropertyName = "part_number")]
        [BsonElement("part_number")]
        public string partNumber { get; set; }
        [BsonElement("type")]
        public string type { get; set; }
        [JsonProperty(PropertyName = "qty_on_hand")]
        [BsonElement("qty_on_hand")]
        public int? qtyOnHand { get; set; }
        [BsonElement("location")]
        public string location { get; set; }
        [BsonElement("height")]
        public double? height { get; set; }
        [BsonElement("width")]
        public double? width { get; set; }
        [BsonElement("length")]
        public double? length { get; set; }
        [BsonElement("uom")]
        public string uom { get; set; }
        [BsonElement("manufacturer")]
        public string manufacturer { get; set; }
        [JsonProperty(PropertyName = "manufacturer_number")]
        [BsonElement("manufacturer_number")]
        public string manufacturerNumber { get; set; }
        [JsonProperty(PropertyName = "used_for")]
        [BsonElement("used_for")]
        public string usedFor { get; set; }
        [BsonElement("horsepower")]
        public double? horsepower { get; set; }
        [BsonElement("amperage")]
        public double? amperage { get; set; }
        [BsonElement("voltage")]
        public double? voltage { get; set; }
        [BsonElement("rpm")]
        public int? rpm { get; set; }

    }
}
