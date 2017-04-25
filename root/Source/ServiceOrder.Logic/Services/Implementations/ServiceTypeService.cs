using System;
using System.Collections.Generic;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class ServiceTypeService : IServiceTypeService
    {
        private IUnitOfWork DataBase { get; set; }
        private IMapper _mapper;

        public ServiceTypeService(IUnitOfWork dataBase, IMapper mapper)
        {
            DataBase = dataBase;
            _mapper = mapper;
        }

        public void Add(ServiceTypeViewModel item)
        {
            DataBase.ServiceTypes.Create(_mapper.Map<ServiceTypeViewModel, ServiceType>(item));
            DataBase.Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new Exception("Wrong serviceType Id");
            DataBase.ServiceTypes.Delete((int)id);
            DataBase.Save();
        }

        public void Update(ServiceTypeViewModel item)
        {
            DataBase.ServiceTypes.Update(_mapper.Map<ServiceTypeViewModel, ServiceType>(item));
            DataBase.Save();
        }

        public ServiceTypeViewModel Get(int? id)
        {           
            if (id == null)
                throw new Exception("Wrong serviceType Id");
            var item = DataBase.ServiceTypes.Get((int)id);

            
            return _mapper.Map<ServiceType, ServiceTypeViewModel>(item);
        }

        public IEnumerable<ServiceTypeViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<ServiceType>, List<ServiceTypeViewModel>>(
                    DataBase.ServiceTypes.GetAll());
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }

        public IEnumerable<ServiceTypeViewModel> FindServicesInCategory(int? categoryId)
        {
            if (categoryId == null)
                throw new NullReferenceException("Category Id is Null");
            var category = DataBase.ServiceCategories.Get(categoryId.Value);
            if (category == null)
            {
                throw new Exception("Wrong category Id");
            }

            return _mapper.Map<IEnumerable<ServiceType>, IEnumerable<ServiceTypeViewModel>>(
                    DataBase.ServiceTypes.Find(src => src.Category == category));
        }
    }
}
