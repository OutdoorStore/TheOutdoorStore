using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly ICart _cart;
        private readonly ICartItem _cartItem;

        public ProductsController(IProductsService productsService, UserManager<Customer> userManager, SignInManager<Customer> signInManager, ICart cart, ICartItem cartItem)
        {
            _productsService = productsService;
            _userManager = userManager;
            _signInManager = signInManager;
            _cart = cart;
            _cartItem = cartItem;
        }
        public IActionResult Index()
        {
            return View();
        }
        
         
        public async Task<ActionResult> GetAllProducts()
        {
            return View("Products", await _productsService.GetAllProducts());
        }


        public async Task<ActionResult<Product>> GetSingleProduct(int id)
        {
            Product product = await _productsService.GetSingleProduct(id);

            return View("Product", product);
        }

        public async Task<ActionResult> AddProductToCart(int productId)
        {
            if(_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);

                List<Cart> allUserCarts = _cart.GetCartsForUser(user.Id);

                if (allUserCarts.Count == 0)
                {
                    Cart cart = await _cart.Create(user.Id);
                    allUserCarts.Add(cart);
                }

                var cartItem = _cartItem.Create(productId, allUserCarts[0].Id);

                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                return RedirectToPage("/Account/Login");
            }
        }

    }
}
