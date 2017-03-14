using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork DataBase { get; set; }

        public CategoryService(IUnitOfWork dataBase)
        {
            DataBase = dataBase;
        }

        public void AddCategory(ServiceCategoryViewModel category)
        {

            Mapper.Initialize(cfg => cfg.CreateMap<ServiceCategoryViewModel, ServiceCategory>());
            var model = Mapper.Map<ServiceCategoryViewModel, ServiceCategory>(category);
            DataBase.ServiceCategories.Create(model);
            DataBase.Save();
        }

        public void DeleteCategory(int? id)
        {
            if (id == null)
                throw new Exception("Не установлено id категории");
            DataBase.ServiceCategories.Delete((int)id);
            DataBase.Save();
        }

        public void UpdateCategory(ServiceCategoryViewModel category)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ServiceCategoryViewModel, ServiceCategory>());
            var model = Mapper.Map<ServiceCategoryViewModel, ServiceCategory>(category);
            DataBase.ServiceCategories.Update(model);
            DataBase.Save();
        }

        public ServiceCategoryViewModel GetCategory(int? id)
        {
            if (id == null)
                throw new Exception("Не установлено id категории");
            var category = DataBase.ServiceCategories.Get((int)id);
           
            
            Mapper.Initialize(cfg => cfg.CreateMap<ServiceCategory, ServiceCategoryViewModel>());
            return Mapper.Map<ServiceCategory, ServiceCategoryViewModel>(category);
        }

        public IEnumerable<ServiceCategoryViewModel> GetCategories()
        {
            Mapper.Initialize(config => config.CreateMap<ServiceCategory,ServiceCategoryViewModel>());
            return
                Mapper.Map<IEnumerable<ServiceCategory>, List<ServiceCategoryViewModel>>(
                    DataBase.ServiceCategories.GetAll());
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
