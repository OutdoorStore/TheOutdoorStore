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

        public CartItemService(StoreDbContext storeContext)
        {
            _storeContext = storeContext;
        }

        // quantity can be added later, hardcoded for now
        public async Task<CartItem> Create(int productId, int cartId, int quantity) 
        {
            CartItem cartItem = new CartItem()
            {
                ProductId = productId,
                CartId = cartId,
                Quantity = (quantity > 0) ? quantity : 1
            };

            _storeContext.Entry(cartItem).State = EntityState.Added;
            await _storeContext.SaveChangesAsync();

            return cartItem;
        }

        public async Task<CartItem> Update(int cartId, int productId, int quantity)
        {
            CartItem cartItem = _storeContext.CartItems.Find(cartId, productId);
            cartItem.Quantity = quantity;
            _storeContext.Entry(cartItem).State = EntityState.Modified;
            await _storeContext.SaveChangesAsync();

            return cartItem;
        }

        public decimal GetCartItemTotal(CartItem cartItem)
        {
            // get product 
            int productId = cartItem.ProductId;


            // product price * product quantity
            decimal productPrice =  _storeContext.Products.Where(p => p.Id == productId)
                                                          .Select(p => p.Price)
                                                          .FirstOrDefault();

            int productQuantity = _storeContext.CartItems.Where(ci => ci.ProductId == productId && ci.CartId == cartItem.CartId)
                                                         .Select(ci => ci.Quantity)
                                                         .FirstOrDefault();

            decimal totalCartItemPrice = productPrice * productQuantity;

            return totalCartItemPrice;
        }

        /// <summary>
        /// Removes a cartItem from a cart in its entirety.
        /// </summary>
        /// <param name="cartId">Target Cart.</param>
        /// <param name="productId">Target Product.</param>
        /// <returns>Completed Action.</returns>
        public async Task Delete(int cartId, int productId)
        {
            CartItem cartItem = await _storeContext.CartItems.Where(ci => ci.CartId == cartId && ci.ProductId == productId)
                                                       .FirstOrDefaultAsync();
            _storeContext.Entry(cartItem).State = EntityState.Deleted;
            await _storeContext.SaveChangesAsync();
        }
    }
}
