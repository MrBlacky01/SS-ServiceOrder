using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation.AlbumViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.PhotoViewModels;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class AlbumService : IAlbumService
    {
        private IUnitOfWork DataBase { get; set; }
        private IMapper _mapper;

        public AlbumService(IUnitOfWork dataBase, IMapper mapper)
        {
            DataBase = dataBase;
            _mapper = mapper;
        }


        public void Add(AlbumViewModel item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int? id)
        {
            if(id == null) return;
            DataBase.Albums.Delete(id);
            DataBase.Save();
        }

        public void Update(AlbumViewModel item)
        {
            DataBase.Albums.Update(_mapper.Map<AlbumViewModel,Album>(item));
            DataBase.Save();
        }

        public AlbumViewModel Get(int? id)
        {
            if (id == null) return null;
            var album = DataBase.Albums.Get((int) id);
            if (album == null) return null;
            return _mapper.Map<Album, AlbumViewModel>(album);
        }

        public IEnumerable<AlbumViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }

        public void AddPhoto(int albumId, PhotoViewModel photo)
        {
            var album = FindAlbum(albumId);
            var mapedPhoto = _mapper.Map<PhotoViewModel, Photo>(photo);
            album.AlbumPhotos.Add(mapedPhoto);
            DataBase.Albums.Update(album);
            DataBase.Save();
        }

        public void AddPhotosList(int albumId, List<PhotoViewModel> photos)
        {
            var album = FindAlbum(albumId);
            var mapedPhotos = _mapper.Map<IEnumerable<PhotoViewModel>, IEnumerable<Photo>>(photos);
            album.AlbumPhotos.AddRange(mapedPhotos);
            DataBase.Albums.Update(album);
            DataBase.Save();
        }

        public IEnumerable<AlbumViewModel> GetProviderAlbums(string providerId)
        {
            var albums = DataBase.Albums.Find(src => src.Provider.UserId == providerId);
            return _mapper.Map<IEnumerable<Album>, IEnumerable<AlbumViewModel>>(albums);
        }

        public void Add(ShortAlbumViewModel item)
        {
            if (DataBase.Albums.Find(src => src.Provider.UserId == item.ServiceProviderId)
                .All(src => src.Title != item.Title))
            {
                DataBase.Albums.Create(_mapper.Map<ShortAlbumViewModel,Album>(item));
                DataBase.Save();
            }
            else
            {
                throw new Exception("This provider has album with such name");
            }
        }

        private Album FindAlbum(int albumId)
        {
            var album = DataBase.Albums.Find(src => src.Id == albumId).FirstOrDefault();
            if (album == null)
            {
                throw new Exception("No such album");
            }
            else
            {
                return album;
            }
        }
    }
}
