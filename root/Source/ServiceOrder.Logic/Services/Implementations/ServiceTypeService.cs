using System;
using System.Collections.Generic;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class ServiceTypeService : IServiceTypeService
    {
        private IUnitOfWork DataBase { get; set; }

        public ServiceTypeService(IUnitOfWork dataBase)
        {
            DataBase = dataBase;
        }

        public void Add(ServiceTypeViewModel item)
        {
            Mapper.Initialize(config => config.CreateMap<ServiceTypeViewModel, ServiceType>()
                .ForMember(x => x.ServiceCategoryId, opt => opt.MapFrom(c => c.CategoryId))
                .ForSourceMember(x => x.CategoryTitle, opt => opt.Ignore()));
                
            var model = Mapper.Map<ServiceTypeViewModel, ServiceType>(item);
            DataBase.ServiceTypes.Create(model);
            DataBase.Save();

        }

        public void Delete(int? id)
        {

            if (id == null)
                throw new Exception("Не установлено id категории");
            DataBase.ServiceTypes.Delete((int)id);
            DataBase.Save();
        }

        public void Update(ServiceTypeViewModel item)
        {
            Mapper.Initialize(config => config.CreateMap<ServiceTypeViewModel, ServiceType>()
                .ForMember(x => x.ServiceCategoryId, opt => opt.MapFrom(c => c.CategoryId))
                .ForSourceMember(x => x.CategoryTitle, opt => opt.Ignore()));
            var model = Mapper.Map<ServiceTypeViewModel, ServiceType>(item);
            DataBase.ServiceTypes.Update(model);
            DataBase.Save();
        }

        public ServiceTypeViewModel Get(int? id)
        {           
            if (id == null)
                throw new Exception("Не установлено id категории");
            var item = DataBase.ServiceTypes.Get((int)id);

            Mapper.Initialize(config => config.CreateMap<ServiceType,ServiceTypeViewModel>()
            .ForMember(x => x.CategoryId, opt => opt.MapFrom(c => c.ServiceCategoryId))
            .ForMember(x =>x.CategoryTitle, opt => opt.MapFrom(c => c.Category.Title)));
            return Mapper.Map<ServiceType, ServiceTypeViewModel>(item);
        }

        public IEnumerable<ServiceTypeViewModel> GetAll()
        {
            Mapper.Initialize(config => config.CreateMap<ServiceType, ServiceTypeViewModel>()
            .ForMember(x => x.CategoryId, opt => opt.MapFrom(c => c.ServiceCategoryId))
            .ForMember(x => x.CategoryTitle, opt => opt.MapFrom(c => c.Category.Title)));
            return Mapper.Map<IEnumerable<ServiceType>, List<ServiceTypeViewModel>>(
                    DataBase.ServiceTypes.GetAll());
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
