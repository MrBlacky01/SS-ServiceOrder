using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ServiceOrder.Logic.Services;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels;

namespace ServiceOrder.WebSite.Controllers
{
    [Authorize(Roles = "admin")]
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
            try
            {
                _categoryService.Add(categoryEntity);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Can't add category.");
                return View(categoryEntity);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int? id)
        {
            _categoryService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult EditCategory(int? id)
        {
            return View(_categoryService.Get(id));
        }

        [HttpPost]
        public ActionResult EditCategory(ServiceCategoryEntityViewModel categoryEntity)
        {
            _categoryService.Update(categoryEntity);
            return RedirectToAction("Index");
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