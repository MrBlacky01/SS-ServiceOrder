using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceOrder.ViewModel.ViewModels.Implementation.RegionViewModels;

namespace ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels
{
    public class ServiceProviderRegionsViewModel
    {
        public List<ProviderRegion> providerRegions;
        public List<RegionEntityViewModel> allRegions;
    }
}
