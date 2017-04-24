using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels;

namespace ServiceOrder.Logic.Automapper.Profiles
{
    public class ServiceProviderProfile:Profile
    {
        public ServiceProviderProfile()
        {
            CreateMap<ServiceProvider, ServiceProviderViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProviderUser.UserName))
            .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.ProviderServiceTypes))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Regions, opt => opt.MapFrom(src => src.ProviderRegions))
            .ReverseMap();
        }
    }
}
