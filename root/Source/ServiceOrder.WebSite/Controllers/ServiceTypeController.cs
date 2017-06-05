using System.Collections.Generic;
using System.Web.Mvc;
using ServiceOrder.Logic.Services;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceCategoryViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.WebSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class ServiceTypeController : Controller
    {
        private IServiceTypeService _serviceTypeService;
        private ICategoryService _categoryService;

        public ServiceTypeController(IServiceTypeService serviceTypeService, ICategoryService categoryService)
        {
            _serviceTypeService = serviceTypeService;
            _categoryService = categoryService;
        }

        // GET: ServiceType
        public ActionResult Index()
        {
            IEnumerable<ServiceTypeViewModel> types = _serviceTypeService.GetAll();
            return View(types);
        }

        public ActionResult Create()
        {
            var model = new ServiceTypeViewModel {Categories = GetSelectListItems(_categoryService.GetAll()) };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ServiceTypeViewModel model)
        {
            if (ModelState.IsValid)
            {   
                _serviceTypeService.Add(model);
                return View("Index", _serviceTypeService.GetAll());
            }
            return View("Index", _serviceTypeService.GetAll());
        }

        public ActionResult Edit(int? id)
        {
            var model = _serviceTypeService.Get(id);
            model.Categories = GetSelectListItems(_categoryService.GetAll());
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ServiceTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                _serviceTypeService.Update(model);
                return View("Index", _serviceTypeService.GetAll());
            }
            return View("Index", _serviceTypeService.GetAll());
        }

        public ActionResult Delete(int? id)
        {
            _serviceTypeService.Delete(id);
            return View("Index", _serviceTypeService.GetAll());
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<ServiceCategoryEntityViewModel> elements)
        {
            var selectList = new List<SelectListItem>();
                 
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Title
                });
            }

            return selectList;
        }
    }
}