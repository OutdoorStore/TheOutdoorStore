using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface ICartItem
    {
        Task<CartItem> Create(int productId, int cartId);
        Task<CartItem> Update(int cartId, int productId, int quantity);
        decimal GetCartItemTotal(CartItem cartItem);

    }
}
