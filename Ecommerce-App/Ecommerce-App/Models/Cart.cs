using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        // Nav Prop

        public List<CartItem> CartItems { get; set; }
    }
}
