using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ServiceOrder.Logic.Services;

namespace ServiceOrder.WebSite.Controllers
{
    public class PhotoController : Controller
    {
        private IPhotoService _photoService;
        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }
        public FileContentResult Get(int? photoId)
        {
            var photo = _photoService.GetPhoto(photoId);
            if (photo == null) return null;
            return File(Convert.FromBase64String(photo.PhotoImage),photo.ContentType,photo.FileName);
        }

        [Authorize(Roles = "service provider")]
        [HttpGet]
        public JsonResult Delete(int? photoId)
        {
            _photoService.Delete(photoId,User.Identity.GetUserId());
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}