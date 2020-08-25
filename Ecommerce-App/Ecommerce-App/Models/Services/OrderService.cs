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
        private ICartItem _cartItem;

        public OrderService(StoreDbContext storeContext, ICartItem cartItem)
        {
            _storeContext = storeContext;
            _cartItem = cartItem;
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

        /// <summary>
        /// Gets the user's most recent order, by filtering the orders table
        /// by userId and then selecting the most recent order
        /// </summary>
        /// <param name="userId">The signed in user</param>
        /// <returns>The most recent order and all of its related cartItems and products</returns>
        public async Task<Order> GetMostRecentOrder(string userId)
        {
           
            Order order =  await _storeContext.Orders.Where(u => u.UserId == userId)
                                               .OrderByDescending(o => o.Date)
                                               .Include(o => o.CartItems)
                                               .ThenInclude(ci => ci.Product)
                                               .FirstOrDefaultAsync();

            return order;
        }

        /// <summary>
        /// Gets the total price of all cart items in a specific order by order ID
        /// </summary>
        /// <param name="orderId">The specific order that is being searched</param>
        /// <returns>The total price of all items in the order</returns>
        public async Task<decimal> GetSpecificOrderTotal(int orderId)
        {
            decimal total = 0;
            Order order = await _storeContext.Orders.FindAsync(orderId);
                                     
            Cart cart = _storeContext.Carts.FirstOrDefault(c => c.Id == order.CartId);
            List<CartItem> cartItems = _storeContext.CartItems.Where(ci => ci.CartId == cart.Id).ToList();
            foreach (var item in cartItems)
            {
                total += _cartItem.GetCartItemTotal(item);
            }

            return total;
        }

    }
}
