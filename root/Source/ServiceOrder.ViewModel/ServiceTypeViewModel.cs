using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServiceOrder.ViewModel
{
    public class ServiceTypeViewModel 
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Service Type")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "CategoryId")]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryTitle { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
