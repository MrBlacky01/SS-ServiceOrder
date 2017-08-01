using System.Collections.Generic;
using System.Web.Http;
using MongoDB.Bson;
using ServiceOrder.LocalizationService.Models;
using ServiceOrder.LocalizationService.Repositories.Implementations;

namespace ServiceOrder.LocalizationService.Controllers
{
    [Route("api/localization")]
    public class LocalizationController : ApiController
    {
        private LocalizationRepository repository;

        public LocalizationController()
        {
            repository = new LocalizationRepository();
        }

        [HttpGet]
        // GET: api/localization
        public IEnumerable<LocalizationPhrase> Get()
        {
            return repository.GetAll();
        }

        [HttpGet]
        // GET: api/localization?id=24digitString
        public LocalizationPhrase GetById(string id)
        {
            return repository.GetById(ObjectId.Parse(id));
        }

        // POST: api/localization
        public void Post([FromBody]LocalizationPhrase value)
        {
            repository.Create(value);
        }

        // PUT: api/localization/5
        public void Put(string id,[FromBody]LocalizationPhrase value)
        {
            value.PhraseId = ObjectId.Parse(id);
            repository.Update(value);
        }

        // DELETE: api/localization/5
        public void Delete(string id)
        {
            repository.Delete(ObjectId.Parse(id));
        }
    }
}
