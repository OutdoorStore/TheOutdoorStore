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
    public class RegisterModel : PageModel
    {
        private UserManager<Customer> _userManager;
        public RegisterModel(UserManager<Customer> userManager)
        {
            _userManager = userManager;
        }
        public RegisterViewModel Input { get; set; }

        public void OnGet()
        {

        }

        public void OnPost()
        {

        }

        public class RegisterViewModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }
    }
}