using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceOrder.Logic.Services;
using ServiceOrder.ViewModel.ViewModels.Implementation;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels;

namespace ServiceOrder.WebSite.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            IEnumerable<ServiceCategoryEntityViewModel> categories = _categoryService.GetAll();
            return View(categories);
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(ServiceCategoryEntityViewModel categoryEntity)
        {
            _categoryService.Add(categoryEntity);
            return View("MessageView", new ResultMessageViewModel() { Message = "Категория успешно добавлена" });
        }

        public ActionResult DeleteCategory(int? id)
        {
            _categoryService.Delete(id);
            return View("MessageView", new ResultMessageViewModel() { Message = "Категория успешно удалена" });
        }

        public ActionResult EditCategory(int? id)
        {
            return View(_categoryService.Get(id));
        }

        [HttpPost]
        public ActionResult EditCategory(ServiceCategoryEntityViewModel categoryEntity)
        {
            _categoryService.Update(categoryEntity);
            return View("MessageView", new ResultMessageViewModel() { Message = "Категория успешно изменена" });
        }

        public ActionResult DetailCategory(int? id)
        {
            return View(_categoryService.Get(id));
        }

        protected override void Dispose(bool disposing)
        {
            _categoryService.Dispose();
            base.Dispose(disposing);
        }
    }
}