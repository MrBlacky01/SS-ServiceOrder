using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.ViewModel.ViewModels.Implementation.RegionViewModels;

namespace ServiceOrder.Logic.Automapper.Profiles
{
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            CreateMap<RegionEntityViewModel, Region>().ReverseMap();
        }
       
    }
}
