using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_App.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View("Products", CerealVM.GetData());
        }


        public IActionResult All()
        {
            Product product = new Product()
            {
                Name = "test"
            };
            return View("Products", product);
        }
    }
}
