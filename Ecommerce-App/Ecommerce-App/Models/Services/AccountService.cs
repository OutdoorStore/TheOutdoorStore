using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class AccountService : IAccount
    {
        private UserDbContext _userDbContext;
        private UserManager<Customer> _userManager;
        private SignInManager<Customer> _signInManager;

        public AccountService( UserDbContext userDbContext, UserManager<Customer> userManger, SignInManager<Customer> signInManager)
        {
            _userDbContext = userDbContext;
            _userManager = userManger;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Updates the First and Last name associated with a user. This method also updates the associated claims.
        /// </summary>
        /// <param name="userId">Unique user ID.</param>
        /// <param name="firstName">User's first name.</param>
        /// <param name="lastName">uer's last name.</param>
        /// <returns>Updated user.</returns>
        public async Task<Customer> UpdateName(string userId, string firstName, string lastName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            // changes Name in DB
            user.FirstName = firstName;
            user.LastName = lastName;
            _userDbContext.Entry(user).State = EntityState.Modified;
            await _userDbContext.SaveChangesAsync();

            // changes name Claims in DB
            var claims = await _userManager.GetClaimsAsync(user);
            await _userManager.RemoveClaimsAsync(user, claims);
            List<Claim> newClaims = new List<Claim>();
            newClaims.Add(new Claim("FullName", $"{user.FirstName} {user.LastName}"));
            newClaims.Add(new Claim("FirstName", $"{user.FirstName}"));
            newClaims.Add(new Claim("LastName", $"{user.LastName}"));
            await _userManager.AddClaimsAsync(user, newClaims);

            // refresh the data for the view
            await _signInManager.SignInAsync(user, false);

            return user;
        }

        /// <summary>
        /// Updates the Billing Information associated with a user.
        /// </summary>
        /// <param name="userId">User that will have information updated.</param>
        /// <param name="billingAddress">User's billing address.</param>
        /// <param name="billingCity">User's billing city.</param>
        /// <param name="billingState">User's billing state.</param>
        /// <param name="billingZip">User's billing zip code.</param>
        /// <returns>Updated user.</returns>
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

        /// <summary>
        /// Replaces all billing information for a specific user.
        /// </summary>
        /// <param name="userId">Targeted user.</param>
        /// <returns>Task of completion.</returns>
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
