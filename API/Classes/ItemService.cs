using API.Interfaces;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

using System.Reflection;

namespace API.Classes
{
    public class ItemService : IItemService
    {
        private IConfiguration configuration;
        private MongoClient dbClient;
        private IMongoDatabase database;
        private IMongoCollection<Item> collection;
        private String MongoDatabase;
        private ICache<Item> cacheService;

        public ItemService(IConfiguration config, ICache<Item> cache)
        {
            this.configuration = config;
            MongoDatabase = configuration.GetConnectionString("Database");
            dbClient = new MongoClient(MongoDatabase);
            database = dbClient.GetDatabase("IMS");
            collection = database.GetCollection<Item>("items");
            cacheService = cache;
        }

        public Item AddItem(Item item)
        {
            try
            {
                collection.InsertOne(item);
                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Item DeleteItem(string id)
        {
            try
            {
                cacheService.Expire("item_" + id);
                return collection.FindOneAndDelete(f => f.id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Item> FindItem(Item item)
        {
            PropertyInfo[] ItemProperties = item.GetType().GetProperties();
            FilterDefinitionBuilder<Item> FilterBuilder = new FilterDefinitionBuilder<Item>();
            List<FilterDefinition<Item>> filters = new List<FilterDefinition<Item>>();
            FilterDefinition<Item> finalFilter;

            for (int i = 0; i < ItemProperties.Length; i++)
            {
                if(ItemProperties[i].GetValue(item) != null)
                {
                    filters.Add(FilterBuilder.Regex(ItemProperties[i].Name, new BsonRegularExpression(ItemProperties[i].GetValue(item).ToString())));
                }
            }

            if (filters.Count() == 0)
                return null;

            finalFilter = FilterBuilder.Or(filters);

            return collection.Find(finalFilter).ToList();
        }

        public Item FindOneItem(string id)
        {
            try
            {
                return collection.Find(f => f.id == id).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Item> getAllItems()
        {
            try
            {
                return collection.Find(Builders<Item>.Filter.Empty).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Item UpdateItem(Item item)
        {
            Item OldItem = collection.Find(f => f.id == item.id).FirstOrDefault();
            UpdateDefinitionBuilder<Item> UpdateBuilder = Builders<Item>.Update;
            PropertyInfo[] ItemProperties = item.GetType().GetProperties();
            PropertyInfo[] OldItemProperties = OldItem.GetType().GetProperties();
            UpdateDefinition<Item> finalUpdate;
            List<UpdateDefinition<Item>> updates = new List<UpdateDefinition<Item>>();

            for (int i = 0; i < OldItemProperties.Length; i++)
            {
                if (ItemProperties[i].GetValue(item) != null)
                {
                    if (!ItemProperties[i].GetValue(item).Equals(OldItemProperties[i].GetValue(OldItem)))
                    {
                        updates.Add(UpdateBuilder.Set(ItemProperties[i].Name, ItemProperties[i].GetValue(item)));
                    }
                }
            }

            finalUpdate = UpdateBuilder.Combine(updates);

            if (updates.Count > 0)
            {
                UpdateResult result = collection.UpdateOne(f => f.id == item.id, finalUpdate);
                if (result.ModifiedCount > 0)
                {
                    cacheService.Expire("item_" + item.id);
                    return collection.Find(f => f.id == item.id).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
