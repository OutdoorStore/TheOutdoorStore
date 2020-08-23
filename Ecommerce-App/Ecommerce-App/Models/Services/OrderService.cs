using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_App.Models.Services
{
    public class OrderService : IOrder
    {
        private StoreDbContext _storeContext;

        public OrderService(StoreDbContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<Order> FinalizeOrder(Order order)
        {
            _storeContext.Entry(order).State = EntityState.Added;
            await _storeContext.SaveChangesAsync();
            return order;
        }

        public Task<List<Order>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrder(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
