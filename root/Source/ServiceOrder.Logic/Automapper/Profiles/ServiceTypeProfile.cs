using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.Logic.Automapper.Profiles
{
    public class ServiceTypeProfile:Profile
    {
        public ServiceTypeProfile()
        {
            CreateMap<ServiceType, ServiceTypeViewModel>()
            .ForMember(x => x.CategoryId, opt => opt.MapFrom(c => c.ServiceCategoryId))
            .ForMember(x => x.CategoryTitle, opt => opt.MapFrom(c => c.Category.Title))
            .ReverseMap();

            CreateMap<ServiceType, Service>().ReverseMap();
        }
    }
}
