using System;
using System.Collections.Generic;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork DataBase { get; set; }
        private IMapper _mapper;

        public CategoryService(IUnitOfWork dataBase, IMapper mapper)
        {
            DataBase = dataBase;
            _mapper = mapper;
        }

        public void Add(ServiceCategoryEntityViewModel categoryEntity)
        {
            DataBase.ServiceCategories.Create(_mapper.Map<ServiceCategoryEntityViewModel, ServiceCategory>(categoryEntity));
            DataBase.Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new Exception("Wrong category Id");
            DataBase.ServiceCategories.Delete((int)id);
            DataBase.Save();
        }

        public void Update(ServiceCategoryEntityViewModel categoryEntity)
        {
            DataBase.ServiceCategories.Update(_mapper.Map<ServiceCategoryEntityViewModel, ServiceCategory>(categoryEntity));
            DataBase.Save();
        }

        public ServiceCategoryEntityViewModel Get(int? id)
        {
            if (id == null)
                throw new Exception("Wrong category Id");
            var category = DataBase.ServiceCategories.Get((int)id);
           
            return _mapper.Map<ServiceCategory, ServiceCategoryEntityViewModel>(category);
        }

        public IEnumerable<ServiceCategoryEntityViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<ServiceCategory>, List<ServiceCategoryEntityViewModel>>(
                    DataBase.ServiceCategories.GetAll());
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
