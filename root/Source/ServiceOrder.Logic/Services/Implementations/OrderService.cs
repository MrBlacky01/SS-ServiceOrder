using System;
using System.Collections.Generic;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation.Order;

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
    }
}
