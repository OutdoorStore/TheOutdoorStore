using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Data;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text;

namespace Ecommerce_App.Pages.Shop
{
    public class CheckoutModel : PageModel
    {
        private SignInManager<Customer> _signInManager;
        private UserManager<Customer> _userManager;
        private UserDbContext _userDbContext;
        private IPayment _payment;
        private IOrder _order;
        private ICart _cart;
        private IEmailSender _emailSenderService;

        [BindProperty]
        public Order Input { get; set; }
        public Order Order { get; set; }

        public Customer Customer { get; set; }
        public string ErrorMessage { get; set; }

        public CheckoutModel(SignInManager<Customer> signInManager, UserManager<Customer> userManager, UserDbContext userDbContext, IPayment payment, IOrder order, ICart cart, IEmailSender emailSenderService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userDbContext = userDbContext;
            _payment = payment;
            _order = order;
            _cart = cart;
            _emailSenderService = emailSenderService;
        }
        public async Task OnGet()
        {
            Customer = await _userManager.GetUserAsync(User);
        }

        public async Task<IActionResult> OnPost()
        {
            // (Other checks may be needed here?)
            // Run() payment transaction
            // if OK response, proceed with Order creation and save
            // else redirect to same page w/ error message
            var user = await _userManager.GetUserAsync(User);
            string response = _payment.Run(Input.FirstName, Input.LastName, Input.BillingAddress, Input.BillingCity, Input.BillingState, Input.BillingZip, Input.PaymentMethod, user.Id);
            if (response == "Successful.")
            {
                // save the order
                Cart cart = await _cart.GetActiveCartForUser(user.Id);
                Input.Date = DateTime.Now;
                Input.UserId = user.Id;
                Input.CartId = cart.Id;
                Input.CartItems = cart.CartItems;
                Input.Total = _cart.GetCartTotal(user.Id);
                await _order.FinalizeOrder(Input);

                // close cart
                await _cart.CloseCart(user.Id);

                // EMAIL receipt to user

                StringBuilder SB = new StringBuilder();

                SB.AppendLine($"<h1>Thank you {user.FirstName} for your purchase at The Outdoor Store!</h1>");
                SB.AppendLine($"<p>Here are your order details:");
                SB.AppendLine($"<table><thead><tr><th>Product Name</th><th>Quantity</th><th>Total</th></tr></thead><tbody>");

                Order = await _order.GetMostRecentOrder(user.Id);

                List<CartItem> cartItems = Order.CartItems.ToList();
                foreach (var item in cartItems)
                {
                    SB.Append($"<tr><td>{item.Product.Name}</td>");
                    SB.Append($"<td>{item.Quantity}</td>");
                    SB.Append($"<td>{item.Product.Price * item.Quantity}</td></tr>");
                }

                SB.AppendLine($"</tbody><tfoot><tr><td></td><td>Total:</td><td>${Order.Total}</td></tr></tfoot></table></p>");

                SB.AppendLine("<p>Enjoy and hope to see you again soon!</p>");

                string subject = "Your TheOutdoorStore Order Receipt";
                string htmlMessage = SB.ToString();

                await _emailSenderService.SendEmailAsync(user.Email, subject, htmlMessage);
            }
            else
            {
                ErrorMessage = "There was a problem with your payment information. Please try again.";
                Customer = await _userManager.GetUserAsync(User);

                //return RedirectToPage("/Shop/Checkout");
                return Page();
            }

            return RedirectToPage("/Shop/Receipt");
        }
    }
}