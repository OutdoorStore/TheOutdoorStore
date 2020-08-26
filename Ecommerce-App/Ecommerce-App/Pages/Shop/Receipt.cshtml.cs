using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Shop
{
    public class ReceiptModel : PageModel
    {
        public Order Order { get; set; }
        public Cart Cart { get; set; }

        private SignInManager<Customer> _signInManager;
        private UserManager<Customer> _userManager;
        private ICart _cart;
        private IOrder _order;

        public ReceiptModel(SignInManager<Customer> signInManager, UserManager<Customer> userManager, ICart cart, IOrder order)
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

                Order = await _order.GetMostRecentOrder(user.Id);

                return Page();
            }
            else
            {
                return RedirectToAction("GetAllProducts", "Products");
            }
        }
    }
}