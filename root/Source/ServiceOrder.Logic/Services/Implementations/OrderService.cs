using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation.Order;
using ServiceOrder.ViewModel.ViewModels.Implementation.RegionViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.ServiceTypeViewModels;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork DataBase { get; set; }
        private readonly IMapper _mapper;
        
        public OrderService(IUnitOfWork dataBase,IMapper mapper )
        {
            DataBase = dataBase;
            _mapper = mapper;
        }

        public void Add(OrderViewModel item)
        {
            var exceptions = new List<Exception>();
            if (item.ServiceProviderId == null)
            {
                exceptions.Add(new NullReferenceException("Null value of provider Id"));
            }
            else
            {
                if (DataBase.ServiceProviders.Get(item.ServiceProviderId) == null)
                {
                    exceptions.Add(new NullReferenceException("No such service provider"));
                }
            }

            if (item.ClientId == null)
            {
                exceptions.Add(new NullReferenceException("Null value of client Id"));
            }

            if (item.RegionId <= 0)
            {
                exceptions.Add(new Exception("Wrong Region Id Parametr"));
            }
            else
            {
                if (
                    DataBase.ServiceProviders.Get(item.ServiceProviderId)
                        .ProviderRegions.Count(region => region.Id == item.RegionId) == 0)
                {
                    exceptions.Add(new NullReferenceException("This provider doesn't work in such region"));
                }
            }
            if (item.ServiceTypeId <= 0)
            {
                exceptions.Add(new Exception("Wrong Service Type Id Parametr"));
            }
            else
            {
                if (
                    DataBase.ServiceProviders.Get(item.ServiceProviderId)
                        .ProviderServiceTypes.Count(service => service.Id == item.ServiceTypeId) == 0)
                {
                    exceptions.Add(new NullReferenceException("This provider doesn't have such services "));
                }
            }
            if (item.Date.ToUniversalTime() < DateTime.UtcNow)
            {
                exceptions.Add(new Exception("Wrong Date parameter"));
            }
            else
            {
                if (item.Date.DayOfWeek == DayOfWeek.Saturday || item.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    exceptions.Add(new Exception("Date can't be on saturday or sunday"));
                }
            }

            item.Date = item.Date.ToUniversalTime();

            if (item.BeginTime.Hour > item.EndTime.Hour)
            {
                exceptions.Add(new Exception("End time can't be less than start time"));
            }

            item.BeginTime = item.BeginTime.ToUniversalTime();
            item.EndTime = item.EndTime.ToUniversalTime();

            var orders = DataBase.Orders.Find(order => (order.OrderProvider.UserId == item.ServiceProviderId)
                                                       && (order.BeginTime.Date == item.Date.Date)
                                                       &&
                                                       (((item.BeginTime.Hour >= order.BeginTime.Hour) &&
                                                         (item.BeginTime.Hour <= order.EndTime.Hour))
                                                        ||
                                                        ((item.EndTime.Hour >= order.BeginTime.Hour) &&
                                                         (item.EndTime.Hour <= order.EndTime.Hour))
                                                         ||
                                                         ((item.BeginTime.Hour < order.BeginTime.Hour)&&
                                                         (item.EndTime.Hour > order.EndTime.Hour))));
            if (orders.Count() > 0x0)
            {
                exceptions.Add(new Exception("This time of this Service Provider is Busy"));
            }
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
            
            item.BeginTime = new DateTime(item.Date.Year,item.Date.Month,item.Date.Day,item.BeginTime.Hour,item.BeginTime.Minute,item.BeginTime.Second,DateTimeKind.Utc);
            item.EndTime = new DateTime(item.Date.Year,item.Date.Month,item.Date.Day,item.EndTime.Hour,item.EndTime.Minute,item.EndTime.Second,DateTimeKind.Utc);
            DataBase.Orders.Create(_mapper.Map<OrderViewModel, Order>(item));
            DataBase.Save();
        }

        public void Delete(int? id)
        {
            if (id == null)
                throw new Exception("Order id has null value");
            DataBase.Orders.Delete((int)id);
            DataBase.Save();
        }

        public void Update(OrderViewModel item)
        {
            DataBase.Orders.Update(_mapper.Map<OrderViewModel, Order>(item));
            DataBase.Save();
        }

        public OrderViewModel Get(int? id)
        {

            if (id == null)
                throw new Exception("Order id has null value");
            var order = DataBase.Orders.Get((int)id);
            if (order == null)
            {
                throw new Exception("No Order with suchs Id");
            }

            return _mapper.Map<Order, OrderViewModel>(order);
        }

        public IEnumerable<OrderViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(DataBase.Orders.GetAll());
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }

        public IEnumerable<OrderViewModel> FindClientOrders(string clientId)
        {
            if (clientId.Equals(String.Empty))
            {
                throw new NullReferenceException("Wrondg client data");
            }
            if (DataBase.Clients.Get(clientId) == null)
            {
                throw new ObjectNotFoundException("No such client in database");
            }
            var model = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(
                DataBase.Orders.Find(order => order.OrderClient.UserId == clientId).OrderByDescending(key => key.BeginTime));
            var findClientOrders = model as IList<OrderViewModel> ?? model.ToList();

            foreach (var item in findClientOrders)
            {
                item.ServiceType = _mapper.Map<ServiceType,ServiceTypeViewModel>( DataBase.ServiceTypes.Get(item.ServiceTypeId));
                item.Region = _mapper.Map<Region,RegionEntityViewModel>(DataBase.Regions.Get(item.RegionId));
                item.ProviderName = DataBase.ServiceProviders.Get(item.ServiceProviderId).ProviderUser.UserName;
                item.BeginTime = item.BeginTime.ToLocalTime();
                item.EndTime = item.EndTime.ToLocalTime();
                item.Date = item.Date.ToLocalTime();
                //item.BeginTime = TimeZoneInfo.ConvertTimeFromUtc(item.BeginTime, TimeZoneInfo.Local);
                //item.EndTime = TimeZoneInfo.ConvertTimeFromUtc(item.EndTime, TimeZoneInfo.Local);
            }
            return findClientOrders;
        }

        public IEnumerable<OrderViewModel> FindProviderOrders(string providerId)
        {
            if (providerId.Equals(String.Empty))
            {
                throw new NullReferenceException("Wrondg client data");
            }
            if (DataBase.ServiceProviders.Get(providerId) == null)
            {
                throw new ObjectNotFoundException("No such client in database");
            }
            var model = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(
                    DataBase.Orders.Find(order => order.OrderProvider.UserId == providerId).OrderByDescending(key => key.BeginTime));
            var findProviderOrders = model as IList<OrderViewModel> ?? model.ToList();
            foreach (var item in findProviderOrders)
            {
                item.ServiceType = _mapper.Map<ServiceType, ServiceTypeViewModel>(DataBase.ServiceTypes.Get(item.ServiceTypeId));
                item.Region = _mapper.Map<Region, RegionEntityViewModel>(DataBase.Regions.Get(item.RegionId));
                item.ClientName = DataBase.Clients.Get(item.ClientId).ClientUser.UserName;
                item.BeginTime = item.BeginTime.ToLocalTime();
                item.EndTime = item.EndTime.ToLocalTime();
                //item.BeginTime = TimeZoneInfo.ConvertTimeFromUtc(item.BeginTime, TimeZoneInfo.Local);
                //item.EndTime = TimeZoneInfo.ConvertTimeFromUtc(item.EndTime, TimeZoneInfo.Local);
            }
            return findProviderOrders;
        }

        public int[] GetProviderFreeTime(DateTime date, string providerId)
        {
            if (DataBase.ServiceProviders.Get(providerId) == null)
            {
                throw new Exception("No such service provider");
            }
            var providerOrders = DataBase.Orders.Find(order => order.OrderProvider.UserId == providerId
                                                               && order.BeginTime.Date == date.Date);
            int[] allTimeArray =  {8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22};
            bool[] needDelete = new bool[15];
            var deleteCount = 0;
            foreach (var order in providerOrders)
            {
                for (var i = 0; i < allTimeArray.Length; i++)
                {
                    if (needDelete[i] == false)
                    {
                        if (allTimeArray[i] >= order.BeginTime.ToLocalTime().Hour && allTimeArray[i] <  order.EndTime.ToLocalTime().Hour)
                        {
                            needDelete[i] = true;
                            deleteCount++;
                        }
                    }
                }
            }

            var resultArray = new int[allTimeArray.Length - deleteCount];
            var index = 0;
            for (var i = 0; i < needDelete.Length; i++)
            {
                if (needDelete[i]) continue;
                resultArray[index] = allTimeArray[i];
                index++;
            }
            return resultArray;
        }
    }
}
