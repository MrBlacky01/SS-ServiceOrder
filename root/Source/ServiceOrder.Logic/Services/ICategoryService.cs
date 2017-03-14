using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceOrder.ViewModel;

namespace ServiceOrder.Logic.Services
{
    public interface ICategoryService
    {
        void AddCategory(ServiceCategoryViewModel category);
        void DeleteCategory(int? id);
        void UpdateCategory(ServiceCategoryViewModel category);
        ServiceCategoryViewModel GetCategory(int? id);
        IEnumerable<ServiceCategoryViewModel> GetCategories();
        void Dispose();
    }
}
