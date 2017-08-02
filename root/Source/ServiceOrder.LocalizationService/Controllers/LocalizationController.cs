using System.Collections.Generic;
using System.Web.Http;
using MongoDB.Bson;
using ServiceOrder.LocalizationService.Models;
using ServiceOrder.LocalizationService.Services.Interfaces;

namespace ServiceOrder.LocalizationService.Controllers
{
    [Route("api/localization")]
    public class LocalizationController : ApiController
    {
        private ILocalizationService _service;

        public LocalizationController(ILocalizationService service)
        {
            _service = service;
        }

        [HttpGet]
        // GET: api/localization
        public IEnumerable<LocalizationPhrase> Get()
        {
            return _service.GetAll();
        }

        [HttpGet]
        // GET: api/localization?id=24digitString
        public LocalizationPhrase GetById(string id)
        {
            return _service.GetById(id);
        }

        [HttpGet]
        public LocalizationPhrase GetByLocalizationKeyAndType(int key, string type)
        {
            return _service.GetByLocalizationKeyAndType(key, type);
        }

        // POST: api/localization
        public void Post([FromBody]LocalizationPhrase value)
        {
            _service.Create(value);
        }

        // PUT: api/localization/5
        public void Put(string id,[FromBody]LocalizationPhrase value)
        {
            
            value.PhraseId = ObjectId.Parse(id);
            _service.Update(value);
        }

        // DELETE: api/localization/5
        public void Delete(string id)
        {
            _service.Delete(id);
        }
    }
}
