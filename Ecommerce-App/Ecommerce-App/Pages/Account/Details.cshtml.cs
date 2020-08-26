using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Account
{
    public class DetailsModel : PageModel
    {
        // [BindProperty]
        Customer Customer { get; set; }
        private UserManager<Customer> _userManager;
        private SignInManager<Customer> _signInManger;
        private IAccount _account;

        public DetailsModel(UserManager<Customer> userManger, SignInManager<Customer> signInManager, IAccount account)
        {
            _userManager = userManger;
            _signInManger = signInManager;
            _account = account;
        }

        public async void OnGet()
        {
            Customer = await _userManager.GetUserAsync(User);
        }

        public async void OnPostName()
        {
            Customer = await _userManager.GetUserAsync(User);
            await _account.UpdateName(Customer.Id, Customer.FirstName, Customer.LastName);
        }
        public async void OnPostBilling()
        {
            Customer = await _userManager.GetUserAsync(User);
            await _account.UpdateBilling(Customer.Id, Customer.BillingAddress, Customer.BillingCity, Customer.BillingState, Customer.BillingZip);
        }

        public async void OnPostRemoveBilling()
        {
            Customer = await _userManager.GetUserAsync(User);
            await _account.RemoveBilling(Customer.Id);
        }
    }
}
