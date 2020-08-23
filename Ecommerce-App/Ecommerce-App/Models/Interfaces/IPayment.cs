using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IPayment
    {
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
        string Run(
            string firstName,
            string lastName,
            string BillingAddress,
            string BillingCity,
            string BillingState,
            string BillingZip,
            string PaymentMethod,
            string userId
            );
    }
}
