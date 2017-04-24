using System;
using System.Collections.Generic;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.Order
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            Regions = new Dictionary<int, string>();
            Services = new Dictionary<int, string>();
        }

        public string ClientId { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RegionId { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceProviderId { get; set; }

        public Dictionary<int,string> Regions { get; set; }
        public Dictionary<int,string> Services { get; set; }
        public  string ProviderName { get; set; }
    }
}
