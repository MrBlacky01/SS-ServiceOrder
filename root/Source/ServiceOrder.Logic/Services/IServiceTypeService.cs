using System.Collections.Generic;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.Logic.Services
{
    public interface IServiceTypeService : IService<ServiceTypeViewModel,int?>
    {
        IEnumerable<ServiceTypeViewModel> FindServicesInCategory(int? category);
    }
}
