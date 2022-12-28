using System;
using System.Collections.Generic;
using MongoDB.Driver;
using ProjektZaliczeniowy.Entities;

namespace ProjektZaliczeniowy.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName = "ProjektZaliczeniowy";
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> itemsCollection;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);

        }
        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public Item GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}