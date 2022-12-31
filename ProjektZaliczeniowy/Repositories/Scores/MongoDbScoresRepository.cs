using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ProjektZaliczeniowy.Entities;
// await keyword provides a nonblocking way to start a task, then continue execution when that task completes.
namespace ProjektZaliczeniowy.Repositories
{
    public class MongoDbScoresRepository : IScoresRepository
    {
        private const string databaseName = "ProjektZaliczeniowy";
        private const string collectionName = "scores";
        private readonly IMongoCollection<Score> scoresCollection;
        private readonly FilterDefinitionBuilder<Score> filterBuilder = Builders<Score>.Filter;

        public MongoDbScoresRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            scoresCollection = database.GetCollection<Score>(collectionName);

        }
        public async Task<IEnumerable<Score>> GetScoresAsync()
        {
            return await scoresCollection.Find(new BsonDocument()).ToListAsync(); // pusty plik jako filtr
            // https://stackoverflow.com/questions/30650722/difference-between-find-and-findasync
        }

        public async Task<Score>  GetScoreAsync(Guid userID, Guid scoreID)
        {
            var filter = filterBuilder.Eq(score => score.Id, userID);
            return await scoresCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task CreateScoreAsync(Guid userID, Score score)
        {
            await scoresCollection.InsertOneAsync(score);
        }

        public async Task UpdateScoreAsync(Guid userID, Guid scoreID, Score score)
        {
            var filter1 = filterBuilder.Eq(existingItem => existingItem.Id, userID);
            var filter2 = filterBuilder.Eq(existingItem =>existingItem.)
            await scoresCollection.ReplaceOneAsync(filter, score);
        }

        public async Task DeleteScoreAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await scoresCollection.DeleteOneAsync(filter);
        }
    }
}