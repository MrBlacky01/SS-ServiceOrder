using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using ServiceOrder.Logic.Services;
using ServiceOrder.ViewModel.ViewModels.Implementation;
using ServiceOrder.ViewModel.ViewModels.Implementation.AlbumViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.PhotoViewModels;

namespace ServiceOrder.WebSite.Controllers
{
    public class AlbumController : Controller
    {
        private IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [Authorize(Roles = "service provider")]
        public ActionResult Add(int albumId)
        {
            var album = _albumService.Get(albumId);
            if (album == null)
            {
                return View("MessageView", new ResultMessageViewModel() {Message = "There's no such album"});
            }
            return User.Identity.GetUserId() != album.ServiceProviderId
                ? View("MessageView", new ResultMessageViewModel() {Message = "You don't has access to this album"})
                : View(album);
        }

        [HttpPost]
        [Authorize(Roles = "service provider")]
        public JsonResult Upload(int albumId)
        {
            var files = new List<AbstractDataUploadResult>();

            _albumService.UploadAndShowResults(HttpContext, files, albumId);

            bool isEmpty = !files.Any();
            return isEmpty ? Json("Error") : Json(new {files = files});
        }

        [Authorize(Roles = "service provider")]
        public ActionResult Edit(int albumId)
        {
            var album = _albumService.Get(albumId);
            if (album == null)
            {
                return View("MessageView", new ResultMessageViewModel() {Message = "There's no such album"});
            }
            return User.Identity.GetUserId() != album.ServiceProviderId
                ? View("MessageView", new ResultMessageViewModel() {Message = "You don't has access to this album"})
                : View(album);
        }

        public JsonResult GetFileList(int albumId)
        {
            var list = _albumService.GetPhotosList(albumId);
            return Json(new {files = list}, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "service provider")]
        public ActionResult ChangeAlbumName(AlbumViewModel newAlbum)
        {
            var oldAlbum = _albumService.Get(newAlbum.Id);
            if (oldAlbum == null)
            {
                return View("MessageView", new ResultMessageViewModel() {Message = "There's no such album"});
            }
            if (User.Identity.GetUserId() != oldAlbum.ServiceProviderId)
            {
                return View("MessageView", new ResultMessageViewModel() {Message = "You don't has access to this album"});
            }
            oldAlbum.Title = newAlbum.Title;
            _albumService.Update(oldAlbum);
            return RedirectToAction("ManageAlbum", "ServiceProviders", new {albumId = oldAlbum.Id});
        }
    }
}