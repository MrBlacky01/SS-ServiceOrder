using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PagedList;
using ServiceOrder.Logic.Services;
using ServiceOrder.ViewModel.ViewModels.Implementation;
using ServiceOrder.ViewModel.ViewModels.Implementation.AlbumViewModels;
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
        private IAlbumService _albumService;
        

        public ServiceProvidersController(IServiceProviderService provider, IRegionService regionService,
            ICategoryService categoryService,IServiceTypeService serviceTypeService, IAlbumService albumService)
        {
            _provider = provider;
            _regionService = regionService;
            _categoryService = categoryService;
            _serviceTypeService = serviceTypeService;
            _albumService = albumService;
        }

        // GET: ServiceProviders
        [Authorize(Roles = "client")]
        public ActionResult Index()
        {
            var model = new ServiceProviderPaginationFiltrationList();
            var pageSize = 5;
            model.Page = 1;
            model.ProviderList = _provider.GetAllWithServiceAndRegion().ToPagedList(model.Page.Value, pageSize);
            
            return View(model);
        }

        [Authorize(Roles = "client")]
        public ActionResult Filter(ServiceProviderPaginationFiltrationList model)
        {
            var pageSize = 5;
            var pageNumber = (model.Page ?? 1);
            model.ProviderList =
                _provider.FilterGetProviders(model.RegionId, model.CategoryId, model.ServiceId)
                    .ToPagedList(pageNumber, pageSize);
            return View("Index",model);
        }

        [Authorize(Roles = "service provider")]
        public ActionResult Manage()
        {
            var provider = _provider.Get(User.Identity.GetUserId());
            return View(provider);
        }

        [HttpPost]
        [Authorize(Roles = "service provider")]
        public ActionResult EditDescription(ServiceProviderViewModel providerModel)
        {
            var provider =_provider.Get(User.Identity.GetUserId());
            provider.Description = providerModel.Description;
            _provider.Update(provider);
            return RedirectToAction("Manage");
        }

        [HttpGet]
        [Authorize(Roles = "service provider")]
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
        [Authorize(Roles = "service provider")]
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


        [Authorize(Roles = "service provider")]
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
        [Authorize(Roles = "service provider")]
        public ActionResult AddService()
        {
            var model = new ServiceProviderCategoryViewModel {Categories = _categoryService.GetAll().ToList()};
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult GetRegions()
        {
            try
            {
                var regions = _regionService.GetAll();
                return Json(new {regions = JsonConvert.SerializeObject(regions)}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,exception.Message);
            }

        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            try
            {
                var categories = _categoryService.GetAll();
                return Json(new { categories = JsonConvert.SerializeObject(categories) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        [HttpGet]
        public ActionResult GetCategoryServicesAll(int Id)
        {
            try
            {
                var servicesAll = _serviceTypeService.FindServicesInCategory(Id);
                var serviceTypeViewModels = servicesAll as IList<ServiceTypeViewModel> ?? servicesAll.ToList();
                var allServices = serviceTypeViewModels.Select(item => new ServiceShortViewModel { Id = item.Id, Title = item.Title }).ToList();
                return Json(new { services = JsonConvert.SerializeObject(allServices) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }

        [HttpGet]
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
            return Json(new {dataAll = JsonConvert.SerializeObject(allServices), chooseData = JsonConvert.SerializeObject(providerServicesIndex) }, JsonRequestBehavior.AllowGet) ;
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

        [Authorize(Roles = "service provider")]
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

        public ActionResult ShowServiceProvider(string providerId)
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "service provider")]
        public ActionResult AddAlbum()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "service provider")]
        public ActionResult AddAlbum(ShortAlbumViewModel album)
        {
            album.Title = album.Title.Trim();
            if (ModelState.IsValid)
            {
                album.ServiceProviderId = User.Identity.GetUserId();
                try
                {
                    _albumService.Add(album);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("",exception.Message);
                    return View(album);
                }
                
                return RedirectToAction("Manage");
            }
            return View(album);
        }

        [HttpGet]
        [Authorize(Roles = "service provider")]
        public ActionResult ManageAlbum(int albumId)
        {
            try
            {
                var album = _albumService.Get(albumId);
                return View(album);
            }
            catch (Exception exception)
            {
                return View("MessageView", new ResultMessageViewModel() {Message = "No such album"});
            }
        }

        [HttpPost]
        [Authorize(Roles = "service provider")]
        public ActionResult AddPhotosToAlbum (int Id,IEnumerable<HttpPostedFileBase> files)
        {
            return new HttpStatusCodeResult(200, "OK");
        }

    }
}