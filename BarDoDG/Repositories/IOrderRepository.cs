using BarDoDG.ApiModels;
using BarDoDG.Models;
using System.Collections.Generic;

namespace BarDoDG.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<IdentifierResponse> Get();
        OrderResponse GetOrder(int id);
        string AddItem(OrderItemRequest item);
        bool OrderClose(int id);
        bool OrderReset(int id);
        void Create(List<Order> orders);
    }
}