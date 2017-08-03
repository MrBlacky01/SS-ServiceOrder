using System.Collections.Generic;
using MongoDB.Driver;
using LocalizationServiceCore.Models;
using LocalizationServiceCore.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace LocalizationServiceCore.Repositories.Implementations
{
    public class LocalizationRepository : ILocalizationRepository
    {
        private readonly IMongoDatabase _database;
        private readonly string _collectionName ;

        public LocalizationRepository(IOptions<Settings> settings)
        {
            _database = Connect(settings);
            _collectionName = settings.Value.Collections["PhrasesCollection"];
            MakeIndex();
        }

        public void Create(LocalizationPhrase element)
        {
            _database.GetCollection<LocalizationPhrase>(_collectionName).InsertOneAsync(element);
        }

        public IEnumerable<LocalizationPhrase> Read(FilterDefinition<LocalizationPhrase> filter)
        {
            return _database.GetCollection<LocalizationPhrase>(_collectionName).Find(filter).ToListAsync().Result;
        }

        public void Update(LocalizationPhrase element, FilterDefinition<LocalizationPhrase> filter)
        {
            _database.GetCollection<LocalizationPhrase>(_collectionName).ReplaceOneAsync(filter, element);
        }

        public void Delete(FilterDefinition<LocalizationPhrase> filter)
        {
            _database.GetCollection<LocalizationPhrase>(_collectionName).DeleteOneAsync(filter);
        }


        private IMongoDatabase Connect(IOptions<Settings> settings)
        {
            return new MongoClient(settings.Value.ConnectionString).GetDatabase(settings.Value.Database); ;
        }

        private void MakeIndex()
        {
            var collection = _database.GetCollection<LocalizationPhrase>(_collectionName);

            collection.Indexes.CreateOne(Builders<LocalizationPhrase>.IndexKeys.Combine(
                Builders<LocalizationPhrase>.IndexKeys.Text(f => f.LocalizationType),
                Builders<LocalizationPhrase>.IndexKeys.Ascending(f => f.PhraseKey)),new CreateIndexOptions() {Unique = true});
        }
    }
}