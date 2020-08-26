using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IAccount
    {
        Task<Customer> UpdateName(string userId, string firstName, string lastName);
        Task<Customer> UpdateBilling(string userId, string billingAddress, string billingCity, string billingState, string billingZip);
        Task RemoveBilling(string userId);
    }
}
