using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels
{
    public class ServiceProviderCategoryViewModel
    {
        public List<ServiceCategoryEntityViewModel> Categories { get; set; }
    }
}
