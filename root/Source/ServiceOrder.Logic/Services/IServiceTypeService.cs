using System;
using System.Collections.Generic;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.Logic.Services
{
    public interface IServiceTypeService : IService<ServiceTypeViewModel,int?>
    {
        IEnumerable<ServiceTypeViewModel> FindServicesInCategory(int? category);
    }
}
