using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeNet.Api.Contracts.V1;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_App.Controllers
{
    public class OrderController : Controller
    {
        private SignInManager<Customer> _signInManager;
        private IPayment _payment;
        private customerAddressType Address { get; set; }

        public OrderController(SignInManager<Customer> signInManager, IPayment payment)
        {
            _signInManager = signInManager;
            _payment = payment;
        }

        public async Task<ActionResult> CompleteOrder(
            string firstName,
            string lastName,
            string BillingAddress,
            string BillingCity,
            string BillingState,
            string BillingZip,
            string PaymentMethod
            )
        {
            // (Other checks may be needed here?)
            // Run() payment transaction
            // if OK response, proceed with Order creation and save
            // else redirect to same page w/ error message
            var user = await _signInManager.GetUser
            string response =_payment.Run(firstName, lastName, BillingAddress, BillingCity, BillingState, BillingZip, PaymentMethod);

        }

    }
}
