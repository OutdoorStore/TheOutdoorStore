using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface ICartItem
    {
        /// <summary>
        /// Creates a new CartItem in the database, 
        /// based on the productId and cartId composite keys
        /// </summary>
        /// <param name="productId">The product the user selected</param>
        /// <param name="cartId">The user's active cart Id</param>
        /// <returns>The newly created CartItem object</returns>
        Task<CartItem> Create(int productId, int cartId);

        /// <summary>
        /// Updates a product's quantity in a specific CartItem entry in the DB
        /// </summary>
        /// <param name="cartId">The active cart Id of the signed in user</param>
        /// <param name="productId">The unique product the user is updating</param>
        /// <param name="quantity">The selected product quantity</param>
        /// <returns>The updated cartItem object</returns>
        Task<CartItem> UpdateCartItemQuantity(int cartId, int productId, int quantity);
        
        /// <summary>
        /// Calculates the total price of all the products in the cartItem 
        /// </summary>
        /// <param name="cartItem">A unique cartItem</param>
        /// <returns>The total price for all of the products in the cartItem</returns>
        decimal GetCartItemTotal(CartItem cartItem);

    }
}
