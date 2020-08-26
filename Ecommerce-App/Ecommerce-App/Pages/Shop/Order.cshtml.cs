using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Shop
{
    public class OrderModel : PageModel
    {
        public Order Order { get; set; }
        public Cart Cart { get; set; }

        private SignInManager<Customer> _signInManager;
        private ICart _cart;
        private IOrder _order;

        public OrderModel(SignInManager<Customer> signInManager, ICart cart, IOrder order)
        {
            _signInManager = signInManager;
            _cart = cart;
            _order = order;
        }
        
        /// <summary>
        /// OnGet checks if the user is signed in, 
        /// and if so gets a single order by order Id
        /// and returns the Razor page
        /// </summary>
        /// <param name="Id">The specific order that is looked up</param>
        /// <returns>The Order Razor page</returns>
        public async Task<IActionResult> OnGet(int Id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                Order = await _order.GetSingleOrderById(Id);

                return Page();
            }
            else
            {
                return RedirectToAction("GetAllProducts", "Products");
            }
        }

    }
}