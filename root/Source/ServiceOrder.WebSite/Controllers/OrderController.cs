using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public OrderController(IOrderService orderService, IServiceProviderService serviceProviderService)
        {
            _orderService = orderService;
            _serviceProviderService = serviceProviderService;
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
                return View("MessageView", new ResultMessageViewModel { Message = exception.Message });
                throw;
            }

            if (provider == null)
            {
                return View("MessageView",new ResultMessageViewModel {Message = "There is not such provider"});
            }
                
            var model = new OrderViewModel();
            foreach (var region in provider.Regions)
            {
                model.Regions.Add(region.Id,region.Title);
            }
            foreach (var service in provider.Services )
            {
                model.Services.Add(service.Id,service.Title);
            }
            model.ServiceProviderId = provider.Id;
            model.ProviderName = provider.Name;

            return View(model);
        }
    }
}