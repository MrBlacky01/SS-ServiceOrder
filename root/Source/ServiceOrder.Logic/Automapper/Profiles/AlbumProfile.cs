using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.ViewModel.ViewModels.Implementation.AlbumViewModels;

namespace ServiceOrder.Logic.Automapper.Profiles
{
    public class AlbumProfile:Profile
    {
        public AlbumProfile()
        {
            CreateMap<AlbumViewModel, Album>()
                .ForMember(desc => desc.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(desc => desc.Provider,
                    opt => opt.MapFrom(src => new ServiceProvider() {UserId = src.ServiceProviderId}))
                .ForMember(desc => desc.AlbumPhotos, opt => opt.MapFrom(src => src.AlbumPhotos))
                .ReverseMap()
                .ForMember(desc => desc.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(desc => desc.AlbumPhotos, opt => opt.MapFrom(src => src.AlbumPhotos))
                .ForMember(dest => dest.ServiceProviderId, conf => conf.MapFrom(src => src.Provider.UserId));
            CreateMap<ShortAlbumViewModel,Album>()
                .ForMember(desc => desc.Provider,
                    opt => opt.MapFrom(src => new ServiceProvider() { UserId = src.ServiceProviderId }))
                .ReverseMap()
                .ForMember(dest => dest.ServiceProviderId, conf => conf.MapFrom(src => src.Provider.UserId));
        }
    }
}
