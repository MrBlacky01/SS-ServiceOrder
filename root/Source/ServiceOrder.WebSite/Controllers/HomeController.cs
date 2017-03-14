using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceOrder.Logic.Services;
using ServiceOrder.ViewModel;

namespace ServiceOrder.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private ICategoryService _categoryService;

        public HomeController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Category()
        {
            IEnumerable<ServiceCategoryViewModel> categories = _categoryService.GetCategories();
           
            return View(categories);
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(ServiceCategoryViewModel category)
        {
            _categoryService.AddCategory(category);
            return Content("<h2>Сатегория успешно добавлена</h2>");
        }

        public ActionResult DeleteCategory(int? id)
        {
            _categoryService.DeleteCategory(id);
            return Content("<h2>Категория успешно удалена</h2>");
        }

        public ActionResult EditCategory(int? id)
        {
            return View(_categoryService.GetCategory(id));
        }

        [HttpPost]
        public ActionResult EditCategory(ServiceCategoryViewModel category)
        {
            _categoryService.UpdateCategory(category);
            return Content("<h2>Категория успешно изменена</h2>");
        }
    }
}