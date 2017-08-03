using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LocalizationServiceCore.Models
{
    public class LocalizationPhrase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PhraseId { get; set; }
        public int PhraseKey { get; set; }
        public string LocalizationType { get; set; }
        public string PhraseText { get; set; }
    }
}