using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Utility;
using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ecommerce_App.Models.Services
{
    public class PaymentService : IPayment
    {
        private IConfiguration _config;
        private StoreDbContext _storeContext;
        private ICart _cart;

        public PaymentService(IConfiguration configuration, StoreDbContext storeContext, ICart cart)
        {
            _config = configuration;
            _storeContext = storeContext;
            _cart = cart;
        }
        public string Run
            (
                string firstName,
                string lastName,
                string BillingAddress,
                string BillingCity,
                string BillingState,
                string BillingZip,
                string PaymentMethod,
                string userId
            )
        {
            // declares the type of environment
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // set up merchant account credentials
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = _config["AuthorizeLoginId"],
                ItemElementName = ItemChoiceType.transactionKey,
                Item = _config["AuthorizeTransactionKey"]
            };

            // creates credit card
            var creditCard = GetCreditCard(PaymentMethod);

            // creates billing address
            customerAddressType billingAddress = GetBillingAddress(
                                                        firstName,
                                                        lastName,
                                                        BillingAddress,
                                                        BillingCity,
                                                        BillingState,
                                                        BillingZip
                                                        );

            //declare that this is going to use an existing credit card to pay
            var paymentType = new paymentType { Item = creditCard };

            // make the transactionRequestType
            var transRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = _cart.GetCartTotal(userId),
                payment = paymentType,
                billTo = billingAddress
            };

            // make the transactionRequest
            var request = new createTransactionRequest { transactionRequest = transRequest };

            var controller = new createTransactionController(request);
            controller.Execute();

            var response = controller.GetApiResponse();
            return response.messages.message[0].text;
        }

        private customerAddressType GetBillingAddress(
           string firstName,
           string lastName,
           string BillingAddress,
           string BillingCity,
           string BillingState,
           string BillingZip)
        {
            customerAddressType address = new customerAddressType
            {
                firstName = firstName,
                lastName = lastName,
                address = BillingAddress,
                city = BillingCity,
                state = BillingState,
                zip = BillingZip
            };
            return address;
        }

        private creditCardType GetCreditCard(string cardProvider)
        {
            creditCardType card = new creditCardType()
            {
                cardCode = "555",
                expirationDate = "1024"
            };
            switch (cardProvider)
            {
                case "Visa":
                    card.cardNumber = "4111111111111111";
                    break;
                case "Mastercard":
                    card.cardNumber = "5424000000000015";
                    break;
                case "American Express":
                    card.cardNumber = "370000000000002";
                    break;
                case "Discover":
                    card.cardNumber = "6011000000000012";
                    break;
                case "JCB":
                    card.cardNumber = "	3088000000000017";
                    break;
                case "Diners Club":
                    card.cardNumber = "38000000000006";
                    break;
                default:
                    card.cardNumber = "	4012888818888";
                    break;
            }
            return card;
        }
    }
}