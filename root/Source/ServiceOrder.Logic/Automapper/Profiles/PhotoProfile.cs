using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.ViewModel.ViewModels.Implementation.PhotoViewModels;

namespace ServiceOrder.Logic.Automapper.Profiles
{
    public class PhotoProfile :Profile
    {
        public PhotoProfile()
        {
            CreateMap<PhotoViewModel, Photo>()
                .ForMember(dest => dest.PhotoImage, conf => conf.MapFrom(src => src.PhotoImage))
                .ForMember(dest => dest.ContentType, conf => conf.MapFrom(src => src.ContentType))
                .ForMember(dest => dest.FileName, conf => conf.MapFrom(src => src.FileName))
                .ReverseMap()
                .ForMember(dest => dest.PhotoImage, conf => conf.MapFrom(src => src.PhotoImage))
                .ForMember(dest => dest.ContentType, conf => conf.MapFrom(src => src.ContentType))
                .ForMember(dest => dest.FileName, conf => conf.MapFrom(src => src.FileName));
        }
    }
}
