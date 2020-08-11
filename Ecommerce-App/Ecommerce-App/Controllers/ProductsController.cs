using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Ecommerce_App.Models.ViewModels;
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

        public IActionResult GetAllProducts()
        {
            return View("Products", _productsService.GetData());
        }


        public IActionResult GetSingleProduct()
        {
            Cereal cereal = new Cereal()
            {
                Name = "test"
            };
            return View("Products", cereal);
        }
    }
}
