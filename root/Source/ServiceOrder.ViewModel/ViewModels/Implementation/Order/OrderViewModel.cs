using System;
using System.Collections.Generic;
using ServiceOrder.ViewModel.ViewModels.Implementation.RegionViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.Order
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            Categories = new List<ServiceCategoryEntityViewModel>();
            Regions = new List<RegionEntityViewModel>();
        }
        public DateTime Date { get; set; }
        public string ClientId { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RegionId { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceProviderId { get; set; }

        public  string ProviderName { get; set; }
        public List<ServiceCategoryEntityViewModel> Categories { get; set; }
        public List<RegionEntityViewModel> Regions { get; set; }
    }
}
