using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models
{
    // not being used at the moment, carts and cartItems will be used instead.
    public class OrderItem
    {
        //public int Id { get; set; }
        public int OrderId { get; set; }
        //public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // nav properties
        public Product Product { get; set; }
    }
}
