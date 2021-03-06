﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Shop
{
    public class CartModel : PageModel
    {
        public Cart Cart { get; set; }
        public decimal Total { get; set; }

        private SignInManager<Customer> _signInManager;
        private UserManager<Customer> _userManager;
        private ICart _cart;

        public CartModel(SignInManager<Customer> signInManager, UserManager<Customer> userManager, ICart cart)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _cart = cart;
        }

        /// <summary>
        /// Gets the signed in user's active cart details,
        /// and calculates the total price of all the cart items
        /// </summary>
        /// <returns>A complete task</returns>
        public async Task<IActionResult> OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                Customer user = await _userManager.GetUserAsync(User);
                Cart = await _cart.GetActiveCartForUser(user.Id);
                if (Cart == null || Cart.CartItems.Count == 0) // order of OR statement matters here
                {
                    return RedirectToAction("Index", "Products");
                }

                Total = _cart.GetCartTotal(user.Id);

                return Page();
            }
            else
            {
                return RedirectToPage("/Account/Login");
            }
        }
    }
}