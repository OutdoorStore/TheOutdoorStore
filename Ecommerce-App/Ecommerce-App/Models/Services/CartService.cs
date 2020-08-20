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

        public CartService(StoreDbContext storeContext, UserDbContext userContext)
        {
            _storeContext = storeContext;
            _userContext = userContext;
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
        public List<Cart> GetCartsForUser(string userId)
        {
            List<Cart> result = _storeContext.Carts.Where(c => c.UserId == userId)
                                                   .ToList();



            return result;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GetSingleCartForUser(string userId)
        {
            var customerCart = await _storeContext.Carts.Where(c => c.UserId == userId)
                                                       .Include(c => c.CartItems)
                                                       .ThenInclude(ci => ci.Product)
                                                       .FirstOrDefaultAsync();

            return customerCart;
        }

        public Task<List<Cart>> GetCarts()
        {
            throw new NotImplementedException();
        }

        public Task<Cart> Update(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
