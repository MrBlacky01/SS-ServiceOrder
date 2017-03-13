using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
