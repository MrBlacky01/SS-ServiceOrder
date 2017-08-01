using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MongoDB.Bson;
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

        public IEnumerable<LocalizationPhrase> GetAll()
        {
            return  _database.GetCollection<LocalizationPhrase>(collectionName).Find(new BsonDocument()).ToListAsync().Result;
        }


        public LocalizationPhrase GetById(ObjectId id)
        {
            var query = Builders<LocalizationPhrase>.Filter.Eq(e => e.PhraseId, id);
            var phrase = _database.GetCollection<LocalizationPhrase>(collectionName).Find(query).ToListAsync();

            return phrase.Result.FirstOrDefault();
        }

        public void Create(LocalizationPhrase element)
        {
            _database.GetCollection<LocalizationPhrase>(collectionName).InsertOneAsync(element);
        }

        public void Update(LocalizationPhrase element)
        {           
            var filterByKey = Builders<LocalizationPhrase>.Filter.Eq(e => e.PhraseKey, element.PhraseKey);
            var filterByLocalizationType = Builders<LocalizationPhrase>.Filter.Eq(e => e.LocalizationType, element.LocalizationType);
            var filter = Builders<LocalizationPhrase>.Filter.And(filterByLocalizationType, filterByKey);
            _database.GetCollection<LocalizationPhrase>(collectionName).ReplaceOneAsync(filter, element);
        }

        public void Delete(ObjectId id)
        {
            var query = Builders<LocalizationPhrase>.Filter.Eq(e => e.PhraseId, id);
            _database.GetCollection<LocalizationPhrase>(collectionName).DeleteOneAsync(query);
        }

        public LocalizationPhrase Find(Func<LocalizationPhrase, bool> predicate)
        {
            throw new NotImplementedException();
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