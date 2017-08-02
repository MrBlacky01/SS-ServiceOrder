using System.Collections.Generic;
using System.Configuration;
using MongoDB.Driver;
using ServiceOrder.LocalizationService.Models;
using ServiceOrder.LocalizationService.Repositories.Interfaces;

namespace ServiceOrder.LocalizationService.Repositories.Implementations
{
    public class LocalizationRepository : ILocalizationRepository
    {
        private readonly IMongoDatabase _database;
        private readonly string collectionName = "phrases";

        public LocalizationRepository()
        {
            _database = Connect();
            MakeIndex();
        }

        public void Create(LocalizationPhrase element)
        {
            _database.GetCollection<LocalizationPhrase>(collectionName).InsertOneAsync(element);
        }

        public IEnumerable<LocalizationPhrase> Read(FilterDefinition<LocalizationPhrase> filter)
        {
            return _database.GetCollection<LocalizationPhrase>(collectionName).Find(filter).ToListAsync().Result;
        }

        public void Update(LocalizationPhrase element, FilterDefinition<LocalizationPhrase> filter)
        {
            _database.GetCollection<LocalizationPhrase>(collectionName).ReplaceOneAsync(filter, element);
        }

        public void Delete(FilterDefinition<LocalizationPhrase> filter)
        {
            _database.GetCollection<LocalizationPhrase>(collectionName).DeleteOneAsync(filter);
        }


        private IMongoDatabase Connect()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            var con = new MongoUrlBuilder(connectionString);

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(con.DatabaseName);

            return database;
        }

        private void MakeIndex()
        {
            var collection = _database.GetCollection<LocalizationPhrase>(collectionName);

            collection.Indexes.CreateOne(Builders<LocalizationPhrase>.IndexKeys.Combine(
                Builders<LocalizationPhrase>.IndexKeys.Text(f => f.LocalizationType),
                Builders<LocalizationPhrase>.IndexKeys.Ascending(f => f.PhraseKey)),new CreateIndexOptions() {Unique = true});
        }
    }
}