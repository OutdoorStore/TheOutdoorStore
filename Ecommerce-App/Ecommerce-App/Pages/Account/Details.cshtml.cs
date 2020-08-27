using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Account
{
    [Authorize(Policy = "User")]
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; }
        private UserManager<Customer> _userManager;
        private SignInManager<Customer> _signInManger;
        private IAccount _account;

        public DetailsModel(UserManager<Customer> userManger, SignInManager<Customer> signInManager, IAccount account)
        {
            _userManager = userManger;
            _signInManger = signInManager;
            _account = account;
        }
        
        public async Task OnGet()
        {
            Customer = await _userManager.GetUserAsync(User);
        }

        public async Task<IActionResult> OnPostName()
        {
            var userId = _userManager.GetUserId(User);
            await _account.UpdateName(userId, Customer.FirstName, Customer.LastName);
            Customer = await _userManager.GetUserAsync(User);
            return RedirectToPage("/Account/Details");
        }
        public async Task OnPostBilling()
        {
            var userId = _userManager.GetUserId(User);
            await _account.UpdateBilling(userId, Customer.BillingAddress, Customer.BillingCity, Customer.BillingState, Customer.BillingZip);
            Customer = await _userManager.GetUserAsync(User);
        }

        public async Task OnGetRemoveBilling()
        {
            var userId = _userManager.GetUserId(User);
            await _account.RemoveBilling(userId);
            Customer = await _userManager.GetUserAsync(User);
        }
    }
}
