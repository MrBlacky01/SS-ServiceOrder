using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.AlbumViewModels
{
    public class ShortAlbumViewModel
    {
        public int Id { get; set; }

        [AllowHtml]
        [Display(Name = "Title")]
        [Required]
        [StringLength(255,ErrorMessage = "The {0} must has maximum {1} characters ",MinimumLength = 1)]
        public string Title { get; set; }

        public string ServiceProviderId { get; set; }
    }
}
