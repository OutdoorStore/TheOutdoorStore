using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce_App.Pages.Shop
{
    [Authorize(Policy = "User")]
    public class OrdersModel : PageModel
    {
        public List<Order> Orders { get; set; }
        public Cart Cart { get; set; }

        private SignInManager<Customer> _signInManager;
        private UserManager<Customer> _userManager;
        private IOrder _order;

        public OrdersModel(SignInManager<Customer> signInManager, UserManager<Customer> userManager, IOrder order)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _order = order;
        }

        /// <summary>
        /// OnGet checks if the user is signed in, and if so, 
        /// gets all of the user's orders from the database
        /// and returns the Orders Razor page
        /// </summary>
        /// <returns>The Orders Razor page</returns>
        public async Task<IActionResult> OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                Customer user = await _userManager.GetUserAsync(User);

                Orders = await _order.GetAllOrdersForUser(user.Id);

                return Page();
            }
            else
            {
                return RedirectToAction("GetAllProducts", "Products");
            }
        }
    }
}