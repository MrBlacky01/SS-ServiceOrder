using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServiceOrder.Logic.Services;
using ServiceOrder.ViewModel.ViewModels.Implementation;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels;

namespace ServiceOrder.WebSite.Controllers
{
    public class ServiceProvidersController : Controller
    {
        private IServiceProviderService _provider;
        private IRegionService _regionService;

        public ServiceProvidersController(IServiceProviderService provider, IRegionService regionService)
        {
            _provider = provider;
            _regionService = regionService;
        }
        // GET: ServiceProviders
        public ActionResult Index()
        {
            var  elements =_provider.GetAll();
            return View(elements);
        }

        public ActionResult Manage()
        {
            var provider = _provider.Get(User.Identity.GetUserId());
            return View(provider);
        }

        [HttpGet]
        public ActionResult AddRegion()
        {
            var regionViewModel = new ServiceProviderRegionsViewModel();
            regionViewModel.allRegions = _regionService.GetAll().ToList();
            regionViewModel.providerRegions = _provider.Get(User.Identity.GetUserId()).Regions.ToList();
            return View(regionViewModel);
        }

        [HttpPost]
        public void AddRegion(string s)
        {
            int x = 0;
            //return View("MessageView", new ResultMessageViewModel {Message = "From add region to provider"});
        }



        public ActionResult DeleteRegion(int regionId)
        {
            return View("Manage");
        }

        public ActionResult Order(string id)
        {
            var a = _provider.Get(id);
            return View(a);
        }
    }
}