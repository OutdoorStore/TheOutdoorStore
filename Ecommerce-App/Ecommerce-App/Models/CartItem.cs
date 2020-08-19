using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models
{
    public class CartItem
    {
        // TODO: delete later...this will prevent CartItems with duplicate products associated to the same Cart. Hence the Quantity property.
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Navigation property

        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
