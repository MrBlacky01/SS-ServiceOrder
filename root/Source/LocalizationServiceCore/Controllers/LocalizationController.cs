using System.Collections.Generic;
using MongoDB.Bson;
using LocalizationServiceCore.Models;
using LocalizationServiceCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LocalizationServiceCore.Controllers
{
    [Route("api/localization")]
    public class LocalizationController : Controller
    {
        private readonly ILocalizationService _service;

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

        [HttpGet("{id}")]
        // GET: api/localization?id=24digitString
        public LocalizationPhrase GetById(string id)
        {
            return _service.GetById(id);
        }

        [HttpGet("phrase")]
        public LocalizationPhrase GetByLocalizationKeyAndType(int key, string type)
        {
            return _service.GetByLocalizationKeyAndType(key, type);
        }

        // POST: api/localization
        [HttpPost]
        public void Post([FromBody]LocalizationPhrase value)
        {
            _service.Create(value);
        }

        // PUT: api/localization/5
        [HttpPut("{id}")]
        public void Put(string id,[FromBody]LocalizationPhrase value)
        {     
            value.PhraseId = id;
            _service.Update(value);
        }

        // DELETE: api/localization/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _service.Delete(id);
        }
    }
}
