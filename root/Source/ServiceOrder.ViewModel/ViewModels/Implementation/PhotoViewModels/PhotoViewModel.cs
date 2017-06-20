using System.ComponentModel.DataAnnotations;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.PhotoViewModels
{
    public class PhotoViewModel :ShortPhotoViewModel
    {
        [Required]
        public byte[] PhotoImage { get; set; }
    }
}
