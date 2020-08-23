using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface ICart
    {
        /// <summary>
        /// Creates a new entry in the Cart database table
        /// based on the userId parameter, and returns the new cart object
        /// </summary>
        /// <param name="userId">The unique id of the user that is creating the cart</param>
        /// <returns>The newly created Cart object</returns>
        Task<Cart> Create(string userId);

        /// <summary>
        /// Calculates the total price of all the cart items within the cart, 
        /// based on the user Id, searches for an active cart, lists all of the cart items, 
        /// calculates the total of each cart item, and returns a total of totals. 
        /// </summary>
        /// <param name="userId">The unique Id of the signed in user</param>
        /// <returns>The total price of all cart items in cart</returns>
        decimal GetCartTotal(string userId);

        /// <summary>
        /// Gets the user's active cart from the database, if exists, 
        /// based on the signed in user Id. 
        /// </summary>
        /// <param name="userId">The unique Id of the signed in user</param>
        /// <returns>The user's active cart object</returns>
        Task<Cart> GetActiveCartForUser(string userId);

        /// <summary>
        /// Updates a cart's Active status from true to false, 
        /// which effectively "closes" it for any other updates.
        /// </summary>
        /// <param name="userId">The unique Id of the signed in user</param>
        /// <returns>The updated closed cart object(active = false)</returns>
        Task<Cart> CloseCart(string userId);

    }
}
