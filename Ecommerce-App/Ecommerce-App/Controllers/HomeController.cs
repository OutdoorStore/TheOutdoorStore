using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Cereal cereal = new Cereal()
            {
                Name = "test"
                //mfr
                //type
                //calories
                //protein
                //fat
                //sodium
                //fiber
                //carbo
                //sugars
                //otass
                //vitamins
                //shelf
                //weight
                //cups
                //rating
            };
            return View();
        }
    }
}
