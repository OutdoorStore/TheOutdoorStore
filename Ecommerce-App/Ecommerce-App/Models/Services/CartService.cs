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

        // TODO: update this method to include all of the cart items and products
        // then inject it into the cartcomponent view so there's no DB in the view
        //public List<Cart> GetCartsForUser(string userId)
        //{
        //    List<Cart> result = _storeContext.Carts.Where(c => c.UserId == userId)
        //                                           .ToList();



        //    return result;
        //}

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GetActiveCartForUser(string userId)
        {
            var customerCart = await _storeContext.Carts.Where(c => c.UserId == userId && c.Active == true)
                                                       .Include(c => c.CartItems)
                                                       .ThenInclude(ci => ci.Product)
                                                       .FirstOrDefaultAsync();

            return customerCart;
        }

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
