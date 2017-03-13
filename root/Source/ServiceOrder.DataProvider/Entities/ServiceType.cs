using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.DataProvider.Entities
{
    public class ServiceType : Entity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public int ServiceCategoryId { get; set; }

        public ServiceCategory Category { get; set; }
    }
}
