using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Data;
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
        private StoreDbContext _storeDbContext;

        public ProductsController(IProductsService productsService, UserManager<Customer> userManager, SignInManager<Customer> signInManager, ICart cart, ICartItem cartItem, StoreDbContext storeDbContext)
        {
            _productsService = productsService;
            _userManager = userManager;
            _signInManager = signInManager;
            _cart = cart;
            _cartItem = cartItem;
            _storeDbContext = storeDbContext;
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


        public async Task<ActionResult> AddProductToCart(int productId, int quantity)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);

                Cart cart = await _cart.GetActiveCartForUser(user.Id);

                if (cart == null)
                {
                    cart = await _cart.Create(user.Id);
                }

                if (_storeDbContext.CartItems.Find(cart.Id, productId) != null)
                {
                    // 1 is hardcoded until a quantity input is added to the view and passed into this route.
                    int increase= 1;
                    await _cartItem.Update(cart.Id, productId, increase);
                }
                else
                {
                    await _cartItem.Create(productId, cart.Id, quantity);
                }

                return RedirectToAction("Index", "Home");

            }
            else
            {
                return RedirectToPage("/Account/Login");
            }
        }

    }
}
