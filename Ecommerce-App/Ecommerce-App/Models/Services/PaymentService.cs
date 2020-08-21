using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Utility;
using Ecommerce_App.Models.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Ecommerce_App.Models.Services
{
    public class PaymentService : IPayment
    {
        private IConfiguration _config;

        public PaymentService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string Run
            (
                string firstName,
                string lastName,
                string BillingAddress,
                string BillingCity,
                string BillingState,
                string BillingZip,
                string PaymentMethod
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

            // hardcoded credit card -> make enum
            var creditCard = new creditCardType
            {
                cardNumber = "4111111111111111",
                expirationDate = "1024",
                cardCode = "555"
            };

            customerAddressType billingAddress = GetBillingAddress(
                                                        firstName,
                                                        lastName,
                                                        BillingAddress,
                                                        BillingCity,
                                                        BillingState,
                                                        BillingZip);

            //declare that thuse is going to use an existing Credit Cart to pay
            var paymentType = new paymentType { Item = creditCard };

            // make the transactionRequest

            var transRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = 150.75m,
                payment = paymentType,
                billTo = billingAddress
            };

            // made teh transaction type, now we need to make the request
            var request = new createTransactionRequest { transactionRequest = transRequest };

            var controller = new createTransactionController(request);
            controller.Execute();

            var response = controller.GetApiResponse();

            return "";
        }

        //private customerAddressType GetsBillingAddress(int orderId)
        //{
        //    // you can pull this data from teh db the individual data from the order/cart itself
        //    customerAddressType address = new customerAddressType
        //    {
        //        firstName = "Josie",
        //        lastName = "Kitty",
        //        address = "123 Cat Nip Lane",
        //        city = "Seattle",
        //        zip = "98004"
        //    };

        //    return address;
        //}

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
                city = BillingState,
                zip = BillingZip
            };
            return address;
        }

    }
}