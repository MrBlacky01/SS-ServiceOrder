using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PagedList;
using ServiceOrder.Logic.Services;
using ServiceOrder.ViewModel.ViewModels.Implementation;
using ServiceOrder.ViewModel.ViewModels.Implementation.Order;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels;

namespace ServiceOrder.WebSite.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        private IServiceProviderService _serviceProviderService;
        private ICategoryService _categoryService;

        public OrderController(IOrderService orderService, IServiceProviderService serviceProviderService,
            ICategoryService categoryService)
        {
            _orderService = orderService;
            _serviceProviderService = serviceProviderService;
            _categoryService = categoryService;
        }

        // GET: Order
        [Authorize(Roles = "client")]
        public ActionResult Index(string providerId)
        {
            var provider = new ServiceProviderViewModel();
            try
            {
                provider = _serviceProviderService.Get(providerId);
            }
            catch (Exception exception)
            {
                return View("MessageView", new ResultMessageViewModel {Message = exception.Message});
            }

            if (provider == null)
            {
                return View("MessageView", new ResultMessageViewModel {Message = "There is not such provider"});
            }

            var model = new OrderViewModel();
            foreach (var region in provider.Regions)
            {
                model.Regions.Add(region);
            }

            foreach (var service in provider.Services)
            {
                if (model.Categories.Count(category => category.Id == service.Category.Id) == 0x0)
                {
                    model.Categories.Add(service.Category);
                }
            }

            model.Regions.Sort((firstRegion, secondRegion) => firstRegion.Id.CompareTo(secondRegion.Id));
            model.Categories.Sort((firstCategory, secondCategory) => firstCategory.Id.CompareTo(secondCategory.Id));
            model.ServiceProviderId = provider.Id;
            model.ProviderName = provider.Name;

            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult GetProviderServicesByCategory(int categoryId, string providerId)
        {
            ServiceProviderViewModel provider;
            try
            {
                provider = _serviceProviderService.Get(providerId);
                if (_categoryService.Get(categoryId) == null)
                {
                    throw new Exception("Wrong category");
                }

            }
            catch (Exception exception)
            {

                return new HttpStatusCodeResult(400, exception.Message);
            }

            if (provider.Services.Count(service => service.Category.Id == categoryId) == 0x0)
            {
                return new HttpStatusCodeResult(400, "This provider doesn't has services in this category");
            }
            var shortServices =
                provider.Services.Where(item => item.Category.Id == categoryId)
                    .Select(item => new ServiceShortViewModel {Id = item.Id, Title = item.Title});
            return Json(new {services = JsonConvert.SerializeObject(shortServices)}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public ActionResult MakeOrder(OrderViewModel order)
        {
            if (order == null)
            {
                return new HttpStatusCodeResult(400, "Null order");
            }
            try
            {
                order.ClientId = User.Identity.GetUserId();
                _orderService.Add(order);
            }
            catch (AggregateException aggregateException)
            {
                var builder = new StringBuilder();
                foreach (var exception in aggregateException.InnerExceptions)
                {
                    builder.Append(exception.Message);
                }
                return new HttpStatusCodeResult(400, builder.ToString());
            }
            catch (Exception exception)
            {
                return new HttpStatusCodeResult(400, exception.Message);
            }
            return new HttpStatusCodeResult(200, "");
        }

        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult ClientOrders(int? page,string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSort = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.RegionSort = sortOrder == "Region" ? "Region_desc" : "Region";
            ViewBag.ServiceTypeSort = sortOrder == "ServiceType" ? "ServiceType_desc" : "ServiceType";
            ViewBag.ProviderNameSort = sortOrder == "ProviderName" ? "ProviderName_desc" : "ProviderName";

            var clientOrders = _orderService.FindClientOrders(User.Identity.GetUserId());
            switch (sortOrder)
            {
                case "Date":
                    clientOrders = clientOrders.OrderBy(key => key.Date);
                    break;
                case "Date_desc":
                    clientOrders = clientOrders.OrderByDescending(key => key.Date);
                    break;
                case "Region":
                    clientOrders = clientOrders.OrderBy(key => key.Region.Title);
                    break;
                case "Region_desc":
                    clientOrders = clientOrders.OrderByDescending(key => key.Region.Title);
                    break;
                case "ServiceType":
                    clientOrders = clientOrders.OrderBy(key => key.ServiceType.Title);
                    break;
                case "ServiceType_desc":
                    clientOrders = clientOrders.OrderByDescending(key => key.ServiceType.Title);
                    break;
                case "ProviderName":
                    clientOrders = clientOrders.OrderBy(key => key.ProviderName);
                    break;
                case "ProviderName_desc":
                    clientOrders = clientOrders.OrderByDescending(key => key.ProviderName);
                    break;
            }
            var pageSize = 5;
            var pageNumber = (page ?? 1);
            return View(clientOrders.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [Authorize(Roles = "service provider")]
        public ActionResult ProviderOrders(int? page, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSort = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.RegionSort = sortOrder == "Region" ? "Region_desc" : "Region";
            ViewBag.ServiceTypeSort = sortOrder == "ServiceType" ? "ServiceType_desc" : "ServiceType";
            ViewBag.ClientNameSort = sortOrder == "ClientName" ? "ClientName_desc" : "ClientName";

            var providerOrders = _orderService.FindProviderOrders(User.Identity.GetUserId());
            switch (sortOrder)
            {
                case "Date":
                    providerOrders = providerOrders.OrderBy(key => key.Date);
                    break;
                case "Date_desc":
                    providerOrders = providerOrders.OrderByDescending(key => key.Date);
                    break;
                case "Region":
                    providerOrders = providerOrders.OrderBy(key => key.Region.Title);
                    break;
                case "Region_desc":
                    providerOrders = providerOrders.OrderByDescending(key => key.Region.Title);
                    break;
                case "ServiceType":
                    providerOrders = providerOrders.OrderBy(key => key.ServiceType.Title);
                    break;
                case "ServiceType_desc":
                    providerOrders = providerOrders.OrderByDescending(key => key.ServiceType.Title);
                    break;
                case "ClientName":
                    providerOrders = providerOrders.OrderBy(key => key.ClientName);
                    break;
                case "ClientName_desc":
                    providerOrders = providerOrders.OrderByDescending(key => key.ClientName);
                    break;
            }
            var pageSize = 5;
            var pageNumber = (page ?? 1);
            return View(providerOrders.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [Authorize(Roles = "client")]
        public ActionResult GetFreeTime(DateTime date, string providerId)
        {
            try
            {
                return Json(new { freeHours = JsonConvert.SerializeObject(_orderService.GetProviderFreeTime(date, providerId)) }, JsonRequestBehavior.AllowGet); 
            }
            catch (Exception exception)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }
        }
    }
}