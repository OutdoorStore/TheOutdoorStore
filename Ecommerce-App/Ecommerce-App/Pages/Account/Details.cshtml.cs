using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Account
{
    public class DetailsModel : PageModel
    {
        // [BindProperty]
        Customer Customer { get; set; }
        private UserManager<Customer> _userManager;
        private SignInManager<Customer> _signInManger;

        public DetailsModel(UserManager<Customer> userManger, SignInManager<Customer> signInManager)
        {
            _userManager = userManger;
            _signInManger = signInManager;
        }

        public async void OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
        }

        public void OnPost()
        {

        }

        public void OnPut()
        {

        }
    }
}
