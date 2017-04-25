using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels
{
    public class ServiceTypeViewModel 
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Service Type")]
        public string Title { get; set; }

        public ServiceCategoryEntityViewModel Category { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
