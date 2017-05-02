using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
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

        public OrderController(IOrderService orderService, IServiceProviderService serviceProviderService,ICategoryService categoryService)
        {
            _orderService = orderService;
            _serviceProviderService = serviceProviderService;
            _categoryService = categoryService;
        }

        // GET: Order
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
                return new HttpStatusCodeResult(400,"This provider doesn't has services in this category");
            }
            var shortServices =
                provider.Services.Where(item => item.Category.Id == categoryId)
                    .Select(item => new ServiceShortViewModel {Id = item.Id, Title = item.Title});
            return Json(new {services = JsonConvert.SerializeObject(shortServices)}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
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
        public ActionResult ClientOrders()
        {
            return View(_orderService.FindClientOrders(User.Identity.GetUserId()));
        }

        [HttpGet]
        public ActionResult ProviderOrders()
        {
            return View(_orderService.FindProviderOrders(User.Identity.GetUserId()));
        }
    }
}