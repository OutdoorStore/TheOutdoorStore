using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface ICart
    {
        //Create
        Task<Cart> Create(string userId);

        // Get All
        Task<List<Cart>> GetCarts();

        // Get Single Cart
        Task<Cart> GetActiveCartForUser(string userId);

        //Update
        Task<Cart> CloseCart(string userId);

        // Delete
        Task Delete(int id);
    }
}
