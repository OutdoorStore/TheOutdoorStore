using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface ICartItem
    {
        Task<CartItem> Create(int productId, int cartId, int quantity);
        Task<CartItem> Update(int cartId, int productId, int quantity);
        decimal GetCartItemTotal(CartItem cartItem);

        /// <summary>
        /// Removes a cartItem from a cart in its entirety.
        /// </summary>
        /// <param name="cartId">Target Cart.</param>
        /// <param name="productId">Target Product.</param>
        /// <returns>Completed Action.</returns>
        Task Delete(int cartId, int productId);

    }
}
