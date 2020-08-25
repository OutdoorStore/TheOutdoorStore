using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_App.Controllers
{
    public class HomeController : Controller
    {
        private IPayment _payment;

        public HomeController(IPayment payment)
        {
            _payment = payment;
        }
        public IActionResult Index()
        {
            //_payment.Run();
            return View();
        }
    }
}
