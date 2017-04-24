using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels
{
    public class ServiceProviderViewModel
    {
        public string Id { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Discription")]
        public string Description { get; set; }

        [Display(Name = "Services")]
        public List<Service> Services { get; set; }
        
        [Display(Name = "Services")]       
        public List<ProviderRegion> Regions { get; set; }
    }

    public class ProviderRegion
    {
        public int Id { get; set; }
        
        [Display(Name = "Region")]
        public string Title { get; set; }
    }

    public class Service
    {
        public int Id { get; set; }
        [Display(Name = "Service")]
        public string Title { get; set; }
        public int ServiceCategoryId { get; set; }
    }
}
