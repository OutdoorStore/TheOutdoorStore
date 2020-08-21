using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    interface IOrder
    {
        Task<Order> Create(Order order);
        Task<Order> GetOrder(string userId);
        Task<Order> GetOrder(int id);
        Task<List<Order>> GetAll();
    }
}
