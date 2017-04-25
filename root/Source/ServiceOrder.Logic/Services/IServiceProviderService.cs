using System.Collections.Generic;
using ServiceOrder.ViewModel.ViewModels.Implementation.RegionViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.Logic.Services
{
    public interface IServiceProviderService : IService<ServiceProviderViewModel,string>
    {
        void UpdateServices(List<ServiceTypeViewModel> services,ServiceProviderViewModel provider,ServiceCategoryEntityViewModel category);
        void DeleteRegion(RegionEntityViewModel region, ServiceProviderViewModel provider);
        void DeleteService(ServiceTypeViewModel service, ServiceProviderViewModel provider);

    }
}
