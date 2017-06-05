using System.Web.Mvc;
using ServiceOrder.Logic.Services;
using ServiceOrder.ViewModel.ViewModels.Implementation.RegionViewModels;

namespace ServiceOrder.WebSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class RegionController : Controller
    {
        private IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        // GET: Region
        public ActionResult Index()
        {
            return View(_regionService.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RegionEntityViewModel model)
        {
            if (ModelState.IsValid)
            {
                _regionService.Add(model);
            }          
            return View("Index",_regionService.GetAll()); 
        }

        public ActionResult Edit(int? id)
        {
            return View(_regionService.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(RegionEntityViewModel model)
        {
            if (ModelState.IsValid)
            {
                _regionService.Update(model);
            }
            return View("Index", _regionService.GetAll());
        }

        public ActionResult Delete(int? id)
        {
            _regionService.Delete(id);
            return View("Index", _regionService.GetAll());
        }

    }
}