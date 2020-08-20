using Ecommerce_App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;

namespace Ecommerce_App.Components
{
    [ViewComponent]
    public class CartCompViewComponent : ViewComponent
    {
        private ICart _cart;

        public CartCompViewComponent(ICart cart)
        {
            _cart = cart;
        }


        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            Cart customerCart = await _cart.GetSingleCartForUser(userId);

            return View(customerCart);
        }

    }
}
