using ServiceOrder.ViewModel.ViewModels.Implementation.PhotoViewModels;

namespace ServiceOrder.Logic.Services
{
    public interface IPhotoService
    {
        PhotoViewModel GetPhoto (int? id);
        void Delete(int? id, string userId);
    }
}
