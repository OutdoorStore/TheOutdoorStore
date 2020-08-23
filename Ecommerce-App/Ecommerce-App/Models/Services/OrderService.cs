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

        /// <summary>
        /// Creates a new entry in the Order database table
        /// with all of the details of the finalized order
        /// </summary>
        /// <param name="order">The order object to be finalized</param>
        /// <returns>The created order object</returns>
        public async Task<Order> FinalizeOrder(Order order)
        {
            _storeContext.Entry(order).State = EntityState.Added;
            await _storeContext.SaveChangesAsync();
            return order;
        }

        
        public async Task<Order> GetMostRecentOrder(string userId)
        {
           
            Order order =  await _storeContext.Orders.Where(u => u.UserId == userId)
                                               .OrderByDescending(o => o.Date)
                                               .Include(o => o.CartItems)
                                               .ThenInclude(ci => ci.Product)
                                               .FirstOrDefaultAsync();

            return order;
        }
    }
}
