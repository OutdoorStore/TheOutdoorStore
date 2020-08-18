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

        public async Task<Cart> Create(Cart cart)
        {
            _storeContext.Entry(cart).State = EntityState.Added;
            await _storeContext.SaveChangesAsync();

            return cart;
        }

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

        public Task<Cart> GetCart(int id)
        {
            throw new NotImplementedException();
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
