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

        /// <summary>
        /// Runs the payment process, by sending all of the 
        /// transaction information to AuthorizeNet, 
        /// getting the API response and returning the response message
        /// </summary>
        /// <param name="firstName">The first name of the credit card holder</param>
        /// <param name="lastName">The last name of the credit card holder</param>
        /// <param name="BillingAddress">The address of the credit card holder</param>
        /// <param name="BillingCity">The city of the credit card holder</param>
        /// <param name="BillingState">The state of the credit card holder</param>
        /// <param name="BillingZip">The zip code of the credit card holder</param>
        /// <param name="PaymentMethod">The type of credit card</param>
        /// <param name="userId">The Id of the signed in user</param>
        /// <returns>The API response message received from AuthorizeNet after the transaction execution</returns>
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
            if (response == null)
            {
                return "error";
            }
            return response.messages.message[0].text;
        }

        /// <summary>
        /// Creates a customerAddressType object called address 
        /// from the customer billing information
        /// </summary>
        /// <param name="firstName">The first name of the credit card holder</param>
        /// <param name="lastName">The last name of the credit card holder</param>
        /// <param name="BillingAddress">The address of the credit card holder</param>
        /// <param name="BillingCity">The city of the credit card holder</param>
        /// <param name="BillingState">The state of the credit card holder</param>
        /// <param name="BillingZip">The zip code of the credit card holder</param>
        /// <returns>The unique address object</returns>
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

        /// <summary>
        /// Takes in the user's credit card provider selection
        /// and matches it to that provider's Id, 
        /// to create a new creditCartType object called card
        /// </summary>
        /// <param name="cardProvider">A specific type of credit card provider</param>
        /// <returns>A unique card object</returns>
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