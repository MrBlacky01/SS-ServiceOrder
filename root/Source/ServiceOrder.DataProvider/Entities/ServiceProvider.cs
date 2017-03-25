using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.DataProvider.Entities
{
    public class ServiceProvider : Entity
    {
        public string Description { get; set; }
        public string WorkingTime { get; set; }

        [Required]
        public int UserId { get; set; }

        public User ProviderUser { get; set; }

        public List<Region> ProviderRegions { get; set; }
        public List<ServiceType> ProviderServiceTypes { get; set; }
        public List<Photo> ProviderPhotos { get; set; }
    }
}
