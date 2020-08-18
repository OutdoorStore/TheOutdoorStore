using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_App.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
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

    }
}
