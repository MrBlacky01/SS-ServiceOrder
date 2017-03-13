using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.DataProvider.Entities
{
    public class Region : Entity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public List<ServiceProvider> ProvidersInRegion { get; set; }
    }
}
