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

        public IEnumerable<ServiceProviderViewModel> GetAllWithServiceAndRegion()
        {

            return
                _mapper.Map<IEnumerable<ServiceProvider>, IEnumerable<ServiceProviderViewModel>>(
                    DataBase.ServiceProviders.Find(provider => provider.ProviderServiceTypes.Any() && provider.ProviderRegions.Any()));
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

        public IEnumerable<ServiceProviderViewModel> FilterGetProviders(int? regionId, int? categoryId, int? serviceId)
        {
            IEnumerable<ServiceProvider> providerList = new List<ServiceProvider>();
            if (regionId != null && !CheckRegion(regionId.Value))
            {
                regionId = null;
            }
            if (serviceId != null && !CheckServiceType(serviceId.Value))
            {
                serviceId = null;
            }
            if (categoryId != null && !CheckCategory(categoryId.Value))
            {
                categoryId = null;
            }
            if (regionId == null && categoryId == null && serviceId == null)
            {
                return GetAllWithServiceAndRegion();
            }

            

            if (regionId != null && categoryId != null &&serviceId != null)
            {
                providerList = DataBase.ServiceProviders.Find(provider => provider.ProviderServiceTypes.Any(src => src.Id == serviceId.Value)
                                                                        && provider.ProviderRegions.Any(src => src.Id == regionId.Value) );
            }
            else
            {
                if (regionId != null && categoryId != null)
                {
                    providerList = DataBase.ServiceProviders.Find(provider =>provider.ProviderRegions.Any(src => src.Id == regionId.Value)
                                                                           && provider.ProviderServiceTypes.Any(src => src.Category.Id == categoryId.Value));
                }
                else
                {
                    if (regionId != null)
                    {
                        providerList =
                            DataBase.ServiceProviders.Find(
                                provider => provider.ProviderRegions.Any(src => src.Id == regionId.Value));

                    }
                    else
                    {
                        if (categoryId != null && serviceId != null)
                        {
                            providerList =
                                DataBase.ServiceProviders.Find(
                                    provider => provider.ProviderServiceTypes.Any(src => src.Id == serviceId.Value));
                        }
                        else
                        {
                            if (categoryId != null)
                            {
                                providerList = DataBase.ServiceProviders.Find(provider => provider.ProviderServiceTypes.Any(src => src.Category.Id == categoryId.Value));
                            }
                        }
                    }
                }
            }
            return _mapper.Map<IEnumerable<ServiceProvider>, IEnumerable<ServiceProviderViewModel>>(providerList);
        }

        public string ChangeDescription(string userId, string newDescription)
        {
            var provider = DataBase.ServiceProviders.Get(userId);
            if (!CheckDescription(newDescription))
            {
                return "Description title must has at least 1 symbol and max length 2000";
            }
            provider.Description = newDescription;
            try
            {
                DataBase.ServiceProviders.Update(provider);
                DataBase.Save();
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            return String.Empty;
        }

        private bool CheckDescription(string description)
        {
            return description != null && description.Length <= 2000 && description.Length >=1;
        }

        private bool CheckRegion(int regionId)
        {
            return DataBase.Regions.Get(regionId) != null;
        }

        private bool CheckCategory(int categoryId)
        {
            return DataBase.ServiceCategories.Get(categoryId) != null;
        }

        private bool CheckServiceType(int serviceId)
        {
            return DataBase.ServiceTypes.Get(serviceId) != null;
        }
    }
}
