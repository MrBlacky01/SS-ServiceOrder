using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceOrder.DataProvider.Entities
{
    public class ServiceProvider 
    {
        public string Description { get; set; }
        public string WorkingTime { get; set; }

        [Key]
        [ForeignKey("ProviderUser")]
        public string UserId { get; set; }

        public User ProviderUser { get; set; }

        public List<Region> ProviderRegions { get; set; }
        public List<ServiceType> ProviderServiceTypes { get; set; }
        public List<Photo> ProviderPhotos { get; set; }
    }
}
