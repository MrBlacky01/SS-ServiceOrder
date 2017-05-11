    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ServiceOrder.ViewModel.ViewModels.Implementation.RegionViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.Order
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            Categories = new List<ServiceCategoryEntityViewModel>();
            Regions = new List<RegionEntityViewModel>();
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        public string ClientId { get; set; }
        [DataType(DataType.Time)]
        public DateTime BeginTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public int RegionId { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceProviderId { get; set; }

        public  string ProviderName { get; set; }
        public string ClientName { get; set; }
        public List<ServiceCategoryEntityViewModel> Categories { get; set; }
        public List<RegionEntityViewModel> Regions { get; set; }
        public RegionEntityViewModel Region { get; set; }
        public ServiceTypeViewModel ServiceType { get; set; }
    }
}
