using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Shop
{
    public class CartModel : PageModel
    {

        private SignInManager<Customer> _signInManager;
        private UserManager<Customer> _userManager;
        private ICart _cart;

        public CartModel(SignInManager<Customer> signInManager, UserManager<Customer> userManager, ICart cart)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _cart = cart;
        }
        public async Task<IActionResult> OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                Customer user = await _userManager.GetUserAsync(User);
                Cart customerCart = await _cart.GetSingleCartForUser(user.Id);
                return Page();
            }
            else
            {
                return RedirectToPage("Login");
            }
        }
    }
}