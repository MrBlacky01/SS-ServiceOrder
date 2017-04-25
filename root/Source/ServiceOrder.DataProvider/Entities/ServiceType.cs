using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.DataProvider.Entities
{
    public class ServiceType : Entity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        
        public ServiceCategory Category { get; set; }
        public List<ServiceProvider> ProvidersOfService { get; set; }
    }
}
