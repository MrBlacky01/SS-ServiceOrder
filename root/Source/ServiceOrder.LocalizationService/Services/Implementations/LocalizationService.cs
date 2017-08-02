using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceOrder.LocalizationService.Models;
using ServiceOrder.LocalizationService.Repositories.Interfaces;
using ServiceOrder.LocalizationService.Services.Interfaces;

namespace ServiceOrder.LocalizationService.Services.Implementations
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ILocalizationRepository _repository;

        public LocalizationService(ILocalizationRepository repository)
        {
            _repository = repository;
        }
        public void Create(LocalizationPhrase element)
        {
            _repository.Create(element);
        }

        public IEnumerable<LocalizationPhrase> GetAll()
        {
            return _repository.Read(new BsonDocument());
        }

        public LocalizationPhrase GetById<TId>(TId id)
        {
            return _repository.Read(CreateFilterById(ObjectId.Parse(id.ToString()))).FirstOrDefault();
        }

        public void Update(LocalizationPhrase element)
        {
            _repository.Update(element,CreateFilterById(element.PhraseId));
        }

        public void Delete<TId>(TId id)
        {
            _repository.Delete(CreateFilterById(ObjectId.Parse(id.ToString())));
        }

        public LocalizationPhrase GetByLocalizationKeyAndType(int key, string type)
        {
            return _repository.Read(CreateFilterByLocalizationKeyAndType(key,type)).FirstOrDefault();
        }

        private FilterDefinition<LocalizationPhrase> CreateFilterById(ObjectId id)
        {
            return Builders<LocalizationPhrase>.Filter.Eq(e => e.PhraseId, id);
        }

        private FilterDefinition<LocalizationPhrase> CreateFilterByLocalizationKeyAndType(int key,string type)
        {
            var filterByKey = Builders<LocalizationPhrase>.Filter.Eq(e => e.PhraseKey, key);
            var filterByLocalizationType = Builders<LocalizationPhrase>.Filter.Eq(e => e.LocalizationType, type);
            return Builders<LocalizationPhrase>.Filter.And(filterByLocalizationType, filterByKey);
        } 

    }
}