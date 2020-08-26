using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class AccountService : IAccount
    {
        private UserDbContext _userDbContext;
        private UserManager<Customer> _userManager;

        public AccountService( UserDbContext userDbContext, UserManager<Customer> userManger)
        {
            _userDbContext = userDbContext;
            _userManager = userManger;
        }

        public async Task<Customer> UpdateName(string userId, string firstName, string lastName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.FirstName = firstName;
            user.LastName = lastName;
            _userDbContext.Entry(user).State = EntityState.Modified;
            await _userDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<Customer> UpdateBilling(string userId, string billingAddress, string billingCity, string billingState, string billingZip)
        {
            var user = _userDbContext.Users.Where(c => c.Id == userId).FirstOrDefault();
            //var user = await _userManager.FindByIdAsync(userId);
            user.BillingAddress = billingAddress;
            user.BillingCity = billingCity;
            user.BillingState = billingState;
            user.BillingZip = billingZip;
            _userDbContext.Entry(user).State = EntityState.Modified;
            await _userDbContext.SaveChangesAsync();
            return user;
        }

        public async Task RemoveBilling(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.BillingAddress = null;
            user.BillingCity = null;
            user.BillingState = null;
            user.BillingZip = null;
            _userDbContext.Entry(user).State = EntityState.Modified;
            await _userDbContext.SaveChangesAsync();
        }
    }
}
