using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceOrder.Logic.Services;

namespace ServiceOrder.WebSite.Controllers
{
    public class ServiceProvidersController : Controller
    {
        private IServiceProviderService _provider;

        public ServiceProvidersController(IServiceProviderService provider)
        {
            _provider = provider;
        }
        // GET: ServiceProviders
        public ActionResult Index()
        {
            var  elements =_provider.GetAll();
            return View(elements);
        }
    }
}