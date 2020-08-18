﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface ICart
    {
        //Create
        Task<Cart> Create(Cart cart);

        // Get All
        Task<List<Cart>> GetCarts();

        // Get Single Cart
        Task<Cart> GetCart(int id);

        // Get Cart by user ID
        List<Cart> GetCartsForUser(string userId)

        //Update
        Task<Cart> Update(Cart cart);

        // Delete
        Task Delete(int id);
    }
}
