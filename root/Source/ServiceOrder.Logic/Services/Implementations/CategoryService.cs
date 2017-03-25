using System;
using System.Collections.Generic;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork DataBase { get; set; }

        public CategoryService(IUnitOfWork dataBase)
        {
            DataBase = dataBase;
        }

        public void Add(ServiceCategoryEntityViewModel categoryEntity)
        {

            Mapper.Initialize(cfg => cfg.CreateMap<ServiceCategoryEntityViewModel, ServiceCategory>());
            var model = Mapper.Map<ServiceCategoryEntityViewModel, ServiceCategory>(categoryEntity);
            DataBase.ServiceCategories.Create(model);
            DataBase.Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new Exception("Не установлено id категории");
            DataBase.ServiceCategories.Delete((int)id);
            DataBase.Save();
        }

        public void Update(ServiceCategoryEntityViewModel categoryEntity)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ServiceCategoryEntityViewModel, ServiceCategory>());
            var model = Mapper.Map<ServiceCategoryEntityViewModel, ServiceCategory>(categoryEntity);
            DataBase.ServiceCategories.Update(model);
            DataBase.Save();
        }

        public ServiceCategoryEntityViewModel Get(int? id)
        {
            if (id == null)
                throw new Exception("Не установлено id категории");
            var category = DataBase.ServiceCategories.Get((int)id);
           
            
            Mapper.Initialize(cfg => cfg.CreateMap<ServiceCategory, ServiceCategoryEntityViewModel>());
            return Mapper.Map<ServiceCategory, ServiceCategoryEntityViewModel>(category);
        }

        public IEnumerable<ServiceCategoryEntityViewModel> GetAll()
        {
            Mapper.Initialize(config => config.CreateMap<ServiceCategory,ServiceCategoryEntityViewModel>());
            return
                Mapper.Map<IEnumerable<ServiceCategory>, List<ServiceCategoryEntityViewModel>>(
                    DataBase.ServiceCategories.GetAll());
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
