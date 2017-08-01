using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceOrder.LocalizationService.Models
{
    public class LocalizationPhrase
    {
        [BsonId]
        public ObjectId PhraseId { get; set; }
        public int PhraseKey { get; set; }
        public string LocalizationType { get; set; }
        public string PhraseText { get; set; }
    }
}