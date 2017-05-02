using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.ViewModel.ViewModels.Implementation.Order;

namespace ServiceOrder.Logic.Automapper.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderViewModel, Order>()
                .ForMember(dest => dest.BeginTime, opt => opt.MapFrom(src => src.BeginTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.OrderClient, opt=> opt.MapFrom(src=> new Client {UserId = src.ClientId}))
                .ForMember(dest => dest.OrderProvider, opt => opt.MapFrom(src => new ServiceProvider {UserId = src.ServiceProviderId}))
                .ForMember(dest => dest.OrderRegion, opt => opt.MapFrom(src =>new Region {Id = src.RegionId}))
                .ForMember(dest => dest.OrderType, opt => opt.MapFrom(src => new ServiceType {Id =  src.ServiceTypeId}))
                .ReverseMap()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.OrderClient.UserId))
                .ForMember(dest => dest.ServiceProviderId, opt => opt.MapFrom(src => src.OrderProvider.UserId))
                .ForMember(dest => dest.ServiceTypeId, opt => opt.MapFrom(src => src.OrderType.Id))
                .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.OrderRegion.Id))
                .ForMember(dest => dest.BeginTime, opt => opt.MapFrom(src => src.BeginTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.BeginTime.Date));
        }
    }
}
