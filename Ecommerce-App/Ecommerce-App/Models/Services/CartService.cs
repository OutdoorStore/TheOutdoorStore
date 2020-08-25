using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_App.Models.Services
{
    public class CartService : ICart
    {
        private StoreDbContext _storeContext;
        private UserDbContext _userContext;
        private ICartItem _cartItem;

        public CartService(StoreDbContext storeContext, UserDbContext userContext, ICartItem cartItem)
        {
            _storeContext = storeContext;
            _userContext = userContext;
            _cartItem = cartItem;
        }

        /// <summary>
        /// Creates a new entry in the Cart database table
        /// based on the userId parameter, and returns the new cart object
        /// </summary>
        /// <param name="userId">The unique id of the user that is creating the cart</param>
        /// <returns>The newly created Cart object</returns>
        public async Task<Cart> Create(string userId)
        {
            Cart cart = new Cart()
            {
                UserId = userId
            };

            _storeContext.Entry(cart).State = EntityState.Added;
            await _storeContext.SaveChangesAsync();

            return cart;
        }

        /// <summary>
        /// Gets the user's active cart from the database, if exists, 
        /// based on the signed in user Id. 
        /// </summary>
        /// <param name="userId">The unique Id of the signed in user</param>
        /// <returns>The user's active cart object</returns>
        public async Task<Cart> GetActiveCartForUser(string userId)
        {
            var customerCart = await _storeContext.Carts.Where(c => c.UserId == userId && c.Active == true)
                                                       .Include(c => c.CartItems)
                                                       .ThenInclude(ci => ci.Product)
                                                       .FirstOrDefaultAsync();

            return customerCart;
        }

        /// <summary>
        /// Calculates the total price of all the cart items within the cart, 
        /// based on the user Id, searches for an active cart, lists all of the cart items, 
        /// calculates the total of each cart item, and returns a total of totals. 
        /// </summary>
        /// <param name="userId">The unique Id of the signed in user</param>
        /// <returns>The total price of all cart items in cart</returns>
        public decimal GetCartTotal(string userId)
        {
            decimal total = 0;
            Cart cart = _storeContext.Carts.FirstOrDefault(c => c.UserId == userId && c.Active == true);
            List<CartItem> cartItems = _storeContext.CartItems.Where(ci => ci.CartId == cart.Id).ToList();
            foreach (var item in cartItems)
            {
                total += _cartItem.GetCartItemTotal(item);
            }
            return total;
        }

        /// <summary>
        /// Updates a cart's Active status from true to false, 
        /// which effectively "closes" it for any other updates.
        /// </summary>
        /// <param name="userId">The unique Id of the signed in user</param>
        /// <returns>The updated closed cart object(active = false)</returns>
        public async Task<Cart> CloseCart(string userId)
        {
            Cart cart = await GetActiveCartForUser(userId);
            cart.Active = false;
            _storeContext.Entry(cart).State = EntityState.Modified;
            await _storeContext.SaveChangesAsync();
            return cart;
        }
    }
}
