using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IPayment
    {
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
