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
        private const string DatabaseName = "ProjektZaliczeniowy";

        private readonly IMongoDatabase _database;
        // private const string collectionName = "scores";
        // private readonly IMongoCollection<Score> ScoresCollection;
        private readonly FilterDefinitionBuilder<Score> _filterBuilder = Builders<Score>.Filter;

        public MongoDbScoresRepository(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase(DatabaseName);
        }
        // public async Task<IEnumerable<Score>> GetScoresAsync()
        // {
        //     return await ScoresCollection.Find(new BsonDocument()).ToListAsync(); // pusty plik jako filtr
        //     // https://stackoverflow.com/questions/30650722/difference-between-find-and-findasync
        // }
        //
        // public async Task<Score> GetScoreAsync(Guid id)
        // {
        //     var filter = filterBuilder.Eq(Score => Score.Id, id);
        //     return await ScoresCollection.Find(filter).SingleOrDefaultAsync();
        // }
        //
        // public async Task CreateScoreAsync(Score Score)
        // {
        //     await ScoresCollection.InsertOneAsync(Score);
        // }
        //
        // public async Task UpdateScoreAsync(Score Score)
        // {
        //     var filter = filterBuilder.Eq(existingScore => existingScore.Id, Score.Id);
        //     await ScoresCollection.ReplaceOneAsync(filter, Score);
        // }
        //
        // public async Task DeleteScoreAsync(Guid id)
        // {
        //     var filter = filterBuilder.Eq(Score => Score.Id, id);
        //     await ScoresCollection.DeleteOneAsync(filter);
        // }
        public async Task<IEnumerable<Score>> GetAllScoresOfStudentAsync(string studentName)
        {
            return await _database.GetCollection<Score>(studentName).Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Score> GetScoreOfStudentAsync(string studentName, Guid scoreId)
        {
            var filter = _filterBuilder.Eq(score => score.Id, scoreId);
            return await _database.GetCollection<Score>(studentName).Find(filter).SingleOrDefaultAsync();
        }

        public async Task CreateScoreAsync(Score score)
        {
            await _database.GetCollection<Score>(score.StudentName).InsertOneAsync(score);
        }

        public async Task UpdateScoreOfStudentAsync(Score score)
        {
            var filter = _filterBuilder.Eq(existingScore => existingScore.Id, score.Id);
            await _database.GetCollection<Score>(score.StudentName).ReplaceOneAsync(filter, score);
        }

        public async Task DeleteScoreAsync(string studentName, Guid id)
        {
            var filter = _filterBuilder.Eq(score => score.Id, id);
            await _database.GetCollection<Score>(studentName).DeleteOneAsync(filter);
        }
        

        public async Task DeleteAllScoresAsync(string studentName)
        {
            await _database.GetCollection<Score>(studentName).DeleteManyAsync(new BsonDocument());
        }
    }
}