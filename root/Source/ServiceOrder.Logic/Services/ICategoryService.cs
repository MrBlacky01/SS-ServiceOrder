using ServiceOrder.ViewModel.ViewModels.Implementation;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels;

namespace ServiceOrder.Logic.Services
{
    public interface ICategoryService : IService<ServiceCategoryEntityViewModel>
    {
    }
}
