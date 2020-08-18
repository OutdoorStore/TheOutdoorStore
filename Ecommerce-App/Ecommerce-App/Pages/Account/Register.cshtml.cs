using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private SignInManager<Customer> _signInManager;

        public RegisterModel(UserManager<Customer> userManager, SignInManager<Customer> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public RegisterViewModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Input.Password == Input.ConfirmPassword)
                {
                    Customer customer = new Customer()
                    {
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        Email = Input.Email,
                        UserName = Input.Email
                    };
                    var result = await _userManager.CreateAsync(customer, Input.Password);
                    if (result.Succeeded)
                    {
                        Claim claim = new Claim("FullName", $"{Input.FirstName} {Input.LastName}");
                        await _userManager.AddClaimAsync(customer, claim);
                        await _signInManager.SignInAsync(customer, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return Page();
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