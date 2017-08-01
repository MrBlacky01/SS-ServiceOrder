using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        // GET: api/localization
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/localization/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/localization
        public void Post([FromBody]LocalizationPhrase value)
        {
            repository.Create(value);
        }

        // PUT: api/localization/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/localization/5
        public void Delete(int id)
        {
        }
    }
}
