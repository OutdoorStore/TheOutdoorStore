using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ecommerce_App.Pages.Shop
{
    public class OrdersModel : PageModel
    {
        public List<Order> Orders { get; set; }
        public Cart Cart { get; set; }

        private SignInManager<Customer> _signInManager;
        private UserManager<Customer> _userManager;
        private ICart _cart;
        private IOrder _order;

        public OrdersModel(SignInManager<Customer> signInManager, UserManager<Customer> userManager, ICart cart, IOrder order)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _cart = cart;
            _order = order;
        }
        public async Task<IActionResult> OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                Customer user = await _userManager.GetUserAsync(User);

                Orders = await _order.GetAllOrdersForUser(user.Id);

                foreach (var order in Orders)
                {
                    order.Total = await _order.GetSpecificOrderTotal(order.Id);
                }

                return Page();
            }
            else
            {
                return RedirectToAction("GetAllProducts", "Products");


            }
        }
    }
}