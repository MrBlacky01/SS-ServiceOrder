using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.ViewModel.ViewModels.Implementation
{
    public class ResultMessageViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Message { get; set; }
    }
}
