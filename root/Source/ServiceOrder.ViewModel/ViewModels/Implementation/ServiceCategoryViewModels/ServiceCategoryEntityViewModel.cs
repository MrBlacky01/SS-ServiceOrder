using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels
{
    public class ServiceCategoryEntityViewModel : TemplateEntityViewModel
    {
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Category")]
        [AllowHtml]
        public string Title { get; set; }
    }
}
