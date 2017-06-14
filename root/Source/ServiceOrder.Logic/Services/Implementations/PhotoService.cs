using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation.PhotoViewModels;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class PhotoService:IPhotoService
    {
        private IUnitOfWork DataBase { get; set; }
        private IMapper _mapper;
        public PhotoService(IUnitOfWork dataBase, IMapper mapper)
        {
            DataBase = dataBase;
            _mapper = mapper;
        }

        public PhotoViewModel GetPhoto(int? id)
        {
            if (id == null) return null;
            var photo = DataBase.Photos.Get((int)id);
            return photo == null ? null : _mapper.Map<Photo,PhotoViewModel>(photo);
        }

        public void Delete(int? id,string userId)
        {
            if(id == null) return;
            if (!CheckOwner((int) id, userId))
            {
                throw new UnauthorizedAccessException("This user can not remove photo");
            }
            var album = DataBase.Albums.Find(src => src.AlbumPhotos.Any(photo => photo.Id ==  id)).FirstOrDefault();
            if (album != null)
            {
                RemoveFromAlbum((int)id, album);
            }           
        }

        private bool CheckOwner(int photoId, string userId)
        { 
            return DataBase.Photos.Find(src => src.Id == photoId && src.PhotoAlbum.Provider.UserId == userId ).Any();
        }

        private void RemoveFromAlbum(int photoId, Album album)
        {
            album.AlbumPhotos.RemoveAt(album.AlbumPhotos.FindIndex(photo => photo.Id == photoId));
            DataBase.Albums.Update(album);
            DataBase.Photos.Delete(photoId);
            DataBase.Save();
        }
    }
}
