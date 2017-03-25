using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.RegionViewModels
{
    public class RegionEntityViewModel : TemplateEntityViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Text)]
        [Display(Name = "Регион действия сервиса")]
        public string Title { get; set; }
    }
}
