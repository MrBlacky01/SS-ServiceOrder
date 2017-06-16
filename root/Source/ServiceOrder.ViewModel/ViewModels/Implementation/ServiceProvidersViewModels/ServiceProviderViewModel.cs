using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using ServiceOrder.ViewModel.ViewModels.Implementation.AlbumViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.PhotoViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.RegionViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels
{
    public class ServiceProviderViewModel
    {
        public string Id { get; set; }

        [AllowHtml]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [AllowHtml]
        [DataType(DataType.Text)]
        [StringLength(2000, ErrorMessage = "The {0} must be at least {2} characters long.",MinimumLength = 1)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Services")]
        public List<ServiceTypeViewModel> Services { get; set; }
        
        [Display(Name = "Regions")]       
        public List<RegionEntityViewModel> Regions { get; set; }

        [Display(Name = "Albums")]
        public List<ShortAlbumViewModel> Albums { get; set; }

        public PhotoViewModel Avatar { get; set; }

        [AllowHtml]
        [DataType(DataType.Text)]
        [Display(Name = "Short Description")]
        public string ShortDescription {
            get
            {
                if (Description == null)
                    return null;
                if (Description.Length < 200)
                    return Description;
                var i = 200;
                Regex regex = new Regex("[\\. \n,!?);]");
                while (i < Description.Length && !regex.IsMatch(Description[i].ToString()))
                {
                    i++;
                }
                return Description.Substring(0, i) + "...";
            } 
        }
    }

    public class ServiceShortViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
    }
}
