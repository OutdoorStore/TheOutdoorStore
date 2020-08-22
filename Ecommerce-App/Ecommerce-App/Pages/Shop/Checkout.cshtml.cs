using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Ecommerce_App.Pages.Shop
{
    public class CheckoutModel : PageModel
    {
        private SignInManager<Customer> _signInManager;
        private UserManager<Customer> _userManager;
        private IPayment _payment;
        [BindProperty]
        public CheckoutViewModel Input { get; set; }

        public CheckoutModel(SignInManager<Customer> signInManager, UserManager<Customer> userManager, IPayment payment)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _payment = payment;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            // (Other checks may be needed here?)
            // Run() payment transaction
            // if OK response, proceed with Order creation and save
            // else redirect to same page w/ error message
            var user = await _userManager.GetUserAsync(User);
            string response = _payment.Run(Input.FirstName, Input.LastName, Input.BillingAddress, Input.BillingCity, Input.BillingState, Input.BillingZip, Input.PaymentMethod, user.Id);
            if (response == "Successful.")
            {
                // save the order

                // email receipt to user
                // 
                // go to receipt page
            }
            else
            {
                // reload page with error message
            }
            // TODO: Redirect to a Receipt Page
            return RedirectToAction("Index", "Home");
        }

        public class CheckoutViewModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string BillingAddress { get; set; }
            public string BillingCity { get; set; }
            public string BillingState { get; set; }
            public string BillingZip { get; set; }
            public string PaymentMethod { get; set; }
        }
    }
}