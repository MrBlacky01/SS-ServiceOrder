using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ServiceOrder.ViewModel.ViewModels.Implementation.PhotoViewModels;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.AlbumViewModels
{
    public class AlbumViewModel
    {
        public int Id { get; set; }

        [AllowHtml]
        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        public string ServiceProviderId { get; set; }

        public List<PhotoViewModel> AlbumPhotos { get; set; } 
    }
}
