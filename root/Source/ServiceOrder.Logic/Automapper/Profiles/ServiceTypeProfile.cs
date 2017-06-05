using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.Logic.Automapper.Profiles
{
    public class ServiceTypeProfile:Profile
    {
        public ServiceTypeProfile()
        {
            CreateMap<ServiceType, ServiceTypeViewModel>()
            .ForMember(x => x.Category, opt => opt.MapFrom(c => c.Category))
            .ReverseMap()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src=>src.Category));

            
        }
    }
}
