using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IAccount
    {
        /// <summary>
        /// Updates the First and Last name associated with a user. This method also updates the associated claims.
        /// </summary>
        /// <param name="userId">Unique user ID.</param>
        /// <param name="firstName">User's first name.</param>
        /// <param name="lastName">uer's last name.</param>
        /// <returns>Updated user.</returns>
        Task<Customer> UpdateName(string userId, string firstName, string lastName);
        
        /// <summary>
        /// Updates the Billing Information associated with a user.
        /// </summary>
        /// <param name="userId">User that will have information updated.</param>
        /// <param name="billingAddress">User's billing address.</param>
        /// <param name="billingCity">User's billing city.</param>
        /// <param name="billingState">User's billing state.</param>
        /// <param name="billingZip">User's billing zip code.</param>
        /// <returns>Updated user.</returns>
        Task<Customer> UpdateBilling(string userId, string billingAddress, string billingCity, string billingState, string billingZip);
        
        /// <summary>
        /// Replaces all billing information for a specific user.
        /// </summary>
        /// <param name="userId">Targeted user.</param>
        /// <returns>Task of completion.</returns>
        Task RemoveBilling(string userId);
    }
}
