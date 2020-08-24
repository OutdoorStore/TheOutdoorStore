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
        private IPayment _payment;
        private IOrder _order;
        private ICart _cart;
        private IEmailSender _emailSenderService;

        [BindProperty]
        public Order Input { get; set; }
        public Order Order { get; set; }

        public decimal Total { get; set; }

        public CheckoutModel(SignInManager<Customer> signInManager, UserManager<Customer> userManager, IPayment payment, IOrder order, ICart cart, IEmailSender emailSenderService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _payment = payment;
            _order = order;
            _cart = cart;
            _emailSenderService = emailSenderService;
        }
        public void OnGet()
        {

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
                await _order.FinalizeOrder(Input);

                // close cart
                await _cart.CloseCart(user.Id);

                // EMAIL receipt to user

                StringBuilder SB = new StringBuilder();

                SB.AppendLine($"<h1>Thank you {user.FirstName} for your purchase at The Outdoor Store!</h1>");
                SB.AppendLine($"<p>Here are your order details:");
                SB.AppendLine($"<table><thead><tr><th>Product Name</th><th>Quantity</th><th>Total</th></tr></thead><tbody>");

                Order = await _order.GetMostRecentOrder(user.Id);
                Total = await _order.GetSpecificOrderTotal(Order.Id);

                List<CartItem> cartItems = Order.CartItems.ToList();
                foreach (var item in cartItems)
                {
                    SB.Append($"<tr><td>{item.Product.Name}</td>");
                    SB.Append($"<td>{item.Quantity}</td>");
                    SB.Append($"<td>{item.Product.Price * item.Quantity}</td></tr>");
                }

                SB.AppendLine($"</tbody><tfoot><tr><td></td><td>Total:</td><td>${Total}</td></tr></tfoot></table></p>");

                SB.AppendLine("<p>Enjoy and hope to see you again soon!</p>");

                string subject = "Your TheOutdoorStore Order Receipt";
                string htmlMessage = SB.ToString();

                await _emailSenderService.SendEmailAsync(user.Email, subject, htmlMessage);
            }
            else
            {
                // reload page with error message
            }

            return RedirectToPage("/Shop/Receipt");
        }
    }
}