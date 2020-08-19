using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_App.Models.Services
{
    public class CartItemService : ICartItem
    {
        private StoreDbContext _storeContext;
        private UserDbContext _userContext;

        public CartItemService(StoreDbContext storeContext, UserDbContext userContext)
        {
            _storeContext = storeContext;
            _userContext = userContext;
        }

        public async Task<CartItem> Create(int productId, int cartId) // quantity can be added later, hardcoded for now
        {
            CartItem cartItem = new CartItem()
            {
                ProductId = productId,
                CartId = cartId,
                Quantity = 1
            };

            _storeContext.Entry(cartItem).State = EntityState.Added;
            await _storeContext.SaveChangesAsync();

            return cartItem;
        }

        public async Task<CartItem> Update(int cartId, int productId, int quantity)
        {
            CartItem cartItem = _storeContext.CartItems.Find(cartId, productId);
            cartItem.Quantity += quantity;
            _storeContext.Entry(cartItem).State = EntityState.Modified;
            await _storeContext.SaveChangesAsync();

            return cartItem;
        }
    }
}
