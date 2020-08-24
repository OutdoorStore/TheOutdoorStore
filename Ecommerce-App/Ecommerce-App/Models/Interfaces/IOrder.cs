using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IOrder
    {
        /// <summary>
        /// Creates a new entry in the Order database table
        /// with all of the details of the finalized order
        /// </summary>
        /// <param name="order">The order object to be finalized</param>
        /// <returns>The created order object</returns>
        Task<Order> FinalizeOrder(Order order);

        /// <summary>
        /// Gets the user's most recent order, by filtering the orders table
        /// by userId and then selecting the most recent order
        /// </summary>
        /// <param name="userId">The signed in user</param>
        /// <returns>The most recent order and all of its related cartItems and products</returns>
        Task<Order> GetMostRecentOrder(string userId);

        /// <summary>
        /// Gets the total price of all cart items in a specific order by order ID
        /// </summary>
        /// <param name="orderId">The specific order that is being searched</param>
        /// <returns>The total price of all items in the order</returns>
        Task<decimal> GetSpecificOrderTotal(int orderId);
    }
}
