using System.Collections.Generic;
using ServiceOrder.ViewModel.ViewModels.Implementation.AlbumViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.PhotoViewModels;

namespace ServiceOrder.Logic.Services
{
    public interface IAlbumService : IService<AlbumViewModel,int?>
    {
        void AddPhoto(int albumId, PhotoViewModel photo);
        void AddPhotosList(int albumId, List<PhotoViewModel> photos);
        IEnumerable<AlbumViewModel> GetProviderAlbums(string providerId);
        void Add( ShortAlbumViewModel item);
    }
}
