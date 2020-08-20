using Ecommerce_App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Ecommerce_App.Models;
using Ecommerce_App.Models.ViewModels;

namespace Ecommerce_App.Components
{
    [ViewComponent]
    public class CartCompViewComponent : ViewComponent
    {
        private StoreDbContext _storeContext;
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;

        public CartCompViewComponent(StoreDbContext storeContext, UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            _storeContext = storeContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CartCompVM> cartCompVMs = new List<CartCompVM>();

            var cartItems = await _storeContext.CartItems.OrderByDescending(c => c.Id).ToListAsync();

            foreach (var item in cartItems)
            {
                Product product = _storeContext.Products.Find(item.ProductId);

                CartCompVM VM = new CartCompVM
                {
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    Price = product.Price
                };

                cartCompVMs.Add(VM);

            }

            return View(cartCompVMs);
        }

    }
}
