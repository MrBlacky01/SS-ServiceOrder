using System.Collections.Generic;
using System.Web;
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
        IEnumerable<ServiceProviderViewModel> FilterGetProviders (int? regionId, int? categoryId,int? serviceId);
        IEnumerable<ServiceProviderViewModel> GetAllWithServiceAndRegion();
        string ChangeDescription(string userId, string newDescription);
        string ChangeAvatarPhoto(string userId, HttpPostedFileBase file);
    }
}
