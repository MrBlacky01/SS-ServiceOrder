using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.ViewModel.ViewModels.Implementation.RegionViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceProvidersViewModels;

namespace ServiceOrder.Logic.Automapper.Profiles
{
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            CreateMap<RegionEntityViewModel, Region>().ReverseMap();
            CreateMap<ProviderRegion, Region>().ReverseMap();
        }
       
    }
}
