using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class ServiceProviderService : IServiceProviderService
    {
        private IUnitOfWork DataBase { get; set; }

        public ServiceProviderService(IUnitOfWork dataBase)
        {
            DataBase = dataBase;
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
            Mapper.Initialize(config => config.CreateMap<ServiceProviderViewModel, ServiceProvider>());             
            var model = Mapper.Map<ServiceProviderViewModel, ServiceProvider>(item);
            DataBase.ServiceProviders.Update(model);
            DataBase.Save();
        }

        public ServiceProviderViewModel Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception("Не установлено id категории");
            var item = DataBase.ServiceProviders.Get(id);

            Mapper.Initialize(config => config.CreateMap<ServiceProvider, ServiceProviderViewModel>());
                
            return Mapper.Map<ServiceProvider, ServiceProviderViewModel>(item);
        }

        public IEnumerable<ServiceProviderViewModel> GetAll()
        {

            Mapper.Initialize(config => config.CreateMap<ServiceProvider, ServiceProviderViewModel>()
            .ForMember(dest => dest.Name, opt=> opt.MapFrom(src => src.ProviderUser.UserName)));
            var providers = DataBase.ServiceProviders.GetAll();
            var serviceProviders = providers as IList<ServiceProvider> ?? providers.ToList();
            var viewModel = Mapper.Map<IEnumerable<ServiceProvider>, List<ServiceProviderViewModel>>(serviceProviders);

            Mapper.Initialize(config => config.CreateMap<ServiceType, Service>());
            for (int i = 0; i < viewModel.Count; i++)
            {
                viewModel[i].Services = Mapper.Map<List<ServiceType>, List<Service>>(serviceProviders.ElementAt(i).ProviderServiceTypes);
            }
               
            
            return viewModel;
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
