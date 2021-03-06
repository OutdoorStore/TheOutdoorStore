﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ecommerce_App.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private UserManager<Customer> _userManager;
        private SignInManager<Customer> _signInManager;
        private IEmailSender _emailSenderService;

        public RegisterModel(UserManager<Customer> userManager, SignInManager<Customer> signInManager, IEmailSender emailSenderService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSenderService = emailSenderService;
        }

        [BindProperty]
        public RegisterViewModel Input { get; set; }

        /// <summary>
        /// Gets the signed in user information
        /// </summary>
        public void OnGet()
        {
            var user = _userManager.GetUserAsync(User);
        }
        
        /// <summary>
        /// Collects the user's registration details and, 
        /// creates a new user in the db, signs in the user, 
        /// and sends the new user a welcome email
        /// </summary>
        /// <returns>A complete task</returns>
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

                        Claim claimFirstName = new Claim("FirstName", $"{Input.FirstName}");
                        await _userManager.AddClaimAsync(customer, claimFirstName);

                        Claim claimSecondName = new Claim("LastName", $"{Input.LastName}");
                        await _userManager.AddClaimAsync(customer, claimSecondName);


                        //sign in the user 
                        await _signInManager.SignInAsync(customer, isPersistent: false);

                        // Send registration confirmation email to the new user
                        string subject = "Welcome to The Outdoor Store!";
                        string htmlMessage = $"<h1>Thank you {customer.FirstName} for joining us at The Outdoor Store.</h1>";

                        await _emailSenderService.SendEmailAsync(customer.Email, subject, htmlMessage);

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