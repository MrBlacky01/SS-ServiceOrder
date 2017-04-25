using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation.RegionViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class ServiceProviderService : IServiceProviderService
    {
        private IUnitOfWork DataBase { get; set; }
        private IMapper _mapper;

        public ServiceProviderService(IUnitOfWork dataBase,IMapper mapper)
        {
            DataBase = dataBase;
            _mapper = mapper;
        }

        public void Add(ServiceProviderViewModel item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(ServiceProviderViewModel item)
        {     
            var model = _mapper.Map<ServiceProviderViewModel, ServiceProvider>(item);
            DataBase.ServiceProviders.Update(model);
            DataBase.Save();
        }

        public ServiceProviderViewModel Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception("Wrong id parametr");
            var item = DataBase.ServiceProviders.Get(id);

            if (item == null)
            {
                throw new Exception("No such user");
            }
                
            return _mapper.Map<ServiceProvider, ServiceProviderViewModel>(item);
        }

        public IEnumerable<ServiceProviderViewModel> GetAll()
        {
            return
                _mapper.Map<IEnumerable<ServiceProvider>, IEnumerable<ServiceProviderViewModel>>(
                    DataBase.ServiceProviders.GetAll());
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }

        public void UpdateServices(List<ServiceTypeViewModel> services, ServiceProviderViewModel provider,ServiceCategoryEntityViewModel category)
        {
            var providerServicesFromAnotherCategory = provider.Services.Where(item => item.Category.Id != category.Id).ToList();
            providerServicesFromAnotherCategory.AddRange(services);
            provider.Services = providerServicesFromAnotherCategory;
            Update(provider);
        }

        public void DeleteRegion(RegionEntityViewModel region, ServiceProviderViewModel provider)
        {
            int index = provider.Regions.FindIndex(item => item.Id == region.Id);
            if (index == -1) return;
            provider.Regions.RemoveAt(index);
            Update(provider);
        }

        public void DeleteService(ServiceTypeViewModel service, ServiceProviderViewModel provider)
        {
            int index = provider.Services.FindIndex(item => item.Id == service.Id);
            if (index == -1) return;
            provider.Services.RemoveAt(index);
            Update(provider);
        }
    }
}
