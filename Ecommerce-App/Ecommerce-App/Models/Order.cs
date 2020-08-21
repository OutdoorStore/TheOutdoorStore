﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime Date { get; set; }

        //public string ShippingAddress { get; set; }
        //public string ShippingCity { get; set; }
        //public string ShippingState { get; set; }
        //public string ShippingZip { get; set; }

        public List<CartItem> CartItems { get; set; }        
    }
}
