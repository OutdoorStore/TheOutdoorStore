using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public bool Active { get; set; } = true;

        // Nav Prop

        public List<CartItem> CartItems { get; set; }
    }
}
