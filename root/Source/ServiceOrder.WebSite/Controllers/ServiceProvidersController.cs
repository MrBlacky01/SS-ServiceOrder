using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Ninject.Infrastructure.Language;
using PagedList;
using ServiceOrder.Logic.Services;
using ServiceOrder.ViewModel.ViewModels.Implementation;
using ServiceOrder.ViewModel.ViewModels.Implementation.RegionViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.WebSite.Controllers
{
    public class ServiceProvidersController : Controller
    {
        private IServiceProviderService _provider;
        private IRegionService _regionService;
        private ICategoryService _categoryService;
        private IServiceTypeService _serviceTypeService;
        

        public ServiceProvidersController(IServiceProviderService provider, IRegionService regionService,
            ICategoryService categoryService,IServiceTypeService serviceTypeService)
        {
            _provider = provider;
            _regionService = regionService;
            _categoryService = categoryService;
            _serviceTypeService = serviceTypeService;
        }

        // GET: ServiceProviders
        public ActionResult Index(int? page)
        {
            var pageSize = 1;
            var pageNumber = (page ?? 1);
            return View(_provider.GetAll().ToPagedList(pageNumber,pageSize));
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
            var allRegions = _regionService.GetAll().ToList();
            var provider = _provider.Get(User.Identity.GetUserId());

            regionViewModel.providerRegions = provider.Regions;
            regionViewModel.allRegions = new List<RegionEntityViewModel>();
            foreach (var item in allRegions)
            {
                if (!regionViewModel.providerRegions.Contains(item))
                {
                    regionViewModel.allRegions.Add(item);
                }
            }
            return View(regionViewModel);
        }

        [HttpPost]
        public ActionResult AddRegion(string s)
        {
            try
            {
                List<RegionEntityViewModel> regions = JsonConvert.DeserializeObject<List<RegionEntityViewModel>>(s);
                if (regions == null)
                {
                    return new HttpStatusCodeResult(400, "Wrong regions dataS");
                }
                var provider = _provider.Get(User.Identity.GetUserId());
                provider.Regions = regions;
                _provider.Update(provider);

            }
            catch (Exception exception)
            {
                return new HttpStatusCodeResult(400,  exception.Message);
            }
            return new HttpStatusCodeResult(200, "OK"); ;
        }



        public ActionResult DeleteRegion(int regionId)
        {
            var provider = _provider.Get(User.Identity.GetUserId());
            try
            {
                var region = _regionService.Get(regionId);
                if (region == null)
                {
                    throw new Exception("Wrond region parametr");
                }
                _provider.DeleteRegion(region,provider);
            }
            catch (Exception exception)
            {
                return View("MessageView", new ResultMessageViewModel { Message = exception.Message });
            }
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult AddService()
        {
            var model = new ServiceProviderCategoryViewModel {Categories = _categoryService.GetAll().ToList()};
            return View(model);
        }

        public ActionResult GetCategoryServices(int Id)
        {
            var servicesAll = _serviceTypeService.FindServicesInCategory(Id);
            var providerServices = _provider.Get(User.Identity.GetUserId()).Services.Where(src => src.Category.Id == Id).ToList();

            var providerServicesIndex = new List<int>();
            var serviceTypeViewModels = servicesAll as IList<ServiceTypeViewModel> ?? servicesAll.ToList();
            var allServices = serviceTypeViewModels.Select(item => new ServiceShortViewModel {Id = item.Id, Title = item.Title}).ToList();

            for (var i = 0; i < allServices.Count; i++)
            {
                var item = allServices[i];
                if (providerServices.Count(providerItem => providerItem.Id == item.Id) != 0x0)
                {
                    providerServicesIndex.Add(i);
                }
            }
            return this.Json(new {dataAll = JsonConvert.SerializeObject(allServices), chooseData = JsonConvert.SerializeObject(providerServicesIndex) }) ;
        }

        [HttpPost]
        public ActionResult AddServices(string services,string category)
        {
            try
            {
                List<ServiceTypeViewModel> servicesList = JsonConvert.DeserializeObject<List<ServiceTypeViewModel>>(services);
                ServiceCategoryEntityViewModel categoryEntity =
                    JsonConvert.DeserializeObject<ServiceCategoryEntityViewModel>(category);
                if (services == null)
                {
                    return new HttpStatusCodeResult(400, "Wrong services data");
                }
                var provider = _provider.Get(User.Identity.GetUserId());
                _provider.UpdateServices(servicesList,provider,categoryEntity);

            }
            catch (Exception exception)
            {
                return new HttpStatusCodeResult(400, exception.Message);
            }
            return  new HttpStatusCodeResult(200, "OK");
        }

        public ActionResult DeleteService(int serviceId)
        {
            var provider = _provider.Get(User.Identity.GetUserId());
            try
            {
                var service = _serviceTypeService.Get(serviceId);
                if (service == null)
                {
                    throw new Exception("Wrond service parametr");
                }
                _provider.DeleteService(service, provider);
            }
            catch (Exception exception)
            {
                return View("MessageView", new ResultMessageViewModel { Message = exception.Message });
            }
            return RedirectToAction("Manage");
        }


        public ActionResult Order(string id)
        {
            var a = _provider.Get(id);
            return View(a);
        }
    }
}