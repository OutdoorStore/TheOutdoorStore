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

        // TODO: add the option to select a quantity other than 1

        /// <summary>
        /// Creates a new CartItem in the database, 
        /// based on the productId and cartId composite keys
        /// </summary>
        /// <param name="productId">The product the user selected</param>
        /// <param name="cartId">The user's active cart Id</param>
        /// <returns>The newly created CartItem object</returns>
        public async Task<CartItem> Create(int productId, int cartId) 
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

        /// <summary>
        /// Updates a product's quantity in a specific CartItem entry in the DB
        /// </summary>
        /// <param name="cartId">The active cart Id of the signed in user</param>
        /// <param name="productId">The unique product the user is updating</param>
        /// <param name="quantity">The selected product quantity</param>
        /// <returns>The updated cartItem object</returns>
        public async Task<CartItem> UpdateCartItemQuantity(int cartId, int productId, int quantity)
        {
            CartItem cartItem = _storeContext.CartItems.Find(cartId, productId);
            cartItem.Quantity += quantity;
            _storeContext.Entry(cartItem).State = EntityState.Modified;
            await _storeContext.SaveChangesAsync();

            return cartItem;
        }

        /// <summary>
        /// Calculates the total price of all the products in the cartItem 
        /// </summary>
        /// <param name="cartItem">A unique cartItem</param>
        /// <returns>The total price for all of the products in the cartItem</returns>
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
    }
}
