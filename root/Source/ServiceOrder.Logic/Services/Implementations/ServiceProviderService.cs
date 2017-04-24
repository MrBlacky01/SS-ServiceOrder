using System;
using System.Collections.Generic;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels;

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
    }
}
