using System;
using System.Collections.Generic;
using ServiceOrder.ViewModel.ViewModels.Implementation.Order;

namespace ServiceOrder.Logic.Services
{
    public interface IOrderService: IService<OrderViewModel,int?>
    {
        IEnumerable<OrderViewModel> FindClientOrders(string clientId);
        IEnumerable<OrderViewModel> FindProviderOrders(string providerId);
        int[] GetProviderFreeTime(DateTime date, string providerId);
    }
}
