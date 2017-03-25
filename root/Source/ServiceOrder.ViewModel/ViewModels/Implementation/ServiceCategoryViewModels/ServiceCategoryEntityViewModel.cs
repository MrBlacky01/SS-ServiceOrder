using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels
{
    public class ServiceCategoryEntityViewModel : TemplateEntityViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Категория сервиса")]
        public string Title { get; set; }
    }
}
