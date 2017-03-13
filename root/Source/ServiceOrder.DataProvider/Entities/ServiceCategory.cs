using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.DataProvider.Entities
{
    public class ServiceCategory: Entity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public List<ServiceType> ServicesInCategory { get; set; }
    }
}
